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

        var usr1 = new Student("Jan", "Kowalski");
        var usr2 = new Student("Anna", "Nowak");
        var usr3 = new Student("Piotr", "Zieliński");
        var usr4 = new Student("Maria", "Wiśniewska");
        var usr5 = new Student("Tomasz", "Lewandowski");
        service.AddUser(usr1);
        service.AddUser(usr2);
        service.AddUser(usr3);
        service.AddUser(usr4);
        service.AddUser(usr5);

        var usr6 = new Employee("Adam", "Mickiewicz");
        var usr7 = new Employee("Wisława", "Szymborska");
        var usr8 = new Employee("Czesław", "Miłosz");
        var usr9 = new Employee("Olga", "Tokarczuk");
        var usr10 = new Employee("Juliusz", "Słowacki");
        service.AddUser(usr6);
        service.AddUser(usr7);
        service.AddUser(usr8);
        service.AddUser(usr9);
        service.AddUser(usr10);

        var eq1 = new Laptop("Dell XPS 13", "Intel i7", 16, 512, "Windows 11");
        var eq2 = new Laptop("MacBook Air M2", "Apple M2", 8, 256, "macOS");
        var eq3 = new Laptop("Lenovo ThinkPad X1", "Intel i5", 16, 512, "Ubuntu 22.04");
        var eq4 = new Laptop("Asus ROG Zephyrus", "AMD Ryzen 9", 32, 1024, "Windows 11");
        var eq5 = new Camera("Sony A7 III", 24);
        var eq6 = new Camera("Canon EOS R6", 20);
        var eq7 = new Camera("Nikon Z6 II", 24);
        var eq8 = new Projector("Epson EB-L210W", 60, 4500);
        var eq9 = new Projector("BenQ TK700STi", 120, 3000);
        var eq10 = new Projector("Optoma UHD38", 240, 4000);
        service.AddEquipment(eq1);
        service.AddEquipment(eq2);
        service.AddEquipment(eq3);
        service.AddEquipment(eq4);
        service.AddEquipment(eq5);
        service.AddEquipment(eq6);
        service.AddEquipment(eq7);
        service.AddEquipment(eq8);
        service.AddEquipment(eq9);
        service.AddEquipment(eq10);



        // printer.PrintAllUsers();
        // printer.PrintAllEquipment();
        // printer.PrintAvailableEquipment();

        // dodanie nowych wypożyczeń
        service.AddRental(usr1, eq1);
        service.AddRental(usr1, eq2);
        service.AddRental(usr2, eq2);
        service.AddRental(usr3, eq3);
        service.AddRental(usr4, eq4);
        service.AddRental(usr5, eq5);

        
        printer.PrintAvailableEquipment();
        printer.PrintAllEquipment();
        printer.PrintRentalHistory(usr1);
        
        Console.WriteLine("\n--- TEST ZWROTÓW ---");
        service.ReturnEquipment(eq1); // Zwrot w terminie
        // Symulacja zwrotu po terminie (manualne ustawienie daty końcowej na przeszłość dla eq2)
        var rental2 = service.GetActiveRental(eq2);
        if (rental2 != null)
        {
            rental2.RentalEndDate = DateTime.Now.AddDays(-5);
        }
        service.ReturnEquipment(eq2); // Zwrot po terminie (5 dni spóźnienia)
        
        // Symulacja sprzętu aktualnie przetrzymywanego (termin minął, ale nie został zwrócony)
        var rental3 = service.GetActiveRental(eq3);
        if (rental3 != null)
        {
            rental3.RentalEndDate = DateTime.Now.AddDays(-2);
        }

        printer.PrintAvailableEquipment();
        printer.PrintOverdueRentals();
        printer.PrintFines();


        service.MarkEquipmentAsUnavailable(eq6, "Użytkownik sprawdzał czy aparat jest wodoodporny");
        
        
        printer.PrintSummaryReport();
    }
}
    

