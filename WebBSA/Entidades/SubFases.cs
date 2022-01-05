using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
    [DataContract]
   public class SubFases
    {
        int id_Gru, numero;
        Fases fase;
        List<Integrantes> lista; 
        string nombre, tipo;
        bool respetareglas;

        [DataMember]
        public int ID_Gru
        {
            get { return id_Gru; }
            set { id_Gru = value; }
        }

        [DataMember]
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set {
                if (value.Length <= 20)
                    nombre = value;
                else
                    throw new Exception("Nombre Incorrecto");
            }
        }

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set { if( value == "Eliminatoria" || value == "Grupo")
                    tipo = value;
            else
                    throw new Exception("Tipo Incorrecto");

            }
        }

        [DataMember]
        public bool RespetaReglas
        {
            get { return respetareglas; }
            set { respetareglas = value; }
        }

        [DataMember]
        public Fases Fase
        {
            get { return fase; }
            set { if(value != null)
                    fase = value;
            else
                    throw new Exception("Se necesita Fase");
            }
        }

        [DataMember]
        public List<Integrantes>Lista
        {
            get { return lista; }
            set { lista = value; }
        }

        public SubFases(int id,int numero,string nombre,string tipo,bool respetareglas)
        {
            ID_Gru = id;
            Numero = numero;
            Nombre = nombre;
            Tipo = tipo;
            RespetaReglas = respetareglas;
        }
        public SubFases(int id, int numero, string nombre, string tipo, bool respetareglas, List<Integrantes> lista)
        {
            ID_Gru = id;
            Numero = numero;
            Nombre = nombre;
            Tipo = tipo;
            RespetaReglas = respetareglas;
            Lista = lista;
        }

        public SubFases(int id, int numero, string nombre, string tipo, bool respetareglas, List<Integrantes> lista, Fases fase)
           : this(id, numero, nombre, tipo, respetareglas)
        {
            Fase = fase;
            Lista = lista;
        }
        public SubFases(int id, int numero, string nombre, string tipo, bool respetareglas, Fases fase)
           : this(id, numero, nombre, tipo, respetareglas)
        {
            Fase = fase; 
        }

        public SubFases() { }

    }
}
