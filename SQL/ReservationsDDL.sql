CREATE DATABASE GotorzTest;
GO

USE GotorzTest;


GO
CREATE TABLE IATA_locations (
    Id   INT            IDENTITY (1, 1),
    IATA NVARCHAR (3)   NOT NULL UNIQUE,
    City NVARCHAR (MAX) NOT NULL,
    CONSTRaint PK_IATA_locations PRIMARY KEY (Id)
);

CREATE TABLE Flights (
    Id                INT      IDENTITY (1, 1),
    DepartureTime     DATETIME NOT NULL,
    ArrivalTime       DATETIME NOT NULL,
    IATADestinationId INT      NOT NULL,
    IATAOriginId      INT      NOT NULL,
    CONSTRAINT FK1_Flights_IATA_locations FOREIGN KEY (IATADestinationId) REFERENCES IATA_locations (Id),
    CONSTRAINT FK2_Flights_IATA_locations FOREIGN KEY (IATAOriginId) REFERENCES IATA_locations (Id)
    , constraint PK_Flights primary key (Id)
);


CREATE TABLE Hotels (
    Id              INT             IDENTITY (1, 1),
    Rate            DECIMAL (15, 2) NOT NULL,
    HotelName       NVARCHAR (MAX)  NOT NULL,
    Address         NVARCHAR (MAX)  NOT NULL,
    Telephonenumber NVARCHAR (MAX)  NOT NULL,
    Email           NVARCHAR (MAX)  NOT NULL,
    Description     NVARCHAR (MAX)  NOT NULL
    , constraint PK_Hotels primary key (Id)
);

CREATE TABLE Reservations (
 Id INT IDENTITY (1,1),
 CheckIn         DATETIME        NOT NULL,
 CheckOut        DATETIME        NOT NULL,
 HotelId		 INT			 NOT NULL,
 CONSTRAINT PK_Reservations PRIMARY KEY (Id),
 CONSTRAINT FK_Reservations_Hotels FOREIGN KEY (HotelId) REFERENCES Hotels (Id)
);

CREATE TABLE TravelPackages (
    Id          INT            IDENTITY (1, 1),
    Title       NVARCHAR (MAX) NOT NULL,
    Description NVARCHAR (MAX) NOT NULL,
    ReservationId     INT            NOT NULL,
    CONSTRAINT FK1_TravelPackages_Reservations FOREIGN KEY (ReservationId) REFERENCES Reservations (Id)
    , constraint PK_TravelPackages Primary key (Id)
);

CREATE TABLE Flightpaths (
    Id                INT             IDENTITY (1, 1),
    Fare              DECIMAL (15, 2) NOT NULL,
    Luggage           BIT             NOT NULL,
    OutboundFlightId  INT             NOT NULL,
    HomeboundFlightId INT             NOT NULL,
    TravelPackageId   INT             NOT NULL,
    CONSTRAINT FK1_Flightpaths_TravelPackages FOREIGN KEY (TravelPackageId) REFERENCES TravelPackages (Id),
    CONSTRAINT FK2_Flightpaths_Flights FOREIGN KEY (OutboundFlightId) REFERENCES Flights (Id),
    CONSTRAINT FK3_Flightpaths_Flights FOREIGN KEY (HomeboundFlightId) REFERENCES Flights (Id)
    , constraint PK_Flightpaths primary key (Id)
);