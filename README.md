# Alpaca API AWS Lambda Function with OpenTelemetry

## Description:

A sample AWS Lambda function written in C# that interacts with the Alpaca API to retrieve and display assets. It also uses OpenTelemetry for tracing Lambda executions.

## LambdaService:

This LambdaService application retrieves and displays assets from the Alpaca API.

## Setup

### Prerequisites
- .NET Core SDK
- Alpaca API Key and Secret Key
- AWS Lambda and API Gateway setup
- Jaeger instance for tracing

### Installation
1. Clone the repository.
2. Open the solution in Visual Studio or any other compatible IDE.
3. Update the Alpaca API key and secret key in the code.
4. Deploy the Lambda function to AWS Lambda.
5. Configure API Gateway to trigger the Lambda function.
6. Set up Jaeger for tracing and update the host and port in the code.

### Usage
The Lambda function can be triggered via the API Gateway endpoint. It retrieves a list of assets from the Alpaca API and returns the asset details in JSON format.

## Functionality
The `FunctionHandler` method in the `Function` class retrieves and processes assets from the Alpaca API. The function also integrates tracing using OpenTelemetry and exports data to a Jaeger instance.

