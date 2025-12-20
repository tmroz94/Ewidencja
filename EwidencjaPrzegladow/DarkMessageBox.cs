namespace EwidencjaPrzegladow;

public class DarkMessageBox : Form
{
    private Label lblMessage;
    private FlowLayoutPanel buttonPanel;

    public DarkMessageBox(string message, string title, MessageBoxButtons buttons)
    {
        Text = title;
        Size = new Size(450, 200);
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
            
        BackColor = Constants.Colors.DarkBackground;
        ForeColor = Constants.Colors.TextColor;

        lblMessage = new Label();
        lblMessage.Text = message;
        lblMessage.AutoSize = false;
        lblMessage.Dock = DockStyle.Fill;
        lblMessage.TextAlign = ContentAlignment.MiddleCenter;
        lblMessage.ForeColor = Constants.Colors.TextColor;
        lblMessage.Font = new Font(Constants.UI.FontName, 10);
        lblMessage.Padding = new Padding(20);
        Controls.Add(lblMessage);

        buttonPanel = new FlowLayoutPanel();
        buttonPanel.FlowDirection = FlowDirection.RightToLeft;
        buttonPanel.Dock = DockStyle.Bottom;
        buttonPanel.Height = 60;
        buttonPanel.BackColor = Constants.Colors.DarkPanel;
        buttonPanel.Padding = new Padding(10);
        Controls.Add(buttonPanel);

        if (buttons == MessageBoxButtons.YesNo)
        {
            var btnNo = CreateButton(Constants.Messages.No, DialogResult.No);
            var btnYes = CreateButton(Constants.Messages.Yes, DialogResult.Yes);
            buttonPanel.Controls.Add(btnNo);
            buttonPanel.Controls.Add(btnYes);
            AcceptButton = btnYes;
            CancelButton = btnNo;
        }
        else
        {
            var btnOk = CreateButton(Constants.Messages.Ok, DialogResult.OK);
            buttonPanel.Controls.Add(btnOk);
            AcceptButton = btnOk;
        }
    }

    private Button CreateButton(string text, DialogResult result)
    {
        var btn = new Button();
        btn.Text = text;
        btn.DialogResult = result;
        btn.FlatStyle = FlatStyle.Flat;
        btn.BackColor = Constants.Colors.DarkButton;
        btn.ForeColor = Constants.Colors.White;
        btn.FlatAppearance.BorderSize = 0;
        btn.Size = new Size(100, 35);
        btn.Margin = new Padding(5);
        btn.Font = new Font(Constants.UI.FontName, 9, FontStyle.Regular);
        btn.Cursor = Cursors.Hand;
            
        btn.MouseEnter += (_, _) => btn.BackColor = Constants.Colors.DarkButtonHover;
        btn.MouseLeave += (_, _) => btn.BackColor = Constants.Colors.DarkButton;

        return btn;
    }

    public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
        using var form = new DarkMessageBox(message, title, buttons);

        return form.ShowDialog();
    }
}
