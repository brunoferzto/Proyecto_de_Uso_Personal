using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    [KnownType(typeof(PremioCesiones))]
    [KnownType(typeof(PremioCreacion))]
    [KnownType(typeof(PremioEdicion))]
    [KnownType(typeof(PremioEterno))]
    [KnownType(typeof(PremioFichajes))]
    [KnownType(typeof(PremioInmunidad))]
    [KnownType(typeof(PremioRetencion))]
    [KnownType(typeof(PremiosOtros))]
    public class Premios
    {
        int id;
        DateTime fecha;
        Competencias competencia;
        string motivo;
        Competidores competidor;

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Competencias Competencia
        {
            get { return competencia; }
            set
            {
                if (value != null)
                    competencia = value;
                else
                    throw new Exception("Se necesita Competencia");
            }
        }

        [DataMember]
        public string Motivo
        {
            get { return motivo; }
            set
            {
                motivo = value;

            }


        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        public Competidores Competidor
        {
            get { return competidor; }
            set
            {
                if (value != null)
                    competidor = value;
                else
                    throw new Exception("No hay Competidor");
            }
        }

        public Premios (int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
        {
            ID = id;
            Motivo = motivo;
            Fecha = fecha;
            Competidor = competidor;
            Competencia = copa;
        }

        public Premios() { }
    }
}
