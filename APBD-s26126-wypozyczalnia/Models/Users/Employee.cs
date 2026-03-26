namespace APBD_s26126_wypozyczalnia.Models;

public class Employee : User
{
    public override int MaxRentals => 5;
    public override UserType Type => UserType.Employee;

    public Employee(string firstName, string lastName) : base(firstName, lastName)
    {
    }
}