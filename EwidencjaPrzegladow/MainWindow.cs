using Microsoft.Reporting.WinForms;
using EwidencjaPrzegladow.Data;

namespace EwidencjaPrzegladow;

public partial class MainWindow : Form
{
    private List<Vehicle> vehicles = [];

    public MainWindow()
    {
        InitializeComponent();
        ApplyDarkTheme();
        btnAddVehicle.Click += BtnAddVehicle_Click;
        Load += MainWindow_Load;
    }

    private void MainWindow_Load(object? sender, EventArgs e)
    {
        AddSampleData();
        RefreshReport();
    }

    private void AddSampleData()
    {
        var now = DateTime.Now;
        var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);

        for (var i = 1; i <= 10; i++)
        {
            var day = Math.Min(i * 2, daysInMonth);
            var inspectionDate = new DateTime(now.Year, now.Month, day);
            var random = i % 2;

            vehicles.Add(new Vehicle
            {
                RegistrationNumber = $"KR {10000 + i}",
                Brand = random == 0 ? "Toyota" : "Ford",
                Model = random == 0 ? "Corolla" : "Focus",
                YearOfProduction = 2015 + (i % 5),
                InspectionDate = inspectionDate,
                Owner = $"Właściciel Testowy {i}"
            });
        }
    }

    private void ApplyDarkTheme()
    {
        BackColor = Constants.Colors.DarkBackground;
        ForeColor = Constants.Colors.TextColor;

        formPanel.BackColor = Constants.Colors.DarkPanel;
        formPanel.ForeColor = Constants.Colors.TextColor;

        foreach (Control control in formPanel.Controls)
        {
            if (control is Label label)
            {
                label.ForeColor = Constants.Colors.TextColor;
                label.BackColor = Constants.Colors.DarkPanel;
                label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Regular);
            }
        }

        foreach (Control control in formPanel.Controls)
        {
            if (control is TextBox textBox)
            {
                textBox.BackColor = Constants.Colors.DarkButton;
                textBox.ForeColor = Constants.Colors.TextColor;
                textBox.BorderStyle = BorderStyle.FixedSingle;
                textBox.Font = new Font(textBox.Font.FontFamily, 10);
            }
        }

        foreach (Control control in formPanel.Controls)
        {
            if (control is DateTimePicker dtp)
            {
                dtp.BackColor = Constants.Colors.DarkButton;
                dtp.ForeColor = Constants.Colors.TextColor;
                dtp.Font = new Font(dtp.Font.FontFamily, 10);
                dtp.CalendarMonthBackground = Constants.Colors.DarkPanel;
                dtp.CalendarTitleBackColor = Constants.Colors.DarkBackground;
                dtp.CalendarTitleForeColor = Constants.Colors.TextColor;
                dtp.CalendarTrailingForeColor = Constants.Colors.Gray;
                dtp.CalendarForeColor = Constants.Colors.TextColor;
            }
        }

        btnAddVehicle.BackColor = Constants.Colors.DarkButton;
        btnAddVehicle.ForeColor = Constants.Colors.White;
        btnAddVehicle.FlatStyle = FlatStyle.Flat;
        btnAddVehicle.FlatAppearance.BorderColor = Constants.Colors.DarkButton;
        btnAddVehicle.FlatAppearance.MouseOverBackColor = Constants.Colors.DarkButtonHover;
        btnAddVehicle.FlatAppearance.MouseDownBackColor = Constants.Colors.DarkButtonDown;
        btnAddVehicle.Font = new Font(btnAddVehicle.Font.FontFamily, 11, FontStyle.Bold);
        btnAddVehicle.Cursor = Cursors.Hand;
        btnAddVehicle.FlatAppearance.BorderSize = 0;

        reportViewer.BackColor = Constants.Colors.DarkPanel;
    }

    private void RefreshReport()
    {
        var currentMonth = DateTime.Now.Month;
        var currentYear = DateTime.Now.Year;

        var filteredVehicles = vehicles
            .Where(v => v.InspectionDate.Month == currentMonth && v.InspectionDate.Year == currentYear)
            .ToList();

        var vehicleData = new VehicleData();
        foreach (var v in filteredVehicles)
        {
            vehicleData.Vehicle.AddVehicleRow(
                v.RegistrationNumber,
                v.Brand,
                v.Model,
                v.YearOfProduction,
                v.InspectionDate,
                v.Owner
            );
        }

        reportViewer.LocalReport.ReportPath = @"Report\VehicleReport.rdlc";
        reportViewer.LocalReport.DataSources.Clear();
        reportViewer.LocalReport.DataSources.Add(new ReportDataSource("VehicleDataSet", vehicleData.Tables["Vehicle"]));

        reportViewer.SetDisplayMode(DisplayMode.Normal);
        reportViewer.ZoomMode = ZoomMode.Percent;
        reportViewer.ZoomPercent = 100;

        reportViewer.RefreshReport();
    }

    private void BtnAddVehicle_Click(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtRegistration.Text) ||
                string.IsNullOrWhiteSpace(txtBrand.Text) ||
                string.IsNullOrWhiteSpace(txtModel.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                string.IsNullOrWhiteSpace(txtOwner.Text))
            {
                DarkMessageBox.Show(Constants.Messages.ValidationEmptyFields, Constants.Messages.ValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year) || year < 1900 || year > DateTime.Now.Year)
            {
                DarkMessageBox.Show(Constants.Messages.ValidationInvalidYear, Constants.Messages.ValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var vehicle = new Vehicle
            {
                RegistrationNumber = txtRegistration.Text,
                Brand = txtBrand.Text,
                Model = txtModel.Text,
                YearOfProduction = year,
                InspectionDate = dtInspection.Value,
                Owner = txtOwner.Text
            };

            vehicles.Add(vehicle);

            RefreshReport();
            ClearForm();

            DarkMessageBox.Show(Constants.Messages.VehicleAdded, Constants.Messages.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            DarkMessageBox.Show($"{Constants.Messages.ErrorTitle}: {ex.Message}\n{ex.StackTrace}", Constants.Messages.ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearForm()
    {
        txtRegistration.Clear();
        txtBrand.Clear();
        txtModel.Clear();
        txtYear.Clear();
        txtOwner.Clear();
        dtInspection.Value = DateTime.Now;
        txtRegistration.Focus();
    }
}
