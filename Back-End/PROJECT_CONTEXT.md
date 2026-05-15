# PROJECT_CONTEXT.md – AI Study Hub

> This document serves as the comprehensive project context reference.  
> Keep it updated as the project evolves.

---

## 1. Project Overview

**AI Study Hub** is an AI-powered learning document management system designed for university students. The platform enables students to upload, organize, search, and interact with study documents using AI-powered chat functionality.

| Attribute | Value |
|-----------|-------|
| Project Name | AI Study Hub |
| Course | SWP391 – Software Development Project |
| University | FPT University |
| Duration | 10 Weeks |
| Team Size | 5–7 Students |
| Project Type | Backend RESTful API |

---

## 2. Technology Stack

| Component | Technology | Version |
|-----------|------------|---------|
| Runtime | .NET | 8.0 |
| Framework | ASP.NET Core Web API | 8.0 |
| ORM | Entity Framework Core | 8.0 (Database First) |
| Database | Microsoft SQL Server | 2019+ |
| Authentication | JWT Bearer | — |
| API Docs | Swagger / OpenAPI | 6.x |
| Testing | xUnit + Moq | Latest |

---

## 3. Solution Structure

```
AIStudyHub.API       → Controllers, Middlewares, Program.cs, appsettings.json
AIStudyHub.BLL       → Service interfaces & implementations
AIStudyHub.DAL       → AppDbContext (scaffolded), Entities (scaffolded), Repositories
AIStudyHub.Shared    → DTOs, Enums, Constants, Exceptions, Response wrappers
AIStudyHub.Tests     → Unit test project
```

### Project References (Dependency Flow)

```
API → BLL → DAL → Shared
API → Shared
Tests → All
```

---

## 4. Layer Responsibilities

### AIStudyHub.API
- Receives HTTP requests
- Validates input (model binding)
- Delegates to BLL services
- Returns consistent `ApiResponse<T>` format
- **Never** contains business logic
- **Never** returns raw entity objects

### AIStudyHub.BLL
- Business rule validation
- Data transformation (Entity ↔ DTO)
- Orchestrates repository calls
- JWT token generation

### AIStudyHub.DAL
- Entity Framework Core DbContext
- Entity classes (auto-generated)
- Repository pattern implementation
- Database queries via LINQ

### AIStudyHub.Shared
- DTOs for API contracts
- Enums for type safety
- Constants for magic values
- Custom exceptions
- `ApiResponse<T>` and `PaginatedResponse<T>` wrappers

---

## 5. Expected Database Tables

| Table | Description |
|-------|-------------|
| Users | User accounts |
| Roles | User roles (Student, Lecturer, Admin) |
| Subjects | Course subjects |
| Documents | Uploaded study documents |
| Tags | Document tags |
| DocumentTags | Many-to-many: Documents ↔ Tags |
| ChatSessions | AI chat sessions per user |
| ChatMessages | Individual chat messages |

---

## 6. Coding Standards

### Naming Conventions

| Element | Convention | Example |
|---------|------------|---------|
| Classes | PascalCase | `DocumentService` |
| Interfaces | I + PascalCase | `IDocumentService` |
| Methods | PascalCase + Async | `GetByIdAsync()` |
| Variables | camelCase | `documentId` |
| Constants | PascalCase | `MaxPageSize` |
| DTOs | PascalCase + Dto | `CreateDocumentDto` |
| Controllers | Plural + Controller | `DocumentsController` |

### Code Rules

1. **No business logic in controllers** – controllers only delegate
2. **Constructor dependency injection** – no `new` for services
3. **Never return entities from controllers** – always use DTOs
4. **Async/await everywhere** – all I/O operations
5. **ApiResponse wrapper** – every endpoint returns `ApiResponse<T>`
6. **TODO comments** – for unimplemented features

---

## 7. API Conventions

| Method | Route Pattern | Purpose |
|--------|---------------|---------|
| GET | `/api/{resource}` | List all (paginated) |
| GET | `/api/{resource}/{id}` | Get by ID |
| POST | `/api/{resource}` | Create |
| PUT | `/api/{resource}/{id}` | Update |
| DELETE | `/api/{resource}/{id}` | Delete |
| GET | `/api/{resource}/search?keyword=x` | Search |

### Response Format

```json
{
  "success": true,
  "message": "Request successful.",
  "data": { },
  "errors": null
}
```

---

## 8. Authentication Flow

1. User sends `POST /api/auth/login` with email + password
2. Server validates credentials, generates JWT
3. Client stores JWT token
4. Client sends token in `Authorization: Bearer {token}` header
5. Server validates token on each request via middleware
6. Role-based access via `[Authorize(Roles = "Admin")]`

---

## 9. Development Roadmap

| Week | Phase | Deliverables |
|------|-------|--------------|
| 1–2 | Setup | Solution structure, DB design, scaffold |
| 3–4 | Core | Auth, Document CRUD, Subject CRUD |
| 5–6 | Features | Search, Tags, File upload |
| 7–8 | AI | Chat integration, OpenAI API |
| 9 | Testing | Unit tests, integration tests |
| 10 | Polish | Bug fixes, documentation, deployment |

---

## 10. Scaffold-DbContext Commands

### Package Manager Console

```powershell
Scaffold-DbContext "Server=.;Database=AIStudyHub;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context AppDbContext -ContextDir . -DataAnnotations -Force
```

### .NET CLI

```bash
dotnet ef dbcontext scaffold "Server=.;Database=AIStudyHub;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --context AppDbContext --context-dir . --data-annotations --force --project AIStudyHub.DAL --startup-project AIStudyHub.API
```

---

## 11. Recommended Next Steps

After this scaffold is complete:

1. **Design the database** – Create tables in SQL Server (see `DATABASE_DESIGN_GUIDE.md`)
2. **Run Scaffold-DbContext** – Generate entities and AppDbContext
3. **Wire up repositories** – Inject AppDbContext, extend GenericRepository
4. **Implement AuthService** – Password hashing + JWT generation
5. **Implement DocumentService** – CRUD with file storage
6. **Implement ChatService** – OpenAI API integration
7. **Write unit tests** – Cover all service methods
8. **Deploy** – Azure App Service or IIS
