USE [Users]
GO

CREATE TABLE IATA_locations
(
	IATA NVarChar(3) PRIMARY KEY,
	City NVarChar(max)
);
GO