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
    /// Lógica de interacción para UIFrmHora.xaml
    /// </summary>
    public partial class UIFrmHora : MetroWindow
    {
        public UIFrmHora()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            cargarComboHora();
            cargarComboMinuto();
        }

        private void cargarComboHora()
        {
            List<String> listaHora = new List<String>();
            for (int i = 0; i < 24; i++)
            {
                if (i < 10)
                    listaHora.Add("0" + i.ToString());
                else
                    listaHora.Add(i.ToString());
            }
            this.cbHora.ItemsSource = listaHora;
        }

        private void cargarComboMinuto()
        {
            List<String> listaMinuto = new List<String>();
            for (int i = 0; i < 60; i+=5)
            {
                if (i < 10)
                    listaMinuto.Add("0" + i.ToString());
                else
                    listaMinuto.Add(i.ToString());
            }
            this.cbMinuto.ItemsSource = listaMinuto;
        }

        private void cargarHora(HORARIO horario)
        {
            Int32 hora =Int32.Parse(this.cbHora.SelectedItem.ToString());
            Int32 minuto = Int32.Parse(this.cbMinuto.SelectedItem.ToString());
            TimeSpan ts = new TimeSpan(hora, minuto, 0);
            horario.Hora = ts;
        }

        private void cargarDetalleDepartamentoHorario(DETALLE_DEPARTAMENTO_HORARIO detalleDepartamentoHorario)
        {
            detalleDepartamentoHorario.IdDepartamento = Utilitarios.Utilitarios.idDepartamento;
        }

        private Boolean registrarDetalleDepartamentoHorario(DETALLE_DEPARTAMENTO_HORARIO detalleDepartamentoHorario)
        {
            RNDetalleDepartamentoHorario negocioDetalleDepartamentoHorario = new RNDetalleDepartamentoHorario();
            return negocioDetalleDepartamentoHorario.registrar(detalleDepartamentoHorario);
        }

        private void btnAgregarHora_Click(object sender, RoutedEventArgs e)
        {
            RNHorario negocioHorario = new RNHorario();

            HORARIO horario = new HORARIO();
            DETALLE_DEPARTAMENTO_HORARIO detalleDepartamentoHorario = new DETALLE_DEPARTAMENTO_HORARIO();
            cargarHora(horario);
            var infoHorario = negocioHorario.traerHorario(horario.Hora);
            if (infoHorario.Count > 0)
            {
                RNItinerario negocioItinerario = new RNItinerario();
                if (negocioItinerario.traerInformacionHorario(Utilitarios.Utilitarios.idDepartamento, infoHorario[0].Id).Count > 0)
                    MessageBox.Show("La hora ya existe");
                else 
                {
                    cargarDetalleDepartamentoHorario(detalleDepartamentoHorario);
                    detalleDepartamentoHorario.IdHorario = infoHorario[0].Id;
                    if (registrarDetalleDepartamentoHorario(detalleDepartamentoHorario))
                    {
                        this.DialogResult = true;
                        this.Close();
                    }
                    else
                        MessageBox.Show("Error en el registro");
                }
            }
            else
            {
                horario.Id = negocioHorario.generarId();
                detalleDepartamentoHorario.IdHorario = horario.Id;
                cargarDetalleDepartamentoHorario(detalleDepartamentoHorario);
                if (negocioHorario.registrar(horario) && registrarDetalleDepartamentoHorario(detalleDepartamentoHorario))
                {
                    this.DialogResult = true;
                    this.Close();
                }
                else
                    MessageBox.Show("Error en el registro");
            }

        }

    }
}
