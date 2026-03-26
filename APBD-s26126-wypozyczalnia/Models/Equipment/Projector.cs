namespace APBD_s26126_wypozyczalnia.Models.Equipment;

public class Projector : Equipment
{

    public int RefreshRate { get; set; }
    public int Lumens { get; set; }
    
    public Projector(string name, int refreshRate, int lumens) : base(name)
    {
        RefreshRate = refreshRate;
        Lumens = lumens;
    }
    
    public override string ToString()
    {
        return $"Projector: {Name}, Refresh Rate: {RefreshRate} Hz, Lumens: {Lumens}, Status: {Status}";
    }
    
}