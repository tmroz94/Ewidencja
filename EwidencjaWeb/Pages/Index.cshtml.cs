using EwidencjaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EwidencjaWeb.Pages;

public class IndexModel : PageModel
{
    private const int _defaultFirstPage = 1;
    private const int _defaultPageSize = 5;

    private static List<Vehicle> _vehicles = [];

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
        for (var i = 1; i <= 20; i++)
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

    public void OnGet(string? sortOrder, string? currentFilter, string? searchString, int? pageIndex)
    {
        PopulateModel(sortOrder, currentFilter, searchString, pageIndex);
    }

    private void PopulateModel(string? sortOrder, string? currentFilter, string? searchString, int? pageIndex)
    {
        CurrentSort = sortOrder;
        NameSort = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        BrandSort = sortOrder == "brand_asc" ? "brand_desc" : "brand_asc";
        ModelSort = sortOrder == "model_asc" ? "model_desc" : "model_asc";
        YearSort = sortOrder == "year_asc" ? "year_desc" : "year_asc";
        DateSort = sortOrder == "date_asc" ? "date_desc" : "date_asc";
        OwnerSort = sortOrder == "owner_asc" ? "owner_desc" : "owner_asc";

        if (searchString != null)
        {
            pageIndex = _defaultFirstPage;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString;

        var vehicles = _vehicles.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            var s = searchString.ToLower();
            vehicles = vehicles.Where(v => 
                v.RegistrationNumber.ToLower().Contains(s) || 
                v.Brand.ToLower().Contains(s) ||
                v.Model.ToLower().Contains(s) ||
                v.Owner.ToLower().Contains(s) ||
                v.YearOfProduction.ToString().Contains(s));
        }

        vehicles = sortOrder switch
        {
            "name_desc" => vehicles.OrderByDescending(s => s.RegistrationNumber),
            "brand_asc" => vehicles.OrderBy(s => s.Brand),
            "brand_desc" => vehicles.OrderByDescending(s => s.Brand),
            "model_asc" => vehicles.OrderBy(s => s.Model),
            "model_desc" => vehicles.OrderByDescending(s => s.Model),
            "year_asc" => vehicles.OrderBy(s => s.YearOfProduction),
            "year_desc" => vehicles.OrderByDescending(s => s.YearOfProduction),
            "date_asc" => vehicles.OrderBy(s => s.InspectionDate),
            "date_desc" => vehicles.OrderByDescending(s => s.InspectionDate),
            "owner_asc" => vehicles.OrderBy(s => s.Owner),
            "owner_desc" => vehicles.OrderByDescending(s => s.Owner),
            _ => vehicles.OrderBy(s => s.RegistrationNumber)
        };

        PageIndex = pageIndex ?? _defaultFirstPage;

        var count = vehicles.Count();
        TotalPages = (int)Math.Ceiling(count / (double)_defaultPageSize);

        if (PageIndex > TotalPages && TotalPages > 0)
        {
            PageIndex = TotalPages;
        }
            
        Vehicles = vehicles.Skip((PageIndex - 1) * _defaultPageSize).Take(_defaultPageSize).ToList();
    }

    public IActionResult OnPostAdd(string? sortOrder, int? pageIndex, string? currentFilter)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Formularz zawiera błędy. Sprawdź poprawność danych.";
            ViewData["OpenAddModal"] = true;

            PopulateModel(sortOrder, currentFilter, currentFilter, pageIndex);

            return Page();
        }

        NewVehicle.Id = Guid.NewGuid();
        _vehicles.Add(NewVehicle);

        TempData["Success"] = "Pojazd został dodany pomyślnie!";
        
        return RedirectToPage(new { sortOrder, pageIndex, currentFilter });
    }

    public IActionResult OnPostEdit(string? sortOrder, int? pageIndex, string? currentFilter)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Formularz zawiera błędy. Sprawdź poprawność danych.";
            ViewData["OpenEditModalId"] = NewVehicle.Id;

            PopulateModel(sortOrder, currentFilter, currentFilter, pageIndex);

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

    public IActionResult OnPostDelete(Guid id, string? sortOrder, int? pageIndex, string? currentFilter)
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