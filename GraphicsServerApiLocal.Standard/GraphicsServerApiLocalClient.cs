// <copyright file="GraphicsServerApiLocalClient.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using APIMatic.Core;
using APIMatic.Core.Authentication;
using APIMatic.Core.Utilities.Logger.Configuration;
using GraphicsServerApiLocal.Standard.Apis;
using GraphicsServerApiLocal.Standard.Authentication;
using GraphicsServerApiLocal.Standard.Http.Client;
using GraphicsServerApiLocal.Standard.Logging;
using GraphicsServerApiLocal.Standard.Utilities;

namespace GraphicsServerApiLocal.Standard
{
    /// <summary>
    /// The gateway for the SDK. This class acts as a factory for Api and holds the
    /// configuration of the SDK.
    /// </summary>
    public sealed class GraphicsServerApiLocalClient : IConfiguration
    {
        // A map of environments and their corresponding servers/baseurls
        private static readonly Dictionary<Environment, Dictionary<Enum, string>> EnvironmentsMap =
            new Dictionary<Environment, Dictionary<Enum, string>>
        {
            {
                Environment.Production, new Dictionary<Enum, string>
                {
                    { Server.Server1, "http://localhost:5235" },
                }
            },
        };

        private readonly GlobalConfiguration globalConfiguration;
        private SdkLoggingConfiguration sdkLoggingConfiguration;
        private const string userAgent = "DotNet-SDK/1.0.1 [OS: {os-info}, Engine: {engine}/{engine-version}]";
        private readonly HttpCallback httpCallback;
        private readonly Lazy<WeatherForecastApi> weatherForecast;

        private GraphicsServerApiLocalClient(
            Environment environment,
            CustomHeaderAuthenticationModel customHeaderAuthenticationModel,
            HttpCallback httpCallback,
            IHttpClientConfiguration httpClientConfiguration,
            SdkLoggingConfiguration sdkLoggingConfiguration)
        {
            this.Environment = environment;
            this.httpCallback = httpCallback;
            this.HttpClientConfiguration = httpClientConfiguration;
            this.sdkLoggingConfiguration = sdkLoggingConfiguration;
            CustomHeaderAuthenticationModel = customHeaderAuthenticationModel;
            var customHeaderAuthenticationManager = new CustomHeaderAuthenticationManager(customHeaderAuthenticationModel);
            globalConfiguration = new GlobalConfiguration.Builder()
                .AuthManagers(new Dictionary<string, AuthManager> {
                    {"apiKey", customHeaderAuthenticationManager},
                })
                .ApiCallback(httpCallback)
                .HttpConfiguration(httpClientConfiguration)
                .ServerUrls(EnvironmentsMap[environment], Server.Server1)
                .LoggingConfig(sdkLoggingConfiguration)
                .UserAgent(userAgent)
                .Build();

            CustomHeaderAuthenticationCredentials = customHeaderAuthenticationManager;

            this.weatherForecast = new Lazy<WeatherForecastApi>(
                () => new WeatherForecastApi(globalConfiguration));
        }

        /// <summary>
        /// Gets WeatherForecastApi.
        /// </summary>
        public WeatherForecastApi WeatherForecastApi => this.weatherForecast.Value;

        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public IHttpClientConfiguration HttpClientConfiguration { get; }

        /// <summary>
        /// Gets Environment.
        /// Current API environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        /// Gets http callback.
        /// </summary>
        public HttpCallback HttpCallback => this.httpCallback;

        /// <summary>
        /// Gets the credentials to use with CustomHeaderAuthentication.
        /// </summary>
        public ICustomHeaderAuthenticationCredentials CustomHeaderAuthenticationCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials model to use with CustomHeaderAuthentication.
        /// </summary>
        public CustomHeaderAuthenticationModel CustomHeaderAuthenticationModel { get; private set; }

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends
        /// it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:SERVER 1.</param>
        /// <returns>Returns the baseurl.</returns>
        public string GetBaseUri(Server alias = Server.Server1)
        {
            return globalConfiguration.ServerUrl(alias);
        }

        /// <summary>
        /// Creates an object of the GraphicsServerApiLocalClient using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            Builder builder = new Builder()
                .Environment(this.Environment)
                .HttpCallback(httpCallback)
                .LoggingConfig(sdkLoggingConfiguration)
                .HttpClientConfig(config => config.Build());

            if (CustomHeaderAuthenticationModel != null)
            {
                builder.CustomHeaderAuthenticationCredentials(CustomHeaderAuthenticationModel.ToBuilder().Build());
            }

            return builder;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return
                $"Environment = {this.Environment}, " +
                $"HttpClientConfiguration = {this.HttpClientConfiguration}, ";
        }

        /// <summary>
        /// Creates the client using builder.
        /// </summary>
        /// <returns> GraphicsServerApiLocalClient.</returns>
        internal static GraphicsServerApiLocalClient CreateFromEnvironment()
        {
            var builder = new Builder();

            string environment = System.Environment.GetEnvironmentVariable("GRAPHICS_SERVER_API_LOCAL_STANDARD_ENVIRONMENT");
            string authorization = System.Environment.GetEnvironmentVariable("GRAPHICS_SERVER_API_LOCAL_STANDARD_AUTHORIZATION");

            if (environment != null)
            {
                builder.Environment(ApiHelper.JsonDeserialize<Environment>($"\"{environment}\""));
            }

            if (authorization != null)
            {
                builder.CustomHeaderAuthenticationCredentials(new CustomHeaderAuthenticationModel
                .Builder(authorization)
                .Build());
            }

            return builder.Build();
        }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private Environment environment = GraphicsServerApiLocal.Standard.Environment.Production;
            private CustomHeaderAuthenticationModel customHeaderAuthenticationModel = new CustomHeaderAuthenticationModel();
            private HttpClientConfiguration.Builder httpClientConfig = new HttpClientConfiguration.Builder();
            private HttpCallback httpCallback;
            private SdkLoggingConfiguration sdkLoggingConfiguration;

            /// <summary>
            /// Sets credentials for CustomHeaderAuthentication.
            /// </summary>
            /// <param name="customHeaderAuthenticationModel">CustomHeaderAuthenticationModel.</param>
            /// <returns>Builder.</returns>
            public Builder CustomHeaderAuthenticationCredentials(CustomHeaderAuthenticationModel customHeaderAuthenticationModel)
            {
                if (customHeaderAuthenticationModel is null)
                {
                    throw new ArgumentNullException(nameof(customHeaderAuthenticationModel));
                }

                this.customHeaderAuthenticationModel = customHeaderAuthenticationModel;
                return this;
            }

            /// <summary>
            /// Sets Environment.
            /// </summary>
            /// <param name="environment"> Environment. </param>
            /// <returns> Builder. </returns>
            public Builder Environment(Environment environment)
            {
                this.environment = environment;
                return this;
            }

            /// <summary>
            /// Sets HttpClientConfig.
            /// </summary>
            /// <param name="action"> Action. </param>
            /// <returns>Builder.</returns>
            public Builder HttpClientConfig(Action<HttpClientConfiguration.Builder> action)
            {
                if (action is null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                action(this.httpClientConfig);
                return this;
            }

            /// <summary>
            /// Sets the default logging configuration, using the Console logger.
            /// </summary>
            /// <returns>Builder.</returns>
            public Builder LoggingConfig()
            {
                this.sdkLoggingConfiguration = SdkLoggingConfiguration.Console();
                return this;
            }

            /// <summary>
            /// Sets the logging configuration using the provided <paramref name="action"/>.
            /// </summary>
            /// <param name="action">The action to perform on the logging configuration builder.</param>
            /// <returns>Builder.</returns>
            /// <exception cref="ArgumentNullException">Thrown when <paramref name="action"/> is null.</exception>
            public Builder LoggingConfig(Action<LogBuilder> action)
            {
                if (action is null) throw new ArgumentNullException(nameof(action));
                var logBuilder =
                    LogBuilder.CreateFromConfig(sdkLoggingConfiguration ?? SdkLoggingConfiguration.Console());
                action(logBuilder);
                this.sdkLoggingConfiguration = logBuilder.Build();
                return this;
            }

            internal Builder LoggingConfig(SdkLoggingConfiguration configuration)
            {
                sdkLoggingConfiguration = configuration;
                return this;
            }



            /// <summary>
            /// Sets the HttpCallback for the Builder.
            /// </summary>
            /// <param name="httpCallback"> http callback. </param>
            /// <returns>Builder.</returns>
            public Builder HttpCallback(HttpCallback httpCallback)
            {
                this.httpCallback = httpCallback;
                return this;
            }

            /// <summary>
            /// Creates an object of the GraphicsServerApiLocalClient using the values provided for the builder.
            /// </summary>
            /// <returns>GraphicsServerApiLocalClient.</returns>
            public GraphicsServerApiLocalClient Build()
            {
                if (customHeaderAuthenticationModel.Authorization == null)
                {
                    customHeaderAuthenticationModel = null;
                }
                return new GraphicsServerApiLocalClient(
                    environment,
                    customHeaderAuthenticationModel,
                    httpCallback,
                    httpClientConfig.Build(),
                    sdkLoggingConfiguration);
            }
        }
    }
}
