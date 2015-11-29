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

using System.Windows.Forms;
using System.Drawing;
using Proyecto.Ventas.Pasaje.Datos;
using Proyecto.Ventas.Pasaje.RN;
using Proyecto.Ventas.Pasaje.Utilitarios;

namespace Proyecto.Ventas.Pasaje.WPF
{
    /// <summary>
    /// Lógica de interacción para UIFrmCrearModeloBus.xaml
    /// </summary>
    public partial class UIFrmCrearModeloBus : Page
    {
        public UIFrmCrearModeloBus()
        {
            InitializeComponent();
            inicializar();
        }

        List<V_BUSTIPO> info;
        PictureBox[,] p;

        private void inicializar()
        {
            cargarComboBus();
        }

        private void CrearDiseno()
        {
            this.dgModelo.Rows.Clear();
            Int32? n;
            switch (info[this.cbBus.SelectedIndex].NroColumnas)
            {

                case 4:
                    {
                        System.Drawing.Size s = new System.Drawing.Size(160, 640);
                        this.dgModelo.Size = s;
                        CrearDataGridView(4);
                        break;
                    }
                case 5:
                    {
                        System.Drawing.Size s = new System.Drawing.Size(200, 640);
                        this.dgModelo.Size = s;
                        CrearDataGridView(5);
                        break;
                    }
            }
            n = info[this.cbBus.SelectedIndex].NroColumnas;
            p = new PictureBox[16, (int)n];
            this.dgModelo.RowCount = 16;
            this.dgModelo.ColumnCount = (int)n;
            for (int i = 0; i < 16; i++)
            {
                this.dgModelo.Rows[i].Height = 40;
                for (int j = 0; j < n; j++)
                {
                    p[i, j] = new PictureBox();
                    p[i, j].Load(Utilitarios.Utilitarios.direccion + "v.gif");
                    this.dgModelo.Columns[j].Width = 40;
                    this.dgModelo.Rows[i].Cells[j].Value = System.Drawing.Image.FromFile(Utilitarios.Utilitarios.direccion + "v.gif");
                }
            }
        }

        private void CrearDataGridView(int n)
        {
            for (int i = 0; i < n; i++)
            {
                DataGridViewImageColumn ima = new DataGridViewImageColumn();
                this.dgModelo.Columns.Add(ima);
            }
        }

        private void cargarComboBus()
        {
            info = new List<V_BUSTIPO>();
            RNBus negocioBus = new RNBus();
            List<V_BUSTIPO> collection = negocioBus.traerBus_Tipo();

            foreach (var item in collection)
            {
                if (!negocioBus.verificarModeloCreacion(item.Id))
                    info.Add(item); 
            }
                        
            this.cbBus.ItemsSource = info;
            this.cbBus.DisplayMemberPath = "Interno";
            this.cbBus.SelectedValuePath = "Id";
        }

        private void cbBus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lblTipoBus.Content = info[this.cbBus.SelectedIndex].Nombre;
            CrearDiseno();
        }

        private void dgModelo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnGuardar.IsEnabled = false;
            if (this.rbtnAsiento.IsChecked.Value)
            {
                p[e.RowIndex, e.ColumnIndex].Load(Utilitarios.Utilitarios.direccion + "a.png");
                this.dgModelo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = System.Drawing.Image.FromFile(Utilitarios.Utilitarios.direccion + "a.png");
            }
            if (this.rbtnEntrada.IsChecked.Value)
            {
                p[e.RowIndex, e.ColumnIndex].Load(Utilitarios.Utilitarios.direccion + "e.png");
                this.dgModelo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = System.Drawing.Image.FromFile(Utilitarios.Utilitarios.direccion + "e.png");
            }
            if (this.rbtnTelevisor.IsChecked.Value)
            {
                p[e.RowIndex, e.ColumnIndex].Load(Utilitarios.Utilitarios.direccion + "t.png");
                this.dgModelo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = System.Drawing.Image.FromFile(Utilitarios.Utilitarios.direccion + "t.png");
            }
        }

        private void dgModelo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.btnGuardar.IsEnabled = false;
            p[e.RowIndex, e.ColumnIndex].Load(Utilitarios.Utilitarios.direccion + "v.gif");
            this.dgModelo.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = System.Drawing.Image.FromFile(Utilitarios.Utilitarios.direccion + "v.gif");
        }

        private void btnEnumerar_Click(object sender, RoutedEventArgs e)
        {
            int cont = 0;
            for (int i = 0; i < this.dgModelo.Rows.Count; i++)
            {
                for (int j = 0; j < this.dgModelo.Columns.Count; j++)
                {
                    if (p[i, j].ImageLocation != Utilitarios.Utilitarios.direccion + "t.png" && p[i, j].ImageLocation != Utilitarios.Utilitarios.direccion + "e.png" && p[i, j].ImageLocation != Utilitarios.Utilitarios.direccion + "v.gif")
                    {
                        cont++;
                        p[i, j].Load(Utilitarios.Utilitarios.direccionEnumerada + "al" + cont + ".png");
                        this.dgModelo.Rows[i].Cells[j].Value = System.Drawing.Image.FromFile(Utilitarios.Utilitarios.direccionEnumerada + "al" + cont + ".png");
                        btnGuardar.IsEnabled = true;
                    }
                }
            }
        }

        private String numeroAsiento(String cad)
        {
            string cad1 = "";
            if (cad != "t.png" && cad != "e.png" && cad != "v.gif")
            {
                for (int i = 2; i < cad.Length; i++)
                {
                    if (cad[i].ToString() != ".")
                        cad1 += cad[i].ToString();
                    else
                        break;
                }
                return cad1;
            }
            else
                return "0";
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (registrarAsiento())
                System.Windows.MessageBox.Show("Modelo creado correctamento");
            else
                System.Windows.MessageBox.Show("Error en la creacion del modelo");
        }

        private Boolean registrarAsiento()
        {
            RNAsiento negocioAsiento = new RNAsiento();
            for (int i = 0; i < this.dgModelo.Rows.Count; i++)
            {
                for (int j = 0; j < this.dgModelo.Columns.Count; j++)
                {
                    ASIENTO asiento = new ASIENTO();
                    asiento.IdBus = Int64.Parse(this.cbBus.SelectedValue.ToString());
                    asiento.NroAsiento = Int32.Parse(numeroAsiento(System.IO.Path.GetFileName(p[i, j].ImageLocation)));
                    asiento.Asiento1 = System.IO.Path.GetFileName(p[i, j].ImageLocation);
                    if (!negocioAsiento.registrar(asiento))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
