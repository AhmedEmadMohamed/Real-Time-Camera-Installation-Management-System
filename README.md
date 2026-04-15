# Camera Installion Management System

A professional web-based dashboard for managing CCTV installation jobs and customer databases. This project features real-time notifications, job tracking, and a sleek, modern UI.

## Features
- **Real-time Notifications:** Powered by SignalR to notify users of job status updates instantly.
- **Job Management:** Comprehensive dashboard to view, filter, and update job details.
- **Customer Directory:** Manage client information and contact details.
- **Modern UI:** Responsive design with a focus on usability and clean aesthetics.

---

## Getting Started

### Prerequisites
- **Backend:** .NET 8.0 SDK
- **Database:** Microsoft SQL Server
- **Frontend:** Any modern web browser

### Step-by-Step Installation

1. **Clone the repository**
   
2. **Database Setup:**
   The project uses Entity Framework Core.
   - Open the solution in Visual Studio or your preferred terminal.
   - Update the ConnectionStrings in appsettings.json to point to your SQL Server instance.
   - Run the following commands in the Package Manager Console:
       **Add-Migration Initial**
       **Update-Database**
     
3. **Run the Backend API:**
   - Press **F5** in Visual Studio or run:
       **dotnet run**
   - Ensure the API is running at https://localhost:7267 (or update the API_BASE_URL in app.js file and inside html files with your host).

4. **Launch the Frontend:**
   - Open index.html with a live server.


## SignalR Implementation
  I implemented Real-time Updates using ASP.NET Core SignalR to ensure the dashboard reflects changes immediately without refreshing.

  **How it works:**
  1. **The Hub:** A JobHub : Hub class was created on the backend to manage connections.
  2. **The Trigger:** Inside the Update endpoint, after a successful database update, the server calls.
  3. **The Client:** The frontend establishes a connection to the Hub and waits for the ReceiveJobUpdate event to happeen.
   
   

