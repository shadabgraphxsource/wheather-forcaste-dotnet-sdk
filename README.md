
# Getting Started with GraphicsServer API-Local

## Introduction

Methods for graphics processing using server-side & cloud processing.

## Install the Package

If you are building with .NET CLI tools then you can also use the following command:

```bash
dotnet add package WheatherForcasteSDK --version 1.0.1
```

You can also view the package at:
https://www.nuget.org/packages/WheatherForcasteSDK/1.0.1

## Initialize the API Client

**_Note:_** Documentation for the client can be found [here.](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/client.md)

The following parameters are configurable for the API Client:

| Parameter | Type | Description |
|  --- | --- | --- |
| Environment | `Environment` | The API environment. <br> **Default: `Environment.Production`** |
| Timeout | `TimeSpan` | Http client timeout.<br>*Default*: `TimeSpan.FromSeconds(30)` |
| HttpClientConfiguration | [`Action<HttpClientConfiguration.Builder>`](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-client-configuration-builder.md) | Action delegate that configures the HTTP client by using the HttpClientConfiguration.Builder for customizing API call settings.<br>*Default*: `new HttpClient()` |
| LogBuilder | [`LogBuilder`](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/log-builder.md) | Represents the logging configuration builder for API calls |
| CustomHeaderAuthenticationCredentials | [`CustomHeaderAuthenticationCredentials`](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/auth/custom-header-signature.md) | The Credentials Setter for Custom Header Signature |

The API client can be initialized as follows:

```csharp
GraphicsServerApiLocalClient client = new GraphicsServerApiLocalClient.Builder()
    .CustomHeaderAuthenticationCredentials(
        new CustomHeaderAuthenticationModel.Builder(
            "Authorization"
        )
        .Build())
    .Environment(GraphicsServerApiLocal.Standard.Environment.Production)
    .LoggingConfig(config => config
        .LogLevel(LogLevel.Information)
        .RequestConfig(reqConfig => reqConfig.Body(true))
        .ResponseConfig(respConfig => respConfig.Headers(true))
    )
    .Build();
```

## Authorization

This API uses the following authentication schemes.

* [`apiKey (Custom Header Signature)`](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/auth/custom-header-signature.md)

## List of APIs

* [Weather Forecast](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/controllers/weather-forecast.md)

## SDK Infrastructure

### Configuration

* [HttpClientConfiguration](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-client-configuration.md)
* [HttpClientConfigurationBuilder](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-client-configuration-builder.md)
* [LogBuilder](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/log-builder.md)
* [LogRequestBuilder](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/log-request-builder.md)
* [LogResponseBuilder](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/log-response-builder.md)
* [ProxyConfigurationBuilder](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/proxy-configuration-builder.md)

### HTTP

* [HttpCallback](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-callback.md)
* [HttpContext](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-context.md)
* [HttpRequest](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-request.md)
* [HttpResponse](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-response.md)
* [HttpStringResponse](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/http-string-response.md)

### Utilities

* [ApiException](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/api-exception.md)
* [ApiResponse](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/api-response.md)
* [ApiHelper](https://www.github.com/shadabgraphxsource/wheather-forcaste-dotnet-sdk/tree/1.0.1/doc/api-helper.md)

