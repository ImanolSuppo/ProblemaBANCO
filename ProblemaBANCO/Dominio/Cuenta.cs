using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaBANCO.Dominio
{
    internal class Cuenta
    {
        public string CBU { get; set; }
        public int Saldo { get; set; }
        public TipoCuenta TipoCuenta { get; set; }
        public string UltimoMovimiento { get; set; }
        public Cuenta()
        {
            CBU = "0000000000000000000000";
            Saldo = 0;
            TipoCuenta = new TipoCuenta();
            UltimoMovimiento = "";
        }
        public Cuenta(string cBU, int saldo, TipoCuenta tipoCuenta, string ultimoMovimiento)
        {
            CBU = cBU;
            Saldo = saldo;
            TipoCuenta = tipoCuenta;
            UltimoMovimiento = ultimoMovimiento;
        }
    }
}
