
# Custom Header Signature



Documentation for accessing and setting credentials for apiKey.

## Auth Credentials

| Name | Type | Description | Setter | Getter |
|  --- | --- | --- | --- | --- |
| Authorization | `string` | - | `Authorization` | `Authorization` |



**Note:** Auth credentials can be set using `CustomHeaderAuthenticationCredentials` in the client builder and accessed through `CustomHeaderAuthenticationCredentials` method in the client instance.

## Usage Example

### Client Initialization

You must provide credentials in the client as shown in the following code snippet.

```csharp
GraphicsServerApiLocalClient client = new GraphicsServerApiLocalClient.Builder()
    .CustomHeaderAuthenticationCredentials(
        new CustomHeaderAuthenticationModel.Builder(
            "Authorization"
        )
        .Build())
    .Build();
```


