
# Web Authentication - ASP.NET

This repository contains a web authentication project built with ASP.NET. It provides features for user registration, login, and secure authentication, ensuring users can securely access the application.

## Features

- User registration with email verification
- User login with authentication
- Secure password storage with hashing
- User profile management
- Role-based access control
- User-friendly interface with responsive design

## Technologies Used

- **Frontend:**
  - HTML
  - CSS
  - JavaScript

- **Backend:**
  - ASP.NET
  - C#

- **Database:**
  - SQL Server

## Getting Started

### Prerequisites

To run this project locally, you need to have the following installed:

- Visual Studio
- .NET SDK
- SQL Server

### Installation

1. Clone the repository:
   \`\`\`bash
   git clone https://github.com/Behzad-Rajabalipour/Web-Authentication-Asp.net.git
   \`\`\`
2. Open the project in Visual Studio:
   \`\`\`bash
   cd Web-Authentication-Asp.net
   start Web-Authentication-Asp.net.sln
   \`\`\`
3. Update the database connection string in `appsettings.json` to match your SQL Server configuration.

### Running the Application

1. Open the solution file in Visual Studio.
2. Build the solution to restore the necessary packages.
3. Update the database by running the migrations:
   \`\`\`bash
   Update-Database
   \`\`\`
4. Press `F5` to run the application.

## Project Structure

\`\`\`
Web-Authentication-Asp.net/
├── Controllers/
│   ├── AccountController.cs
│   ├── HomeController.cs
│   └── ManageController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── Migrations/
├── Models/
│   ├── ApplicationUser.cs
│   └── AccountViewModels.cs
├── Views/
│   ├── Account/
│   ├── Home/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
├── appsettings.json
├── Startup.cs
└── Program.cs
\`\`\`

## Contributing

Contributions are welcome! Please follow these steps to contribute:

1. Fork the repository
2. Create a new branch (\`git checkout -b feature/your-feature-name\`)
3. Make your changes
4. Commit your changes (\`git commit -m 'Add some feature'\`)
5. Push to the branch (\`git push origin feature/your-feature-name\`)
6. Open a pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- Special thanks to all the contributors who have helped in developing this project.

---

Feel free to reach out if you have any questions or need further assistance!
