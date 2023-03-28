using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemaBANCO.Dominio
{
    internal class Cliente
    {
        public List<Cuenta> Cuentas { get; set; }
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Cliente()
        {
            List<Cuenta> Cuentas = new List<Cuenta>();
        }
        public Cliente(List<Cuenta> cuentas, int dNI, string nombre, string apellido)
        {
            Cuentas = cuentas;
            DNI = dNI;
            Nombre = nombre;
            Apellido = apellido;
        }
        public void AgregarCuenta(Cuenta cuenta)
        {
            Cuentas.Add(cuenta);
        }
        public void EliminarCuenta(int indice)
        {
            Cuentas.RemoveAt(indice);
        }
    }
}
