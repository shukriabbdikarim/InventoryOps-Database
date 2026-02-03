USE InventoryOps;
GO

-------------------------------------------------
-- CREATE (INSERT)
-------------------------------------------------

DECLARE @CustomerId INT =
(
    SELECT TOP 1 CustomerId
    FROM dbo.Customers
    ORDER BY CustomerId
);

INSERT INTO dbo.Orders (CustomerId, OrderDate)
VALUES (@CustomerId, GETDATE());

-------------------------------------------------
-- READ (SELECT)
-------------------------------------------------

DECLARE @OrderId INT =
(
    SELECT TOP 1 OrderId
    FROM dbo.Orders
    ORDER BY OrderId DESC
);

SELECT * 
FROM dbo.Orders
WHERE OrderId = @OrderId;

-------------------------------------------------
-- UPDATE
-------------------------------------------------

UPDATE dbo.Orders
SET OrderDate = DATEADD(DAY, -1, OrderDate)
WHERE OrderId = @OrderId;

-------------------------------------------------
-- DELETE (rätt ordning: barn ? förälder)
-------------------------------------------------

DELETE FROM dbo.OrderRows
WHERE OrderId = @OrderId;

DELETE FROM dbo.Orders
WHERE OrderId = @OrderId;