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
using Proyecto.Ventas.Pasaje.Utilitarios;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmPrecio.xaml
    /// </summary>
    public partial class UIFrmPrecio : MetroWindow
    {
        public UIFrmPrecio()
        {
            InitializeComponent();
            cargarInformacion();
        }

        private void cargarInformacion()
        {
            this.txbPrecioMaximo.Text = Utilitarios.Utilitarios.precioMaximo.ToString();
            this.txbPrecioMinimo.Text = Utilitarios.Utilitarios.precioMinimo.ToString();
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            RNAuxiliar negocioAuxiliar = new RNAuxiliar();
            AUXILIAR auxiliar = new AUXILIAR();
            auxiliar.Id = Utilitarios.Utilitarios.idAuxiliar;
            Decimal precioMaximo,precioMinimo;
            if (Decimal.TryParse(this.txbPrecioMaximo.Text, out precioMaximo) && Decimal.TryParse(this.txbPrecioMinimo.Text,out precioMinimo))
            {
                auxiliar.PrecioMaximo = precioMaximo;
                auxiliar.PrecioMinimo = precioMinimo;
                if (negocioAuxiliar.actualizarPrecio(auxiliar))
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                    MessageBox.Show("Error al actualizar el Precio...!");
            }
            else
                MessageBox.Show("PorFavor Introducir un Numero");
        }
    }
}
