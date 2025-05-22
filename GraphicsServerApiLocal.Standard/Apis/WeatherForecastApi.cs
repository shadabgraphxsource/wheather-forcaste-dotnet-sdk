// <copyright file="WeatherForecastApi.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using APIMatic.Core;
using APIMatic.Core.Types;
using APIMatic.Core.Utilities;
using APIMatic.Core.Utilities.Date.Xml;
using GraphicsServerApiLocal.Standard;
using GraphicsServerApiLocal.Standard.Http.Client;
using GraphicsServerApiLocal.Standard.Http.Response;
using GraphicsServerApiLocal.Standard.Utilities;
using Newtonsoft.Json.Converters;
using System.Net.Http;

namespace GraphicsServerApiLocal.Standard.Apis
{
    /// <summary>
    /// WeatherForecastApi.
    /// </summary>
    public class WeatherForecastApi : BaseApi
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherForecastApi"/> class.
        /// </summary>
        internal WeatherForecastApi(GlobalConfiguration globalConfiguration) : base(globalConfiguration) { }

        /// <summary>
        /// /WeatherForecast EndPoint.
        /// </summary>
        /// <param name="accept">Required parameter: .</param>
        /// <returns>Returns the ApiResponse of List&lt;Models.Success&gt; response from the API call.</returns>
        public ApiResponse<List<Models.Success>> MWeatherForecast(
                string accept)
            => CoreHelper.RunTask(MWeatherForecastAsync(accept));

        /// <summary>
        /// /WeatherForecast EndPoint.
        /// </summary>
        /// <param name="accept">Required parameter: .</param>
        /// <param name="cancellationToken"> cancellationToken. </param>
        /// <returns>Returns the ApiResponse of List&lt;Models.Success&gt; response from the API call.</returns>
        public async Task<ApiResponse<List<Models.Success>>> MWeatherForecastAsync(
                string accept,
                CancellationToken cancellationToken = default)
            => await CreateApiCall<List<Models.Success>>()
              .RequestBuilder(_requestBuilder => _requestBuilder
                  .Setup(HttpMethod.Get, "/WeatherForecast")
                  .WithAuth("apiKey")
                  .Parameters(_parameters => _parameters
                      .Header(_header => _header.Setup("Accept", accept).Required())))
              .ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }
}