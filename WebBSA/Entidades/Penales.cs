using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Penales
    {
        int relocal, revisita;
        List<DatosTanda> lista;

        [DataMember]
        public int Relocal
        {
            get { return relocal; }
            set { relocal = value; }
        }

        [DataMember]
        public int Revisita
        {
            get { return revisita; }
            set { revisita = value; }


        }


        [DataMember]
        public List<DatosTanda> Lista
        {
            get { return lista; }
            set { lista = value; }


        }

        public Penales (int reslocal, int resvisita, List<DatosTanda> lista)
        {
            Relocal = reslocal;
            Revisita = resvisita;
            Lista = lista; 
        }
    }
    }
