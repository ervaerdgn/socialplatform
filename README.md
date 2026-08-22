# SocialPlatform

A full-featured social media backend REST API built from scratch with ASP.NET Core, showcasing a complete data model, JWT authentication, file uploads, real-time-style notifications, and a live cloud deployment.

**Live demo (Swagger UI):** https://socialplatform-db.onrender.com/swagger/index.html

> Note: the demo runs on a free hosting tier, so the first request after a period of inactivity may take 30-60 seconds to wake up.

## Features

- **User accounts** — registration with BCrypt password hashing, login with JWT authentication
- **Posts** — create, read, paginate, and search
- **Comments** — linked to both users and posts
- **Likes** — with automatic duplicate-safe logic
- **Follow system** — self-referencing many-to-many relationship between users
- **Personalized feed** — shows posts only from followed users, sorted by most recent
- **Direct messaging** — private, bidirectional conversations between two users
- **Notifications** — automatically generated on likes, comments, follows, and messages
- **Image uploads** — profile pictures and post images served as static files
- **Pagination & search** — efficient, page-based querying for posts and users
- **Unit tests** — xUnit tests covering authentication logic and the feed algorithm
- **Dockerized & cloud-deployed** — containerized with Docker and deployed on Render with a PostgreSQL database

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 8) |
| ORM | Entity Framework Core |
| Database | PostgreSQL (production) |
| Authentication | JWT Bearer tokens + BCrypt password hashing |
| Testing | xUnit, EF Core InMemory provider |
| API docs | Swagger / OpenAPI |
| Deployment | Docker, Render |

## Project Structure

```
socialplatform/
├── Controllers/       # API endpoints (Users, Posts, Comments, Likes, Follows, Messages, Notifications, Auth)
├── Models/             # Entity classes (User, Post, Comment, Like, Follow, Message, Notification)
├── Data/               # AppDbContext and EF Core configuration
├── Migrations/         # EF Core database migrations
└── wwwroot/uploads/    # Uploaded profile and post images
```

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (or SQL Server, with a provider change — see below)
- Visual Studio 2022 or any C# editor

### Setup

1. Clone the repository
   ```bash
   git clone https://github.com/ervaerdgn/socialplatform.git
   cd socialplatform
   ```

2. Restore dependencies
   ```bash
   dotnet restore
   ```

3. Configure your secrets (do **not** put real values in `appsettings.json`). Using .NET User Secrets:
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string"
   dotnet user-secrets set "Jwt:Key" "a-secret-key-at-least-32-characters-long"
   ```

4. Apply database migrations
   ```bash
   dotnet ef database update
   ```

5. Run the project
   ```bash
   dotnet run
   ```

6. Open the Swagger UI at `https://localhost:<port>/swagger`

## API Overview

| Controller | Endpoints |
|---|---|
| `Auth` | `POST /api/Auth/login` |
| `Users` | `GET/POST /api/Users`, `POST /api/Users/upload-profil-resmi`, `GET /api/Users/ara` |
| `Posts` | `GET/POST /api/Posts`, `GET /api/Posts/feed/{userId}`, `GET /api/Posts/ara`, `POST /api/Posts/{postId}/upload-resim` |
| `Comments` | `GET/POST /api/Comments` |
| `Likes` | `GET/POST /api/Likes` |
| `Follows` | `GET/POST /api/Follows` |
| `Messages` | `POST /api/Messages`, `GET /api/Messages/konusma/{digerKullaniciId}` |
| `Notifications` | `GET /api/Notifications`, `PUT /api/Notifications/{id}/okundu` |

All write operations (except registration and login) require a valid JWT, sent as `Authorization: Bearer <token>`.

## Authentication Flow

1. Register a user via `POST /api/Users`
2. Log in via `POST /api/Auth/login` with email and password
3. Copy the returned token and paste it into Swagger's **Authorize** button
4. Authenticated requests will automatically resolve the current user from the token — no need to pass `userId` manually

## Running Tests

```bash
dotnet test
```

Tests use an in-memory database, so they run independently of any real database connection.

## Deployment

The project is containerized with a `Dockerfile` and deployed on [Render](https://render.com) using a free PostgreSQL instance. The connection string parsing logic in `Program.cs` automatically converts Render's URI-style connection string into the format Npgsql expects.

## License

This project was built for learning purposes as part of a personal backend development journey.
## 📸 Screenshots

Here is a visual walk-through of the API in action, demonstrating core functionalities, secure authentication, and robust error handling:

### 1. API Overview & Structure

<img width="1189" height="789" alt="2 (2)" src="https://github.com/user-attachments/assets/4d73f5a0-93d3-4363-8339-0dd9f0a3df8a" />
<img width="1245" height="915" alt="1" src="https://github.com/user-attachments/assets/889c0c8b-9448-4272-a3a4-1724475564eb" />


### 2. User Registration & Secure Authentication (JWT)
<img width="573" height="279" alt="5" src="https://github.com/user-attachments/assets/f59de5a4-adde-4679-ad1d-38a3d74f1074" />
<img width="1193" height="623" alt="4" src="https://github.com/user-attachments/assets/c7ca8d44-77d2-45e7-8452-ab27e3271d30" />
<img width="1198" height="659" alt="3" src="https://github.com/user-attachments/assets/00061dd6-0ac4-4fd9-9647-af6953861262" />


### 3. Core Social Features in Action
<img width="1919" height="1079" alt="8" src="https://github.com/user-attachments/assets/915ac9fa-b9e4-4a12-974d-77a158b69d16" />
<img width="1193" height="611" alt="6" src="https://github.com/user-attachments/assets/6eee61c4-168e-4cda-abb5-79ed8d3919d1" />


### 4. Robust Error Handling (Referential Integrity)
<img width="1919" height="1079" alt="7" src="https://github.com/user-attachments/assets/5e71694c-a295-4270-abda-efbfbe463f5c" />


