USE [Users]
GO

/****** Object: Table [dbo].[FlightsTable] Script Date: 19-03-2025 20:12:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FlightsTable1] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [DepartureTime]   DATETIME2 (7)  NOT NULL,
    [ArrivalTime]     DATETIME2 (7)  NOT NULL,
    [IataDestination] NVARCHAR (3)   NOT NULL FOREIGN KEY REFERENCES IATA_locations(IATA),
    [IataOrigin]      NVARCHAR (3)   NOT NULL FOREIGN KEY REFERENCES IATA_locations(IATA)
);