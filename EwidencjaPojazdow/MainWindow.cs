using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EwidencjaPojazdow
{
    public partial class MainWindow : Form
    {
        private List<Vehicle> vehicles = [];

        public MainWindow()
        {
            InitializeComponent();
            ApplyDarkTheme();
            SetupDataGridView();
            btnAddVehicle.Click += BtnAddVehicle_Click;
            dataGridView.CellClick += DataGridView_CellClick;
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

            dataGridView.BackgroundColor = Constants.Colors.DarkBackground;
            dataGridView.GridColor = Constants.Colors.GridLine;
            dataGridView.DefaultCellStyle.BackColor = Constants.Colors.DarkPanel;
            dataGridView.DefaultCellStyle.ForeColor = Constants.Colors.TextColor;
            dataGridView.DefaultCellStyle.SelectionBackColor = Constants.Colors.GridSelection;
            dataGridView.DefaultCellStyle.SelectionForeColor = Constants.Colors.White;
            dataGridView.DefaultCellStyle.Font = new Font(dataGridView.Font.FontFamily, 10);
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Constants.Colors.GridHeader;
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Constants.Colors.TextColor;
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView.Font.FontFamily, 10, FontStyle.Bold);
            dataGridView.ColumnHeadersHeight = 40;
            dataGridView.RowTemplate.Height = 35;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Constants.Colors.GridAlternatingRow;
            dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = Constants.Colors.TextColor;
            dataGridView.ScrollBars = ScrollBars.Vertical;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SetupDataGridView()
        {
            dataGridView.Columns.Clear();
            dataGridView.Rows.Clear();

            dataGridView.Columns.Add("RegistrationNumber", Constants.UI.ColumnRegistration);
            dataGridView.Columns.Add("Brand", Constants.UI.ColumnBrand);
            dataGridView.Columns.Add("Model", Constants.UI.ColumnModel);
            dataGridView.Columns.Add("YearOfProduction", Constants.UI.ColumnYear);
            dataGridView.Columns.Add("InspectionDate", Constants.UI.ColumnInspection);
            dataGridView.Columns.Add("Owner", Constants.UI.ColumnOwner);
            
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.HeaderText = Constants.UI.ColumnDelete;
            deleteColumn.Text = Constants.UI.DeleteSymbol;
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.FlatStyle = FlatStyle.Flat;
            deleteColumn.DefaultCellStyle.BackColor = Constants.Colors.AccentRed;
            deleteColumn.DefaultCellStyle.ForeColor = Constants.Colors.White;
            deleteColumn.DefaultCellStyle.Font = new Font(dataGridView.Font.FontFamily, 12, FontStyle.Bold);
            dataGridView.Columns.Add(deleteColumn);
        }

        private void DataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                var result = DarkMessageBox.Show(
                    Constants.Messages.DeleteConfirmation,
                    Constants.Messages.DeleteTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dataGridView.Rows.RemoveAt(e.RowIndex);
                    
                    if (e.RowIndex < vehicles.Count)
                    {
                        vehicles.RemoveAt(e.RowIndex);
                    }

                    DarkMessageBox.Show(Constants.Messages.VehicleDeleted, Constants.Messages.SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
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

                dataGridView.Rows.Add(
                    vehicle.RegistrationNumber,
                    vehicle.Brand,
                    vehicle.Model,
                    vehicle.YearOfProduction,
                    vehicle.InspectionDate.ToString(Constants.UI.DateFormat),
                    vehicle.Owner,
                    Constants.UI.DeleteSymbol
                );

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
}
