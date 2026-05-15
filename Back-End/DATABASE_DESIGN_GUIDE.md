# DATABASE_DESIGN_GUIDE.md – AI Study Hub

> Instructions for designing and creating the SQL Server database  
> that will be scaffolded into Entity Framework Core entities.

---

## 1. Prerequisites

- SQL Server 2019+ installed and running
- SQL Server Management Studio (SSMS) or Azure Data Studio
- A login with `db_owner` permissions

---

## 2. Create the Database

```sql
CREATE DATABASE AIStudyHub;
GO
USE AIStudyHub;
GO
```

---

## 3. Expected Table Schema

Below is the **reference schema**. Adjust column types and constraints based on your team's ERD.

### 3.1 Roles

```sql
CREATE TABLE Roles (
    RoleId      INT PRIMARY KEY IDENTITY(1,1),
    RoleName    NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255) NULL,
    CreatedAt   DATETIME2 NOT NULL DEFAULT GETDATE()
);
```

### 3.2 Users

```sql
CREATE TABLE Users (
    UserId       INT PRIMARY KEY IDENTITY(1,1),
    FullName     NVARCHAR(100) NOT NULL,
    Email        NVARCHAR(255) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    RoleId       INT NOT NULL,
    AvatarUrl    NVARCHAR(500) NULL,
    IsActive     BIT NOT NULL DEFAULT 1,
    CreatedAt    DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt    DATETIME2 NULL,
    CONSTRAINT FK_Users_Roles FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
```

### 3.3 Subjects

```sql
CREATE TABLE Subjects (
    SubjectId   INT PRIMARY KEY IDENTITY(1,1),
    SubjectName NVARCHAR(200) NOT NULL UNIQUE,
    Description NVARCHAR(500) NULL,
    CreatedAt   DATETIME2 NOT NULL DEFAULT GETDATE()
);
```

### 3.4 Documents

```sql
CREATE TABLE Documents (
    DocumentId  INT PRIMARY KEY IDENTITY(1,1),
    Title       NVARCHAR(300) NOT NULL,
    Description NVARCHAR(1000) NULL,
    FileUrl     NVARCHAR(500) NOT NULL,
    FileType    NVARCHAR(20) NOT NULL,
    FileSize    BIGINT NOT NULL,
    Status      INT NOT NULL DEFAULT 1,          -- 1=Pending, 2=Approved, 3=Rejected
    SubjectId   INT NOT NULL,
    UploadedBy  INT NOT NULL,
    CreatedAt   DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt   DATETIME2 NULL,
    CONSTRAINT FK_Documents_Subjects FOREIGN KEY (SubjectId) REFERENCES Subjects(SubjectId),
    CONSTRAINT FK_Documents_Users FOREIGN KEY (UploadedBy) REFERENCES Users(UserId)
);
```

### 3.5 Tags

```sql
CREATE TABLE Tags (
    TagId   INT PRIMARY KEY IDENTITY(1,1),
    TagName NVARCHAR(100) NOT NULL UNIQUE
);
```

### 3.6 DocumentTags (Junction Table)

```sql
CREATE TABLE DocumentTags (
    DocumentId INT NOT NULL,
    TagId      INT NOT NULL,
    PRIMARY KEY (DocumentId, TagId),
    CONSTRAINT FK_DocumentTags_Documents FOREIGN KEY (DocumentId) REFERENCES Documents(DocumentId) ON DELETE CASCADE,
    CONSTRAINT FK_DocumentTags_Tags FOREIGN KEY (TagId) REFERENCES Tags(TagId) ON DELETE CASCADE
);
```

### 3.7 ChatSessions

```sql
CREATE TABLE ChatSessions (
    SessionId  INT PRIMARY KEY IDENTITY(1,1),
    UserId     INT NOT NULL,
    Title      NVARCHAR(300) NULL,
    CreatedAt  DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt  DATETIME2 NULL,
    CONSTRAINT FK_ChatSessions_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
```

### 3.8 ChatMessages

```sql
CREATE TABLE ChatMessages (
    MessageId  INT PRIMARY KEY IDENTITY(1,1),
    SessionId  INT NOT NULL,
    Content    NVARCHAR(MAX) NOT NULL,
    SenderType INT NOT NULL,                     -- 1=User, 2=AI
    SentAt     DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_ChatMessages_ChatSessions FOREIGN KEY (SessionId) REFERENCES ChatSessions(SessionId) ON DELETE CASCADE
);
```

---

## 4. Seed Data

```sql
-- Insert default roles
INSERT INTO Roles (RoleName, Description) VALUES
('Student', 'Regular student user'),
('Lecturer', 'Lecturer/Teacher role'),
('Admin', 'System administrator');

-- Insert sample subjects
INSERT INTO Subjects (SubjectName, Description) VALUES
('SWP391', 'Software Development Project'),
('PRN231', 'Building Cross-Platform Back-End Application With .NET'),
('SWD392', 'Software Architecture and Design');
```

---

## 5. After Creating the Database

Run the scaffold command to generate entity classes:

### Package Manager Console

```powershell
Scaffold-DbContext "Server=.;Database=AIStudyHub;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context AppDbContext -ContextDir . -DataAnnotations -Force
```

### .NET CLI

```bash
dotnet ef dbcontext scaffold "Server=.;Database=AIStudyHub;Trusted_Connection=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Entities --context AppDbContext --context-dir . --data-annotations --force --project AIStudyHub.DAL --startup-project AIStudyHub.API
```

---

## 6. Post-Scaffolding Checklist

- [ ] Verify all entities are generated in `AIStudyHub.DAL/Entities/`
- [ ] Verify `AppDbContext.cs` is generated in `AIStudyHub.DAL/`
- [ ] Delete the placeholder `AppDbContext.cs` and `.gitkeep.cs` files
- [ ] Update `GenericRepository<T>` — change `DbContext` to `AppDbContext`
- [ ] Update all repository classes to inject `AppDbContext`
- [ ] Uncomment `AddDbContext<AppDbContext>` in `Program.cs`
- [ ] Uncomment `IGenericRepository<>` registration in `Program.cs`
- [ ] Build the solution: `dotnet build`
- [ ] Run and verify Swagger loads: `dotnet run --project AIStudyHub.API`

---

## 7. Re-Scaffolding After Schema Changes

If you modify the database schema, re-run the scaffold command with `-Force` flag.  
This will **overwrite** existing entity and context files.

> ⚠️ Any manual changes to entity files will be lost. Use partial classes if needed.
