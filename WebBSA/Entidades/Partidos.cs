using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
    [KnownType(typeof(PartidosEspeciales))]
    [DataContract]
    public class Partidos
    {
        int id,  locaLres, visiTares;
        Equipos local, visitante;
        string fechapertenece, estadio;
        DateTime fecha;
        bool simulado;
        SubFases subfase;
        Penales penal;
        List<PartidosDatos> lista;
        List<Sustituciones> listaS;

        [DataMember]
        public List<Sustituciones> ListaS
        {
            get { return listaS; }
            set { listaS = value; }
        }

        [DataMember]
        public List<PartidosDatos>Lista
        {
            get { return lista; }
            set { lista = value; }
        }

        [DataMember]
        public SubFases Subfase
        {
            get { return subfase; }
            set { subfase = value; }
        }

        [DataMember]
        public bool Simulado
        {
            get { return simulado; }
            set { simulado = value; }
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        public Equipos Visitante
        {
            get { return visitante; }
            set { visitante = value; }
        }

        [DataMember]
        public Equipos Local
        {
            get { return local; }
            set { local = value; }
        }

        [DataMember]
        public string Estadio
        {
            get { return estadio; }
            set { estadio = value; }
        }

        [DataMember]
        public int VisiTares
        {
            get { return visiTares; }
            set { visiTares = value; }
        }

        [DataMember]
        public int LocaLres
        {
            get { return locaLres; }
            set { locaLres = value; }
        }

        [DataMember]
        public string FechaPertenece
        {
            get { return fechapertenece; }
            set {if (value.Length <= 15)
                    fechapertenece = value;
                else
                    throw new Exception("Error Fecha Pertenece");
            }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public Penales Penal
        {
            get { return penal; }
            set { penal = value; }
        }


        public Partidos(int id, Equipos local, int localres, Equipos visita, int vistares, string fechaPertenece, DateTime fecha, string estadio, bool simulado, Penales penal,List<PartidosDatos> lstDatos, List<Sustituciones> lstSustitucion)
        {
            ID = id;
            Local = local;
            LocaLres = localres;
            Visitante = visita;
            VisiTares = vistares;
            FechaPertenece = fechaPertenece;
            Fecha = fecha;
            Estadio = estadio;
            Simulado = simulado;
            Penal = penal;
            Lista = lstDatos;
            ListaS = lstSustitucion;

        }

        public Partidos(int id, Equipos local, int localres, Equipos visita, int vistares, string fechaPertenece, DateTime fecha, string estadio, bool simulado)
        {
            ID = id;
            Local = local;
            LocaLres = localres;
            Visitante = visita;
            VisiTares = vistares;
            FechaPertenece = fechaPertenece;
            Fecha = fecha;
            Estadio = estadio;
            Simulado = simulado;
            Penal = penal;

        }

        public Partidos() { }
    }
}
