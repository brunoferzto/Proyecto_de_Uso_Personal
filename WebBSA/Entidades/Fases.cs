using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Fases
    {
        int id, numero, cantEquipos, cantSubfases;
        bool simula, datosSimula, activo;
        string nombre, tipo;
        Competencias competencia;
        List<SubFases> lista;

        [DataMember]
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [DataMember]
        public int CantEquipos
        {
            get { return cantEquipos; }
            set { if(value > 0)
                    cantEquipos = value;
            else
                    throw new Exception("Cantidad de Equipos Incorrecto");
            }
        }

        [DataMember]
        public int CantSubfases
        {
            get { return cantSubfases; }
            set { if(value > 0)
                  cantSubfases = value;
            else
                    throw new Exception("Cantidad de Equipos Incorrecto");
            }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (value == "Eliminatoria" || value == "Grupo")
                    tipo = value;
                else
                    throw new Exception("Tipo Incorrecto");

            }
        }

        [DataMember]
        public bool Simula
        {
            get { return simula; }
            set { simula = value; }
        }

        [DataMember]
        public bool DatosSimula
        {
            get { return datosSimula; }
            set { datosSimula = value; }
        }

        [DataMember]
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        [DataMember]
        public Competencias Competencia
        {
            get { return competencia; }
            set { if(value != null)
                    competencia = value;
            else
                    throw new Exception("Se necesita Competencia");
            }
        }

        [DataMember]
        public List<SubFases> Subfase
        {
            get { return lista; }
            set { lista = value; }

        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { if(value.Length <= 30)
                    nombre = value;
            else
                    throw new Exception("Cantidad de Equipos Incorrecto");
            }
        }


        public Fases(int id, string nombre, int numero, int cantEquipos, int cantSubfases, string tipo, bool simula, bool datosSimula,
                        bool activo,List<SubFases> lista)
        {
            ID = id;
            Numero = numero;
            Nombre = nombre;
            CantEquipos = cantEquipos;
            CantSubfases = cantSubfases;
            Tipo = tipo;
            Simula = simula;
            DatosSimula = datosSimula;
            Activo = activo;
            Subfase = lista; 
        }

        public Fases(int id, string nombre, int numero, int cantEquipos, int cantSubfases, string tipo, bool simula, bool datosSimula,
                        bool activo, Competencias competencia)
        {
            ID = id;
            Numero = numero;
            Nombre = nombre;
            CantEquipos = cantEquipos;
            CantSubfases = cantSubfases;
            Tipo = tipo;
            Simula = simula;
            DatosSimula = datosSimula;
            Activo = activo;
            Competencia = competencia;
        }

        public Fases(int id, string nombre, int numero, int cantEquipos, int cantSubfases, string tipo,bool simula, bool datosSimula,
                       bool activo)
        {
            ID = id;
            Numero = numero;
            Nombre = nombre;
            CantEquipos = cantEquipos;
            CantSubfases = cantSubfases;
            Tipo = tipo;
            Simula = simula;
            DatosSimula = datosSimula;
            Activo = activo;
        }

        public Fases() { }

    }
}
