namespace EwidencjaPrzegladow;

public static class Constants
{
    public static class Colors
    {
        public static readonly Color DarkBackground = Color.FromArgb(30, 30, 30);
        public static readonly Color DarkPanel = Color.FromArgb(45, 45, 45);
        public static readonly Color TextColor = Color.FromArgb(240, 240, 240);
        public static readonly Color AccentGreen = Color.FromArgb(76, 175, 80);
        public static readonly Color AccentRed = Color.FromArgb(200, 50, 50);
        public static readonly Color ButtonHover = Color.FromArgb(100, 200, 120);
        public static readonly Color DarkButton = Color.FromArgb(60, 60, 60);
        public static readonly Color DarkButtonHover = Color.FromArgb(80, 80, 80);
        public static readonly Color DarkButtonDown = Color.FromArgb(40, 40, 40);
        public static readonly Color GridSelection = Color.FromArgb(80, 80, 80);
        public static readonly Color GridHeader = Color.FromArgb(35, 35, 35);
        public static readonly Color GridAlternatingRow = Color.FromArgb(50, 50, 50);
        public static readonly Color GridLine = Color.FromArgb(70, 70, 70);
        public static readonly Color SelectionBlue = Color.FromArgb(0, 120, 180);
        public static readonly Color White = Color.White;
        public static readonly Color Gray = Color.Gray;
    }

    public static class Messages
    {
        public const string DeleteConfirmation = "Czy na pewno chcesz usunąć ten pojazd?";
        public const string DeleteTitle = "Potwierdzenie usunięcia";
        public const string VehicleDeleted = "Pojazd został usunięty!";
        public const string SuccessTitle = "Sukces";
        public const string ValidationEmptyFields = "Wszystkie pola muszą być wypełnione!";
        public const string ValidationTitle = "Błąd walidacji";
        public const string ValidationInvalidYear = "Podaj poprawny rok produkcji!";
        public const string VehicleAdded = "Pojazd został dodany pomyślnie!";
        public const string ErrorTitle = "Błąd";
        public const string Yes = "Tak";
        public const string No = "Nie";
        public const string Ok = "OK";
    }

    public static class UI
    {
        public const string AppTitle = "Ewidencja Przeglądów";
        public const string LabelRegistration = "Nr rejestracyjny:";
        public const string LabelBrand = "Marka:";
        public const string LabelModel = "Model:";
        public const string LabelYear = "Rok produkcji:";
        public const string LabelInspection = "Data przeglądu:";
        public const string LabelOwner = "Właściciel:";
        public const string ButtonAddVehicle = "Dodaj pojazd";
        public const string ColumnRegistration = "Nr rejestracyjny";
        public const string ColumnBrand = "Marka";
        public const string ColumnModel = "Model";
        public const string ColumnYear = "Rok produkcji";
        public const string ColumnInspection = "Data przeglądu";
        public const string ColumnOwner = "Właściciel";
        public const string ColumnDelete = "Usuń";
        public const string DeleteSymbol = "✕";
        public const string FontName = "Segoe UI";
        public const string DateFormat = "yyyy-MM-dd";
    }
}
