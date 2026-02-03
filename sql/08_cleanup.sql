USE InventoryOps;
GO
-- Cleanup script (körs endast manuellt vid behov)

-- Drop views
IF OBJECT_ID('dbo.v_ReportLatestOrders', 'V') IS NOT NULL
    DROP VIEW dbo.v_ReportLatestOrders;
GO

IF OBJECT_ID('dbo.v_PublicCustomers', 'V') IS NOT NULL
    DROP VIEW dbo.v_PublicCustomers;
GO

-- Drop tables (i rätt ordning pga FK)
IF OBJECT_ID('dbo.StockMovements', 'U') IS NOT NULL
    DROP TABLE dbo.StockMovements;

IF OBJECT_ID('dbo.OrderRows', 'U') IS NOT NULL
    DROP TABLE dbo.OrderRows;

IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL
    DROP TABLE dbo.Orders;

IF OBJECT_ID('dbo.Products', 'U') IS NOT NULL
    DROP TABLE dbo.Products;

IF OBJECT_ID('dbo.Suppliers', 'U') IS NOT NULL
    DROP TABLE dbo.Suppliers;

IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
    DROP TABLE dbo.Customers;
GO
