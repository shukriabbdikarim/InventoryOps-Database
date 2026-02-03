USE InventoryOps;
GO

-- Customers (10)
INSERT INTO dbo.Customers (Name, Email)
VALUES
('Johan Lindberg','johan1@mail.com'),
('Sara Nilsson','sara1@mail.com'),
('Peter Andersson','peter1@mail.com'),
('Amina Ali','amina1@mail.com'),
('Lina Svensson','lina1@mail.com'),
('Omar Hassan','omar1@mail.com'),
('Fatima Karim','fatima1@mail.com'),
('Erik Johansson','erik1@mail.com'),
('Maja Berg','maja1@mail.com'),
('Noah Ek','noah1@mail.com');
GO

-- Suppliers (6)
INSERT INTO dbo.Suppliers (Name, ContactEmail)
VALUES
('TechSupplier AB','contact@techsupplier.se'),
('OfficeGoods AB','info@officegoods.se'),
('Nordic IT AB','sales@nordicit.se'),
('Gadgets & Co','hello@gadgetsco.se'),
('PaperTown AB','support@papertown.se'),
('Hardware Hub','contact@hardwarehub.se');
GO

-- Products (6)  (secondary entity >=6)
INSERT INTO dbo.Products (Name, SupplierId, UnitPrice, StockQuantity, Status)
VALUES
('Laptop', 1, 12500.00, 20, 'Active'),
('Mouse', 2, 199.00, 200, 'Active'),
('Keyboard', 2, 499.00, 120, 'Active'),
('Monitor', 3, 1899.00, 40, 'Active'),
('USB-C Cable', 4, 129.00, 300, 'Active'),
('Printer Paper A4', 5, 79.00, 500, 'Active');
GO

-- Orders (10)
INSERT INTO dbo.Orders (CustomerId)
VALUES
(1),(2),(3),(4),(5),(6),(7),(8),(9),(10);
GO

-- OrderRows (minst 25) + UnitPrice måste med
-- Order 1
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(1,1,1,12500.00),
(1,2,2,199.00),
(1,5,3,129.00);

-- Order 2
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(2,3,1,499.00),
(2,2,1,199.00),
(2,6,5,79.00);

-- Order 3
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(3,4,2,1899.00),
(3,5,2,129.00);

-- Order 4
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(4,1,1,12500.00),
(4,3,1,499.00),
(4,2,1,199.00);

-- Order 5
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(5,6,10,79.00),
(5,5,5,129.00);

-- Order 6
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(6,2,4,199.00),
(6,3,2,499.00),
(6,5,2,129.00);

-- Order 7
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(7,4,1,1899.00),
(7,1,1,12500.00);

-- Order 8
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(8,6,20,79.00),
(8,2,2,199.00);

-- Order 9
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(9,5,10,129.00),
(9,3,2,499.00),
(9,4,1,1899.00);

-- Order 10 (så vi kommer över 25 rader)
INSERT INTO dbo.OrderRows (OrderId, ProductId, Quantity, UnitPrice) VALUES
(10,2,1,199.00),
(10,3,1,499.00),
(10,6,15,79.00);
GO

-- StockMovements (skapa activity feed från ordrar)
INSERT INTO dbo.StockMovements (ProductId, ChangeQty, Reason)
SELECT ProductId, -Quantity, CONCAT('OrderId ', OrderId)
FROM dbo.OrderRows;
GO