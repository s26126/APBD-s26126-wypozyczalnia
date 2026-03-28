using APBD_s26126_wypozyczalnia.Models;
using APBD_s26126_wypozyczalnia.Models.Equipment;
using APBD_s26126_wypozyczalnia.Models.Rental;

namespace APBD_s26126_wypozyczalnia;

public class Program
{
    public static void Main(string[] args)
    {
        var service = new RentalService();
        var printer = new RentalPrinter(service);

        Console.WriteLine("=== SCENARIUSZ DEMONSTRACYJNY ===");

        // 11. Dodanie kilku egzemplarzy sprzętu różnych typów
        Console.WriteLine("\n[11] Dodawanie sprzętu...");
        var laptop = new Laptop("Dell XPS 13", "Intel i7", 16, 512, "Windows 11");
        var camera = new Camera("Sony A7 III", 24);
        var projector = new Projector("Epson EB-L210W", 60, 4500);
        service.AddEquipment(laptop);
        service.AddEquipment(camera);
        service.AddEquipment(projector);
        service.AddEquipment(new Laptop("MacBook Air M2", "Apple M2", 8, 256, "macOS"));

        // 12. Dodanie kilku użytkowników różnych typów
        Console.WriteLine("\n[12] Dodawanie użytkowników...");
        var student = new Student("Jan", "Kowalski"); // limit 2
        var employee = new Employee("Adam", "Nowak"); // limit 5
        service.AddUser(student);
        service.AddUser(employee);

        // 13. Poprawne wypożyczenie sprzętu
        Console.WriteLine("\n[13] Poprawne wypożyczenie...");
        service.AddRental(student, laptop);

        // 14. Próba wykonania niepoprawnej operacji
        Console.WriteLine("\n[14] Próby niepoprawnych operacji...");
        // a) Wypożyczenie sprzętu już wypożyczonego/niedostępnego
        service.AddRental(employee, laptop); 
        
        // b) Oznaczenie jako niedostępny i próba wypożyczenia
        service.MarkEquipmentAsUnavailable(camera, "W naprawie");
        service.AddRental(employee, camera);

        // c) Przekroczenie limitu (Student ma limit 2)
        var eq3 = new Laptop("Lenovo ThinkPad", "i5", 16, 512, "Win 10");
        var eq4 = new Projector("BenQ", 100, 3000);
        service.AddEquipment(eq3);
        service.AddEquipment(eq4);
        service.AddRental(student, eq3); // Drugie wypożyczenie - OK
        service.AddRental(student, eq4); // Trzecie wypożyczenie - BŁĄD (limit 2)

        // 15. Zwrot sprzętu w terminie
        Console.WriteLine("\n[15] Zwrot w terminie...");
        service.ReturnEquipment(laptop);

        // 16. Zwrot opóźniony skutkujący naliczeniem kary
        Console.WriteLine("\n[16] Zwrot opóźniony z karą...");
        var rental2 = service.GetActiveRental(eq3);
        if (rental2 != null)
        {
            // Symulacja: termin minął 5 dni temu
            rental2.RentalEndDate = DateTime.Now.AddDays(-5);
        }
        service.ReturnEquipment(eq3);

        // 17. Wyświetlenie raportu końcowego o stanie systemu
        Console.WriteLine("\n[17] Raport końcowy...");
        printer.PrintSummaryReport();
        
        Console.WriteLine("\n--- Dodatkowe raporty ---");
        printer.PrintAllEquipment();
        printer.PrintOverdueRentals();
    }
}
    

