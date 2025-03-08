namespace TProg.StarWarsDan.Domain;

public class Person
{
    public required string Name { get; init; }
    public double Height { get; set; }
    public double Mass { get; set; }
    public int BirthYear { get; set; }
    public Gender Gender { get; set; }
}
