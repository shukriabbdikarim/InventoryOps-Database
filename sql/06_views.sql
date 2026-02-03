USE InventoryOps;
GO

/* =====================================================
   1) PUBLIC VIEW – döljer känsliga kolumner (Email)
===================================================== /

IF OBJECT_ID('dbo.v_PublicCustomers', 'V') IS NOT NULL
    DROP VIEW dbo.v_PublicCustomers;
GO

CREATE VIEW dbo.v_PublicCustomers
AS
SELECT
    CustomerId,
    Name
FROM dbo.Customers;
GO


/ =====================================================
   2) REPORT VIEW – senaste ordrar med kund
===================================================== */

IF OBJECT_ID('dbo.v_ReportLatestOrders', 'V') IS NOT NULL
    DROP VIEW dbo.v_ReportLatestOrders;
GO

CREATE VIEW dbo.v_ReportLatestOrders
AS
SELECT
    o.OrderId,
    o.OrderDate,
    c.CustomerId,
    c.Name AS CustomerName
FROM dbo.Orders o
JOIN dbo.Customers c
    ON c.CustomerId = o.CustomerId;
GO