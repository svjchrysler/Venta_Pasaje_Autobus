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
    /// Lógica de interacción para UIFrmVentaPasaje.xaml
    /// </summary>
    public partial class UIFrmVentaPasaje : MetroWindow
    {
        public UIFrmVentaPasaje()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            listAsiento = new List<ASIENTO>();
            cargarInformacionAsiento();
            cargarInformacionInicial();
            cargarInformacioItinerario();
            crearColumnas();
            agregarFilas();
            cambiarTamanoColumna();
        }

        List<ASIENTO> listAsiento;

        private void cargarInformacionAsiento()
        {
            RNAsiento negocioAsiento = new RNAsiento();
            foreach (var item in Utilitarios.Utilitarios.pasajesAgregados)
            {
                var info = negocioAsiento.traerAsiento(item, Utilitarios.Utilitarios.idBus);
                if (info.Count > 0)
                    listAsiento.Add(info[0]);
            }
        }

        private void cargarInformacionInicial()
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("/Images/sale.png", UriKind.Relative);
            image.EndInit();
            this.imgTipo.Source = image;

            this.lblTipoPedido.Content = "Venta";

            this.lblFecha.Content = DateTime.Now.Date.Day + "/" + DateTime.Now.Date.Month + "/" + DateTime.Now.Date.Year;
        }

        private void cargarInformacioItinerario()
        {
            RNItinerario negocioItinerario = new RNItinerario();
            var info = negocioItinerario.traerItinerario(Utilitarios.Utilitarios.idItinerario);
            if (info.Count > 0)
                this.txbPrecio.Text = info[0].Precio.ToString();
        }

        private void crearColumnas()
        {
            this.dgAgregarCliente.Columns.Add("NroAsiento", "NroAsiento");
            this.dgAgregarCliente.Columns.Add("CI", "CI");
            this.dgAgregarCliente.Columns.Add("Cliente", "Cliente");
            this.dgAgregarCliente.Columns.Add("Id", "Id");
            this.dgAgregarCliente.Columns.Add("IdAsiento", "IdAsiento");
        }

        private void cambiarTamanoColumna()
        {
            this.dgAgregarCliente.Columns[0].Width = 80;
            this.dgAgregarCliente.Columns[1].Width = 120;
            this.dgAgregarCliente.Columns[2].Width = 250;
            this.dgAgregarCliente.Columns[3].Visible = false;
            this.dgAgregarCliente.Columns[4].Visible = false;
        }

        private void agregarFilas()
        {
            for (int i = 0; i < Utilitarios.Utilitarios.pasajesAgregados.Count; i++)
            {   
                this.dgAgregarCliente.Rows.Add();
                this.dgAgregarCliente.Rows[i].Cells[0].ReadOnly = true;
                this.dgAgregarCliente.Rows[i].Cells[0].Value = listAsiento[i].NroAsiento;
                this.dgAgregarCliente.Rows[i].Cells[4].Value = listAsiento[i].Id;
            }
        }

        private void dgAgregarCliente_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                String cliente = this.dgAgregarCliente.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                Int32 ci;
                if (Int32.TryParse(cliente, out ci))
                {
                    RNCliente negocioCliente = new RNCliente();
                    var info = negocioCliente.traerClienteNatural(ci);
                    if (info.Count > 0)
                    {
                        this.dgAgregarCliente.Rows[e.RowIndex].Cells[2].Value = info[0].NombreCompleto;
                        this.dgAgregarCliente.Rows[e.RowIndex].Cells[3].Value = info[0].Id;
                    }
                    else
                    {
                        dgAgregarCliente.Rows[e.RowIndex].Cells[2].Value = String.Empty;
                        dgAgregarCliente.Rows[e.RowIndex].Cells[3].Value = String.Empty;
                    }
                }
            }
        }

        List<INFORMACION_PASAJE> informacion;
        private Boolean prepararInformacion()
        {
            informacion = new List<INFORMACION_PASAJE>();
            foreach (System.Windows.Forms.DataGridViewRow item in this.dgAgregarCliente.Rows)
            {
                INFORMACION_PASAJE inform = new INFORMACION_PASAJE();
                inform.IdAsiento = Int64.Parse(item.Cells[4].Value.ToString());
                inform.NroAsiento = Int32.Parse(item.Cells[0].Value.ToString());
                inform.Precio = Decimal.Parse(this.txbPrecio.Text);
                if (item.Cells[3].Value != null && item.Cells[3].Value.ToString() !=String.Empty)
                {   
                    inform.IdCliente = Int64.Parse(item.Cells[3].Value.ToString());
                    inform.Cliente = item.Cells[2].Value.ToString();
                    inform.CI = Int32.Parse(item.Cells[1].Value.ToString());
                }
                else
                {
                    RNCliente negocioCliente = new RNCliente();
                    RNControlCliente negocioControlCliente = new RNControlCliente();
                    CLIENTE cliente = new CLIENTE();
                    cliente.Id = negocioCliente.generarId();
                    inform.IdCliente = cliente.Id;
                    NATURAL natural = new NATURAL();
                    natural.Id = cliente.Id;
                    natural.CI = Int32.Parse(item.Cells[1].Value.ToString());
                    inform.CI = Int32.Parse(item.Cells[1].Value.ToString());
                    natural.NombreCompleto = item.Cells[2].Value.ToString();
                    inform.Cliente = natural.NombreCompleto;
                    if (!negocioControlCliente.registrarClienteNatural(cliente, natural))
                        return false;
                }
                informacion.Add(inform);
            }
            return true;
        }

        private Boolean validarCampos()
        {
            foreach (System.Windows.Forms.DataGridViewRow item in this.dgAgregarCliente.Rows)
            {
                if ((item.Cells[1].Value == null || item.Cells[1].Value.ToString() == String.Empty) || (item.Cells[2].Value == null || item.Cells[2].Value.ToString() == String.Empty))
                    return false;
            }
            return true;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                if (prepararInformacion())
                {
                    if (checkFactura.IsChecked.Value)
                    {
                        Utilitarios.Utilitarios.TipoFacturacion = 1;
                        foreach (var item in informacion)
                        {
                            Utilitarios.Utilitarios.informacionAuxiliar = new List<INFORMACION_PASAJE>();
                            Utilitarios.Utilitarios.informacionAuxiliar.Add(item);
                            UIFrmFactura formFactura = new UIFrmFactura();
                            if (formFactura.ShowDialog().Value)
                            {

                            }
                        }
                    }
                    else
                    {
                        Utilitarios.Utilitarios.TipoFacturacion = 0;
                        Utilitarios.Utilitarios.informacionAuxiliar = informacion;
                        UIFrmFactura formFactura = new UIFrmFactura();
                        if (formFactura.ShowDialog().Value)
                        {

                        }
                    }
                    this.DialogResult = true;
                    this.Close();
                }
                else
                    MessageBox.Show("Lo sentimos ocurrio un error en el servidor..!");
            }
            else
                MessageBox.Show("Completar los campos");
        }
    }
}
