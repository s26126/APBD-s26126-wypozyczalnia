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
        Console.WriteLine("--- ZAREJESTROWANI UŻYTKOWNICY ---");
        _users.ForEach(Console.WriteLine);
    }
    public void PrintAllEquipment()
    {
        Console.WriteLine("\n--- ZAREJESTROWANY SPRZĘT ---");
        _equipment.ForEach(Console.WriteLine);
    }
    
    public void PrintAvailableEquipment()
    {
        Console.WriteLine("\n--- DOSTĘPNE SPRZĘT ---");
        foreach (var e in GetAvailableEquipment())
        {
            Console.WriteLine(e);
        }
    }
    public List<Equipment> GetAvailableEquipment()
    {
        return _equipment.Where(e => e.Status == EquipmentStatus.Available).ToList();
    }

    public void AddRental(User user, Equipment equipment)
    {
        if (user == null || equipment == null)
        {
            throw new ArgumentException("User i equipment nie może być null");
        }
        
        _rentals.Add(new Rental(user, equipment));
    }
    
}
