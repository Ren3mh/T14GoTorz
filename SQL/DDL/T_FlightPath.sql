USE Gotorz
GO

CREATE TABLE [dbo].[Flightpaths] (
    [Id]          INT             IDENTITY (1, 1) primary key,
    [Fare]        DECIMAL (18, 2) NOT NULL,
    [Luggage]     BIT             NOT NULL,
    [OutboundId]  INT             NOT NULL FOREIGN KEY REFERENCES Flights(Id),
    [HomeboundId] INT             NOT NULL FOREIGN KEY REFERENCES Flights(Id)
);