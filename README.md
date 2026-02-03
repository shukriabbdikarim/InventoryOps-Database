# InventoryOps

Inventory- och ordersystem med SQL Server-databas och .NET 8.0 Console App (EF Core, Database First).

---

## Scenario

InventoryOps hanterar kunder, leverantörer, produkter, ordrar och lagerrörelser.
Systemet består av:

1. **SQL Server-databas** med tabeller, views, säkerhet och testdata
2. **.NET 8.0 Console App** som använder EF Core (Database First) för CRUD-operationer och rapporter

---

## Projektstruktur

```
├── InventoryOps-Database-main.sln    # Visual Studio solution
├── InventoryOps.ConsoleApp/          # .NET 8.0 Console App
│   ├── Data/                         # DbContext
│   ├── Models/                       # EF Core-modeller
│   ├── Services/                     # CRUD och rapportlogik
│   ├── Menus/                        # Konsolmenyer
│   └── Helpers/                      # Input-validering, UX-hjälpare
├── sql/                              # SQL-script (körs i ordning 01–08)
├── erd/                              # Databasdiagram
└── screenshots/                      # Skärmbilder (fylls i manuellt)
```

---

## Kom igång

### 1. Skapa databasen

Kör SQL-scripten i SQL Server Management Studio (SSMS) i denna ordning:

```
sql/01_create_database.sql
sql/02_create_tables.sql
sql/03_seed_data.sql
sql/06_views.sql
sql/07_security.sql
```

### 2. Starta Console App

Öppna `InventoryOps-Database-main.sln` i Visual Studio 2022 och tryck **F5** (eller Ctrl+F5).

Alternativt via terminal:
```bash
dotnet run --project InventoryOps.ConsoleApp
```

> **Anslutningssträng**: Ändra i `Program.cs` om din SQL Server-instans skiljer sig.
> Standard: `Server=localhost;Database=InventoryOps;Trusted_Connection=True;TrustServerCertificate=True;`

---

## Database First (Scaffold)

Om du vill scaffold:a modellerna direkt från databasen (istället för att använda de manuellt skapade):

```bash
cd InventoryOps.ConsoleApp

dotnet ef dbcontext scaffold "Server=localhost;Database=InventoryOps;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Data --context InventoryOpsContext --force
```

---

## Menyflöde

```
=== InventoryOps ===
1. Kunder
   ├── 1. Lista alla kunder
   ├── 2. Skapa ny kund
   ├── 3. Uppdatera kund
   ├── 4. Ta bort kund
   └── 0. Tillbaka
2. Produkter
   ├── 1. Lista alla produkter
   ├── 2. Ändra status (Active/Inactive)
   └── 0. Tillbaka
3. Ordrar
   ├── 1. Lista alla ordrar
   ├── 2. Skapa ny order
   ├── 3. Lägg till orderrad
   └── 0. Tillbaka
4. Rapporter
   ├── 1. Top 5 kunder (flest ordrar)
   ├── 2. Lågt lager (under 10)
   ├── 3. Senaste 20 lagerrörelser
   ├── 4. Omsättning per produkt
   └── 0. Tillbaka
0. Avsluta
```

---

## Beskrivning av SQL-script

| Script | Beskrivning |
|---|---|
| `01_create_database.sql` | Skapar databasen InventoryOps |
| `02_create_tables.sql` | Skapar tabeller med PK, FK och constraints |
| `03_seed_data.sql` | Lägger in testdata |
| `04_crud_examples.sql` | Visar INSERT, SELECT, UPDATE, DELETE |
| `05_queries_joins.sql` | SELECT med JOINs, GROUP BY, aggregering |
| `06_views.sql` | Views för rapportering |
| `07_security.sql` | Login, user, role och rättigheter |
| `08_cleanup.sql` | Tar bort views och tabeller (manuellt) |

---

## Databasdiagram

Se `erd/InventoryOps-Diagram.png`

---

## Teknik

- SQL Server / T-SQL
- .NET 8.0
- Entity Framework Core 8.0 (Database First)
- C# Console App
