USE [Users]
GO

/****** Object: View [dbo].[Flights] Script Date: 19-03-2025 22:37:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW Flights AS
SELECT
	[Id]
	,[DepartureTime] 
	,[ArrivalTime]
	,dest.[City] AS [Destination]
	,ori.[City] AS [Origin]
	,dest.[IATA] AS [IataDestination]
	,ori.[IATA] AS [IataOrigin]
FROM 
	FlightsTable1 AS ft 
	INNER JOIN
	IATA_locations AS dest ON ft.IataDestination = dest.IATA
	INNER JOIN
	IATA_locations AS ori ON ft.IataOrigin = ori.IATA;
