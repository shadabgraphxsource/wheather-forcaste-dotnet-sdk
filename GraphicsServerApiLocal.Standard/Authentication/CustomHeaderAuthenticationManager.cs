// <copyright file="CustomHeaderAuthenticationManager.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphicsServerApiLocal.Standard.Http.Request;
using APIMatic.Core.Authentication;

namespace GraphicsServerApiLocal.Standard.Authentication
{
    /// <summary>
    /// CustomHeaderAuthenticationManager Class.
    /// </summary>
    internal class CustomHeaderAuthenticationManager : AuthManager, ICustomHeaderAuthenticationCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeaderAuthenticationManager"/> class.
        /// </summary>
        /// <param name="customHeaderAuthentication">CustomHeaderAuthentication.</param>
        public CustomHeaderAuthenticationManager(CustomHeaderAuthenticationModel customHeaderAuthenticationModel)
        {
            Authorization = customHeaderAuthenticationModel?.Authorization;
            Parameters(paramBuilder => paramBuilder
                .Header(header => header.Setup("Authorization", Authorization).Required())
            );
        }

        /// <summary>
        /// Gets string value for authorization.
        /// </summary>
        public string Authorization { get; }

        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <param name="authorization"> The string value for credentials.</param>
        /// <returns> True if credentials matched.</returns>
        public bool Equals(string authorization)
        {
            return authorization.Equals(this.Authorization);
        }
    }

    public sealed class CustomHeaderAuthenticationModel
    {
        internal CustomHeaderAuthenticationModel()
        {
        }

        internal string Authorization { get; set; } = "{{apiKey}}";

        /// <summary>
        /// Creates an object of the CustomHeaderAuthenticationModel using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            return new Builder(Authorization);
        }

        /// <summary>
        /// Builder class for CustomHeaderAuthenticationModel.
        /// </summary>
        public class Builder
        {
            private string authorization;

            public Builder(string authorization)
            {
                this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
            }

            /// <summary>
            /// Sets Authorization.
            /// </summary>
            /// <param name="authorization">Authorization.</param>
            /// <returns>Builder.</returns>
            public Builder Authorization(string authorization)
            {
                this.authorization = authorization ?? throw new ArgumentNullException(nameof(authorization));
                return this;
            }


            /// <summary>
            /// Creates an object of the CustomHeaderAuthenticationModel using the values provided for the builder.
            /// </summary>
            /// <returns>CustomHeaderAuthenticationModel.</returns>
            public CustomHeaderAuthenticationModel Build()
            {
                return new CustomHeaderAuthenticationModel()
                {
                    Authorization = this.authorization
                };
            }
        }
    }
    
}