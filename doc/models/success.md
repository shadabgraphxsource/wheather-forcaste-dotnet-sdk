
# Success

*This model accepts additional fields of type object.*

## Structure

`Success`

## Fields

| Name | Type | Tags | Description |
|  --- | --- | --- | --- |
| `Date` | `string` | Required | - |
| `TemperatureC` | `int` | Required | - |
| `TemperatureF` | `int` | Required | - |
| `Summary` | `string` | Required | - |
| `AdditionalProperties` | `object this[string key]` | Optional | - |

## Example (as JSON)

```json
{
  "date": "2025-02-06T12:59:41.497Z",
  "temperatureC": 98230202,
  "temperatureF": 45866459,
  "summary": "fugiat ullamco pariatur sit",
  "exampleAdditionalProperty": {
    "key1": "val1",
    "key2": "val2"
  }
}
```

