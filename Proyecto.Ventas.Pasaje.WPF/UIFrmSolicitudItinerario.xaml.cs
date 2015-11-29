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
using Proyecto.Ventas.Pasaje.Utilitarios;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmSolicitudItinerario.xaml
    /// </summary>
    public partial class UIFrmSolicitudItinerario : Page
    {
        public UIFrmSolicitudItinerario()
        {
            InitializeComponent();
            init();
        }

        private void cargarFecha()
        {
            this.calFecha.SelectedDate = DateTime.Now.Date;
        }

        private void init()
        {
            cargarFecha();
            generarControles();
            this.dgModeloBus.GridColor = System.Drawing.Color.White;
        }

        private void crearColumnasPasajeros()
        {
            this.dgPasajeros.Columns.Clear();
            this.dgPasajeros.AutoGenerateColumns = false;
            this.dgPasajeros.IsReadOnly = false;

            DataGridTextColumn dt = new DataGridTextColumn();
            dt.Header = "NRO";
            dt.Binding = new Binding("NroAsiento");
            dt.IsReadOnly = true;

            this.dgPasajeros.Columns.Add(dt);

            dt = new DataGridTextColumn();
            dt.Header = "PASAJERO";
            dt.Binding = new Binding("NombreCompleto");
            dt.IsReadOnly = true;

            this.dgPasajeros.Columns.Add(dt);

        }

        private void cargarPasajeros(Int64 IdItinerario)
        {
            RNItinerario negocioItinerario = new RNItinerario();
            var info = negocioItinerario.traerAsientoCliente(IdItinerario);
            if (info != null && info.Count > 0)
            {
                crearColumnasPasajeros();
                this.dgPasajeros.ItemsSource = info;
            }
        }

        private void cargarInformacionViaje()
        {
            this.lblDepartamento.Content = info[0].Departamento;
            this.lblHora.Content = info[0].Hora;
            String fecha = info[0].Fecha.Date.Day + "/" + info[0].Fecha.Date.Month + "/" + info[0].Fecha.Date.Year;
            this.lblFecha.Content = fecha;
            this.lblInterno.Content = info[0].Interno;
            this.lblTipoBus.Content = info[0].TipoBus;
        }

        private List<V_INFORMACION_ITINERARIOS> datosItinerario(Int64 IdDepartamento)
        {
            RNItinerario negocioItinerario = new RNItinerario();
            DateTime dtp = DateTime.Parse(this.calFecha.SelectedDate.ToString());
            return negocioItinerario.traerInformacionItinerario(IdDepartamento, dtp);
        }
        
        private void generarControles()
        {
            StackPanel stackPanelPadre = new StackPanel();
            RNDepartamento negocioDepartamento = new RNDepartamento();
            var info = negocioDepartamento.traerDepartamento(0);
            foreach (var item in info)
            {
                var infoHoraItinerario = datosItinerario(item.Id);
                if (infoHoraItinerario.Count > 0)
                {
                    Expander expander = new Expander();
                    expander.Margin.Top.Equals(20);
                    expander.Header = item.Nombre;
                    StackPanel stackpanel = new StackPanel();
                    expander.Content = stackpanel;

                    ListBox listbox = new ListBox();
                    listbox.MouseDoubleClick += ClickItemListView;
                    listbox.Height = 100;
                    listbox.ItemsSource = infoHoraItinerario;
                    listbox.DisplayMemberPath = "DetalleCompleto";
                    listbox.SelectedValuePath = "IdItinerario";
                    listbox.Margin.Left.Equals(10);
                    stackpanel.Children.Add(listbox);
                    stackPanelPadre.Children.Add(expander);
                }
            }

            this.borderDepartamentos.Child = stackPanelPadre;
        }

        private void ClickItemListView(object sender, RoutedEventArgs e)
        {
            try
            {
                String objeto;
                objeto = ((ListBox)sender).SelectedValue.ToString();
                cargarDatosBus(Int64.Parse(objeto));
                cargarInformacionItinerario(Int64.Parse(objeto));
                cargarInformacionViaje();
                Utilitarios.Utilitarios.idBus = info[0].IdBus;
                Utilitarios.Utilitarios.idItinerario = Int64.Parse(objeto);
                cargarPasajeros(Int64.Parse(objeto));
            }
            catch (Exception)
            {
                return;
            }
        }

        System.Windows.Forms.PictureBox[,] p;
        List<V_ITINERARIO_BUS_ASIENTO> info;

        private void cargarInformacionItinerario(Int64 IdItinerario)
        {
            agregados = new List<String>();
            RNItinerario negocioItinerario = new RNItinerario();
            info = negocioItinerario.traerItinerarioAsiento(IdItinerario);
            p = new System.Windows.Forms.PictureBox[16, info[0].NroColumnas.Value];
            crearDiseno();
            this.dgModeloBus.RowCount = 16;
            this.dgModeloBus.ColumnCount = info[0].NroColumnas.Value;
            int n = 0;
            int av = 0;
            for (int i = 0; i < 16; i++)
            {
                this.dgModeloBus.Rows[i].Height = 45;
                for (int j = 0; j < info[0].NroColumnas; j++)
                {
                    p[i, j] = new System.Windows.Forms.PictureBox();
                    p[i, j].Load(Utilitarios.Utilitarios.direccionEnumerada + info[n].Asiento);
                    this.dgModeloBus.Columns[j].Width = 45;
                    System.Drawing.Bitmap image = new System.Drawing.Bitmap(Utilitarios.Utilitarios.direccionEnumerada + info[n].Asiento);

                    if (negocioItinerario.verificarVentaPasaje(info[n].IdAsiento,info[n].IdItinerario))
                    {
                        av++;
                        p[i, j].Load(Utilitarios.Utilitarios.direccionEnumerada + "av" + info[n].NroAsiento + ".png");
                        image = new System.Drawing.Bitmap(Utilitarios.Utilitarios.direccionEnumerada + "av" + info[n].NroAsiento + ".png");
                    }
                    
                    
                    this.dgModeloBus.Rows[i].Cells[j].Value = image;
                    this.dgModeloBus.Rows[i].Cells[j].Selected = false;
                    n++;
                }
            }
            this.lblNroAsientosVacantes.Content = (Int32.Parse(this.lblNroAsientos.Content.ToString()) - av).ToString();
            this.lblNroAsientosVendidos.Content = av.ToString();
            contexMenu();
        }

        private void crearDiseno()
        {
            this.dgModeloBus.Rows.Clear();
            switch (info[0].NroColumnas)
            {

                case 4:
                    {
                        System.Drawing.Size s = new System.Drawing.Size(180, 720);
                        this.dgModeloBus.Size = s;
                        CrearDataGridView(4);
                        break;
                    }
                case 5:
                    {
                        System.Drawing.Size s = new System.Drawing.Size(225, 720);
                        this.dgModeloBus.Size = s;
                        CrearDataGridView(5);
                        break;
                    }
            }
            CrearDataGridView(info[0].NroColumnas.Value);
        }

        private void CrearDataGridView(int n)
        {
            for (int i = 0; i < n; i++)
            {
                System.Windows.Forms.DataGridViewImageColumn ima = new System.Windows.Forms.DataGridViewImageColumn();
                this.dgModeloBus.Columns.Add(ima);
            }
        }

        private void cargarDatosBus(Int64 IdItinerario)
        {
            RNItinerario negocioItinerario = new RNItinerario();
            this.lblNroAsientos.Content = negocioItinerario.traerItinerarioAsientoTotal(IdItinerario).Count;
        }

        private void calFecha_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            generarControles();
        }

        private String nombreImagen(String ruta)
        {
            String cad = String.Empty;
            for (int i = ruta.Length - 1; i > -1; i--)
            {
                if (!ruta[i].ToString().Equals(@"\"))
                    cad = ruta[i].ToString() + cad;
                else
                    break;
            }
            return cad;
        }

        private void contexMenu()
        {
            System.Windows.Forms.ContextMenuStrip contextMenu = new System.Windows.Forms.ContextMenuStrip();
            contextMenu.Items.Add("Vender",null,menuVender_Click);
            contextMenu.Items.Add("Reservar",null,menuReservar_Click);
            this.dgModeloBus.ContextMenuStrip = contextMenu;

        }

        private void menuReservar_Click(object sender, EventArgs e)
        {
            if (agregados.Count > 0)
            {
                Utilitarios.Utilitarios.pasajesAgregados = agregados;
                UIFrmReserva formReserva = new UIFrmReserva();
                if (formReserva.ShowDialog().Value)
                {

                }
            }
            else
                MessageBox.Show("Seleccionar un asiento");
        }

        private void menuVender_Click(object sender, EventArgs e)
        {
            if (agregados.Count > 0)
            {
                Utilitarios.Utilitarios.pasajesAgregados = agregados;
                UIFrmVentaPasaje formVentas = new UIFrmVentaPasaje();
                if (formVentas.ShowDialog().Value)
                {
                    cargarInformacionItinerario(Utilitarios.Utilitarios.idItinerario);
                    cargarInformacionViaje();
                    cargarDatosBus(Utilitarios.Utilitarios.idItinerario);
                    cargarPasajeros(Utilitarios.Utilitarios.idItinerario);
                }
            }
            else
                MessageBox.Show("Seleccionar un asiento");
        }

        List<String> agregados;
        private void dgModeloBus_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            String nImagen = nombreImagen(p[e.RowIndex, e.ColumnIndex].ImageLocation);
            if (nImagen.Substring(0, 2).Equals("al"))
            {
                if (buscarElemento(nImagen))
                {
                    agregados.Add(nImagen);
                    System.Windows.Forms.DataGridViewCellStyle styleCell = new System.Windows.Forms.DataGridViewCellStyle();
                    styleCell.SelectionBackColor = System.Drawing.Color.Black;
                    this.dgModeloBus.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = styleCell;
                    this.dgModeloBus.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.Black;
                }
                else
                {
                    System.Windows.Forms.DataGridViewCellStyle styleCell = new System.Windows.Forms.DataGridViewCellStyle();
                    styleCell.SelectionBackColor = System.Drawing.Color.White;
                    this.dgModeloBus.Rows[e.RowIndex].Cells[e.ColumnIndex].Style = styleCell;
                    this.dgModeloBus.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = System.Drawing.Color.White;
                    agregados.Remove(nImagen);
                }
            }
            else
            {
                this.dgModeloBus.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = false;
            }          
        }

        private Boolean buscarElemento(String nombreI)
        {
            foreach (var item in agregados)
            {
                if (item.Equals(nombreI))
                    return false;
            }
            return true;
        }
    }
}
