InventoryOps-Database,

Detta repository innehåller SQL-script för en relationsdatabas i SQL Server.
Projektet är en databasinlämning som visar hur man skapar, strukturerar och hanterar en databas för ett enkelt inventory- och ordersystem.

---

Innehåll,
Projektet innehåller script för:
Skapande av databas,
Skapande av tabeller med relationer,
Testdata (seed data),
CRUD-exempel,
Queries med JOINs,
Views för rapportering,
Säkerhet (login, user och roller),
Cleanup-script,

Ett databasdiagram finns också med som visar tabellernas relationer.

---

Beskrivning av script,

01_create_database.sql
Skapar databasen InventoryOps om den inte redan finns.

02_create_tables.sql
Skapar samtliga tabeller med primärnycklar, främmande nycklar och constraints.

03_seed_data.sql
Lägger in exempeldata för testning.

04_crud_examples.sql
Visar exempel på INSERT, SELECT, UPDATE och DELETE.

05_queries_joins.sql
Innehåller SELECT-frågor med JOINs och sortering.

06_views.sql
Skapar views för rapportering, bland annat senaste ordrar med kundinformation.

07_security.sql
Skapar login, user och role samt tilldelar rättigheter via views.

08_cleanup.sql
Tar bort views och tabeller i korrekt ordning.
Körs endast manuellt vid behov.

---

Databasdiagram,
Databasens struktur visas i filen:

InventoryOps-Diagram.png

---

Rekommenderad körordning,
01_create_database.sql,
02_create_tables.sql,
03_seed_data.sql,
04_crud_examples.sql,
05_queries_joins.sql,
06_views.sql,
07_security.sql,
08_cleanup.sql körs endast om databasen ska rensas.

---

Teknik,
SQL Server,
T-SQL,
Visual Studio / SSMS,
Git och GitHub,

---
