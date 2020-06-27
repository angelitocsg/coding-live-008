# AutoMapper use or not use it? | Coding Live #008

## Getting Started

These instructions is a part of a live coding video.

### Prerequisites

- .NET Core 3.1 SDK - https://dotnet.microsoft.com/download
- AutoMapper - https://automapper.org/

## Projects

Create a base folder `CodingLive008`.

Create the `.gitignore` file based on file https://github.com/github/gitignore/blob/master/VisualStudio.gitignore

### API with mapper examples

```shell
dotnet new webapi -o ApiMapped
```

```shell
dotnet add package Bogus
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
```

```shell
dotnet ef database update
```

## References

https://automapper.org/

https://jimmybogard.com/automapper-usage-guidelines/

https://www.c-sharpcorner.com/UploadFile/f1047f/mapping-viewmodel-to-model-using-implicit-conversion-operato/

https://www.codeproject.com/Articles/61629/AutoMapper
