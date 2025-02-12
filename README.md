MeDirect Technical Assessment - Project Overview

This project provides trading functionality, enabling users to retrieve their current trade status, balance, and additional trade details. Users can view trades, create/execute trades, cancel, and retry trades via API while ensuring seamless integration with RabbitMQ for message queuing.

Key Features

● Trade Execution & Retrieval

RESTful APIs allow users to execute trades, retrieve trade history, check their current trade status, balance, and view additional trade details. Users can also cancel and retry trades.

● Database Integration

Trades are stored using Entity Framework Core with a code-first approach in a relational database.

● RabbitMQ Messaging

Trades trigger messages sent to a RabbitMQ queue for further processing. This is hosted using Docker for seamless containerized message brokering.

● Unit Testing

Only data model functionality has been covered by unit tests, excluding the terminal and web applications.


Current Status

● All core functionalities, including APIs, database integration, message queues, and Docker, are implemented.

● For logging, only the default logging provided by the project is available; no additional logging mechanisms or features have been implemented.

Setup Instructions
1. Update appsettings.json with the correct database connection.
2. Configure the RabbitMQ host (currently set to the generic default).
3. The project should automatically update TerminalAppTrade’s appsettings.json on clean & build. If not, manually copy the updated configuration from the WebApp project.

For any questions or clarifications, feel free to reach out.
