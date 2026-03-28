namespace APBD_s26126_wypozyczalnia.Models.Rental;
using APBD_s26126_wypozyczalnia.Models.Equipment;

public class RentalService
{
    private List<User> _users = new List<User>();
    private List<Equipment> _equipment = new List<Equipment>();
    private List<Rental> _rentals = new List<Rental>();

    public List<User> Users => _users;
    public List<Equipment> Equipment => _equipment;
    public List<Rental> Rentals => _rentals;

    public void AddUser(User user)
    {
        _users.Add(user);
    }
    public void AddEquipment(Equipment equipment)
    {
        _equipment.Add(equipment);
    }
    
    public bool AddRental(User user, Equipment equipment)
    {
        if (user is null || equipment is null)
        {
            Console.WriteLine("[BŁĄD] Użytkownik lub sprzęt nie może być null.");
            return false;
        }

        var currentRentals = GetActiveRentalsCount(user);
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

    public bool ReturnEquipment(Equipment equipment)
    {
        if (equipment == null)
        {
            Console.WriteLine("[BŁĄD] Sprzęt nie może być null.");
            return false;
        }

        var rental = _rentals.FirstOrDefault(r => r.Equipment == equipment && r.RentalReturnDate == null);
        if (rental == null)
        {
            Console.WriteLine($"[BŁĄD] Nie znaleziono aktywnego wypożyczenia dla sprzętu: {equipment.Name}.");
            return false;
        }

        rental.RentalReturnDate = DateTime.Now;
        equipment.Status = EquipmentStatus.Available;

        if (rental.RentalReturnDate > rental.RentalEndDate)
        {
            rental.IsOverdue = true;
            rental.DaysOverdue = (int)(rental.RentalReturnDate.Value - rental.RentalEndDate.Value).TotalDays;
            
            // Przyjmijmy stawkę kary 10 PLN za dzień
            rental.Fine = rental.DaysOverdue * 10;
            Console.WriteLine($"[ZWRÓCONO] Sprzęt '{equipment.Name}' został zwrócony z opóźnieniem: {rental.DaysOverdue} dni. Kara: {rental.Fine} PLN.");
        }
        else
        {
            Console.WriteLine($"[ZWRÓCONO] Sprzęt '{equipment.Name}' został zwrócony w terminie.");
        }

        return true;
    }

    public Rental? GetActiveRental(Equipment equipment)
    {
        return _rentals.FirstOrDefault(r => r.Equipment == equipment && r.RentalReturnDate == null);
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipment.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }

    
    public int GetActiveRentalsCount(User user)
    {
        if (user == null)
        {
            Console.WriteLine("[BŁĄD] Użytkownik nie może być null.");
            return 0;
        }
        
        return _rentals.Count(r => r.User == user && r.RentalReturnDate == null);
    }
}


