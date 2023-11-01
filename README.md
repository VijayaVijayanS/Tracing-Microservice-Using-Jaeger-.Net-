# Alpaca API Console Application - Retrieve and Display Assets

This console application demonstrates how to use the Alpaca API to retrieve and display a list of assets. The application utilizes the Alpaca .NET SDK and OpenTelemetry for tracing and monitoring.

# Prerequisites

* .NET 7.0 
* Alpaca API credentials (API key and secret key) from the Alpaca dashboard

# Configuration

-> Ensure that you have valid Alpaca API credentials. Update the SecretKey constructor in the Main method of the Program.cs file with your API key and secret key:

             var api = Environments.Paper.GetAlpacaTradingClient(new SecretKey("YOUR_API_KEY", "YOUR_SECRET_KEY"));


# Usage

* Build the solution.
* Run the application.

The application will retrieve a list of active assets from the Alpaca API and display them in the console.


# OpenTelemetry Configuration

-> The application is configured to use OpenTelemetry for tracing and monitoring. It exports traces to Jaeger. Ensure that you have Jaeger running and replace the AgentHost and AgentPort with your Jaeger agent's host and port:

             .AddJaegerExporter(o =>
            {
             o.AgentHost = "YOUR_JAEGER_HOST";
             o.AgentPort = YOUR_JAEGER_PORT;
             })



# Code Explanation

The application has the following structure:

* Program.cs: Contains the main code for the Alpaca API console application.

* OpenTelemetry Configuration: The application is instrumented with OpenTelemetry for distributed tracing. It captures and exports 
  traces to a Jaeger instance running at 54.196.55.118 on port 6831.

* RetrieveAndDisplayAssetsAsync: This method retrieves a list of active assets from the Alpaca API and displays them on the console. If 
  no assets are found or an error occurs during the retrieval process, appropriate messages are displayed.
