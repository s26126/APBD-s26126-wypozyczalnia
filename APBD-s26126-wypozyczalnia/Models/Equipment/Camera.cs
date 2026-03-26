namespace APBD_s26126_wypozyczalnia.Models.Equipment;

public class Camera : Equipment
{
    public int MegaPixels { get; set; }
    
    public Camera(string name, int megaPixels) : base(name)
    {
        MegaPixels = megaPixels;
        
    }
    
    public override string ToString()
    {
        return $"Camera: {Name}, Mega Pixels: {MegaPixels}, Status: {Status}";
    }
    
}