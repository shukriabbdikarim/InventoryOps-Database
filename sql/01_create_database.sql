USE master;
GO

IF DB_ID('InventoryOps') IS NULL
BEGIN
    CREATE DATABASE InventoryOps;
END
GO

USE InventoryOps;
GO