using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; 

namespace Entidades
{
    [DataContract]
    public class Temporadas
    {
        int id, num;
        string nombre;
        DateTime fechacomienzo;
        DateTime fechafinal;
        bool estado;
        List<Competencias> lista;




        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int Numero
        {
            get { return num; }
            set
            {
                if (value > 0)
                    num = value;
                else
                    throw new Exception("Número de Temporada");
            }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Length <= 30)
                    nombre = value;
                else
                    throw new Exception("Nombre de Temporada");
            }
        }

        [DataMember]
        public DateTime FechaComienzo
        {
            get { return fechacomienzo; }
            set { fechacomienzo = value; }
        }

        [DataMember]
        public DateTime FechaFinal
        {
            get { return fechafinal; }
            set { fechafinal = value; }
        }

        [DataMember]
        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        [DataMember]
        public List<Competencias> Copas
        {
            get { return lista; }
            set { lista = value; }
        }




        public Temporadas(int id, string nom, DateTime fcom, DateTime ffin,bool estado, int num)
        {
            ID = id;
            Nombre = nom;
            FechaComienzo = fcom;
            FechaFinal = ffin;
            Estado = estado;
            Numero = num;
        }

        public Temporadas(int id, string nom, DateTime fcom, DateTime ffin, bool estado, int num, List<Competencias> lista)
        {
            ID = id;
            Nombre = nom;
            FechaComienzo = fcom;
            FechaFinal = ffin;
            Estado = estado;
            Numero = num;
            Copas = lista; 
        }

        public Temporadas() { }


    }
}
