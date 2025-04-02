USE Gotorz;
Go

-- Insert IATA locations
INSERT INTO IATA_locations (IATA, City)
VALUES
('CDG', 'Paris, France'),
('DPS', 'Bali, Indonesia'),
('JFK', 'New York, USA'),
('HND', 'Tokyo, Japan'),
('FCO', 'Rome, Italy'),
('DXB', 'Dubai, UAE'),
('SYD', 'Sydney, Australia'),
('LHR', 'London, UK'),
('JTR', 'Santorini, Greece'),
('GIG', 'Rio de Janeiro, Brazil'),
('ANC', 'Anchorage, Alaska, USA'),
('VCE', 'Venice, Italy'),
('BCN', 'Barcelona, Spain'),
('HNL', 'Honolulu, Hawaii, USA'),
('KEF', 'Reykjavik, Iceland'),
('AMS', 'Amsterdam, Netherlands'),
('RAK', 'Marrakech, Morocco'),
('ZRH', 'Zurich, Switzerland'),
('BKK', 'Bangkok, Thailand'),
('LAX', 'Los Angeles, USA'),
('MEX', 'Mexico City, Mexico'),
('BOM', 'Mumbai, India'),
('SIN', 'Singapore'),
('CPT', 'Cape Town, South Africa'),
('IST', 'Istanbul, Turkey'),
('HKG', 'Hong Kong, China'),
('MNL', 'Manila, Philippines'),
('YVR', 'Vancouver, Canada'),
('GRU', 'São Paulo, Brazil'),
('ICN', 'Seoul, South Korea');

-- Insert more Hotels
INSERT INTO Hotels (CheckIn, CheckOut, Rate, HotelName, Address, Telephonenumber, Email, Description)
VALUES
('2025-05-10', '2025-05-17', 175.00, 'Santorini Sunset Resort', 'Santorini, Greece', '+302286987654', 'info@santoriniresort.com', 'Beautiful sunset views in Santorini'),
('2025-05-15', '2025-05-22', 220.00, 'Dubai Luxury Suites', 'Downtown Dubai, UAE', '+97145556789', 'info@dubailuxury.com', 'Experience luxury in Dubai'),
('2025-06-05', '2025-06-12', 195.00, 'Bangkok Riverside Hotel', 'Chao Phraya River, Bangkok', '+66287654321', 'info@bangkokhotel.com', 'Scenic riverside stay in Bangkok');

-- Insert more Flights
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
('2025-05-12 14:00', '2025-05-12 20:00', 10, 11),
('2025-05-18 16:30', '2025-05-18 22:45', 12, 13),
('2025-06-08 08:20', '2025-06-08 13:30', 14, 15),
('2025-06-15 10:00', '2025-06-15 16:30', 16, 17),
('2025-06-22 07:45', '2025-06-22 14:15', 18, 19);

-- Insert more TravelPackages
INSERT INTO TravelPackages (Title, Description, HotelId)
VALUES
('Santorini Summer Escape', 'Enjoy the beautiful beaches and whitewashed houses of Santorini.', 9),
('Dubai Extravaganza', 'Experience ultimate luxury in the heart of Dubai.', 10),
('Bangkok Cultural Experience', 'Explore the rich culture and cuisine of Thailand.', 11);

-- Insert more Flightpaths
INSERT INTO Flightpaths (Fare, Luggage, OutboundFlightId, HomeboundFlightId, TravelPackageId)
VALUES
(850.00, 1, 9, 10, 9),
(1200.00, 1, 11, 12, 10),
(980.00, 1, 13, 14, 11);
