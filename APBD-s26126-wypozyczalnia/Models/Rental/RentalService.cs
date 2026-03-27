namespace APBD_s26126_wypozyczalnia.Models.Rental;
using APBD_s26126_wypozyczalnia.Models.Equipment;

public class RentalService
{
    private List<User> _users = new List<User>();
    private List<Equipment> _equipment = new List<Equipment>();
    private List<Rental> _rentals = new List<Rental>();
    
    public void AddUser(User user)
    {
        _users.Add(user);
    }
    public void AddEquipment(Equipment equipment)
    {
        _equipment.Add(equipment);
    }
    public void PrintAllUsers()
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine("--- ZAREJESTROWANI UŻYTKOWNICY ---");
        _users.ForEach(Console.WriteLine);
        Console.WriteLine("=====================================================================================\n");
    }
    public void PrintAllEquipment()
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine("--- ZAREJESTROWANY SPRZĘT ---");
        _equipment.ForEach(Console.WriteLine);
        Console.WriteLine("=====================================================================================\n");
    }
    
    public void PrintAvailableEquipment()
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine("--- DOSTĘPNE SPRZĘT ---");
        foreach (var e in GetAvailableEquipment())
        {
            Console.WriteLine(e);
        }
        Console.WriteLine("=====================================================================================\n");
    }

    public bool AddRental(User user, Equipment equipment)
    {
        if (user is null || equipment is null)
        {
            Console.WriteLine("[BŁĄD] Użytkownik lub sprzęt nie może być null.");
            return false;
        }

        var currentRentals = GetNumberOfRentals(user);
        if (currentRentals >= user.MaxRentals)
        {
            Console.WriteLine($"[BŁĄD] {user.FirstName} {user.LastName} przekroczył limit wypożyczeń ({user.MaxRentals}).");
            return false;
        }

        if (equipment.Status != EquipmentStatus.Available)
        {
            Console.WriteLine($"[BŁĄD] Sprzęt '{equipment.Name}' jest obecnie niedostępny (Status: {equipment.Status}).");
            return false;
        }

        _rentals.Add(new Rental(user, equipment));
        Console.WriteLine($"[SUKCES] Wypożyczono {equipment.Name} użytkownikowi {user.FirstName} {user.LastName}.");
        return true;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipment.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }

    
    public int GetNumberOfRentals(User user)
    {
        if (user == null) { throw new ArgumentNullException("User nie może być null"); }
        
        return _rentals.Count(r => r.User == user);
    }
    
    public void PrintRentalHistory(User user)
    {
        Console.WriteLine("\n=====================================================================================");
        Console.WriteLine($"--- Historia wypożyczeń użytkownika {user.FirstName} {user.LastName} ---");
        foreach (var rental in _rentals.Where(r => r.User == user))
        {
            Console.WriteLine($"Data wypożyczenia: {rental.RentalStartDate}, Data zwrotu: {rental.RentalEndDate}, Sprzęt: {rental.Equipment.Name}");
        }
        Console.WriteLine("=====================================================================================\n");
    }
    
}


