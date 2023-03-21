# PubSub-Pattern
This small solution demonstrates the concepts of the Publish/Subscribe pattern.

### How to set set up? ###
A docker environment is required.

From the compose folder run the command: "docker compose -f infrastructure.yml up -d" This will start a container for Redis. Redis acts as the message broker in this solution.

The docker compose setup is not yet working as expected for the microservices.

In order to test the solution please start it in debug mode from Visual Studio.
Go to the properties of the solution and setup multiple startup projects: CentralBank, DebositBranch, LoanBranch.

A Swagger UI should be oppend at start-up, when posting a request to /api/CentralBank/index, the CentralBank solution will receive the request, process it and publish it.
The subscriber services(DepositBranch and LoanBranch) will be subscribed to that topic and display the information published by the CentralBank.
