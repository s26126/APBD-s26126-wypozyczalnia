namespace APBD_s26126_wypozyczalnia.Models;

public enum UserType
{
    Student,
    Employee
}

public abstract class User
{
    private static int _idCounter = 1;
    public int Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public abstract int MaxRentals { get; }
    public abstract UserType Type { get; }

    protected User(string firstName, string lastName)
    {
        Id = _idCounter++;
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        return $"[{Id}] {FirstName} {LastName} ({Type}, Limit: {MaxRentals})";
    }
}