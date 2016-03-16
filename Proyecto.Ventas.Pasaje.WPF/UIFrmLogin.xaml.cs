using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

using Proyecto.Ventas.Pasaje.Datos;
using Proyecto.Ventas.Pasaje.RN;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmLogin.xaml
    /// </summary>
    public partial class UIFrmLogin : MetroWindow
    {
        public UIFrmLogin()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            if (this.txbUsername.Text != String.Empty && this.txbPassword.Password != String.Empty)
            {
                RNUsuario negocioUsuario = new RNUsuario();
                var info = negocioUsuario.validarLogin(this.txbUsername.Text, this.txbPassword.Password);
                if (info != null && info.Count > 0)
                {
                    Utilitarios.Utilitarios.idEmpleado = info[0].Id;
                    Utilitarios.Utilitarios.Username = info[0].Username;
                    Utilitarios.Utilitarios.NombreEmpleado = info[0].Nombre + info[0].ApellidoPaterno + info[0].ApellidoMaterno;
                    UIFrmPrincipal formPrincipal = new UIFrmPrincipal();
                    formPrincipal.Show();
                    this.Close();
                }
                else
                    MessageBox.Show("Datos Incorrectos");
            }
            else
                MessageBox.Show("Completar los campos");
        }
    }
}
