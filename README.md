# System Wypożyczalni Sprzętu - APBD

Aplikacja symuluje działanie wypożyczalni sprzętu elektronicznego, obsługując różne typy użytkowników, rodzaje sprzętu oraz procesy wypożyczeń, zwrotów i raportowania.

## Uzasadnienie decyzji projektowych

### Podział na klasy i pliki
Projekt został podzielony na logiczne moduły (foldery wewnątrz `Models`), co ułatwia nawigację i utrzymanie kodu:
- **Models/Users**: Zawiera hierarchię użytkowników (`User`, `Student`, `Employee`).
- **Models/Equipment**: Zawiera hierarchię sprzętu (`Equipment`, `Laptop`, `Camera`, `Projector`).
- **Models/Rental**: Zawiera logikę biznesową (`RentalService`), model danych wypożyczenia (`Rental`) oraz klasę odpowiedzialną za prezentację danych (`RentalPrinter`).

### Warstwy projektu
Zastosowano separację odpowiedzialności:
1. **Warstwa Modelu Danych**: Klasy POCO reprezentujące stan (np. `User`, `Equipment`, `Rental`).
2. **Warstwa Logiki Biznesowej**: `RentalService` – jedyne miejsce zarządzające kolekcjami i realizujące reguły (np. limity wypożyczeń, naliczanie kar).
3. **Warstwa Prezentacji**: `RentalPrinter` (odpowiedzialna za UI konsolowe) oraz `Program.cs` (orkiestracja scenariusza).

---

## Kohezja, Coupling i Odpowiedzialność

### Kohezja
Wysoka kohezja jest widoczna w klasie `RentalService`. Klasa ta skupia się wyłącznie na operacjach związanych z procesem wypożyczania: dodawanie użytkowników/sprzętu, realizacja wypożyczeń, obsługa zwrotów. Nie zajmuje się ona formatowaniem tekstu ani interakcją z użytkownikiem, co jest delegowane do innych klas.

### Coupling
Zadbano o niski coupling poprzez:
- **Wstrzykiwanie zależności**: Klasa `RentalPrinter` nie tworzy własnej instancji serwisu, lecz przyjmuje go w konstruktorze (`public RentalPrinter(RentalService rentalService)`). Pozwala to na łatwą zmianę źródła danych w przyszłości.
- **Wykorzystanie polimorfizmu**: Metody w `RentalService` operują na abstrakcyjnych klasach `User` i `Equipment`. Dzięki temu serwis nie musi wiedzieć, czy wypożycza laptopa studentowi, czy aparat pracownikowi – reguły są egzekwowane poprzez właściwości polimorficzne (np. `MaxRentals`).

### Odpowiedzialność
Każda klasa ma jasno zdefiniowany cel:
- `Rental`: Reprezentuje pojedynczy fakt wypożyczenia i przechowuje jego historię.
- `RentalPrinter`: Odpowiada **wyłącznie** za wyświetlanie raportów i formatowanie wyjścia konsolowego. Dzięki temu zmiana sposobu wyświetlania danych nie wymaga modyfikacji logiki biznesowej w `RentalService`.
- `User` i `Equipment`: Przechowują stan obiektów i definiują ich specyficzne cechy.

---

## Scenariusz Demonstracyjny
Główny punkt wejścia aplikacji (`Program.cs`) realizuje kompletny scenariusz:
1. Rejestracja zasobów.
2. Poprawne operacje biznesowe.
3. Obsługa sytuacji błędnych (przekroczenie limitów, próba wypożyczenia niedostępnego sprzętu).
4. Symulacja zwrotów (w tym opóźnionych).
5. Generowanie raportów końcowych.
