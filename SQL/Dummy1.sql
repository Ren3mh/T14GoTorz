USE GotorzAppContext;
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

-- Insert 30 Hotels (10 per month from May to July 2025)
INSERT INTO Hotels (CheckIn, CheckOut, Rate, HotelName, Address, Telephonenumber, Email, Description)
VALUES
-- May Hotels
('2025-05-10 14:00:00', '2025-05-15 11:00:00', 180.00, 'Hotel F', '101 Sunset Blvd, Los Angeles', '111-222-3333', 'hotelF@example.com', 'Stylish stay in downtown LA.'),
('2025-05-11 15:00:00', '2025-05-16 10:00:00', 220.00, 'Hotel G', '505 King St, London', '222-333-4444', 'hotelG@example.com', 'Elegant British charm in London.'),
('2025-05-12 13:00:00', '2025-05-17 09:00:00', 160.00, 'Hotel H', '77 Champs-Élysées, Paris', '333-444-5555', 'hotelH@example.com', 'Parisian luxury with Eiffel views.'),
('2025-05-13 14:00:00', '2025-05-18 12:00:00', 275.00, 'Hotel I', 'Palm Ave, Dubai', '444-555-6666', 'hotelI@example.com', 'Sleek oasis near Dubai Marina.'),
('2025-05-14 15:00:00', '2025-05-19 11:00:00', 210.00, 'Hotel J', '123 Tokyo Tower Rd, Tokyo', '555-666-7777', 'hotelJ@example.com', 'Tokyo views near the tower.'),
('2025-05-15 16:00:00', '2025-05-20 10:00:00', 190.00, 'Hotel K', 'Ocean Dr, Honolulu', '666-777-8888', 'hotelK@example.com', 'Steps away from the beach.'),
('2025-05-16 13:00:00', '2025-05-21 09:00:00', 165.00, 'Hotel L', 'Colosseum St, Rome', '777-888-9999', 'hotelL@example.com', 'Roman charm near Colosseum.'),
('2025-05-17 14:00:00', '2025-05-22 12:00:00', 240.00, 'Hotel M', 'Gondola Way, Venice', '888-999-0000', 'hotelM@example.com', 'Venetian elegance on the canal.'),
('2025-05-18 15:00:00', '2025-05-23 11:00:00', 195.00, 'Hotel N', 'Harbourfront, Sydney', '999-000-1111', 'hotelN@example.com', 'Modern stay near Opera House.'),
('2025-05-19 16:00:00', '2025-05-24 10:00:00', 230.00, 'Hotel O', 'Sagrada Plaza, Barcelona', '000-111-2222', 'hotelO@example.com', 'Next to the Sagrada Familia.'),

-- June Hotels
('2025-06-01 14:00:00', '2025-06-06 12:00:00', 185.00, 'Hotel P', 'Canal St, Amsterdam', '123-987-4561', 'hotelP@example.com', 'Cozy and central in Amsterdam.'),
('2025-06-02 15:00:00', '2025-06-07 11:00:00', 205.00, 'Hotel Q', 'Blue Lagoon Ave, Reykjavik', '321-654-9870', 'hotelQ@example.com', 'Icelandic vibes and hot springs.'),
('2025-06-03 13:00:00', '2025-06-08 10:00:00', 195.00, 'Hotel R', 'Zocalo Plaza, Mexico City', '456-321-6549', 'hotelR@example.com', 'Cultural heart of Mexico City.'),
('2025-06-04 14:00:00', '2025-06-09 09:00:00', 175.00, 'Hotel S', 'Golden Mile, Cape Town', '654-789-3210', 'hotelS@example.com', 'Spectacular views of Table Mountain.'),
('2025-06-05 12:00:00', '2025-06-10 08:00:00', 280.00, 'Hotel T', 'Ayala Ave, Manila', '789-456-1230', 'hotelT@example.com', 'Metro Manila luxury and comfort.'),
('2025-06-06 13:30:00', '2025-06-11 10:30:00', 215.00, 'Hotel U', 'Gangnam Blvd, Seoul', '456-789-1234', 'hotelU@example.com', 'Trendy spot in Seoul’s heart.'),
('2025-06-07 15:00:00', '2025-06-12 11:00:00', 190.00, 'Hotel V', 'Chao Phraya Rd, Bangkok', '654-321-9876', 'hotelV@example.com', 'Riverside elegance in Bangkok.'),
('2025-06-08 16:00:00', '2025-06-13 12:00:00', 250.00, 'Hotel W', 'Fifth Ave, New York', '321-123-4567', 'hotelW@example.com', 'Iconic views near Central Park.'),
('2025-06-09 17:00:00', '2025-06-14 09:00:00', 270.00, 'Hotel X', 'Hollywood Blvd, LA', '987-321-6543', 'hotelX@example.com', 'Celebrity-style comfort in LA.'),
('2025-06-10 18:00:00', '2025-06-15 08:00:00', 300.00, 'Hotel Y', 'Bayfront, Singapore', '123-654-7890', 'hotelY@example.com', 'Singapore skyline and Marina Bay.'),

-- July Hotels
('2025-07-01 14:00:00', '2025-07-06 12:00:00', 200.00, 'Hotel Z', 'La Rambla, Barcelona', '654-321-7777', 'hotelZ@example.com', 'Culture and cuisine in Barcelona.'),
('2025-07-02 15:00:00', '2025-07-07 11:00:00', 185.00, 'Hotel AA', 'Beach Rd, Bali', '321-555-7894', 'hotelAA@example.com', 'Beachfront bliss in Bali.'),
('2025-07-03 13:00:00', '2025-07-08 09:00:00', 230.00, 'Hotel AB', 'Ipanema Ave, Rio', '777-888-9998', 'hotelAB@example.com', 'Vibrant Rio beach escape.'),
('2025-07-04 14:00:00', '2025-07-09 10:00:00', 160.00, 'Hotel AC', 'Old Town, Marrakech', '999-111-3333', 'hotelAC@example.com', 'Moroccan magic in the medina.'),
('2025-07-05 15:00:00', '2025-07-10 11:00:00', 220.00, 'Hotel AD', 'Santorini Cliffs, Greece', '123-456-9999', 'hotelAD@example.com', 'Sunsets and sea views.'),
('2025-07-06 16:00:00', '2025-07-11 12:00:00', 210.00, 'Hotel AE', 'Zurich Lakefront', '222-333-8888', 'hotelAE@example.com', 'Swiss precision and lake breeze.'),
('2025-07-07 17:00:00', '2025-07-12 10:00:00', 195.00, 'Hotel AF', 'Damrak St, Amsterdam', '333-444-9999', 'hotelAF@example.com', 'Dutch charm in central city.'),
('2025-07-08 18:00:00', '2025-07-13 09:00:00', 240.00, 'Hotel AG', 'Victoria Harbour, Hong Kong', '444-555-1111', 'hotelAG@example.com', 'Luxury at the harbor.'),
('2025-07-09 19:00:00', '2025-07-14 08:00:00', 205.00, 'Hotel AH', 'Gastown, Vancouver', '555-666-0000', 'hotelAH@example.com', 'Trendy retreat in Vancouver.'),
('2025-07-10 20:00:00', '2025-07-15 08:00:00', 260.00, 'Hotel AI', 'Arctic View Rd, Alaska', '666-777-1111', 'hotelAI@example.com', 'Northern lights and comfort.');


-- Insert more Flights for may
-- Insert dummy data into Flights table for May
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
-- Hotel F (Los Angeles, LAX, ID: 1)
('2025-05-10 08:00:00', '2025-05-10 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CDG'), (SELECT Id FROM IATA_locations WHERE IATA = 'LAX')), -- Depart from Paris (CDG)
('2025-05-15 12:00:00', '2025-05-15 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LAX'), (SELECT Id FROM IATA_locations WHERE IATA = 'CDG')), -- Return to Paris (CDG)

-- Hotel G (London, LHR, ID: 2)
('2025-05-11 13:00:00', '2025-05-11 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'JFK'), (SELECT Id FROM IATA_locations WHERE IATA = 'LHR')), -- Depart from New York (JFK)
('2025-05-16 10:00:00', '2025-05-16 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LHR'), (SELECT Id FROM IATA_locations WHERE IATA = 'JFK')), -- Return to New York (JFK)

-- Hotel H (Paris, CDG, ID: 3)
('2025-05-12 13:00:00', '2025-05-12 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'DXB'), (SELECT Id FROM IATA_locations WHERE IATA = 'CDG')), -- Depart from Dubai (DXB)
('2025-05-17 09:00:00', '2025-05-17 13:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CDG'), (SELECT Id FROM IATA_locations WHERE IATA = 'DXB')), -- Return to Dubai (DXB)

-- Hotel I (Dubai, DXB, ID: 4)
('2025-05-13 14:00:00', '2025-05-13 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'HND'), (SELECT Id FROM IATA_locations WHERE IATA = 'DXB')), -- Depart from Tokyo (HND)
('2025-05-18 12:00:00', '2025-05-18 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'DXB'), (SELECT Id FROM IATA_locations WHERE IATA = 'HND')), -- Return to Tokyo (HND)

-- Hotel J (Tokyo, HND, ID: 5)
('2025-05-14 15:00:00', '2025-05-14 19:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'FCO'), (SELECT Id FROM IATA_locations WHERE IATA = 'HND')), -- Depart from Rome (FCO)
('2025-05-19 11:00:00', '2025-05-19 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'HND'), (SELECT Id FROM IATA_locations WHERE IATA = 'FCO')), -- Return to Rome (FCO)

-- Hotel K (Honolulu, HNL, ID: 6)
('2025-05-15 16:00:00', '2025-05-15 20:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'SYD'), (SELECT Id FROM IATA_locations WHERE IATA = 'HNL')), -- Depart from Sydney (SYD)
('2025-05-20 10:00:00', '2025-05-20 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'HNL'), (SELECT Id FROM IATA_locations WHERE IATA = 'SYD')), -- Return to Sydney (SYD)

-- Hotel L (Rome, FCO, ID: 7)
('2025-05-16 13:00:00', '2025-05-16 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'JTR'), (SELECT Id FROM IATA_locations WHERE IATA = 'FCO')), -- Depart from Santorini (JTR)
('2025-05-21 09:00:00', '2025-05-21 13:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'FCO'), (SELECT Id FROM IATA_locations WHERE IATA = 'JTR')), -- Return to Santorini (JTR)

-- Hotel M (Venice, VCE, ID: 8)
('2025-05-17 14:00:00', '2025-05-17 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'GIG'), (SELECT Id FROM IATA_locations WHERE IATA = 'VCE')), -- Depart from Rio de Janeiro (GIG)
('2025-05-22 12:00:00', '2025-05-22 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'VCE'), (SELECT Id FROM IATA_locations WHERE IATA = 'GIG')), -- Return to Rio de Janeiro (GIG)

-- Hotel N (Sydney, SYD, ID: 9)
('2025-05-18 15:00:00', '2025-05-18 19:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'ANC'), (SELECT Id FROM IATA_locations WHERE IATA = 'SYD')), -- Depart from Anchorage (ANC)
('2025-05-23 11:00:00', '2025-05-23 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'SYD'), (SELECT Id FROM IATA_locations WHERE IATA = 'ANC')), -- Return to Anchorage (ANC)

-- Hotel O (Barcelona, BCN, ID: 10)
('2025-05-19 16:00:00', '2025-05-19 20:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'KEF'), (SELECT Id FROM IATA_locations WHERE IATA = 'BCN')), -- Depart from Reykjavik (KEF)
('2025-05-24 10:00:00', '2025-05-24 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'BCN'), (SELECT Id FROM IATA_locations WHERE IATA = 'KEF')); -- Return to Reykjavik (KEF)

-- Insert dummy data into Flights table for June
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
-- Hotel P (Amsterdam, AMS, ID: 11)
('2025-06-01 08:00:00', '2025-06-01 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CDG'), (SELECT Id FROM IATA_locations WHERE IATA = 'AMS')), -- Depart from Paris (CDG)
('2025-06-06 12:00:00', '2025-06-06 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'AMS'), (SELECT Id FROM IATA_locations WHERE IATA = 'CDG')), -- Return to Paris (CDG)

-- Hotel Q (Reykjavik, KEF, ID: 12)
('2025-06-02 13:00:00', '2025-06-02 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'JFK'), (SELECT Id FROM IATA_locations WHERE IATA = 'KEF')), -- Depart from New York (JFK)
('2025-06-07 11:00:00', '2025-06-07 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'KEF'), (SELECT Id FROM IATA_locations WHERE IATA = 'JFK')), -- Return to New York (JFK)

-- Hotel R (Mexico City, MEX, ID: 13)
('2025-06-03 13:00:00', '2025-06-03 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LHR'), (SELECT Id FROM IATA_locations WHERE IATA = 'MEX')), -- Depart from London (LHR)
('2025-06-08 10:00:00', '2025-06-08 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'MEX'), (SELECT Id FROM IATA_locations WHERE IATA = 'LHR')), -- Return to London (LHR)

-- Hotel S (Cape Town, CPT, ID: 14)
('2025-06-04 14:00:00', '2025-06-04 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'DXB'), (SELECT Id FROM IATA_locations WHERE IATA = 'CPT')), -- Depart from Dubai (DXB)
('2025-06-09 09:00:00', '2025-06-09 13:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CPT'), (SELECT Id FROM IATA_locations WHERE IATA = 'DXB')), -- Return to Dubai (DXB)

-- Hotel T (Manila, MNL, ID: 15)
('2025-06-05 12:00:00', '2025-06-05 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'HND'), (SELECT Id FROM IATA_locations WHERE IATA = 'MNL')), -- Depart from Tokyo (HND)
('2025-06-10 08:00:00', '2025-06-10 12:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'MNL'), (SELECT Id FROM IATA_locations WHERE IATA = 'HND')), -- Return to Tokyo (HND)

-- Hotel U (Seoul, ICN, ID: 16)
('2025-06-06 13:30:00', '2025-06-06 17:30:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LAX'), (SELECT Id FROM IATA_locations WHERE IATA = 'ICN')), -- Depart from Los Angeles (LAX)
('2025-06-11 10:30:00', '2025-06-11 14:30:00', (SELECT Id FROM IATA_locations WHERE IATA = 'ICN'), (SELECT Id FROM IATA_locations WHERE IATA = 'LAX')), -- Return to Los Angeles (LAX)

-- Hotel V (Bangkok, BKK, ID: 17)
('2025-06-07 15:00:00', '2025-06-07 19:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'SYD'), (SELECT Id FROM IATA_locations WHERE IATA = 'BKK')), -- Depart from Sydney (SYD)
('2025-06-12 11:00:00', '2025-06-12 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'BKK'), (SELECT Id FROM IATA_locations WHERE IATA = 'SYD')), -- Return to Sydney (SYD)

-- Hotel W (New York, JFK, ID: 18)
('2025-06-08 16:00:00', '2025-06-08 20:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LHR'), (SELECT Id FROM IATA_locations WHERE IATA = 'JFK')), -- Depart from London (LHR)
('2025-06-14 09:00:00', '2025-06-14 13:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'JFK'), (SELECT Id FROM IATA_locations WHERE IATA = 'LHR')), -- Return to London (LHR)

-- Hotel X (Los Angeles, LAX, ID: 19)
('2025-06-09 17:00:00', '2025-06-09 21:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CDG'), (SELECT Id FROM IATA_locations WHERE IATA = 'LAX')), -- Depart from Paris (CDG)
('2025-06-14 10:00:00', '2025-06-14 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LAX'), (SELECT Id FROM IATA_locations WHERE IATA = 'CDG')), -- Return to Paris (CDG)

-- Hotel Y (Singapore, SIN, ID: 20)
('2025-06-10 18:00:00', '2025-06-10 22:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'HKG'), (SELECT Id FROM IATA_locations WHERE IATA = 'SIN')), -- Depart from Hong Kong (HKG)
('2025-06-15 08:00:00', '2025-06-15 12:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'SIN'), (SELECT Id FROM IATA_locations WHERE IATA = 'HKG')); -- Return to Hong Kong (HKG)

-- Insert dummy data into Flights table for July
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
-- Hotel Z (Barcelona, BCN, ID: 21)
('2025-07-01 14:00:00', '2025-07-01 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'FCO'), (SELECT Id FROM IATA_locations WHERE IATA = 'BCN')), -- Depart from Rome (FCO)
('2025-07-06 12:00:00', '2025-07-06 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'BCN'), (SELECT Id FROM IATA_locations WHERE IATA = 'FCO')), -- Return to Rome (FCO)

-- Hotel AA (Bali, DPS, ID: 22)
('2025-07-02 15:00:00', '2025-07-02 19:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'SYD'), (SELECT Id FROM IATA_locations WHERE IATA = 'DPS')), -- Depart from Sydney (SYD)
('2025-07-07 11:00:00', '2025-07-07 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'DPS'), (SELECT Id FROM IATA_locations WHERE IATA = 'SYD')), -- Return to Sydney (SYD)

-- Hotel AB (Rio de Janeiro, GIG, ID: 23)
('2025-07-03 13:00:00', '2025-07-03 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LHR'), (SELECT Id FROM IATA_locations WHERE IATA = 'GIG')), -- Depart from London (LHR)
('2025-07-08 09:00:00', '2025-07-08 13:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'GIG'), (SELECT Id FROM IATA_locations WHERE IATA = 'LHR')), -- Return to London (LHR)

-- Hotel AC (Marrakech, RAK, ID: 24)
('2025-07-04 14:00:00', '2025-07-04 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CDG'), (SELECT Id FROM IATA_locations WHERE IATA = 'RAK')), -- Depart from Paris (CDG)
('2025-07-09 10:00:00', '2025-07-09 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'RAK'), (SELECT Id FROM IATA_locations WHERE IATA = 'CDG')), -- Return to Paris (CDG)

-- Hotel AD (Santorini, JTR, ID: 25)
('2025-07-05 15:00:00', '2025-07-05 19:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'FCO'), (SELECT Id FROM IATA_locations WHERE IATA = 'JTR')), -- Depart from Rome (FCO)
('2025-07-10 11:00:00', '2025-07-10 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'JTR'), (SELECT Id FROM IATA_locations WHERE IATA = 'FCO')), -- Return to Rome (FCO)

-- Hotel AE (Zurich, ZRH, ID: 26)
('2025-07-06 16:00:00', '2025-07-06 20:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'AMS'), (SELECT Id FROM IATA_locations WHERE IATA = 'ZRH')), -- Depart from Amsterdam (AMS)
('2025-07-11 12:00:00', '2025-07-11 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'ZRH'), (SELECT Id FROM IATA_locations WHERE IATA = 'AMS')), -- Return to Amsterdam (AMS)

-- Hotel AF (Amsterdam, AMS, ID: 27)
('2025-07-07 17:00:00', '2025-07-07 21:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LHR'), (SELECT Id FROM IATA_locations WHERE IATA = 'AMS')), -- Depart from London (LHR)
('2025-07-12 10:00:00', '2025-07-12 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'AMS'), (SELECT Id FROM IATA_locations WHERE IATA = 'LHR')), -- Return to London (LHR)

-- Hotel AG (Hong Kong, HKG, ID: 28)
('2025-07-08 18:00:00', '2025-07-08 22:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'SIN'), (SELECT Id FROM IATA_locations WHERE IATA = 'HKG')), -- Depart from Singapore (SIN)
('2025-07-13 09:00:00', '2025-07-13 13:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'HKG'), (SELECT Id FROM IATA_locations WHERE IATA = 'SIN')), -- Return to Singapore (SIN)

-- Hotel AH (Vancouver, YVR, ID: 29)
('2025-07-09 19:00:00', '2025-07-09 23:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LAX'), (SELECT Id FROM IATA_locations WHERE IATA = 'YVR')), -- Depart from Los Angeles (LAX)
('2025-07-14 08:00:00', '2025-07-14 12:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'YVR'), (SELECT Id FROM IATA_locations WHERE IATA = 'LAX')), -- Return to Los Angeles (LAX)

-- Hotel AI (Anchorage, ANC, ID: 30)
('2025-07-10 20:00:00', '2025-07-11 00:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'HNL'), (SELECT Id FROM IATA_locations WHERE IATA = 'ANC')), -- Depart from Honolulu (HNL)
('2025-07-15 08:00:00', '2025-07-15 12:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'ANC'), (SELECT Id FROM IATA_locations WHERE IATA = 'HNL')); -- Return to Honolulu (HNL)

-- Insert 30 TravelPackages (one per hotel from HotelId 1 to 30)
INSERT INTO TravelPackages (Title, Description, HotelId)
VALUES
-- May TravelPackages
('Los Angeles Glamour', 'Explore the glitz and glam of Los Angeles.', 1),
('London Royal Stay', 'Enjoy high tea and history in the heart of London.', 2),
('Parisian Charm', 'Experience the elegance and romance of Paris.', 3),
('Dubai Luxury', 'Indulge in the opulence and modernity of Dubai.', 4),
('Tokyo Tech Tour', 'Explore the cutting-edge technology and culture of Tokyo.', 5),
('Honolulu Beach Escape', 'Relax on the beautiful beaches of Honolulu.', 6),
('Roman Holiday', 'Immerse yourself in the rich history and cuisine of Rome.', 7),
('Venetian Elegance', 'Experience the charm and beauty of Venice.', 8),
('Sydney Harbor Views', 'Enjoy stunning views of the Sydney Harbor.', 9),
('Barcelona Sagrada', 'Explore the iconic Sagrada Familia and vibrant culture of Barcelona.', 10),

-- June TravelPackages
('Amsterdam Canals', 'Explore the charming canals and culture of Amsterdam.', 11),
('Reykjavik Adventure', 'Discover the natural wonders and vibrant culture of Reykjavik.', 12),
('Mexico City Fiesta', 'Experience the vibrant culture and history of Mexico City.', 13),
('Cape Town Vistas', 'Enjoy breathtaking views and adventures in Cape Town.', 14),
('Manila Luxury', 'Indulge in the modern comforts and vibrant energy of Manila.', 15),
('Seoul Trends', 'Experience the latest trends and technology in Seoul.', 16),
('Bangkok Riverside', 'Relax along the riverside with stunning views in Bangkok.', 17),
('New York Iconic', 'Experience the iconic sights and sounds of New York City.', 18),
('Hollywood Glamour', 'Live like a celebrity in the heart of Los Angeles.', 19),
('Singapore Skyline', 'Enjoy breathtaking views of the Singapore skyline.', 20),

-- July TravelPackages
('Barcelona Culture', 'Immerse yourself in the rich culture and cuisine of Barcelona.', 21),
('Bali Beach Bliss', 'Relax on the beautiful beaches of Bali.', 22),
('Rio de Janeiro Vibes', 'Experience the vibrant energy and stunning beaches of Rio.', 23),
('Marrakech Magic', 'Discover the enchanting medinas and rich history of Marrakech.', 24),
('Santorini Sunsets', 'Enjoy breathtaking sunsets and sea views in Santorini.', 25),
('Zurich Lake Breeze', 'Experience the serene beauty of Zurich’s lakefront.', 26),
('Amsterdam Charm', 'Explore the charming canals and historic sites of Amsterdam.', 27),
('Hong Kong Luxury', 'Indulge in luxury and stunning views at the harbor in Hong Kong.', 28),
('Vancouver Retreat', 'Relax and explore the trendy spots in Vancouver.', 29),
('Alaskan Adventure', 'Experience the natural beauty and northern lights of Alaska.', 30);

-- Insert dummy data into Flightpaths table
INSERT INTO FlightPaths (Fare, Luggage, OutboundFlightId, HomeboundFlightId, TravelPackageId)
VALUES
(850.00, 1, 2, 1, 1),  -- With luggage
(1000.00, 0, 4, 3, 2),  -- No luggage
(1150.00, 1, 6, 5, 3),  -- With luggage
(1300.00, 0, 8, 7, 4),  -- No luggage
(1450.00, 1, 10, 9, 5),  -- With luggage
(1600.00, 0, 12, 11, 6),  -- No luggage
(1750.00, 1, 14, 13, 7),  -- With luggage
(1900.00, 0, 16, 15, 8),  -- No luggage
(2050.00, 1, 18, 17, 9),  -- With luggage
(2200.00, 0, 20, 19, 10),  -- No luggage
-- June
(950.00, 1, 22, 21, 11),  -- With luggage
(1100.00, 0, 24, 23, 12),  -- No luggage
(1250.00, 1, 26, 25, 13),  -- With luggage
(1400.00, 0, 28, 27, 14),  -- No luggage
(1550.00, 1, 30, 29, 15),  -- With luggage
(1700.00, 0, 32, 31, 16),  -- No luggage
(1850.00, 1, 34, 33, 17),  -- With luggage
(2000.00, 0, 36, 35, 18),  -- No luggage
(2150.00, 1, 38, 37, 19),  -- With luggage0
(2300.00, 0, 40, 39, 20),  -- No luggage
-- July
(2450.00, 1, 42, 41, 21),  -- With luggage
(2600.00, 0, 44, 43, 22),  -- No luggage
(2750.00, 1, 46, 45, 23),  -- With luggage
(2900.00, 0, 48, 47, 24),  -- No luggage
(3050.00, 1, 50, 49, 25),  -- With luggage
(3200.00, 0, 52, 51, 26),  -- No luggage
(3350.00, 1, 54, 53, 27),  -- With luggage
(3500.00, 0, 56, 55, 28),  -- No luggage
(3650.00, 1, 58, 57, 29),  -- With luggage
(3800.00, 0, 60, 59, 30);  -- No luggage


-- Extra flight for May
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
-- Hotel F (Los Angeles, LAX, ID: 1)
('2025-05-10 08:00:00', '2025-05-10 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'JFK'), (SELECT Id FROM IATA_locations WHERE IATA = 'LAX')), -- Depart from New York (JFK)
('2025-05-15 12:00:00', '2025-05-15 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LAX'), (SELECT Id FROM IATA_locations WHERE IATA = 'JFK')), -- Return to New York (JFK) 

-- Hotel G (London, LHR, ID: 2)
('2025-05-11 13:00:00', '2025-05-11 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CDG'), (SELECT Id FROM IATA_locations WHERE IATA = 'LHR')), -- Depart from Paris (CDG)
('2025-05-16 10:00:00', '2025-05-16 14:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'LHR'), (SELECT Id FROM IATA_locations WHERE IATA = 'CDG')); -- Return to Paris (CDG)

-- Extra flight for June
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
-- Hotel P (Amsterdam, AMS, ID: 11)
('2025-06-01 08:00:00', '2025-06-01 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'JFK'), (SELECT Id FROM IATA_locations WHERE IATA = 'AMS')), -- Depart from New York (JFK)
('2025-06-06 12:00:00', '2025-06-06 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'AMS'), (SELECT Id FROM IATA_locations WHERE IATA = 'JFK')), -- Return to New York (JFK)

-- Hotel Q (Reykjavik, KEF, ID: 12)
('2025-06-02 13:00:00', '2025-06-02 17:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'CDG'), (SELECT Id FROM IATA_locations WHERE IATA = 'KEF')), -- Depart from Paris (CDG)
('2025-06-07 11:00:00', '2025-06-07 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'KEF'), (SELECT Id FROM IATA_locations WHERE IATA = 'CDG')); -- Return to Paris (CDG)

-- Extra flight for July
INSERT INTO Flights (DepartureTime, ArrivalTime, IATADestinationId, IATAOriginId)
VALUES
-- Hotel Z (Barcelona, BCN, ID: 21)
('2025-07-01 14:00:00', '2025-07-01 18:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'SYD'), (SELECT Id FROM IATA_locations WHERE IATA = 'BCN')), -- Depart from Sydney (SYD)
('2025-07-06 12:00:00', '2025-07-06 16:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'BCN'), (SELECT Id FROM IATA_locations WHERE IATA = 'SYD')), -- Return to Sydney (SYD)

-- Hotel AA (Bali, DPS, ID: 22)
('2025-07-02 15:00:00', '2025-07-02 19:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'FCO'), (SELECT Id FROM IATA_locations WHERE IATA = 'DPS')), -- Depart from Rome (FCO)
('2025-07-07 11:00:00', '2025-07-07 15:00:00', (SELECT Id FROM IATA_locations WHERE IATA = 'DPS'), (SELECT Id FROM IATA_locations WHERE IATA = 'FCO')); -- Return to Rome (FCO)

INSERT INTO Flightpaths (Fare, Luggage, OutboundFlightId, HomeboundFlightId, TravelPackageId)
VALUES
(850.00, 1, 62, 61, 1),  -- With luggage
(1000.00, 0, 64, 63, 2),  -- No luggage
(1150.00, 1, 66, 65, 11),  -- With luggage
(1300.00, 0, 68, 67, 12),  -- No luggage
(1450.00, 1, 70, 69, 21),  -- With luggage
(1600.00, 0, 72, 71, 22);  -- No luggage