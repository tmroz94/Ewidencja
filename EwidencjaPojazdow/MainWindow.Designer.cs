namespace EwidencjaPojazdow;

partial class MainWindow
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1000, 650);
        this.Name = "Ewidencja Pojazdów";
        this.Text = Constants.UI.AppTitle;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        var mainLayout = new System.Windows.Forms.TableLayoutPanel();
        mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
        mainLayout.RowCount = 2;
        mainLayout.ColumnCount = 1;
        mainLayout.RowStyles.Clear();
        mainLayout.ColumnStyles.Clear();
        mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
        mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
        mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

        this.formPanel = new System.Windows.Forms.Panel();
        this.formPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.formPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.formPanel.Padding = new System.Windows.Forms.Padding(10);
        this.formPanel.BackColor = System.Drawing.SystemColors.Control;

        this.lblRegistration = new System.Windows.Forms.Label();
        this.lblRegistration.Text = Constants.UI.LabelRegistration;
        this.lblRegistration.AutoSize = true;
        this.lblRegistration.Location = new System.Drawing.Point(10, 15);
        this.formPanel.Controls.Add(this.lblRegistration);

        this.txtRegistration = new System.Windows.Forms.TextBox();
        this.txtRegistration.Location = new System.Drawing.Point(140, 12);
        this.txtRegistration.Size = new System.Drawing.Size(150, 23);
        this.formPanel.Controls.Add(this.txtRegistration);

        this.lblBrand = new System.Windows.Forms.Label();
        this.lblBrand.Text = Constants.UI.LabelBrand;
        this.lblBrand.AutoSize = true;
        this.lblBrand.Location = new System.Drawing.Point(10, 45);
        this.formPanel.Controls.Add(this.lblBrand);

        this.txtBrand = new System.Windows.Forms.TextBox();
        this.txtBrand.Location = new System.Drawing.Point(140, 42);
        this.txtBrand.Size = new System.Drawing.Size(150, 23);
        this.formPanel.Controls.Add(this.txtBrand);

        this.lblModel = new System.Windows.Forms.Label();
        this.lblModel.Text = Constants.UI.LabelModel;
        this.lblModel.AutoSize = true;
        this.lblModel.Location = new System.Drawing.Point(10, 75);
        this.formPanel.Controls.Add(this.lblModel);

        this.txtModel = new System.Windows.Forms.TextBox();
        this.txtModel.Location = new System.Drawing.Point(140, 72);
        this.txtModel.Size = new System.Drawing.Size(150, 23);
        this.formPanel.Controls.Add(this.txtModel);

        this.lblYear = new System.Windows.Forms.Label();
        this.lblYear.Text = Constants.UI.LabelYear;
        this.lblYear.AutoSize = true;
        this.lblYear.Location = new System.Drawing.Point(330, 15);
        this.formPanel.Controls.Add(this.lblYear);

        this.txtYear = new System.Windows.Forms.TextBox();
        this.txtYear.Location = new System.Drawing.Point(460, 12);
        this.txtYear.Size = new System.Drawing.Size(100, 23);
        this.formPanel.Controls.Add(this.txtYear);

        this.lblInspection = new System.Windows.Forms.Label();
        this.lblInspection.Text = Constants.UI.LabelInspection;
        this.lblInspection.AutoSize = true;
        this.lblInspection.Location = new System.Drawing.Point(330, 45);
        this.formPanel.Controls.Add(this.lblInspection);

        this.dtInspection = new EwidencjaPojazdow.DarkDateTimePicker();
        this.dtInspection.Location = new System.Drawing.Point(460, 42);
        this.dtInspection.Size = new System.Drawing.Size(150, 23);
        this.dtInspection.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        this.formPanel.Controls.Add(this.dtInspection);

        this.lblOwner = new System.Windows.Forms.Label();
        this.lblOwner.Text = Constants.UI.LabelOwner;
        this.lblOwner.AutoSize = true;
        this.lblOwner.Location = new System.Drawing.Point(330, 75);
        this.formPanel.Controls.Add(this.lblOwner);

        this.txtOwner = new System.Windows.Forms.TextBox();
        this.txtOwner.Location = new System.Drawing.Point(460, 72);
        this.txtOwner.Size = new System.Drawing.Size(150, 23);
        this.formPanel.Controls.Add(this.txtOwner);

        this.btnAddVehicle = new System.Windows.Forms.Button();
        this.btnAddVehicle.Text = Constants.UI.ButtonAddVehicle;
        this.btnAddVehicle.Location = new System.Drawing.Point(650, 12);
        this.btnAddVehicle.Size = new System.Drawing.Size(120, 30);
        this.formPanel.Controls.Add(this.btnAddVehicle);

        mainLayout.Controls.Add(this.formPanel, 0, 0);

        this.dataGridView = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
        this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        this.dataGridView.AutoGenerateColumns = false;
        this.dataGridView.AllowUserToAddRows = false;
        this.dataGridView.ReadOnly = true;
        this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
        this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView.EnableHeadersVisualStyles = false;
        ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();

        mainLayout.Controls.Add(this.dataGridView, 0, 1);

        this.Controls.Add(mainLayout);

        this.ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Panel formPanel = null!;
    private System.Windows.Forms.Label lblRegistration = null!;
    private System.Windows.Forms.Label lblBrand = null!;
    private System.Windows.Forms.Label lblModel = null!;
    private System.Windows.Forms.Label lblYear = null!;
    private System.Windows.Forms.Label lblInspection = null!;
    private System.Windows.Forms.Label lblOwner = null!;
    private System.Windows.Forms.TextBox txtRegistration = null!;
    private System.Windows.Forms.TextBox txtBrand = null!;
    private System.Windows.Forms.TextBox txtModel = null!;
    private System.Windows.Forms.TextBox txtYear = null!;
    private EwidencjaPojazdow.DarkDateTimePicker dtInspection = null!;
    private System.Windows.Forms.TextBox txtOwner = null!;
    private System.Windows.Forms.Button btnAddVehicle = null!;
    private System.Windows.Forms.DataGridView dataGridView = null!;
}