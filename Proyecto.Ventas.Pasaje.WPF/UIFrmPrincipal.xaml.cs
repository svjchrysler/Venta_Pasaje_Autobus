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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Behaviours;

using Proyecto.Ventas.Pasaje.Datos;
using Proyecto.Ventas.Pasaje.RN;
using Proyecto.Ventas.Pasaje.Utilitarios;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class UIFrmPrincipal : MetroWindow
    {

        public UIFrmPrincipal()
        {
            InitializeComponent();
            cargarInformacion();
            vistaFrame("UIFrmBienvenida.xaml");
        }

        public void redimensionarFrame()
        {
            this.framePrimary.Height = this.Height;
            this.framePrimary.Width = this.Width - this.MenuItemsCollection.Width;

        }

        public void cargarInformacion()
        {
            this.Title = "USERNAME : " + Utilitarios.Utilitarios.Username;
            RNAuxiliar negocioAuxiliar = new RNAuxiliar();
            var info = negocioAuxiliar.traerAuxiliar();
            Utilitarios.Utilitarios.precioMaximo = info.PrecioMaximo;
            Utilitarios.Utilitarios.precioMinimo = info.PrecioMinimo;
            Utilitarios.Utilitarios.idAuxiliar = info.Id;
        }

        private void vistaFrame(String form)
        {
            this.framePrimary.Source = new Uri(BaseUriHelper.GetBaseUri(this), form);
            this.framePrimary.Focus();
        }

        private void menuBus_Click(object sender, RoutedEventArgs e)
        {
            vistaFrame("UIFrmBus.xaml"); 
        }

        private void menuModelo_Click(object sender, RoutedEventArgs e)
        {
            vistaFrame("UIFrmCrearModeloBus.xaml");
        }

        private void menuItinerario_Click(object sender, RoutedEventArgs e)
        {
            vistaFrame("UIFrmItinerario.xaml");
        }

        private void menuSolicitudItinerario_Click(object sender, RoutedEventArgs e)
        {
            vistaFrame("UIFrmSolicitudItinerario.xaml");
        }

        private void menuReporte_Click(object sender, RoutedEventArgs e)
        {
            vistaFrame("EstadisticaVentas.xaml");
        }

        private void menuReporteVenta_Click(object sender, RoutedEventArgs e)
        {
            vistaFrame("UIFrmReporteVentaEmpleado.xaml");
        }

    }
}
