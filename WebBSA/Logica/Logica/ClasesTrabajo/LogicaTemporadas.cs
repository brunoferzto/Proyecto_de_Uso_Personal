using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;


namespace Logica
{
    public class LogicaTemporadas
    {
        private static LogicaTemporadas _instancia = null;
        private LogicaTemporadas() { }
        public static LogicaTemporadas GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaTemporadas();
            return _instancia;
        }


      
    }
}
