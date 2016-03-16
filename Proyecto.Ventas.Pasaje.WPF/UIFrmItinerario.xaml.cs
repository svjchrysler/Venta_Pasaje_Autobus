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
using System.Data;


namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmItinerario.xaml
    /// </summary>
    public partial class UIFrmItinerario : Page
    {
        public UIFrmItinerario()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            cargarFecha();
            cargarPrecio();
            cargarComboDepartamento();
        }

        private void cargarPrecio()
        {
            this.lblPrecioMaximo.Content = String.Empty;
            this.lblPrecioMaximo.Content += "PRECIO MAXIMO : " + Utilitarios.Utilitarios.precioMaximo.ToString() + "Bs";

            this.lblPrecioMinimo.Content = String.Empty;
            this.lblPrecioMinimo.Content += "PRECIO MINIMO : " + Utilitarios.Utilitarios.precioMinimo.ToString() + "Bs";
        }

        private void cargarComboDepartamento()
        {
            RNDepartamento negocioDepartamento = new RNDepartamento();
            this.cbDtestino.ItemsSource = negocioDepartamento.traerDepartamento(0);
            this.cbDtestino.DisplayMemberPath = "Nombre";
            this.cbDtestino.SelectedValuePath = "Id";
            this.cbDtestino.SelectedIndex = 0;
        }

        private void cargarFecha()
        {
            this.dtpItinerario.SelectedDate = DateTime.Now.Date;
        }

        Boolean sw = false;

        
        private void crearColumnas()
        {
            this.dgItinerario.Columns.Clear();
            this.dgItinerario.AutoGenerateColumns = false;
            this.dgItinerario.IsReadOnly = false;

            DataGridTextColumn dt = new DataGridTextColumn();
            dt.Header = " HORA ";
            dt.Binding = new Binding("Hora");
            dt.Width = 100;
            dt.IsReadOnly = true;

            this.dgItinerario.Columns.Add(dt);

            dt = new DataGridTextColumn();
            dt.Header = " DETALLE ";
            dt.Binding = new Binding("Detalle");
            dt.Width = 500;
            dt.IsReadOnly = true;

            this.dgItinerario.Columns.Add(dt);
        }

        private void cargarInformacionItinerario(Int64 IdDepartamento)
        {
            try
            {
                crearColumnas();
                RNItinerario negocioItinerario = new RNItinerario();
                List<V_INFORMACION_HORARIO> infoHorario = negocioItinerario.traerInformacionHorario(IdDepartamento);
                List<V_INFORMACION_ITINERARIOS> infoItinerario = new List<V_INFORMACION_ITINERARIOS>();
                DateTime dtp = DateTime.Parse(this.dtpItinerario.SelectedDate.ToString());
                foreach (var item in infoHorario)
                {
                    var info = negocioItinerario.traerInformacionItinerario(IdDepartamento, dtp,item.IdHorario);
                    if (info.Count > 0)
                    {
                        for (int i = 0; i < info.Count; i++)
                        {
                            infoItinerario.Add(info[i]);    
                        }
                        
                    }
                    else
                    {
                        V_INFORMACION_ITINERARIOS vinfo = new V_INFORMACION_ITINERARIOS();
                        vinfo.Hora = item.Hora;
                        vinfo.Detalle = "";
                        infoItinerario.Add(vinfo);
                    }
                }

                this.dgItinerario.ItemsSource = infoItinerario;
                sw = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void cbDtestino_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilitarios.Utilitarios.idDepartamento = Int64.Parse(this.cbDtestino.SelectedValue.ToString());
            cargarInformacionItinerario(Utilitarios.Utilitarios.idDepartamento);
        }

        private void btnAgregarHora_Click(object sender, RoutedEventArgs e)
        {
            Utilitarios.Utilitarios.idDepartamento = Int64.Parse(this.cbDtestino.SelectedValue.ToString());
            UIFrmHora formHora = new UIFrmHora();
            aplicarEfecto();
            if (formHora.ShowDialog().Value)
                cargarInformacionItinerario(Int64.Parse(this.cbDtestino.SelectedValue.ToString()));
            revertirEfecto();
        }

        private void revertirEfecto()
        {
            this.Effect = null;
        }

        private void aplicarEfecto()
        {
            var objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = 5;
            this.Effect = objBlur;
        }

        private void btnAgregarBus_Click(object sender, RoutedEventArgs e)
        {
            Utilitarios.Utilitarios.idDepartamento = Int64.Parse(this.cbDtestino.SelectedValue.ToString());
            UIFrmItinerarioBus formItinerarioBus = new UIFrmItinerarioBus();
            aplicarEfecto();
            if (formItinerarioBus.ShowDialog().Value)
                cargarInformacionItinerario(Utilitarios.Utilitarios.idDepartamento);
            revertirEfecto();
        }

        private void btnPrecio_Click(object sender, RoutedEventArgs e)
        {
            UIFrmPrecio formPrecio = new UIFrmPrecio();
            aplicarEfecto();
            if (formPrecio.ShowDialog().Value)
            {
                UIFrmPrincipal formPrincipal = new UIFrmPrincipal();
                formPrincipal.cargarInformacion();
                formPrincipal.Close();
                cargarPrecio();
            }
            revertirEfecto();
        }
        
        private void dtpItinerario_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sw)
                cargarInformacionItinerario(Utilitarios.Utilitarios.idDepartamento);
        }
        
    }
}
