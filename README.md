# Task Manager (Fullstack)

## 📌 Description
This is a simple fullstack application where users can create, view, update, and delete tasks.

The frontend is built with:
- HTML
- CSS
- JavaScript (DOM + fetch)

The backend is a .NET Web API that uses:
- Controllers
- Services
- DTOs
- Entity Framework Core
- SQL Server database

---

## ⚙️ How to run the project

### Backend
1. Open the project in Visual Studio
2. Navigate to `TaskManagerApi`
3. Run:
   ```bash
   dotnet restore
   dotnet ef database update
   dotnet run

Api runs on http://localhost:5043
Open Swagger: http://localhost:5043/swagger

#Frontend
Go to TaskManager/Frontend
Open index.html in your browser
The frontend connect to: http://localhost:5043/api/Tasks 

Api endpoints:
Method	  Endpoint	 Description
GET	    /api/Tasks	 Get all tasks
GET	   /api/Tasks/{id}	Get task by id
POST	/api/Tasks	   Create new task
PUT	  /api/Tasks/{id}	Update task
DELETE /api/Tasks/{id}	Delete task

Reflection

What went well:

Implementing CRUD functionality
Connecting frontend to backend
Understanding how data flows from UI → API → database → back

What was challenging:

Setting up the database and migrations
Debugging frontend fetch errors

Improvements:

Add validation for input fields
Improve UI design
Add editing functionality instead of only toggle