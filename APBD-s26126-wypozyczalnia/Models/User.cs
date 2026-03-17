namespace APBD_s26126_wypozyczalnia.Models;

public abstract class User
{
    private String firstName;
    private String lastName;


    protected User(string firstName, string lastName)
    {
        this.firstName = firstName;
        this.lastName = lastName;
    }
}