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

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmReporteVentaEmpleado.xaml
    /// </summary>
    public partial class UIFrmReporteVentaEmpleado : Page
    {
        public UIFrmReporteVentaEmpleado()
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
            DateTime desde = this.dpDesde.SelectedDate.Value;
            DateTime hasta = this.dpHasta.SelectedDate.Value;
            RNSolicitudPasaje negocioSolicitud = new RNSolicitudPasaje();
            this.reportEmpleado.Reset();
            ReportDataSource source = new ReportDataSource();
            source.Name = "DataSet1";
            source.Value = negocioSolicitud.traerVentaEmpleado(desde, hasta);
            this.reportEmpleado.LocalReport.DataSources.Add(source);
            this.reportEmpleado.LocalReport.ReportPath = Utilitarios.Utilitarios.ruta + "ReportVentaEmpleado.rdl";
            this.reportEmpleado.RefreshReport();
        }
    }
}
