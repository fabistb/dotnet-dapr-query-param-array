# dotnet-dapr-query-param-array
Small application to to demonstrate the behavior of dapr and and the dotnet-sdk during method invocation .
Using different variants the following query is send to the CalleeController.

__?name=test&name=test1&name=test3__

Expected result:

```json
{
    "receivedNames": [
        "test",
        "test2",
        "test3"
    ]
}
```

## Dapr method invocation dotnet-sdk
The caller calls the calle using the Dapr method invocation.

```
curl --location --request POST 'http://localhost:5000/api/Caller/dapr-method-invocation
```

Actual resposne:
```json
{
  "receivedNames": [
    "test"
  ]
}
```


## Http Client
The caller calls the callee using the HTTP Client. Dapr isn't involved.

```
curl --location --request POST 'http://localhost:5000/api/Caller/http-client'
```
Actual  result:
```json
{
    "receivedNames": [
        "test",
        "test2",
        "test3"
    ]
}
```

## Http Client dapr endpoint.
The caller calls the callee using the HTTP Client calling the dapr method invocation endpoint.

```
curl --location --request POST 'http://localhost:5000/api/Caller/http-client-dapr'
```
Actual response:
```json
{
    "receivedNames": [
        "test"
    ]
}
```
