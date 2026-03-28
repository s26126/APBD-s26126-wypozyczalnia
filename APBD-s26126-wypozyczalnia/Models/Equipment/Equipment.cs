namespace APBD_s26126_wypozyczalnia.Models.Equipment;


public enum EquipmentStatus
{
    Available,
    Rented,
    Reserved,
    Damaged,
    InRepair
}

public abstract class Equipment
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

    
    protected Equipment(string name)
    {
        Name = name;
    }
    
    
    
}