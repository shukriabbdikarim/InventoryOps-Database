USE InventoryOps;
GO

/* =====================================================
   07_security.sql
   - Skapa login + user
   - Skapa role
   - Ge SELECT på VIEWS (inte tabeller)
===================================================== */

-- 1) Skapa LOGIN (server-nivå) om den inte finns
IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = 'inventory_reader_login')
BEGIN
    CREATE LOGIN inventory_reader_login WITH PASSWORD = 'StrongPass!123';
END
GO

-- 2) Skapa USER i databasen kopplat till login
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'inventory_reader_user')
BEGIN
    CREATE USER inventory_reader_user FOR LOGIN inventory_reader_login;
END
GO

-- 3) Skapa ROLE
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = 'inventory_reader_role')
BEGIN
    CREATE ROLE inventory_reader_role;
END
GO

-- 4) Lägg user i role
IF NOT EXISTS (
    SELECT 1
    FROM sys.database_role_members rm
    JOIN sys.database_principals r ON rm.role_principal_id = r.principal_id
    JOIN sys.database_principals u ON rm.member_principal_id = u.principal_id
    WHERE r.name = 'inventory_reader_role'
      AND u.name = 'inventory_reader_user'
)
BEGIN
    ALTER ROLE inventory_reader_role ADD MEMBER inventory_reader_user;
END
GO

/* =====================================================
   5) Ge SELECT på era VIEWS (och inget på tabeller)
   Anpassat till views vi skapade i 06:
   - dbo.v_PublicCustomers
   - dbo.v_ReportLatestOrders
===================================================== */

GRANT SELECT ON dbo.v_PublicCustomers TO inventory_reader_role;
GRANT SELECT ON dbo.v_ReportLatestOrders TO inventory_reader_role;
GO

/* =====================================================
   6) Säkerställ att rollen INTE har SELECT på tabeller
   (om du råkat ge tidigare)
===================================================== */
DENY SELECT ON dbo.Customers TO inventory_reader_role;
DENY SELECT ON dbo.Orders TO inventory_reader_role;
DENY SELECT ON dbo.OrderRows TO inventory_reader_role;
DENY SELECT ON dbo.Products TO inventory_reader_role;
DENY SELECT ON dbo.Suppliers TO inventory_reader_role;
DENY SELECT ON dbo.StockMovements TO inventory_reader_role;
GO