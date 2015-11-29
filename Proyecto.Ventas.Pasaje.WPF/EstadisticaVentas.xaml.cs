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

using Microsoft.Reporting.WinForms;

using Proyecto.Ventas.Pasaje.Datos;
using Proyecto.Ventas.Pasaje.RN;
using Proyecto.Ventas.Pasaje.Utilitarios;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para EstadisticaVentas.xaml
    /// </summary>
    public partial class EstadisticaVentas : Page
    {
        public EstadisticaVentas()
        {
            InitializeComponent();
            cargarFecha();
        }

        private void cargarFecha()
        {
            this.dpDesde.SelectedDate = DateTime.Now.Date;
            this.dpHasta.SelectedDate = DateTime.Now.Date;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            cargarReporte();
        }

        private void cargarReporte()
        {
            this.reportEstadistica.Reset();
            RNSolicitudPasaje negocioSolicitudPasaje = new RNSolicitudPasaje();
            ReportDataSource source = new ReportDataSource();
            source.Name = "DataSet1";
            DateTime desde = this.dpDesde.SelectedDate.Value;
            DateTime hasta = this.dpHasta.SelectedDate.Value;
            source.Value = negocioSolicitudPasaje.traerDetalleViaje(desde,hasta);
            this.reportEstadistica.LocalReport.DataSources.Add(source);
            this.reportEstadistica.LocalReport.ReportPath = Utilitarios.Utilitarios.ruta + "Report1.rdl";
            this.reportEstadistica.RefreshReport();
        }
    }
}
