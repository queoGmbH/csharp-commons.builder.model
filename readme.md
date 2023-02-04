# queo-commons ModelBuilder (TestDataBuilder)

[![Test](https://github.com/queoGmbH/csharp-commons.builder.model/actions/workflows/main.yml/badge.svg)](https://github.com/queoGmbH/csharp-commons.builder.model/actions/workflows/main.yml)

### Cake Pipeline

-   use `./build.ps1` to run the cake-pipeline
  - `--target=Default` Default target with test, publish and upload artifacts
  - `--target=BuildAndTest` Building and running tests only
  - `--target=BuildPackage` Publish the version to nuget
-   to change the _dotnet version_ used for the build adjust 2 things
    1. sdk version in `global.json`
    2. sdk version in `build.config`
        > Both are require together that the exact dotnet version is used
