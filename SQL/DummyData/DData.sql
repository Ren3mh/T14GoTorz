USE Users;
GO

INSERT INTO IATA_locations (IATA, City)
VALUES
    ('JFK', 'New York'),
    ('LAX', 'Los Angeles'),
    ('LHR', 'London'),
    ('CDG', 'Paris'),
    ('DXB', 'Dubai'),
    ('HKG', 'Hong Kong'),
    ('SIN', 'Singapore'),
    ('FRA', 'Frankfurt'),
    ('PEK', 'Beijing'),
    ('SYD', 'Sydney');

INSERT INTO FlightsTable1 (DepartureTime, ArrivalTime, IataDestination, IataOrigin)
VALUES
    ('2025-03-20 08:00:00', '2025-03-20 16:00:00', 'LHR', 'JFK'),
    ('2025-03-20 09:00:00', '2025-03-20 17:00:00', 'CDG', 'LAX'),
    ('2025-03-20 10:00:00', '2025-03-20 18:00:00', 'DXB', 'HKG'),
    ('2025-03-20 11:00:00', '2025-03-20 19:00:00', 'SIN', 'FRA'),
    ('2025-03-20 12:00:00', '2025-03-20 20:00:00', 'PEK', 'SYD'),
    ('2025-03-20 13:00:00', '2025-03-20 21:00:00', 'JFK', 'LAX'),
    ('2025-03-20 14:00:00', '2025-03-20 22:00:00', 'LHR', 'CDG'),
    ('2025-03-20 15:00:00', '2025-03-20 23:00:00', 'DXB', 'SIN'),
    ('2025-03-20 16:00:00', '2025-03-21 01:00:00', 'HKG', 'FRA'),
    ('2025-03-20 17:00:00', '2025-03-21 02:00:00', 'PEK', 'SYD');

    INSERT INTO FlightPathsTable1 (Fare, Luggage, OutboundId, HomeboundId)
VALUES
    (500.00, 1, 1, 6),
    (600.00, 1, 2, 7),
    (700.00, 0, 3, 8),
    (800.00, 1, 4, 9),
    (900.00, 0, 5, 10),
    (550.00, 1, 6, 1),
    (650.00, 0, 7, 2),
    (750.00, 1, 8, 3),
    (850.00, 0, 9, 4),
    (950.00, 1, 10, 5);
