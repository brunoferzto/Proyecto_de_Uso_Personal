using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization; 

namespace Entidades
{
   public class ComentariosPartido
    {
        int id;
        Competidores competidor;
        string comentario;
        string url;

        [DataMember]
        public string Comentario
        {
            get { return comentario; }
            set { comentario = value; }
        }

        [DataMember]
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        [DataMember]
        public Competidores Competidor
        {
            get { return competidor; }
            set { competidor = value; }
        }

        [DataMember]
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public ComentariosPartido(int id, Competidores competidor, string comentario, string url)
        {
            ID = id;
            Competidor = competidor;
            Comentario = comentario;
            Url = url; 
        }

        public ComentariosPartido() { }
    }
}
