using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaBANCO.Dominio
{
    internal class TipoCuenta
    {
        public int IdTipoCuenta { get; set; }
        public string Nombre { get; set; }
        public TipoCuenta()
        {
            IdTipoCuenta = 0;
            Nombre = "";
        }
        public TipoCuenta(int idTipoCuenta, string tipoCuenta)
        {
            IdTipoCuenta = idTipoCuenta;
            Nombre = tipoCuenta;
        }

    }
}
