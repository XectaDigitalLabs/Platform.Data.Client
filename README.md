# Platform Data Client OAS client


This project was created to allow polyglot support of api clients. Our api services support 
[OpenAPI Specification 3.0](https://swagger.io/specification/) which can be used to autogenerate a base client library.
We add support for a XectAPI class that assists in supporting MTLS and our API Client authentication schema. Currently we
support clients for both python and csharp but others will follow in the coming months. 

The code generation requires a link to our api schema which can be found in the schemas directory. In the build process
the schema location can be overridden but the default would be schemas.

## Code Genration
When schemas are updated it is typically required to regenerate the core client code to have compatibility with
the api service.

We use gradle for code generation and this project comes with a platform-agnostic runtime that will build your 
updated client code without having to install any outside dependencies.

### Python
We suport a python api which uses urllib3 to invoke the rest api.

**Linux / OSX**
```shell
./gradlew generateSwaggerCodeDataclient-python
```

**Windows**
```shell
./gradlew.bat generateSwaggerCodeDataclient-python
```


### CSharp


**Linux / OSX**
```shell
./gradlew generateSwaggerCodeDataclient-csharp
```

**Windows**
```shell
./gradlew.bat generateSwaggerCodeDataclient-csharp
```
