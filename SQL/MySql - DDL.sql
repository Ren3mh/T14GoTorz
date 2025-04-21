DROP SCHEMA Gotorz;

CREATE SCHEMA `Gotorz` ;

USE Gotorz;

CREATE TABLE IATA_locations (
    Id   INT AUTO_INCREMENT,
    IATA VARCHAR(3)   NOT NULL UNIQUE,
    City TEXT NOT NULL,
    CONSTRAINT PK_IATA_locations PRIMARY KEY (Id)
);

CREATE TABLE Flights (
    Id                INT AUTO_INCREMENT,
    DepartureTime     DATETIME NOT NULL,
    ArrivalTime       DATETIME NOT NULL,
    IATADestinationId INT      NOT NULL,
    IATAOriginId      INT      NOT NULL,
    CONSTRAINT FK1_Flights_IATA_locations FOREIGN KEY (IATADestinationId) REFERENCES IATA_locations (Id),
    CONSTRAINT FK2_Flights_IATA_locations FOREIGN KEY (IATAOriginId) REFERENCES IATA_locations (Id),
    CONSTRAINT PK_Flights PRIMARY KEY (Id)
);

CREATE TABLE Hotels (
    Id              INT AUTO_INCREMENT,
    CheckIn         DATETIME        NOT NULL,
    CheckOut        DATETIME        NOT NULL,
    Rate            DECIMAL (15, 2) NOT NULL,
    HotelName       TEXT            NOT NULL,
    Address         TEXT            NOT NULL,
    Telephonenumber TEXT            NOT NULL,
    Email           TEXT            NOT NULL,
    Description     TEXT            NOT NULL,
    CONSTRAINT PK_Hotels PRIMARY KEY (Id)
);

CREATE TABLE TravelPackages (
    Id          INT AUTO_INCREMENT,
    Title       TEXT NOT NULL,
    Description TEXT NOT NULL,
    HotelId     INT  NOT NULL,
    CONSTRAINT FK1_TravelPackages_Hotels FOREIGN KEY (HotelId) REFERENCES Hotels (Id),
    CONSTRAINT PK_TravelPackages PRIMARY KEY (Id)
);

CREATE TABLE Flightpaths (
    Id                INT AUTO_INCREMENT,
    Fare              DECIMAL (15, 2) NOT NULL,
    Luggage           BIT(1)      NOT NULL,
    OutboundFlightId  INT             NOT NULL,
    HomeboundFlightId INT             NOT NULL,
    TravelPackageId   INT             NOT NULL,
    CONSTRAINT FK1_Flightpaths_TravelPackages FOREIGN KEY (TravelPackageId) REFERENCES TravelPackages (Id),
    CONSTRAINT FK2_Flightpaths_Flights FOREIGN KEY (OutboundFlightId) REFERENCES Flights (Id),
    CONSTRAINT FK3_Flightpaths_Flights FOREIGN KEY (HomeboundFlightId) REFERENCES Flights (Id),
    CONSTRAINT PK_Flightpaths PRIMARY KEY (Id)
);