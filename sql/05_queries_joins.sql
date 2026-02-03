USE InventoryOps;
GO

/* ============================================================
   05_queries_joins.sql
   Minst 8 SELECT:
   - 4 med JOIN
   - 2 med GROUP BY / aggregat
   - 1 med WHERE + ORDER BY
   - 1 rapportfråga (ex: latest activity / top customers)
   ============================================================ */

---------------------------------------------------------------
-- 1) JOIN: Ordrar med kundnamn + datum
---------------------------------------------------------------
SELECT
    o.OrderId,
    c.Name AS CustomerName,
    o.OrderDate
FROM dbo.Orders o
JOIN dbo.Customers c ON c.CustomerId = o.CustomerId
ORDER BY o.OrderDate DESC;
GO

---------------------------------------------------------------
-- 2) JOIN: Orderrader med produktnamn
---------------------------------------------------------------
SELECT
    o.OrderId,
    p.Name AS ProductName,
    r.Quantity,
    r.UnitPrice
FROM dbo.OrderRows r
JOIN dbo.Orders o ON o.OrderId = r.OrderId
JOIN dbo.Products p ON p.ProductId = r.ProductId
ORDER BY o.OrderId, p.Name;
GO

---------------------------------------------------------------
-- 3) JOIN: Produkter + leverantör
---------------------------------------------------------------
SELECT
    p.ProductId,
    p.Name AS ProductName,
    s.Name AS SupplierName,
    p.UnitPrice,
    p.StockQuantity,
    p.Status
FROM dbo.Products p
JOIN dbo.Suppliers s ON s.SupplierId = p.SupplierId
ORDER BY s.Name, p.Name;
GO

---------------------------------------------------------------
-- 4) JOIN: Totalbelopp per order (Order + Customer + OrderRows)
---------------------------------------------------------------
SELECT
    o.OrderId,
    c.Name AS CustomerName,
    SUM(r.Quantity * r.UnitPrice) AS OrderTotal
FROM dbo.Orders o
JOIN dbo.Customers c ON c.CustomerId = o.CustomerId
JOIN dbo.OrderRows r ON r.OrderId = o.OrderId
GROUP BY o.OrderId, c.Name
ORDER BY OrderTotal DESC;
GO

---------------------------------------------------------------
-- 5) GROUP BY / aggregat: Antal ordrar per kund
---------------------------------------------------------------
SELECT
    c.CustomerId,
    c.Name AS CustomerName,
    COUNT(o.OrderId) AS OrdersCount
FROM dbo.Customers c
LEFT JOIN dbo.Orders o ON o.CustomerId = c.CustomerId
GROUP BY c.CustomerId, c.Name
ORDER BY OrdersCount DESC;
GO

---------------------------------------------------------------
-- 6) GROUP BY / aggregat: Omsättning per produkt
---------------------------------------------------------------
SELECT
    p.ProductId,
    p.Name AS ProductName,
    SUM(r.Quantity) AS TotalQuantitySold,
    SUM(r.Quantity * r.UnitPrice) AS Revenue
FROM dbo.Products p
JOIN dbo.OrderRows r ON r.ProductId = p.ProductId
GROUP BY p.ProductId, p.Name
ORDER BY Revenue DESC;
GO

---------------------------------------------------------------
-- 7) WHERE + ORDER BY: Produkter med lågt lager (at risk)
---------------------------------------------------------------
SELECT
    p.ProductId,
    p.Name AS ProductName,
    p.StockQuantity,
    p.Status
FROM dbo.Products p
WHERE p.StockQuantity <= 10
ORDER BY p.StockQuantity ASC, p.Name;
GO

---------------------------------------------------------------
-- 8) Rapportfråga: Senaste 20 ordrar (rapportkrav)
---------------------------------------------------------------
SELECT TOP 20
    o.OrderId,
    c.Name AS CustomerName,
    o.OrderDate
FROM dbo.Orders o
JOIN dbo.Customers c ON c.CustomerId = o.CustomerId
ORDER BY o.OrderDate DESC;
GO