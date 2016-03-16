using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using Proyecto.Ventas.Pasaje.Datos;

namespace Proyecto.Ventas.Pasaje.RN
{
    public class RNBus:Contexto
    {
        VENTAS_PASAJE_ENTITIES esquema;
        public RNBus()
        {
            try
            {
                esquema = this.traerContexto();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private Int64 generarId()
        {
            try
            {
                return (from f in esquema.BUS select f.Id).Max() + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public String registrar(BUS bus)
        {
            try
            {
                bus.Id = generarId();
                bus.Estado = 1;
                esquema.BUS.Add(bus);
                return (esquema.SaveChanges() == 1) ? "Bus Registrado Correctamento" : "No se pudo registrar el Bus";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                esquema.Dispose();
            }
        }

        public Boolean validarRegistro(String Placa,Int32 Interno)
        {
            try
            {
                return (from f in esquema.BUS where f.Interno.Equals(Interno) || f.Placa.Equals(Placa) select f).ToList().Count > 0;
            }
            catch (Exception)
            {
                
                throw;
            }
        }   

        public Boolean verificarModeloCreacion(Int64 IdBus)
        {
            try
            {
                return (from b in esquema.BUS
                        join a in esquema.ASIENTO on b.Id equals a.IdBus
                        where b.Id.Equals(IdBus)
                        select new
                        {
                            b.Id
                        }).ToList().Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean verificarModelo(Int64 IdBus)
        {
            try
            {
                return (from b in esquema.BUS
                        join a in esquema.ASIENTO on b.Id equals a.IdBus
                        where b.Id.Equals(IdBus) && b.Estado.Equals(1)
                        select new
                        {
                            b.Id
                        }).ToList().Count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<V_BUSTIPO> traerBus_Tipo()
        {
            try
            {
                return (from f in esquema.V_BUSTIPO select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<V_BUSTIPO> traerBus_Tipo(Int32 Interno)
        {
            try
            {
                return (Interno.Equals(0)) ? (from f in esquema.V_BUSTIPO select f).ToList() : (from f in esquema.V_BUSTIPO where f.Interno.Equals(Interno) select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Boolean actualizarEstado(BUS bus)
        {
            try
            {
                var info = esquema.BUS.Find(bus.Id);
                info.Estado = 0;
                return esquema.SaveChanges() == 1;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                esquema.Dispose();
            }
        }

        public String actualizar(BUS bus)
        {
            try
            {
                var info = esquema.BUS.Find(bus.Id);
                info.Estado = 1;
                return (esquema.SaveChanges() == 1) ? "Datos Actualizados" : "Error en la Actualizacion";
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                esquema.Dispose();
            }
        }

        public List<V_BUSTIPO> traerBus(Int64 idBus)
        {
            try
            {
                return (idBus.Equals(0)) ? (from f in esquema.V_BUSTIPO select f).ToList() : (from f in esquema.V_BUSTIPO where f.Id.Equals(idBus) select f).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                esquema.Dispose();
            }
        }
    }
}
