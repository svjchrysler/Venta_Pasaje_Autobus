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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Proyecto.Ventas.Pasaje.Datos;
using Proyecto.Ventas.Pasaje.RN;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmTipoBus.xaml
    /// </summary>
    public partial class UIFrmTipoBus : Page
    {
        public UIFrmTipoBus()
        {
            InitializeComponent();
        }

        private void cargarTipoBus(TIPO_BUS tipoBus)
        {
            tipoBus.Nombre = this.txbNombre.Text;
            tipoBus.NroColumnas = Int32.Parse(this.txbNroColumnas.Text);
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            RNTipoBus negocioTipoBus = new RNTipoBus();
            TIPO_BUS tipoBus = new TIPO_BUS();
            cargarTipoBus(tipoBus);
            MessageBox.Show(negocioTipoBus.registrar(tipoBus));
        }
    }
}
