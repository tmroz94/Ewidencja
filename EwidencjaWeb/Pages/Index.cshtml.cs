using EwidencjaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EwidencjaWeb.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    private static List<Vehicle> _vehicles = new List<Vehicle>();

    static IndexModel()
    {
        var brands = new[] { "Toyota", "Ford", "Volkswagen", "BMW", "Audi", "Mercedes", "Honda", "Hyundai" };
        var models = new Dictionary<string, string[]>
        {
            { "Toyota", new[] { "Corolla", "Yaris", "Camry", "RAV4" } },
            { "Ford", new[] { "Focus", "Fiesta", "Mondeo", "Kuga" } },
            { "Volkswagen", new[] { "Golf", "Passat", "Polo", "Tiguan" } },
            { "BMW", new[] { "Series 3", "Series 5", "X3", "X5" } },
            { "Audi", new[] { "A3", "A4", "A6", "Q5" } },
            { "Mercedes", new[] { "C-Class", "E-Class", "A-Class", "GLC" } },
            { "Honda", new[] { "Civic", "CR-V", "HR-V", "Jazz" } },
            { "Hyundai", new[] { "i30", "Tucson", "Kona", "Santa Fe" } }
        };

        var random = new Random();
        for (int i = 1; i <= 18; i++)
        {
            var brand = brands[random.Next(brands.Length)];
            var model = models[brand][random.Next(models[brand].Length)];
            var year = random.Next(2010, 2024);
                
            _vehicles.Add(new Vehicle 
            { 
                RegistrationNumber = $"KR {random.Next(10000, 99999)}", 
                Brand = brand, 
                Model = model, 
                YearOfProduction = year, 
                InspectionDate = DateTime.Today.AddDays(random.Next(-365, 365)), 
                Owner = $"Właściciel {i}" 
            });
        }
    }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IList<Vehicle> Vehicles { get; set; } = default!;

    [BindProperty]
    public Vehicle NewVehicle { get; set; } = new Vehicle();

    public string NameSort { get; set; }
    public string BrandSort { get; set; }
    public string ModelSort { get; set; }
    public string YearSort { get; set; }
    public string DateSort { get; set; }
    public string OwnerSort { get; set; }
    public string CurrentSort { get; set; }
    public string CurrentFilter { get; set; }
        
    public int PageIndex { get; set; } = 1;
    public int TotalPages { get; set; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public void OnGet(string sortOrder, string currentFilter, string searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        BrandSort = sortOrder == "Brand" ? "brand_desc" : "Brand";
        ModelSort = sortOrder == "Model" ? "model_desc" : "Model";
        YearSort = sortOrder == "Year" ? "year_desc" : "Year";
        DateSort = sortOrder == "Date" ? "date_desc" : "Date";
        OwnerSort = sortOrder == "Owner" ? "owner_desc" : "Owner";

        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        IQueryable<Vehicle> vehiclesIQ = _vehicles.AsQueryable();

        if (!String.IsNullOrEmpty(searchString))
        {
            var s = searchString.ToLower();
            vehiclesIQ = vehiclesIQ.Where(v => 
                v.RegistrationNumber.ToLower().Contains(s) || 
                v.Brand.ToLower().Contains(s) ||
                v.Model.ToLower().Contains(s) ||
                v.Owner.ToLower().Contains(s) ||
                v.YearOfProduction.ToString().Contains(s));
        }

        switch (sortOrder)
        {
            case "name_desc":
                vehiclesIQ = vehiclesIQ.OrderByDescending(s => s.RegistrationNumber);
                break;
            case "Brand":
                vehiclesIQ = vehiclesIQ.OrderBy(s => s.Brand);
                break;
            case "brand_desc":
                vehiclesIQ = vehiclesIQ.OrderByDescending(s => s.Brand);
                break;
            case "Model":
                vehiclesIQ = vehiclesIQ.OrderBy(s => s.Model);
                break;
            case "model_desc":
                vehiclesIQ = vehiclesIQ.OrderByDescending(s => s.Model);
                break;
            case "Year":
                vehiclesIQ = vehiclesIQ.OrderBy(s => s.YearOfProduction);
                break;
            case "year_desc":
                vehiclesIQ = vehiclesIQ.OrderByDescending(s => s.YearOfProduction);
                break;
            case "Date":
                vehiclesIQ = vehiclesIQ.OrderBy(s => s.InspectionDate);
                break;
            case "date_desc":
                vehiclesIQ = vehiclesIQ.OrderByDescending(s => s.InspectionDate);
                break;
            case "Owner":
                vehiclesIQ = vehiclesIQ.OrderBy(s => s.Owner);
                break;
            case "owner_desc":
                vehiclesIQ = vehiclesIQ.OrderByDescending(s => s.Owner);
                break;
            default:
                vehiclesIQ = vehiclesIQ.OrderBy(s => s.RegistrationNumber);
                break;
        }

        int pageSize = 5;
        PageIndex = pageIndex ?? 1;
        var count = vehiclesIQ.Count();
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            
        Vehicles = vehiclesIQ.Skip((PageIndex - 1) * pageSize).Take(pageSize).ToList();
    }

    public IActionResult OnPostAdd(string sortOrder, int? pageIndex, string currentFilter)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Formularz zawiera błędy. Sprawdź poprawność danych.";
            return Page();
        }

        NewVehicle.Id = Guid.NewGuid();
        _vehicles.Add(NewVehicle);
        TempData["Success"] = "Pojazd został dodany pomyślnie!";
        return RedirectToPage(new { sortOrder, pageIndex, currentFilter });
    }

    public IActionResult OnPostEdit(string sortOrder, int? pageIndex, string currentFilter)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Formularz zawiera błędy. Sprawdź poprawność danych.";
            return Page();
        }

        var existing = _vehicles.FirstOrDefault(v => v.Id == NewVehicle.Id);
        if (existing != null)
        {
            existing.RegistrationNumber = NewVehicle.RegistrationNumber;
            existing.Brand = NewVehicle.Brand;
            existing.Model = NewVehicle.Model;
            existing.YearOfProduction = NewVehicle.YearOfProduction;
            existing.InspectionDate = NewVehicle.InspectionDate;
            existing.Owner = NewVehicle.Owner;
            TempData["Success"] = "Dane pojazdu zostały zaktualizowane!";
        }
        else
        {
            TempData["Error"] = "Nie znaleziono pojazdu do edycji.";
        }

        return RedirectToPage(new { sortOrder, pageIndex, currentFilter });
    }

    public IActionResult OnPostDelete(Guid id, string sortOrder, int? pageIndex, string currentFilter)
    {
        var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle != null)
        {
            _vehicles.Remove(vehicle);
            TempData["Success"] = "Pojazd został usunięty!";
        }
        else
        {
            TempData["Error"] = "Nie znaleziono pojazdu do usunięcia.";
        }
        return RedirectToPage(new { sortOrder, pageIndex, currentFilter });
    }
}