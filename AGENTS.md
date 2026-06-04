# CatalogAPI – Agent Instructions

## Project Overview
ASP.NET Core 8 Web API that is part of a larger ticketing/events booking system. It manages **Events**, **Venues**, **Sections**, **Seats**, and **SeatManifests**. It communicates with a sibling `OrderApi` microservice to fetch seat statuses.

## Solution Structure

| Project | Purpose |
|---|---|
| `CatalogAPI/` | Web API entry point – controllers, middleware, DI wiring |
| `CatalogAPI.Domain/` | Use cases (MediatR handlers), DTOs/models, storage interfaces, domain exceptions |
| `CatalogAPI.DAL/` | EF Core (SQL Server), storage implementations, entity configurations, specifications |
| `CatalogAPI.Tests/` | xUnit + Moq unit tests |

## Build & Test Commands

```bash
dotnet build CatalogAPI.sln
dotnet test CatalogAPI.sln
```

EF Core migrations (run from solution root):
```bash
dotnet ef migrations add <Name> --project CatalogAPI.DAL --startup-project CatalogAPI
dotnet ef database update --project CatalogAPI.DAL --startup-project CatalogAPI
```

## Architecture: CQRS via MediatR

Every operation follows this **four-layer pattern**:

```
Controller → IMediator.Send(Request) → Handler → IStorage → Storage implementation (EF Core)
```

When adding a new use case (e.g. `GetFoo`):

1. **Domain/UseCases/GetFoo/**
   - `GetFooRequest.cs` – implements `IRequest<ResultModel<FooModel>>`
   - `GetFooRequest.Handler.cs` – `IRequestHandler`, injects `IGetFooStorage`
2. **Domain/Storage/GetFoo/**
   - `IGetFooStorage.cs` – interface used by the handler
3. **DAL/Storage/GetFoo/**
   - `GetFooStorage.cs` – implements `IGetFooStorage` using `CatalogDbContext`
4. Register in `CatalogAPI.DAL/ServiceCollectionExtensions.cs` (`AddServices()`)

> Handlers registered in `Program.cs` via `AddMediatR` scanning both `CatalogAPI` and `CatalogAPI.Domain` assemblies.

## Key Conventions

### Pagination & Response shape
All list endpoints accept `page` (1-based) and `pageSize` query params and return `ResultModel<List<T>>` (from `Homework.Ticketing.System.Shared.Models`) with `Data` and `Count` properties.

### Entities
All EF Core entities inherit `BaseDbEntity` from `Shared.DAL.Entities`. EF configurations live in `CatalogAPI.DAL/Configurations/` and are applied via `ApplyConfigurationsFromAssembly`.

### Specification pattern (DAL)
Filtering logic is encapsulated in `ISpecification<T>` implementations under `CatalogAPI.DAL/Specifications/`. Use `.Where(new MySpec(value).ToExpression())` in storage classes.

### Exception handling
- Throw exceptions that derive from `CatalogAPI.Domain.Exceptions.NotFoundException` for not-found scenarios.
- `ExceptionHandlerMiddleware` maps `NotFoundException` → HTTP 404, all others → HTTP 500.

### Caching
- Response caching (`[ResponseCache]`) is applied on GET controller actions.
- Redis (`IDistributedCache`) is available for programmatic caching (connection string key: `Redis`).

### Authentication
JWT Bearer auth is configured. Use `[Authorize]` on controller actions/controllers that require authentication. JWT settings are in `appsettings.json` under `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`.

### External service
`IOrderApiClient` / `OrderApiClient` calls `OrderApi` (configured via `OrderApi:BaseUrl`). The named `HttpClient` is registered as `"OrderApi"`.

## Testing Patterns

- **Controller tests** – mock `IMediator`, assert the correct request type is sent and the response is returned as `OkObjectResult`.
- **Handler tests** – mock the `IStorage` interface, verify delegation and return value.
- **DAL tests** – use `Microsoft.EntityFrameworkCore.InMemory` provider against a real `CatalogDbContext`.

Test files mirror the source structure under `CatalogAPI.Tests/`.

## Known Pitfalls

- The folder `CreaateVenue` (double 'a') exists in both `CatalogAPI.DAL/Storage/` and `CatalogAPI.Domain/Storage/` – this is a typo; treat it as `CreateVenue`.
- `appsettings.json` contains a placeholder JWT key. Do not treat it as production-safe.
- SQL Server connection string is only present in `appsettings.Development.json` (not checked in). Ensure it is configured locally before running migrations or the API.
