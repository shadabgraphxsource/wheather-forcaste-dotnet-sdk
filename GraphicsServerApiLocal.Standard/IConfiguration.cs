// <copyright file="IConfiguration.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
using System;
using System.Net;
using GraphicsServerApiLocal.Standard.Authentication;
using GraphicsServerApiLocal.Standard.Models;

namespace GraphicsServerApiLocal.Standard
{
    /// <summary>
    /// IConfiguration.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets Current API environment.
        /// </summary>
        Environment Environment { get; }

        /// <summary>
        /// Gets the credentials to use with CustomHeaderAuthentication.
        /// </summary>
        ICustomHeaderAuthenticationCredentials CustomHeaderAuthenticationCredentials { get; }

        /// <summary>
        /// Gets the credentials model to use with CustomHeaderAuthentication.
        /// </summary>
        CustomHeaderAuthenticationModel CustomHeaderAuthenticationModel { get; }

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:SERVER 1.</param>
        /// <returns>Returns the baseurl.</returns>
        string GetBaseUri(Server alias = Server.Server1);
    }
}