using System.Runtime.InteropServices;

namespace EwidencjaPrzegladow;

public class DarkDateTimePicker : DateTimePicker
{
    [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
    private static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

    public DarkDateTimePicker()
    {
        Format = DateTimePickerFormat.Short;
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        SetWindowTheme(this.Handle, "", "");
    }
}
