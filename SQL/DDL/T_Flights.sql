USE Gotorz
GO

CREATE TABLE [dbo].[Flights] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [DepartureTime]   DATETIME2 (7)  NOT NULL,
    [ArrivalTime]     DATETIME2 (7)  NOT NULL,
    [IataDestinationId] int   NOT NULL FOREIGN KEY REFERENCES IATA_locations(Id),
    [IataOriginId]      int   NOT NULL FOREIGN KEY REFERENCES IATA_locations(Id)
);