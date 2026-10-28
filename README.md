# VoteMonitorPoc

The project was generated using the [Clean.Architecture.Solution.Template](https://github.com/jasontaylordev/CleanArchitecture) version 10.8.0.

## Build

Run `dotnet build` to build the solution.

## Run

To run the application:

```bash
dotnet run --project .\src\AppHost
```

The Aspire dashboard will open automatically, showing the application URLs and logs.

This is a PoC exploring the following:
- integrating Wolverine for event publishing and handling
- OpenTelemetry inetegration to Sentry
- FastEndpoints integration with Ardalis.Result and HTTPStatusCode mapping 
