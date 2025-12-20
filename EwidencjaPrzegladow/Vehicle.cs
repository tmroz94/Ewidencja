namespace EwidencjaPrzegladow;

public class Vehicle
{
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int YearOfProduction { get; set; }
    public DateTime InspectionDate { get; set; }
    public string Owner { get; set; } = string.Empty;
}
