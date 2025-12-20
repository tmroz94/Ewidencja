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
            this.btnAddVehicle.Click += BtnAddVehicle_Click;
            this.dataGridView.CellClick += DataGridView_CellClick;
        }

        private void ApplyDarkTheme()
        {
            this.BackColor = Constants.Colors.DarkBackground;
            this.ForeColor = Constants.Colors.TextColor;

            this.formPanel.BackColor = Constants.Colors.DarkPanel;
            this.formPanel.ForeColor = Constants.Colors.TextColor;

            foreach (Control control in this.formPanel.Controls)
            {
                if (control is Label label)
                {
                    label.ForeColor = Constants.Colors.TextColor;
                    label.BackColor = Constants.Colors.DarkPanel;
                    label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Regular);
                }
            }

            foreach (Control control in this.formPanel.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.BackColor = Constants.Colors.DarkButton;
                    textBox.ForeColor = Constants.Colors.TextColor;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Font = new Font(textBox.Font.FontFamily, 10);
                }
            }

            foreach (Control control in this.formPanel.Controls)
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

            this.btnAddVehicle.BackColor = Constants.Colors.DarkButton;
            this.btnAddVehicle.ForeColor = Constants.Colors.White;
            this.btnAddVehicle.FlatStyle = FlatStyle.Flat;
            this.btnAddVehicle.FlatAppearance.BorderColor = Constants.Colors.DarkButton;
            this.btnAddVehicle.FlatAppearance.MouseOverBackColor = Constants.Colors.DarkButtonHover;
            this.btnAddVehicle.FlatAppearance.MouseDownBackColor = Constants.Colors.DarkButtonDown;
            this.btnAddVehicle.Font = new Font(this.btnAddVehicle.Font.FontFamily, 11, FontStyle.Bold);
            this.btnAddVehicle.Cursor = Cursors.Hand;
            this.btnAddVehicle.FlatAppearance.BorderSize = 0;

            this.dataGridView.BackgroundColor = Constants.Colors.DarkBackground;
            this.dataGridView.GridColor = Constants.Colors.GridLine;
            this.dataGridView.DefaultCellStyle.BackColor = Constants.Colors.DarkPanel;
            this.dataGridView.DefaultCellStyle.ForeColor = Constants.Colors.TextColor;
            this.dataGridView.DefaultCellStyle.SelectionBackColor = Constants.Colors.GridSelection;
            this.dataGridView.DefaultCellStyle.SelectionForeColor = Constants.Colors.White;
            this.dataGridView.DefaultCellStyle.Font = new Font(this.dataGridView.Font.FontFamily, 10);
            this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Constants.Colors.GridHeader;
            this.dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Constants.Colors.TextColor;
            this.dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font(this.dataGridView.Font.FontFamily, 10, FontStyle.Bold);
            this.dataGridView.ColumnHeadersHeight = 40;
            this.dataGridView.RowTemplate.Height = 35;
            this.dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Constants.Colors.GridAlternatingRow;
            this.dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = Constants.Colors.TextColor;
            this.dataGridView.ScrollBars = ScrollBars.Vertical;
            this.dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void SetupDataGridView()
        {
            this.dataGridView.Columns.Clear();
            this.dataGridView.Rows.Clear();

            this.dataGridView.Columns.Add("RegistrationNumber", Constants.UI.ColumnRegistration);
            this.dataGridView.Columns.Add("Brand", Constants.UI.ColumnBrand);
            this.dataGridView.Columns.Add("Model", Constants.UI.ColumnModel);
            this.dataGridView.Columns.Add("YearOfProduction", Constants.UI.ColumnYear);
            this.dataGridView.Columns.Add("InspectionDate", Constants.UI.ColumnInspection);
            this.dataGridView.Columns.Add("Owner", Constants.UI.ColumnOwner);
            
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.HeaderText = Constants.UI.ColumnDelete;
            deleteColumn.Text = Constants.UI.DeleteSymbol;
            deleteColumn.UseColumnTextForButtonValue = true;
            deleteColumn.FlatStyle = FlatStyle.Flat;
            deleteColumn.DefaultCellStyle.BackColor = Constants.Colors.AccentRed;
            deleteColumn.DefaultCellStyle.ForeColor = Constants.Colors.White;
            deleteColumn.DefaultCellStyle.Font = new Font(this.dataGridView.Font.FontFamily, 12, FontStyle.Bold);
            this.dataGridView.Columns.Add(deleteColumn);
        }

        private void DataGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                DialogResult result = DarkMessageBox.Show(
                    Constants.Messages.DeleteConfirmation,
                    Constants.Messages.DeleteTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    this.dataGridView.Rows.RemoveAt(e.RowIndex);
                    
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
                if (string.IsNullOrWhiteSpace(this.txtRegistration.Text) ||
                    string.IsNullOrWhiteSpace(this.txtBrand.Text) ||
                    string.IsNullOrWhiteSpace(this.txtModel.Text) ||
                    string.IsNullOrWhiteSpace(this.txtYear.Text) ||
                    string.IsNullOrWhiteSpace(this.txtOwner.Text))
                {
                    DarkMessageBox.Show(Constants.Messages.ValidationEmptyFields, Constants.Messages.ValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(this.txtYear.Text, out int year) || year < 1900 || year > DateTime.Now.Year)
                {
                    DarkMessageBox.Show(Constants.Messages.ValidationInvalidYear, Constants.Messages.ValidationTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var vehicle = new Vehicle
                {
                    RegistrationNumber = this.txtRegistration.Text,
                    Brand = this.txtBrand.Text,
                    Model = this.txtModel.Text,
                    YearOfProduction = year,
                    InspectionDate = this.dtInspection.Value,
                    Owner = this.txtOwner.Text
                };

                vehicles.Add(vehicle);

                this.dataGridView.Rows.Add(
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
            this.txtRegistration.Clear();
            this.txtBrand.Clear();
            this.txtModel.Clear();
            this.txtYear.Clear();
            this.txtOwner.Clear();
            this.dtInspection.Value = DateTime.Now;
            this.txtRegistration.Focus();
        }
    }
}
