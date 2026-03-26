namespace APBD_s26126_wypozyczalnia.Models.Equipment;

public class Laptop : Equipment
{

    public String Processor { get; set; }
    public int RamInGB { get; set; }
    public int HardDriveInGB { get; set; }
    public String OperatingSystem { get; set; }

    public Laptop(string name, string processor, int ramInGb, int hardDriveInGb, string operatingSystem) : base(name)
    {
        Processor = processor;
        RamInGB = ramInGb;
        HardDriveInGB = hardDriveInGb;
        OperatingSystem = operatingSystem;
    }
    public override string ToString()
    {
        return $"Laptop: {Name}, Processor: {Processor}, Ram: {RamInGB} GB, Hard Drive: {HardDriveInGB} GB, Operating System: {OperatingSystem}, Status: {Status}";
    }
}