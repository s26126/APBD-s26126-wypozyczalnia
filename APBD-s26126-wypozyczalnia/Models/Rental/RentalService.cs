namespace APBD_s26126_wypozyczalnia.Models.Rental;
using APBD_s26126_wypozyczalnia.Models.Equipment;

public class RentalService
{
    private List<User> _users = new List<User>();
    private List<Equipment> _equipment = new List<Equipment>();
    private List<Rental> _rentals = new List<Rental>();
    private int penaltyValue = 10;

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
        if (equipment is null)
        {
            Console.WriteLine("[BŁĄD] Sprzęt nie może być null.");
            return false;
        }

        var rental = _rentals.FirstOrDefault(r => r.Equipment == equipment && r.RentalReturnDate == null);
        if (rental is null)
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
            
            // 10 PLN kary za dzień opóźnienia
            rental.Fine = rental.DaysOverdue * penaltyValue;
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
        if (equipment is null)
        {
            Console.WriteLine("[BŁĄD] Sprzęt nie może być null.");
            return null;
        }
        
        return _rentals.FirstOrDefault(r => r.Equipment == equipment && r.RentalReturnDate == null);
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipment.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }

    
    public int GetActiveRentalsCount(User user)
    {
        if (user is null)
        {
            Console.WriteLine("[BŁĄD] Użytkownik nie może być null.");
            return 0;
        }
        
        return _rentals.Count(r => r.User == user && r.RentalReturnDate == null);
    }

    public List<Rental> GetOverdueRentals()
    {
        var now = DateTime.Now;
        return _rentals.Where(r => 
            r.IsOverdue || // Już zwrócone, ale spóźnione
            (r.RentalReturnDate == null && now > r.RentalEndDate) // Nadal wypożyczone, po terminie
        ).ToList();
    }
    public bool MarkEquipmentAsUnavailable(Equipment equipment, String reason)
    {
        if (equipment is null)
        {
            Console.WriteLine("[BŁĄD] Sprzęt nie może być null.");
            return false;
        }
        
        if (String.IsNullOrEmpty(reason))
        {
            Console.WriteLine("[BŁĄD] Podaj powód oznaczania sprzętu jako niedostępnego.");
            return false;
        }
        
        switch (equipment.Status)
        {
            case EquipmentStatus.Unavailable:
                Console.WriteLine("[BŁĄD] Sprzęt jest już oznaczony jako niedostępny.");
                return false;
            case EquipmentStatus.Rented:
                Console.WriteLine("[BŁĄD] Sprzęt jest aktualnie wypożyczony.");
                return false;
            case EquipmentStatus.Reserved:
                Console.WriteLine("[BŁĄD] Sprzęt jest aktualnie rezerwowany.");
                return false;
            default:
                
                equipment.Status = EquipmentStatus.Unavailable;
                equipment.ReasonOfUnavailable = reason;
                Console.WriteLine($"[INFO] Sprzęt oznaczony jako niedostępny. Powód: {reason}");
                return true;
        }
    }
}


