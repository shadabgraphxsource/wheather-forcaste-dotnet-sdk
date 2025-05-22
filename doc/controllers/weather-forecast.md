# Weather Forecast

```csharp
WeatherForecastApi weatherForecastApi = client.WeatherForecastApi;
```

## Class Name

`WeatherForecastApi`


# Weather Forecast

```csharp
MWeatherForecastAsync(
    string accept)
```

## Parameters

| Parameter | Type | Tags | Description |
|  --- | --- | --- | --- |
| `accept` | `string` | Header, Required | - |

## Response Type

This method returns an [`ApiResponse`](../../doc/api-response.md) instance. The `Data` property of this instance returns the response data which is of type [List<Models.Success>](../../doc/models/success.md).

## Example Usage

```csharp
string accept = "text/plain";
try
{
    ApiResponse<List<Success>> result = await weatherForecastApi.MWeatherForecastAsync(accept);
}
catch (ApiException e)
{
    // TODO: Handle exception here
    Console.WriteLine(e.Message);
}
```

## Example Response *(as JSON)*

```json
[
  {
    "date": "2025-02-06T12:59:41.497Z",
    "temperatureC": 98230202,
    "temperatureF": 45866459,
    "summary": "fugiat ullamco pariatur sit"
  },
  {
    "date": "2023-06-24T00:59:08.454Z",
    "temperatureC": -1129121,
    "temperatureF": -52196879,
    "summary": "aute aliquip incididunt tempor"
  }
]
```

