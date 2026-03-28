using APBD_s26126_wypozyczalnia.Models.Equipment;

namespace APBD_s26126_wypozyczalnia.Models.Rental;

public class RentalPrinter
{
    private readonly RentalService _rentalService;

    public RentalPrinter(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public void PrintAllUsers()
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine("--- ZAREJESTROWANI UŻYTKOWNICY ---");
        _rentalService.Users.ForEach(Console.WriteLine);
        Console.WriteLine("=====================================================================================\n");
    }

    public void PrintAllEquipment()
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine("--- ZAREJESTROWANY SPRZĘT ---");
        _rentalService.Equipment.ForEach(Console.WriteLine);
        Console.WriteLine("=====================================================================================\n");
    }

    public void PrintAvailableEquipment()
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine("--- DOSTĘPNE SPRZĘT ---");
        foreach (var e in _rentalService.GetAvailableEquipment())
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("=====================================================================================\n");
    }

    public void PrintFines()
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine("--- NALICZONE KARY ---");
        var rentalsWithFines = _rentalService.Rentals.Where(r => r.Fine > 0).ToList();
        if (!rentalsWithFines.Any())
        {
            Console.WriteLine("Brak naliczonych kar.");
        }
        else
        {
            foreach (var rental in rentalsWithFines)
            {
                Console.WriteLine($"Użytkownik: {rental.User.FirstName} {rental.User.LastName}, Sprzęt: {rental.Equipment.Name}, Kara: {rental.Fine} PLN (Opóźnienie: {rental.DaysOverdue} dni)");
            }
        }
        Console.WriteLine("=====================================================================================\n");
    }

    public void PrintRentalHistory(User user)
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine($"--- Historia wypożyczeń użytkownika {user.FirstName} {user.LastName} ---");
        foreach (var rental in _rentalService.Rentals.Where(r => r.User == user))
        {
            Console.WriteLine($"Data wypożyczenia: {rental.RentalStartDate}, Data zwrotu: {rental.RentalEndDate}, Sprzęt: {rental.Equipment.Name}");
        }
        Console.WriteLine("=====================================================================================\n");
    }
}
