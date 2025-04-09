USE Gotorz;

GO
CREATE VIEW vwFlights AS
SELECT
	ft.[Id]
	,[DepartureTime] 
	,[ArrivalTime]
	,dest.[City] AS [Destination]
	,ori.[City] AS [Origin]
	,dest.[IATA] AS [IataDestination]
	,ori.[IATA] AS [IataOrigin]
FROM 
	Flights AS ft 
	INNER JOIN
	IATA_locations AS dest ON ft.IataDestinationId = dest.Id
	INNER JOIN
	IATA_locations AS ori ON ft.IataOriginId = ori.Id;


GO

