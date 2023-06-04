# Missing Person Registration and Searching

This project is a web application for registering and searching missing persons. It provides a platform for users to submit information and images of missing individuals, and allows others to search and view the registered missing persons. It also supports search by image by comparing the searched image with our images that are stored in the databse by using dotnetFaceRecognition package.

## Features

- User Registration and Authentication: Users can create an account, log in, and manage their profile.
- Missing Person Registration: Users can submit information about missing persons, including their name, description, and images.
- Search Functionality: Users can search for missing persons based on their name or image.
- Admin Approval: Administrators can approve or disapprove registered missing persons.
- Role Management: Administrators can assign roles to users for managing the system.
- Show statistic: Administrators can see stats about the system.

## Technologies Used

- ASP.NET Core: The web application framework used for building the application.
- Entity Framework Core: The object-relational mapper used for database operations.
- Microsoft SQL Server: The database management system for storing application data.
- Razor Pages: The view engine used for rendering the user interface.
- Azure: The cloud platform used for hosting and deployment.

## Getting Started

To run this project locally, follow these steps:

1. Clone the repository: `git clone https://github.com/your-username/Missing-Person-Registration-and-Searching.git`
2. Navigate to the project directory: `cd Missing-Person-Registration-and-Searching`
3. Install the necessary packages: `dotnet restore`
4. Set up the database connection in the appsettings.json file.
5. Apply the database migrations: vs code`dotnet ef database update` or visual studio `Add-Migration` then `Update-Database`
6. Start the application: `dotnet run`
7. Access the application in your web browser: `http://localhost:5000`

## Contributing

Contributions to this project are welcome. If you have any ideas, bug reports, or feature requests, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
