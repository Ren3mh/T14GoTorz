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

-- Insert dummy data into Flights table
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
('2025-03-25 08:00:00', '2025-03-25 16:00:00', 2, 1), -- JFK to LAX
('2025-03-26 09:00:00', '2025-03-26 17:00:00', 3, 1), -- JFK to LHR
('2025-03-27 10:00:00', '2025-03-27 18:00:00', 4, 1), -- JFK to CDG
('2025-03-28 11:00:00', '2025-03-28 20:00:00', 5, 1), -- JFK to DXB
('2025-03-29 12:00:00', '2025-03-29 21:00:00', 1, 2); -- LAX to JFK

-- Insert dummy data into Hotels table
INSERT INTO Hotels (CheckIn, CheckOut, Rate, HotelName, Address, Telephonenumber, Email, Description)
VALUES
('2025-03-25 14:00:00', '2025-03-30 12:00:00', 150.00, 'Hotel A', '123 Main St, New York', '123-456-7890', 'hotelA@example.com', 'A luxurious hotel in the heart of New York.'),
('2025-03-26 15:00:00', '2025-03-31 11:00:00', 200.00, 'Hotel B', '456 Elm St, Los Angeles', '234-567-8901', 'hotelB@example.com', 'A modern hotel with stunning views of Los Angeles.'),
('2025-03-27 16:00:00', '2025-04-01 10:00:00', 250.00, 'Hotel C', '789 Oak St, London', '345-678-9012', 'hotelC@example.com', 'A historic hotel in central London.'),
('2025-03-28 17:00:00', '2025-04-02 09:00:00', 300.00, 'Hotel D', '101 Pine St, Paris', '456-789-0123', 'hotelD@example.com', 'A charming hotel near the Eiffel Tower.'),
('2025-03-29 18:00:00', '2025-04-03 08:00:00', 350.00, 'Hotel E', '202 Cedar St, Dubai', '567-890-1234', 'hotelE@example.com', 'A luxurious hotel with a rooftop pool in Dubai.');

-- Insert dummy data into TravelPackages table
INSERT INTO TravelPackages (Title, Description, HotelId)
VALUES
('New York Getaway', 'A weekend getaway to New York City.', 1),
('Los Angeles Adventure', 'An adventure-packed trip to Los Angeles.', 2),
('London Experience', 'Experience the best of London.', 3),
('Paris Romance', 'A romantic getaway to Paris.', 4),
('Dubai Luxury', 'Luxury trip to Dubai.', 5);

-- Insert dummy data into Flightpaths table
USE Gotorz;
INSERT INTO Flightpaths (Fare, Luggage, OutboundFlightId, HomeboundFlightId, TravelPackageId)
VALUES
(400.00, 1, 1, 5, 2), -- JFK to LAX and back
(500.00, 1, 2, 5, 3), -- JFK to LHR and back
(600.00, 1, 3, 5, 4), -- JFK to CDG and back
(700.00, 1, 4, 5, 5), -- JFK to DXB and back
(800.00, 1, 5, 1, 1); -- LAX to JFK and back