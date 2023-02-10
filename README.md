# IonosDynamicDNSUpdater

This is an automatic Dynamic DNS updater for Ionos written in C#. It uses the Ionos API to request an UpdateURL and updates the DNS record with the current IP address. It also supports running in a Docker container and using Docker Compose.

## Prerequisites

To use IonosDynamicDNSUpdater, you need to have the following installed on your system:

- [.NET Core](https://dotnet.microsoft.com/download) (if running outside of a Docker container)
- [Docker](https://www.docker.com/get-started) (if running in a Docker container)
- [Docker Compose](https://docs.docker.com/compose/install) (if using Docker Compose)

## Installation

To install the IonosDynamicDNSUpdater, follow these steps:

1. Clone the repository to your local machine using `git clone https://github.com/eliasstepanik/IonosDynamicDNSUpdater.git`.
2. Navigate to the project directory using `cd IonosDynamicDNSUpdater`.

## Usage

To use the IonosDynamicDNSUpdater, follow these steps:

### Running outside of a Docker container

1. Create an API token in Ionos.
2. Replace the placeholder values in `appsettings.json` with your Ionos API token and the hostname for the DNS record you want to update.
3. Run the application using `dotnet run`.

### Running in a Docker container

1. Build the Docker image using `docker build -t ionosdynamicdnsupdater .`.
2. Run the Docker container using `docker run -d -e API_KEY=<your_api_key> -e DOMAINS=<comma_separated_list_of_domains> ionosdynamicdnsupdater`.

### Using Docker Compose

1. Create an API token in Ionos.
2. Replace the placeholder values in `docker-compose.yml` with your Ionos API token and the hostname for the DNS record you want to update.
3. Start the services using `docker-compose up -d`.

## Contributing

To contribute to the IonosDynamicDNSUpdater project, follow these steps:

1. Fork the repository.
2. Create a new branch for your changes.
3. Make your changes and commit them to your branch.
4. Submit a pull request to the main repository.

Please make sure to follow the coding style and add appropriate documentation for your changes.

## License

This project is licensed under the [MIT License](LICENSE).

## Contact

For any questions or feedback, please contact the maintainer at [eliasstepanik@web.de](mailto:eliasstepanik@web.de).
