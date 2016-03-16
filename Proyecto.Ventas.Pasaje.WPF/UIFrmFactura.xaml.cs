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
using System.IO;
using Gma.QrCodeNet.Encoding;
using Microsoft.Reporting.WinForms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmFactura.xaml
    /// </summary>
    public partial class UIFrmFactura : MetroWindow
    {
        public UIFrmFactura()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            cargarDatosVenta();
            cargarInformacion();
        }

        private void cargarDatosVenta()
        {
            this.txbNitCi.Text = Utilitarios.Utilitarios.informacionAuxiliar[0].CI.ToString();
            this.txbNombre.Text = Utilitarios.Utilitarios.informacionAuxiliar[0].Cliente;
            this.txbMontoTotal.Text = (Utilitarios.Utilitarios.informacionAuxiliar[0].Precio * Utilitarios.Utilitarios.informacionAuxiliar.Count).ToString();
        }

        private void crearColumnas()
        {
            this.dgPasaje.Columns.Clear();
            this.dgPasaje.AutoGenerateColumns = false;
            this.dgPasaje.IsReadOnly = false;

            DataGridTextColumn dt = new DataGridTextColumn();
            dt.Visibility = Visibility.Visible;
            dt.Header = "IdCliente";
            dt.Binding = new Binding("IdCliente");
            dt.IsReadOnly = true;

            this.dgPasaje.Columns.Add(dt);

            dt = new DataGridTextColumn();
            dt.Visibility = Visibility.Visible;
            dt.Header = "Cliente";
            dt.Binding = new Binding("Cliente");
            dt.IsReadOnly = true;

            this.dgPasaje.Columns.Add(dt);

            dt = new DataGridTextColumn();
            dt.Visibility = Visibility.Visible;
            dt.Header = "NroAsiento";
            dt.Binding = new Binding("NroAsiento");
            dt.IsReadOnly = true;

            this.dgPasaje.Columns.Add(dt);

        }
        
        private void cargarInformacion()
        {
            crearColumnas();
            dgPasaje.ItemsSource = Utilitarios.Utilitarios.informacionAuxiliar;
                       
        }
        
        private Boolean cargarInformacionClienteNaturalNIT(CLIENTE cliente)
        {
            RNCliente negocioCliente = new RNCliente();
            var infoNIT = negocioCliente.verificarInformacionNATURALNIT(Int32.Parse(this.txbNitCi.Text));
            if (infoNIT.Count > 0)
            {
                cliente.Id = infoNIT[0].Id;
                cliente.NIT = Int32.Parse(this.txbNitCi.Text);
                return true;
            }
            return false;
        }

        private Boolean cargarInformacionClienteJuridicoNIT(CLIENTE cliente)
        {
            RNCliente negocioCliente = new RNCliente();
            var infoNIT = negocioCliente.verificarInformacionJURIDICONIT(Int32.Parse(this.txbNitCi.Text));
            if (infoNIT.Count > 0)
            {
                cliente.Id = infoNIT[0].Id;
                cliente.NIT = Int32.Parse(this.txbNitCi.Text);
                return true;
            }
            return false;

        }

        private void cargarInformacionSolicitud(SOLICITUD_PASAJE solicitudPasaje,CLIENTE cliente)
        {
            Int64 idCliente = cliente.Id;
            solicitudPasaje.IdCliente = idCliente;
            
            solicitudPasaje.Fecha =DateTime.Parse(DateTime.Now.Date.ToShortDateString());
            solicitudPasaje.MontoTotal = Decimal.Parse(this.txbMontoTotal.Text);
        }

        private void cargarInformacionDetalleSolicitudAsiento(List<DETALLE_SOLICITUD_ASIENTO> listDetalleSolicitudAsiento,SOLICITUD_PASAJE solicitud)
        {
            foreach (var item in Utilitarios.Utilitarios.informacionAuxiliar)
            {
                DETALLE_SOLICITUD_ASIENTO detalle = new DETALLE_SOLICITUD_ASIENTO();
                detalle.IdSolicitudPasaje = solicitud.Id;
                detalle.IdAsiento = item.IdAsiento;
                detalle.IdCliente = item.IdCliente;
                detalle.IdItinerario = Utilitarios.Utilitarios.idItinerario;
                listDetalleSolicitudAsiento.Add(detalle);
            }
        }

        private void cargarJuridico(JURIDICO juridico)
        {
            juridico.RazonSocial = this.txbNombre.Text;
        }

        private void cargarCliente(CLIENTE cliente)
        {
            Int32 nit;
            if (Int32.TryParse(this.txbNitCi.Text, out nit))
                cliente.NIT = nit;
            else
                cliente.NIT = 0;
        }

        private void cargarNatural(NATURAL natural)
        {
            natural.NombreCompleto = this.txbNombre.Text;
        }

        private void cargarFactura(FACTURA factura,String NIT)
        {
            RNDosificacion negocioDosificacion = new RNDosificacion();
            var info = negocioDosificacion.traerDosificacion();
            if (info.Count > 0)
            {
                factura.IdDosificacion = info[0].Id;
                factura.NroFactura = info[0].NroIncial;
                RNCodigoControl negocioCodigoControl = new RNCodigoControl();
                Double montoTotal = Double.Parse(this.txbMontoTotal.Text);
                String nroAutorizacion = info[0].NroTramite.ToString();
                String nroFactura = factura.NroFactura.ToString();
                String Nit = NIT;
                String llave = info[0].Llave;
                factura.CodigoControl = negocioCodigoControl.DevolverCodigoControl(montoTotal, nroAutorizacion, nroFactura, Nit, llave);
                factura.QR = imagen(GetQRBitmap(this.qrControl.Text));
                factura.MontoTotal = Decimal.Parse(montoTotal.ToString());
                negocioDosificacion.actualizarDosificacion(info[0]);
            }
        }

        private Bitmap GetQRBitmap(string content)
        {
            var qrEncoder = new QrEncoder();
            var code = qrEncoder.Encode(content);
            var bitmapImage = new Bitmap(code.Matrix.Width, code.Matrix.Height);

            for (int j = 0; j < code.Matrix.Height; j++)
                for (int i = 0; i < code.Matrix.Width; i++)
                    bitmapImage.SetPixel(i, j, code.Matrix[i, j] ? System.Drawing.Color.Black : System.Drawing.Color.White);
            qrControl.Visibility = Visibility.Hidden;
            return bitmapImage;
        }

        public Byte[] imagen(Bitmap bitmap)
        {
            MemoryStream memoriaImagen = new MemoryStream();
            System.Windows.Forms.PictureBox picture = new System.Windows.Forms.PictureBox();
            picture.Image = bitmap;
            picture.Image.Save(memoriaImagen, ImageFormat.Jpeg);
            Byte[] byteFoto = new Byte[memoriaImagen.Length];
            memoriaImagen.Position = 0;
            memoriaImagen.Read(byteFoto, 0, Convert.ToInt32(memoriaImagen.Length));
            return byteFoto;
        }

        private Boolean registrarDetalleSolicitudCliente(List<DETALLE_SOLICITUD_ASIENTO> detalleSolicitudAsiento)
        {
            RNDetalleSolicitudAsiento negocioDetalleSolicitudAsiento = new RNDetalleSolicitudAsiento();
            foreach (var item in detalleSolicitudAsiento)
            {
                if (!negocioDetalleSolicitudAsiento.registrar(item))
                    return false;
            }
            return true;
        }

        private void registrarVenta(CLIENTE cliente)
        {
            this.qrControl.Text = "MONTO TOTAL : " + this.txbMontoTotal.Text + " FECHA : " + DateTime.Now.Date.ToShortDateString() + " CLIENTE : " + this.txbNombre.Text;

            SOLICITUD_PASAJE solicitudPasaje = new SOLICITUD_PASAJE();
            RNSolicitudPasaje negocioSolicitudPasaje = new RNSolicitudPasaje();
            solicitudPasaje.Id = negocioSolicitudPasaje.generarId();
            solicitudPasaje.IdEmpleado = Utilitarios.Utilitarios.idEmpleado;
            cargarInformacionSolicitud(solicitudPasaje, cliente);

            List<DETALLE_SOLICITUD_ASIENTO> detalleSolicitudAsiento = new List<DETALLE_SOLICITUD_ASIENTO>();
            cargarInformacionDetalleSolicitudAsiento(detalleSolicitudAsiento, solicitudPasaje);

            RNFactura negocioFactura = new RNFactura();
            FACTURA factura = new FACTURA();
            factura.Id = negocioFactura.generarId();
            Utilitarios.Utilitarios.IdFactura = factura.Id;
            factura.IdSolicitudPasaje = solicitudPasaje.Id;
            cargarFactura(factura, cliente.NIT.ToString());

            RNControlVentaPasaje negocioControlVenta = new RNControlVentaPasaje();

            if (negocioControlVenta.registroVentaFactura(solicitudPasaje, factura, detalleSolicitudAsiento))
            {
                MessageBox.Show("Datos Registrados Correctamento");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void btnFacturar_Click(object sender, RoutedEventArgs e)
        {
            if (this.rbtnNatural.IsChecked.Value)
            {
                CLIENTE cliente = new CLIENTE();
                NATURAL natural = new NATURAL();

                if (cargarInformacionClienteNaturalNIT(cliente))
                {
                    natural.Id = cliente.Id;
                    natural.NombreCompleto = this.txbNombre.Text;
                }
                else
                {
                    RNCliente negocioCliente = new RNCliente();
                    RNControlCliente negocioControlCliente = new RNControlCliente();
                    cliente.Id = negocioCliente.generarId();
                    natural.Id = cliente.Id;
                    cargarCliente(cliente);
                    cargarNatural(natural);
                    negocioControlCliente.registrarClienteNatural(cliente, natural);
                    
                }
                registrarVenta(cliente);
            }
            else
            {
                CLIENTE cliente = new CLIENTE();
                JURIDICO juridico = new JURIDICO();
                if (cargarInformacionClienteJuridicoNIT(cliente))
                {
                    juridico.Id = cliente.Id;
                    juridico.RazonSocial = this.txbNombre.Text;
                }
                else
                {
                    RNCliente negocioCliente = new RNCliente();
                    RNControlCliente negocioControlCliente = new RNControlCliente();
                    cliente.Id = negocioCliente.generarId();
                    juridico.Id = cliente.Id;
                    cargarCliente(cliente);
                    cargarJuridico(juridico);
                    negocioControlCliente.registrarClienteJuridico(cliente, juridico);
                }
                registrarVenta(cliente);
            }
            
            DialogResult = true;
            ejecutar();
            this.Close();
        }



        private int m_currentPageIndex;
        private IList<Stream> m_streams;

        private List<V_FACTURA> CargarInformacion()
        {
            RNFactura negocioFactura = new RNFactura();
            return negocioFactura.traerFactura(Utilitarios.Utilitarios.IdFactura);
        }

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Exportar(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>
            </DeviceInfo>";
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }

        private void imprimirPagina(object sender, PrintPageEventArgs ev)
        {

            System.Drawing.Imaging.Metafile pageImage = new System.Drawing.Imaging.Metafile(m_streams[m_currentPageIndex]);

            System.Drawing.Rectangle adjustedRect = new System.Drawing.Rectangle(
                ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
                ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
                ev.PageBounds.Width,
                ev.PageBounds.Height);

            ev.Graphics.FillRectangle(System.Drawing.Brushes.White, adjustedRect);

            ev.Graphics.DrawImage(pageImage, adjustedRect);

            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        private void Imprimir()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: en la impresion");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: al buscar impresora");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(imprimirPagina);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        // Create a local report for Report.rdlc, load the data,
        //    export the report to an .emf file, and print it.
        private void ejecutar()
        {
            LocalReport report = new LocalReport();
            report.ReportPath = Utilitarios.Utilitarios.ruta + "ReportFactura.rdl";
            report.DataSources.Add(new ReportDataSource("DataSet1", CargarInformacion()));
            Exportar(report);
            Imprimir();
        }

        public void Dispose()
        {
            if (m_streams != null)
            {
                foreach (Stream stream in m_streams)
                    stream.Close();
                m_streams = null;
            }
        }
    }
}
