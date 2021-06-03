using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auth.GG
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.Licensing.dll", "Bunifu.Licensing.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.1.5.3.dll", "Bunifu.UI.WinForms.1.5.3.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuButton.dll", "Bunifu.UI.WinForms.BunifuButton.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuCheckBox.dll", "Bunifu.UI.WinForms.BunifuCheckBox.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuCircleProgress.dll", "Bunifu.UI.WinForms.BunifuCircleProgress.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuColorTransition.dll", "Bunifu.UI.WinForms.BunifuColorTransition.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuDataGridView.dll", "Bunifu.UI.WinForms.BunifuDataGridView.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuDatePicker.dll", "Bunifu.UI.WinForms.BunifuDatePicker.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuDropdown.dll", "Bunifu.UI.WinForms.BunifuDropdown.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuFormDock.dll", "Bunifu.UI.WinForms.BunifuFormDock.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuGauge.dll", "Bunifu.UI.WinForms.BunifuGauge.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuGradientPanel.dll", "Bunifu.UI.WinForms.BunifuGradientPanel.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuGroupBox.dll", "Bunifu.UI.WinForms.BunifuGroupBox.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuImageButton.dll", "Bunifu.UI.WinForms.BunifuImageButton.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuLabel.dll", "Bunifu.UI.WinForms.BunifuLabel.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuPages.dll", "Bunifu.UI.WinForms.BunifuPages.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuPanel.dll", "Bunifu.UI.WinForms.BunifuPanel.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuPictureBox.dll", "Bunifu.UI.WinForms.BunifuPictureBox.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuProgressBar.dll", "Bunifu.UI.WinForms.BunifuProgressBar.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuRadioButton.dll", "Bunifu.UI.WinForms.BunifuRadioButton.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuRating.dll", "Bunifu.UI.WinForms.BunifuRating.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuScrollBar.dll", "Bunifu.UI.WinForms.BunifuScrollBar.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuSeparator.dll", "Bunifu.UI.WinForms.BunifuSeparator.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuShadowPanel.dll", "Bunifu.UI.WinForms.BunifuShadowPanel.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuShapes.dll", "Bunifu.UI.WinForms.BunifuShapes.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuSlider.dll", "Bunifu.UI.WinForms.BunifuSlider.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuSnackbar.dll", "Bunifu.UI.WinForms.BunifuSnackbar.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuTextBox.dll", "Bunifu.UI.WinForms.BunifuTextBox.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuToggleSwitch.dll", "Bunifu.UI.WinForms.BunifuToggleSwitch.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuToolTip.dll", "Bunifu.UI.WinForms.BunifuToolTip.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuTransition.dll", "Bunifu.UI.WinForms.BunifuTransition.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.BunifuUserControl.dll", "Bunifu.UI.WinForms.BunifuUserControl.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Bunifu.UI.WinForms.Deprecated.dll", "Bunifu.UI.WinForms.Deprecated.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Leaf.xNet.dll", "Leaf.xNet.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Newtonsoft.Json.dll", "Newtonsoft.Json.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.Siticone.UI.dll", "Siticone.UI.dll");
            EmbeddedAssembly.Load("Auth.GG.Dlls.NETCore.Encrypt.dll", "NETCore.Encrypt.dll");
            //EmbeddedAssembly.Load("Auth.GG.Dlls.name", "name");

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Auth.GG.Winforms.Initialform());
        }

        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
