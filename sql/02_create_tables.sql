USE InventoryOps;
GO

-- Customers
IF OBJECT_ID('dbo.Customers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Customers (
        CustomerId INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Email NVARCHAR(200) NOT NULL,
        CONSTRAINT UQ_Customers_Email UNIQUE (Email)
    );
END
GO

-- Suppliers
IF OBJECT_ID('dbo.Suppliers', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Suppliers (
        SupplierId INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        ContactEmail NVARCHAR(200) NOT NULL,
        CONSTRAINT UQ_Suppliers_ContactEmail UNIQUE (ContactEmail)
    );
END
GO

-- Products
IF OBJECT_ID('dbo.Products', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Products (
        ProductId INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        SupplierId INT NOT NULL,
        UnitPrice DECIMAL(10,2) NOT NULL,
        StockQuantity INT NOT NULL,
        Status NVARCHAR(20) NOT NULL,
        CONSTRAINT CK_Products_Status CHECK (Status IN ('Active','Inactive')),
        CONSTRAINT FK_Products_Suppliers FOREIGN KEY (SupplierId) REFERENCES dbo.Suppliers(SupplierId)
    );
END
GO

-- Orders
IF OBJECT_ID('dbo.Orders', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Orders (
        OrderId INT IDENTITY(1,1) PRIMARY KEY,
        CustomerId INT NOT NULL,
        OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES dbo.Customers(CustomerId)
    );
END
GO

-- OrderRows (transaction / kopplingstabell)
IF OBJECT_ID('dbo.OrderRows', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderRows (
        OrderRowId INT IDENTITY(1,1) PRIMARY KEY,
        OrderId INT NOT NULL,
        ProductId INT NOT NULL,
        Quantity INT NOT NULL,
        UnitPrice DECIMAL(10,2) NOT NULL,
        CONSTRAINT CK_OrderRows_Quantity CHECK (Quantity > 0),
        CONSTRAINT FK_OrderRows_Orders FOREIGN KEY (OrderId) REFERENCES dbo.Orders(OrderId),
        CONSTRAINT FK_OrderRows_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
    );
END
GO

-- StockMovements (för "latest activity feed")
IF OBJECT_ID('dbo.StockMovements', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.StockMovements (
        MovementId INT IDENTITY(1,1) PRIMARY KEY,
        ProductId INT NOT NULL,
        ChangeQty INT NOT NULL,
        Reason NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_StockMovements_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
    );
END
GO