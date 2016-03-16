using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNItinerario:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNItinerario()
        {
            esquema = this.traerContexto();
        }

        public Int64 generarId()
        {
            try
            {
                return (from f in esquema.ITINERARIO select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<V_ASIENTO_CLIENTE> traerAsientoCliente(Int64 IdItinerario)
        {
            try
            {
                return (from f in esquema.V_ASIENTO_CLIENTE where f.Id == IdItinerario orderby f.NroAsiento ascending select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Boolean registrar(ITINERARIO itinerario)
        {
            try
            {
                itinerario.Id = generarId();
                esquema.ITINERARIO.Add(itinerario);
                return esquema.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean verificarVentaPasaje(Int64 IdAsiento, Int64 IdItinerario)
        {
            try
            {
                return (from i in esquema.ITINERARIO
                        join d in esquema.DETALLE_SOLICITUD_ASIENTO on i.Id equals d.IdItinerario
                        where i.Id ==IdItinerario && d.IdAsiento == IdAsiento
                        select new
                        {
                            i.Id
                        }).ToList().Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<V_INFORMACION_ITINERARIOS> traerInformacionItinerario(Int64 IdDepartamento, DateTime Fecha)
        {
            try
            {
                return (from f in esquema.V_INFORMACION_ITINERARIOS where f.IdDepartamento == IdDepartamento && f.Fecha == Fecha select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_INFORMACION_ITINERARIOS> traerInformacionItinerario(Int64 IdDepartamento, DateTime Fecha,Int64 IdHora)
        {
            try
            {
                return (from f in esquema.V_INFORMACION_ITINERARIOS where f.IdHorario == IdHora && f.Fecha == Fecha && f.IdDepartamento == IdDepartamento select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_INFORMACION_HORARIO> traerInformacionHorario(Int64 IdDepartamento,Int64 IdHorario)
        {
            try
            {
                return (from f in esquema.V_INFORMACION_HORARIO where f.IdDepartamento.Equals(IdDepartamento) && f.IdHorario.Equals(IdHorario) select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_INFORMACION_HORARIO> traerInformacionHorario(Int64 IdDepartamento)
        {
            try
            {
                return (from f in esquema.V_INFORMACION_HORARIO where f.IdDepartamento.Equals(IdDepartamento) orderby f.Hora ascending select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<ITINERARIO> traerItinerario(Int64 IdItinerario)
        {
            try
            {
                return (from f in esquema.ITINERARIO where f.Id == IdItinerario select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_ITINERARIO_BUS_ASIENTO> traerItinerarioAsiento(Int64 IdItinerario)
        {
            try
            {
                return (from f in esquema.V_ITINERARIO_BUS_ASIENTO where f.IdItinerario.Equals(IdItinerario) select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_ITINERARIO_BUS_ASIENTO> traerItinerarioAsientoTotal(Int64 IdItinerario)
        {
            try
            {
                return (from f in esquema.V_ITINERARIO_BUS_ASIENTO where f.IdItinerario.Equals(IdItinerario) && f.Asiento.Contains("a") select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
