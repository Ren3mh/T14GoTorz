CREATE DATABASE Gotorz;
GO

USE Gotorz
GO

CREATE TABLE IATA_locations
(
	Id int identity(1,1) PRIMARY KEY
	,IATA NVarChar(3) NOT NULL
	,City NVarChar(max) NOT NULL
);
GO