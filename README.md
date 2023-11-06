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

## Time Delay

In this Lambda function, a time delay is introduced between 20 and 70 seconds. This time delay is implemented for a specific purpose, and here's why:

- **Simulating Realistic Behavior**: The time delay simulates a more realistic scenario, where the function takes some time to process data before responding. In a real-world application, there might be various tasks like data processing, external API calls, or calculations that can take different amounts of time to complete.

- **Load Testing and Resilience**: By adding variability to the response time, the function can be subjected to load testing to assess its resilience. It helps evaluate how well the function handles varying workloads and ensures that it doesn't become a bottleneck under heavy usage.

- **Demonstrating Tracing with OpenTelemetry**: The time delay is used to demonstrate the usage of tracing and performance monitoring using OpenTelemetry. The delay is recorded as an attribute in the tracing information, allowing you to analyze the impact of the delay on the function's performance.

## How the Time Delay Works

The time delay is introduced as follows in the code:

int delayTimeSeconds = 20 + (int)(rnd.NextDouble() * 50) * 1000;
Thread.Sleep(delayTimeSeconds);
span.SetAttribute("DelayTimeSeconds", delayTimeSeconds / 1000);

- **delayTimeSeconds is calculated as a random value between 20 and 70 seconds (20000 to 70000 milliseconds). This introduces variability in the response time.

- **Thread.Sleep(delayTimeSeconds) causes the function to pause for the specified time, simulating the delay.

- **The delay time is then recorded as an attribute in the tracing information for monitoring and analysis.



### Usage
The Lambda function can be triggered via the API Gateway endpoint. It retrieves a list of assets from the Alpaca API and returns the asset details in JSON format.

## Functionality
The `FunctionHandler` method in the `Function` class retrieves and processes assets from the Alpaca API. The function also integrates tracing using OpenTelemetry and exports data to a Jaeger instance.
