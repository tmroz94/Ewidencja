using System.ComponentModel.DataAnnotations;

namespace EwidencjaWeb.Models;

public class Vehicle
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Nr rejestracyjny jest wymagany")]
    [Display(Name = "Nr rejestracyjny")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Nr rejestracyjny musi mieć od 3 do 20 znaków")]
    [RegularExpression(@"^[A-Z]{1,3}\s?[A-Z0-9]{3,5}$", ErrorMessage = "Niepoprawny format nr rejestracyjnego (np. KR 12345)")]
    public string RegistrationNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Marka jest wymagana")]
    [Display(Name = "Marka")]
    [StringLength(50, ErrorMessage = "Marka nie może być dłuższa niż 50 znaków")]
    public string Brand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Model jest wymagany")]
    [Display(Name = "Model")]
    [StringLength(50, ErrorMessage = "Model nie może być dłuższy niż 50 znaków")]
    public string Model { get; set; } = string.Empty;

    [Required(ErrorMessage = "Rok produkcji jest wymagany")]
    [Display(Name = "Rok produkcji")]
    [Range(1900, 2100, ErrorMessage = "Podaj poprawny rok")]
    public int YearOfProduction { get; set; }

    [Required(ErrorMessage = "Data przeglądu jest wymagana")]
    [Display(Name = "Data przeglądu")]
    [DataType(DataType.Date)]
    public DateTime InspectionDate { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Właściciel jest wymagany")]
    [Display(Name = "Właściciel")]
    [StringLength(100, ErrorMessage = "Właściciel nie może być dłuższy niż 100 znaków")]
    public string Owner { get; set; } = string.Empty;
}