USE [Users]
GO

/****** Object: Table [dbo].[FlightPathsTable] Script Date: 19-03-2025 20:12:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FlightPathsTable1] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Fare]        DECIMAL (18, 2) NOT NULL,
    [Luggage]     BIT             NOT NULL,
    [OutboundId]  INT             NOT NULL,
    [HomeboundId] INT             NOT NULL
);

GO
ALTER TABLE [dbo].[FlightPathsTable1]
    ADD CONSTRAINT [PK_FlightPathsTable1] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[FlightPathsTable1]
    ADD CONSTRAINT [FK_FlightPathsTable_FlightsTable_HomeboundId1] FOREIGN KEY ([HomeboundId]) REFERENCES [dbo].[FlightsTable1] ([Id]);


GO
ALTER TABLE [dbo].[FlightPathsTable1]
    ADD CONSTRAINT [FK_FlightPathsTable_FlightsTable_OutboundId1] FOREIGN KEY ([OutboundId]) REFERENCES [dbo].[FlightsTable1] ([Id]);
