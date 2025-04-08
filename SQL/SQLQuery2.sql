USE Gotorz;
GO

-- Insert dummy data into IATA_locations table
INSERT INTO IATA_locations (IATA, City)
VALUES
('JFK', 'New York'),
('LAX', 'Los Angeles'),
('LHR', 'London'),
('CDG', 'Paris'),
('DXB', 'Dubai');

-- Insert dummy data into Flights table (All May Dates)
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
('2025-05-05 08:00:00', '2025-05-05 16:00:00', 2, 1), -- JFK to LAX
('2025-05-06 09:00:00', '2025-05-06 17:00:00', 3, 1), -- JFK to LHR
('2025-05-07 10:00:00', '2025-05-07 18:00:00', 4, 1), -- JFK to CDG
('2025-05-08 11:00:00', '2025-05-08 20:00:00', 5, 1), -- JFK to DXB
('2025-05-09 12:00:00', '2025-05-09 21:00:00', 1, 2); -- LAX to JFK

-- Insert additional Flights for June
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
('2025-06-10 08:00:00', '2025-06-10 16:00:00', 2, 1),
('2025-06-11 09:00:00', '2025-06-11 17:00:00', 3, 1),
('2025-06-12 10:00:00', '2025-06-12 18:00:00', 4, 1),
('2025-06-13 11:00:00', '2025-06-13 20:00:00', 5, 1),
('2025-06-14 12:00:00', '2025-06-14 21:00:00', 1, 2);

-- Insert dummy data into Hotels table (All May Dates)
INSERT INTO Hotels (CheckIn, CheckOut, Rate, HotelName, Address, Telephonenumber, Email, Description)
VALUES
('2025-05-05 14:00:00', '2025-05-10 12:00:00', 150.00, 'Hotel A', '123 Main St, New York', '123-456-7890', 'hotelA@example.com', 'A luxurious hotel in the heart of New York.'),
('2025-05-06 15:00:00', '2025-05-11 11:00:00', 200.00, 'Hotel B', '456 Elm St, Los Angeles', '234-567-8901', 'hotelB@example.com', 'A modern hotel with stunning views of Los Angeles.'),
('2025-05-07 16:00:00', '2025-05-12 10:00:00', 250.00, 'Hotel C', '789 Oak St, London', '345-678-9012', 'hotelC@example.com', 'A historic hotel in central London.'),
('2025-05-08 17:00:00', '2025-05-13 09:00:00', 300.00, 'Hotel D', '101 Pine St, Paris', '456-789-0123', 'hotelD@example.com', 'A charming hotel near the Eiffel Tower.'),
('2025-05-09 18:00:00', '2025-05-14 08:00:00', 350.00, 'Hotel E', '202 Cedar St, Dubai', '567-890-1234', 'hotelE@example.com', 'A luxurious hotel with a rooftop pool in Dubai.');

-- Insert additional Hotels for June
INSERT INTO Hotels (CheckIn, CheckOut, Rate, HotelName, Address, Telephonenumber, Email, Description)
VALUES
('2025-06-10 14:00:00', '2025-06-15 12:00:00', 155.00, 'Hotel F', '123 Main St, New York', '123-456-7890', 'hotelF@example.com', 'Cozy stay in New York.'),
('2025-06-11 15:00:00', '2025-06-16 11:00:00', 210.00, 'Hotel G', '456 Elm St, Los Angeles', '234-567-8901', 'hotelG@example.com', 'Modern luxury in LA.'),
('2025-06-12 16:00:00', '2025-06-17 10:00:00', 260.00, 'Hotel H', '789 Oak St, London', '345-678-9012', 'hotelH@example.com', 'Central London comfort.'),
('2025-06-13 17:00:00', '2025-06-18 09:00:00', 310.00, 'Hotel I', '101 Pine St, Paris', '456-789-0123', 'hotelI@example.com', 'Romantic Paris escape.'),
('2025-06-14 18:00:00', '2025-06-19 08:00:00', 360.00, 'Hotel J', '202 Cedar St, Dubai', '567-890-1234', 'hotelJ@example.com', '5-star Dubai stay.');

-- Insert dummy data into TravelPackages table
INSERT INTO TravelPackages (Title, Description, HotelId)
VALUES
('New York Getaway', 'A weekend getaway to New York City.', 1),
('Los Angeles Adventure', 'An adventure-packed trip to Los Angeles.', 2),
('London Experience', 'Experience the best of London.', 3),
('Paris Romance', 'A romantic getaway to Paris.', 4),
('Dubai Luxury', 'Luxury trip to Dubai.', 5),
('New York Summer Trip', 'Summer fun in NYC.', 6),
('LA Sunshine Tour', 'Explore sunny LA.', 7),
('London Royal Tour', 'Visit royal landmarks in London.', 8),
('Paris Summer Escape', 'Summer escape in Paris.', 9),
('Dubai Hotspot', 'Heat up your summer in Dubai.', 10);

-- Insert dummy data into Flightpaths table
USE Gotorz;
INSERT INTO Flightpaths (Fare, Luggage, OutboundFlightId, HomeboundFlightId, TravelPackageId)
VALUES
(400.00, 1, 1, 5, 2),
(500.00, 1, 2, 5, 3),
(600.00, 1, 3, 5, 4),
(700.00, 1, 4, 5, 5),
(800.00, 1, 5, 1, 1),
(405.00, 1, 6, 10, 6),
(510.00, 1, 7, 10, 7),
(620.00, 1, 8, 10, 8),
(730.00, 1, 9, 10, 9),
(840.00, 1, 10, 6, 10);
