namespace APBD_s26126_wypozyczalnia.Models.Rental;
using APBD_s26126_wypozyczalnia.Models.Equipment;

public class Rental
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime RentalStartDate { get; set; }
    public DateTime? RentalReturnDate { get; set; }
    public DateTime? RentalEndDate { get; set; }
    public User User { get; set; }
    public Equipment Equipment { get; set; }
    


    public Rental(User user, Equipment equipment)
    {
        User = user;
        Equipment = equipment;
        RentalStartDate = DateTime.Now;
        RentalReturnDate = null;
        RentalEndDate = DateTime.Now.AddDays(14);
        equipment.Status = EquipmentStatus.Rented;
        
    }
    
    public override string ToString()
    {
        return $"Rental: {Equipment.Name} for {User.FirstName} {User.LastName}";
    }
    
}