using ProblemaBANCO.Datos;
using ProblemaBANCO.Dominio;
using ProblemaBANCO.Presentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProblemaBANCO
{
    public partial class FormConsultar : Form
    {
        private ConexionDB helper;
        private Cliente cliente;
        public FormConsultar()
        {
            InitializeComponent();
            helper = new ConexionDB();
        }

        private void FormConsultar_Load(object sender, EventArgs e)
        {
        }

        private void btnNuevaCuenta_Click(object sender, EventArgs e)
        {
            FormAgregar form=new FormAgregar();
            form.ShowDialog();
        }
        public bool Validar()
        {
            if (String.IsNullOrEmpty(txtApellido.Text) || String.IsNullOrEmpty(txtNombre.Text))
                return false;
            return true;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                cliente = new Cliente();
                cliente.Nombre = txtNombre.Text;
                cliente.Apellido = txtApellido.Text;
                DataTable data = helper.ConsultarSP("CONSULTAR_CUENTAS", cliente,null);
                dataGridView1.Rows.Clear();
                foreach (DataRow item in data.Rows)
                {
                    string cbu = item["cbu"].ToString();
                    string tipoCuenta = item["tipo_cuenta"].ToString();
                    int saldo = Convert.ToInt32(item["saldo"].ToString());
                    dataGridView1.Rows.Add(cbu, tipoCuenta, saldo);
                }
                label3.Text = "Total de cuentas: " + (dataGridView1.Rows.Count - 1);
            }
            else
                MessageBox.Show("Verifique los campos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
