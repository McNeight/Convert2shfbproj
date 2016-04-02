using System;
using System.Windows.Forms;

namespace Convert2shfbproj
{
    static class Convert2shfbproj
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SandcastleBuilder.Utils.Conversion.NewFromOtherFormatDlg());
        }
    }
}
