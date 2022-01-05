using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
 
namespace Entidades
{
    [DataContract]
    class PremiosOtros:Premios
    {
        string infO; 

        public string Info
        {
            get { return infO; }
            set { infO = value; }
        }

        public PremiosOtros (string info, int id, string motivo, DateTime fecha, Competidores competidor, Competencias copa)
            :base( id,  motivo,  fecha,  competidor,  copa)
        {
            Info = infO; 

        }

        public PremiosOtros() { }
    }
}
