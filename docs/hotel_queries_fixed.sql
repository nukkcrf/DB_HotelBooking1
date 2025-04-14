
-- 1. SELECT  WHERE ORDER BY
SELECT FirstName, LastName, Email
FROM Customers
WHERE LastName LIKE 'S%'
ORDER BY FirstName;

-- 2. JOIN  Bookings, Customers and Rooms
SELECT b.BookingId, c.FirstName, c.LastName, r.RoomNumber, b.CheckInDate, b.CheckOutDate
FROM Bookings b
JOIN Customers c ON b.CustomerId = c.CustomerId
JOIN Rooms r ON b.RoomId = r.RoomId;

-- 3. GROUP BY + COUNT 
SELECT c.FirstName, c.LastName, COUNT(b.BookingId) AS TotalBookings
FROM Customers c
LEFT JOIN Bookings b ON c.CustomerId = b.CustomerId
GROUP BY c.FirstName, c.LastName;

-- 4. SUBQUERY - BOOKINGS MORE THAN 1
SELECT FirstName, LastName
FROM Customers
WHERE CustomerId IN (
    SELECT CustomerId
    FROM Bookings
    GROUP BY CustomerId
    HAVING COUNT(*) > 1
);
