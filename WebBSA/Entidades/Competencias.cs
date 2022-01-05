using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization; 

namespace Entidades
{
    [DataContract]
    public class Competencias
    {
        string nombre, descripcion, trofeo, tipo;
        Temporadas tempo;
        int id, numero; 
        bool activo;
        List<Fases> lista;

        [DataMember] // se crea hacia abajo
        public List<Fases> Lista
        {
            get { return lista; }
            set { lista = value; }
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
                    throw new Exception("Nombre Incorrecto");
            }
        }

        [DataMember]
        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                if (value.Length <= 500)
                    descripcion = value;

                else
                    throw new Exception("Descripcion Incorrecta");
            }
        }

        [DataMember]
        public string Trofeo
       {
           get { return trofeo; }
           set {
                if (value.Length <= 255)
                    trofeo = value;
                else
                    throw new Exception("Demasiados caracteres en Trofeo");
                    }
       }

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set
            {
                if (value == "BSA" || value == "Mundial" || value == "Copa" ||
                    value == "Oficial Menor Equipos" || value == "Oficial Menor Selecciones" || value == "No Oficial" || value == "Otros")
                    tipo = value;
                else
                    throw new Exception("Error de Tipo de Competencia");
            }
        }

        [DataMember]
        public Temporadas Tempo
       {
           get { return tempo; }
           set
            {
                if (value != null)
                    tempo = value;
                else
                    throw new Exception("No hay Temporada");
            }
       } 

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [DataMember]
        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
        public Competencias (int id,string nombre, string tipo, string descr, string trofeo,  bool act, int numero)
       {
           ID = id;
           Nombre = nombre;
           Numero = numero;
           Descripcion = descr;
           Trofeo = trofeo;         
            Activo = act;
            Tipo = tipo; 
            
        }
        public Competencias(Temporadas tempo, int id, string nombre, string tipo, string descr, string trofeo, bool act, int numero) 
            : this( id,  nombre,  tipo,  descr,  trofeo,  act,  numero)
        {
            Tempo = tempo;
        }

        public Competencias(List<Fases> lista, int id, string nombre, string tipo, string descr, string trofeo, bool act, int numero)
            : this(id, nombre, tipo, descr, trofeo, act, numero)
        {
            Lista = lista;
        }


    public Competencias() { }
     
    }
}
