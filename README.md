# Tracing-Microservice-Using-Jaeger-.Net-

* This code demonstrates a simple .NET console application that retrieves and displays assets from the Alpaca API. 
  The application integrates OpenTelemetry for distributed tracing and utilizes the Jaeger exporter for tracing visualization. 

* The application retrieves active assets from the Alpaca API and prints informationabout each asset, such as its symbol, name, exchange, 
  tradability, and status.

* By leveraging the OpenTelemetry library, the application traces various operations, such as asset retrieval, using the Jaeger exporter 
  to send traces to a Jaeger instance running on the specified IP address and port. The code sets up the tracer with a specific service 
  name and configures it to collect traces for the "AlpacaApp" service.

* This application, when executed within the context of AWS Lambda, can efficiently harness the capabilities of serverless computing to 
  manage and trace these operations. By setting up OpenTelemetry and Jaeger for distributed tracing.

Additionally, the code creates spans to trace the asset retrieval and display operations, providing visibility into the application's behavior.
