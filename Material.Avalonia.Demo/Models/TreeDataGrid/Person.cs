namespace Material.Avalonia.Demo.Models.TreeDataGrid;

public sealed class Person
{
    public Person(string firstName, string lastName, int age, string city, bool isActive)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        City = city;
        IsActive = isActive;
    }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Age { get; set; }

    public string City { get; set; }

    public bool IsActive { get; set; }
}
