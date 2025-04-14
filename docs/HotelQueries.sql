
--Clients with last name starting with 'n'

SELECT FirstName, LastName, Email
FROM Customers
WHERE LastName LIKE 'N%';

-- Join 
SELECT b.BookingId, c.FirstName, r.RoomNumber
FROM Bookings b
JOIN Customers c ON b.CustomerId = c.CustomerId
JOIN Rooms r ON b.RoomId = r.RoomId;

-- Count + GROUP BY
SELECT c.FirstName, COUNT(*) AS TotalBookings
FROM Customers c
JOIN Bookings b ON c.CustomerId = b.CustomerId
GROUP BY c.FirstName;

-- Subquery
SELECT FirstName, LastName
FROM Customers
WHERE CustomerId IN (
    SELECT CustomerId
    FROM Bookings
    GROUP BY CustomerId
    HAVING COUNT(*) > 1
);
