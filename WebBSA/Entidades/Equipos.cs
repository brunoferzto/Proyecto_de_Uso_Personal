using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class Equipos
    {
        string nombre, nombrecompleto, ciudad, nacionalidad, estadio; 
        DateTime fundado;
        Competidores competidor;
        bool activo, seleccion;
        int capacidad, id; 

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }

        [DataMember]
        public bool Seleccion
        {
            get { return seleccion; }
            set { seleccion = value; }
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

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { if (value.Length <= 25)
                    nombre = value;
            else
                    throw new Exception("Nombre Incorrecto");

            }
        }

        [DataMember]
        public string NombreCompleto
        {
            get { return nombrecompleto; }
            set
            {
                if (value.Length <= 60)
                    nombrecompleto = value;
            
                else
                    throw new Exception("Nombre Incorrecto");
                }

        }

        [DataMember]
        public string Ciudad
        {
            get { return ciudad; }
            set
            {
                if (value.Length <= 25)
                    ciudad = value;               
                else
                    throw new Exception("Ciudad Incorrecta");
            }
        }

        [DataMember]
        public string Estadio
        {
            get { return estadio; }
            set
            {
                if (value.Length <= 30)
                    estadio = value;

                else
                throw new Exception("Nombre Estadio Incorrecto");
                     }
        }

        [DataMember]
        public int Capacidad
        {
            get { return capacidad; }
            set
            {
                if (value < 0)
                    throw new Exception("Capacidad de Estadio Incorrecta");
                else
                    capacidad = value;
            }
        }

        [DataMember]
        public DateTime Fundado
        {
            get { return fundado; }
            set { fundado = value; }
        }

        [DataMember]
        public string Pais 
        {
            get { return nacionalidad; }
            set
            {   if (value.Length <= 25)
                    nacionalidad = value; 

            else
                    throw new Exception("Pais Incorrecto");
             }
        }


        public Equipos(int id, string nombre, string nomCompleto, string ciudad, 
                       DateTime fundacion, string pais, Competidores competidor, bool activo, string estadio,
                      int capacidad, bool seleccion)
        {
            ID = id;
            Nombre = nombre;
            NombreCompleto = nomCompleto;
            Ciudad = ciudad;
            Fundado = fundacion;
            Pais = pais;
            Competidor = competidor;
            Activo = activo;
            Estadio = estadio;
            Capacidad = capacidad;
            Seleccion = seleccion; 
        }

        public Equipos(int id, string nombre, string nomCompleto, string ciudad,
                      DateTime fundacion, string pais, bool activo, string estadio,
                     int capacidad, bool seleccion)
        {
            ID = id;
            Nombre = nombre;
            NombreCompleto = nomCompleto;
            Ciudad = ciudad;
            Fundado = fundacion;
            Pais = pais;
            Activo = activo;
            Estadio = estadio;
            Capacidad = capacidad;
            Seleccion = seleccion;
        }

        public Equipos()
        { }
       
    }
}
