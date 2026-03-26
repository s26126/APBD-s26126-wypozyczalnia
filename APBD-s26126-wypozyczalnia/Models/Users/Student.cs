namespace APBD_s26126_wypozyczalnia.Models;

public class Student : User
{
    public override int MaxRentals => 2;
    public override UserType Type => UserType.Student;

    public Student(string firstName, string lastName) : base(firstName, lastName)
    {
    }
}