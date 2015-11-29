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
    /// Lógica de interacción para UIFrmItinerarioBus.xaml
    /// </summary>
    public partial class UIFrmItinerarioBus : MetroWindow
    {
        public UIFrmItinerarioBus()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            cargarFecha();
            cargarComboBus();
            cargarComboHora();
            cargarPrecio();
        }

        private void cargarPrecio()
        {
            this.txbPrecio.Text = Utilitarios.Utilitarios.precioMaximo.ToString();
        }

        private void cargarFecha()
        {
            this.dtpFecha.SelectedDate = DateTime.Now.Date;
        }

        List<V_BUSTIPO> listaBusTipo;
        private void cargarComboBus()
        {
            RNBus negocioBus = new RNBus();
            listaBusTipo = new List<V_BUSTIPO>();
            var info = negocioBus.traerBus_Tipo();
            foreach (var item in info)
            {
                if (negocioBus.verificarModelo(item.Id))
                    listaBusTipo.Add(item);
            }
            this.cbInterno.ItemsSource = listaBusTipo;
            this.cbInterno.DisplayMemberPath = "Interno";
            this.cbInterno.SelectedValuePath = "Id";
            this.cbInterno.SelectedIndex = 0;
            cargarTipoBus();
        }

        private void cargarComboHora()
        {
            RNHorario negocioHorario = new RNHorario();
            this.cbHora.ItemsSource = negocioHorario.traerHorarioDepartamento(Utilitarios.Utilitarios.idDepartamento);
            this.cbHora.DisplayMemberPath = "Hora";
            this.cbHora.SelectedValuePath = "IdHorario";
            this.cbHora.SelectedIndex = 0;
        }

        private void cargarTipoBus()
        {
            if (listaBusTipo.Count > 0)
                this.lblTipoBus.Content = listaBusTipo[this.cbInterno.SelectedIndex].Nombre;
        }

        private Boolean cargarItinerario(ITINERARIO itinerario)
        {
            try
            {
                Decimal precio;
                if (Decimal.TryParse(this.txbPrecio.Text, out precio))
                {
                    if (precio <= Utilitarios.Utilitarios.precioMaximo && precio >= Utilitarios.Utilitarios.precioMinimo)
                    {
                        RNItinerario negocioItinerario = new RNItinerario();
                        itinerario.IdBus = Int64.Parse(this.cbInterno.SelectedValue.ToString());
                        itinerario.IdUsuario = Utilitarios.Utilitarios.idEmpleado;
                        itinerario.IdDepartamento = Utilitarios.Utilitarios.idDepartamento;
                        itinerario.IdHorario = Int64.Parse(this.cbHora.SelectedValue.ToString());
                        itinerario.Fecha = this.dtpFecha.SelectedDate.Value;
                        itinerario.Precio = precio;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Introducir un Precio <= a " + Utilitarios.Utilitarios.precioMaximo.ToString() + " o <= a " + Utilitarios.Utilitarios.precioMinimo.ToString());
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Error en la informacion Introducida...!");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ocurrio algun Problema con el servidor vualva a intentarlo mas tarde..!");
                return false;
            }
        }

        private void btnAgregarBusItinerario_Click(object sender, RoutedEventArgs e)
        {
            if (listaBusTipo.Count > 0)
            {
                ITINERARIO itinerario = new ITINERARIO();
                RNItinerario negocioItinerario = new RNItinerario();
                RNBus negocioBus = new RNBus();
                BUS bus = new BUS();
                bus.Id = Int64.Parse(this.cbInterno.SelectedValue.ToString());
                if (cargarItinerario(itinerario))
                {
                    if (negocioItinerario.registrar(itinerario) && negocioBus.actualizarEstado(bus))
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Error en el registro..!");
                }
            }
            else
                MessageBox.Show("Lo sentimos no hay buses disponibles");
        }

        private void cbInterno_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cargarTipoBus();
        }
    }
}
