# Koçluk Sistemi

![Coach Service](https://github.com/user-attachments/assets/afc090c0-b1f6-45af-ae31-f10e83b0253f)

This repository contains the microservices architecture for a coaching system. It leverages gRPC and SOAP services to provide a robust and efficient platform for coaches and students.

## Architecture Overview

The system is composed of the following microservices:

* **AuthServer:**  Handles user authentication and authorization.
* **gRPC Service:** Provides real-time communication for coach analysis data.
* **SOAP Service:**  Used for user registration.
* **Student Frontend:**  Built with Vue.js, providing a user-friendly interface for students.
* **Coach Dashboard:**  Developed with .NET Core MVC and Blazor components, offering coaches tools to manage their clients and analyze data.

## Technology Stack

* **.NET Core:**  Used for building the AuthServer, gRPC Service, and Coach Dashboard.
* **gRPC:**  Enables high-performance communication for real-time data analysis.
* **SOAP:**  Facilitates user registration through a standardized protocol.
* **Vue.js:**  Powers the Student Frontend, providing a dynamic and responsive user experience.
* **Blazor:**  Used to create interactive components within the Coach Dashboard.

## Communication Flow

1. **Registration:** Students register using the SOAP service, which interacts with the AuthServer for user creation.
2. **Authentication:** Users authenticate through the AuthServer to access the system.
3. **Data Retrieval:** Coaches utilize the gRPC service to retrieve and analyze student data in real-time.
4. **Dashboard Interaction:** Coaches interact with the Coach Dashboard to manage clients, schedule sessions, and review analysis data.

## Project Structure

* **AuthServer:** Contains the authentication and authorization logic.
* **GrpcService:**  Houses the gRPC service implementation for data streaming.
* **SoapService:**  Includes the SOAP service for user registration.
* **StudentFrontend:**  Contains the Vue.js code for the student interface.
* **CoachDashboard:**  Includes the .NET Core MVC and Blazor code for the coach dashboard.

## Getting Started

1. Clone the repository: `git clone https://github.com/your-username/KoclukSistemi.git`
2. Navigate to each microservice directory and follow the specific instructions for building and running the services.
3. Configure the necessary dependencies and environment variables.

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvement, please feel free to open an issue or submit a pull request.

## License

This repository is licensed under the [MIT License](LICENSE).
