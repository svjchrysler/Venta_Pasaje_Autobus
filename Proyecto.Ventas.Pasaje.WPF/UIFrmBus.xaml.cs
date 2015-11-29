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
    /// Lógica de interacción para UIFrmBus.xaml
    /// </summary>
    public partial class UIFrmBus : Page
    {
        public UIFrmBus()
        {
            InitializeComponent();
            cargarComboTipoBus();
        }

        private void cargarComboTipoBus()
        {
            RNTipoBus negocioTipoBus = new RNTipoBus();
            this.cbTipoBus.ItemsSource = negocioTipoBus.traerTipoBus();
            this.cbTipoBus.DisplayMemberPath = "Nombre";
            this.cbTipoBus.SelectedValuePath = "Id";
            this.cbTipoBus.SelectedIndex = 0;
        }

        private void cargarBus(BUS bus)
        {
            bus.Interno = Int32.Parse(this.txbInterno.Text);
            bus.IdTipoBus = Int64.Parse(this.cbTipoBus.SelectedValue.ToString());
            bus.Placa = this.txbPlaca.Text;
            bus.Estado = 0;
        }

        private void crearColumnas()
        {
            this.dgBuscar.Columns.Clear();
            this.dgBuscar.AutoGenerateColumns = false;
            this.dgBuscar.IsReadOnly = false;

            DataGridTextColumn dc = new DataGridTextColumn();
            dc.Header = "Id";
            dc.Binding = new Binding("Id");
            dc.IsReadOnly = true;

            this.dgBuscar.Columns.Add(dc);

            dc = new DataGridTextColumn();
            dc.Header = "Interno";
            dc.Width = 90;
            dc.Binding = new Binding("Interno");
            dc.IsReadOnly = true;

            this.dgBuscar.Columns.Add(dc);

            dc = new DataGridTextColumn();
            dc.Header = "Placa";
            dc.Width = 100;
            dc.Binding = new Binding("Placa");
            dc.IsReadOnly = true;

            this.dgBuscar.Columns.Add(dc);

            dc = new DataGridTextColumn();
            dc.Header = "Tipo Bus";
            dc.Width = 100;
            dc.Binding = new Binding("Nombre");
            dc.IsReadOnly = true;

            this.dgBuscar.Columns.Add(dc);

        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Int32 interno;
            if (Int32.TryParse(this.txbInterno.Text, out interno))
            {
                RNBus negocioBus = new RNBus();
                if (!negocioBus.validarRegistro(this.txbPlaca.Text, interno))
                {
                    BUS bus = new BUS();
                    cargarBus(bus);
                    MessageBox.Show(negocioBus.registrar(bus));

                }
                else
                    MessageBox.Show("Los datos Introducidos ya existen");
            }
            else
                MessageBox.Show("Porfavor colocar un interno valido");
        }

        private void cargarInformacionBus(Int32 Interno)
        {
            crearColumnas();
            RNBus negocioBus = new RNBus();
            this.dgBuscar.ItemsSource = negocioBus.traerBus_Tipo(Interno);
        }


        private void txbBuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Int32 interno;
            if (Int32.TryParse(this.txbBuscar.Text, out interno))
                cargarInformacionBus(interno);
        }
    }
}
