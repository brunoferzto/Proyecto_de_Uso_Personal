using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
   public class PartidosEspeciales:Partidos

    {      
        int valor;
        bool goleada, estrella, clasico, azar, nopresenta, retira, finexpulsados;

        [DataMember]
        public bool Goleada
        {
            get { return goleada; }
            set { goleada = value; }
        }

        [DataMember]
        public bool Estrella
        {
            get { return estrella; }
            set { estrella = value; }
        }

        [DataMember]
        public bool Clasico
        {
            get { return clasico; }
            set { clasico = value; }
        }

        [DataMember]
        public bool Azar
        {
            get { return azar; }
            set { azar = value; }
        }

        [DataMember]
        public bool Nopresenta
        {
            get { return nopresenta; }
            set { nopresenta = value; }
        }

        [DataMember]
        public bool Retira
        {
            get { return retira; }
            set { retira = value; }
        }

        [DataMember]
        public bool FinExpulsiones
        {
            get { return finexpulsados; }
            set { finexpulsados = value; }
        }

        [DataMember]
        public int Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public PartidosEspeciales(int valor, bool goleada, bool estrella, bool clasico, bool azar, bool nopresenta, bool retira, bool finexpulsados,
            int id, Equipos local, int localres, Equipos visita, int vistares, string fechaPertenece, DateTime fecha, string estadio, bool simulado, Penales penal, List<PartidosDatos> lstDatos, List<Sustituciones> lstSustitucion)
            :base( id,  local,  localres,  visita,  vistares,  fechaPertenece,  fecha,  estadio,  simulado, penal,  lstDatos, lstSustitucion)
        {
            Valor = valor;
            Goleada = goleada;
            Estrella = estrella;
            Clasico = clasico;
            Azar = azar;
            Nopresenta = nopresenta;
            Retira = retira;
            FinExpulsiones = finexpulsados; 
        }
        public PartidosEspeciales() { }
    }
}
