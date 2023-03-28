using ProblemaBANCO.Datos;
using ProblemaBANCO.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProblemaBANCO.Presentacion
{
    public partial class FormAgregar : Form
    {
        private ConexionDB helper;
        private Cliente cliente;
        public FormAgregar()
        {
            InitializeComponent();
            helper = new ConexionDB();
        }

        private void FormAgregar_Load(object sender, EventArgs e)
        {
            Consultar("SP_CONSULTAR_TIPO_CUENTA", "id_tipo_cuenta", "tipo_cuenta", cboTipoCuenta);
            Limpiar();

        }
        private void Consultar(string SP, string valueMember, string displayMember, ComboBox cbo)
        {
            DataTable data = helper.ConsultarSP(SP, null, null);
            cbo.DataSource = data;
            cbo.ValueMember = valueMember;
            cbo.DisplayMember = displayMember;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        public void Limpiar()
        {
            txtApellido.Clear();
            txtNombre.Clear();
            cboTipoCuenta.SelectedIndex = -1;
            txtDNI.Clear();
            txtCBU.Clear();
            txtSalario.Clear();
            txtUltMov.Clear();
            cliente = new Cliente();
        }
        public bool ValidarCliente()
        {
            if (String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtApellido.Text) || String.IsNullOrEmpty(txtDNI.Text))
                return false;
            if(!int.TryParse(txtDNI.Text,out int resul))
                return false;
            return true;
        }
        public bool ValidarCuenta()
        {
            if (String.IsNullOrEmpty(txtCBU.Text))
                return false;
            if (!long.TryParse(txtCBU.Text, out long a) || !int.TryParse(txtSalario.Text, out int b))
                return false;
            if (cboTipoCuenta.SelectedIndex == -1)
                return false;
            return true;
        }
        public bool Existe()
        {
            DataTable data = helper.ConsultarSP("VERIFICAR_CLIENTE", null,txtDNI.Text);
            if (data.Rows.Count == 0)
                return false;
            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCuenta())
            {
                MessageBox.Show("Verifique los campos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtCBU.Text.Length != 10)
            {
                MessageBox.Show("Ha olvidado algún digito en el CBU?", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string CBU = txtCBU.Text;
            int idTipoCuenta = (int)cboTipoCuenta.SelectedValue;
            string nombreTC = cboTipoCuenta.Text;
            int salario = Convert.ToInt32(txtSalario.Text);
            string ultimoMovimiento = txtUltMov.Text;
            TipoCuenta tipoCuenta = new TipoCuenta(idTipoCuenta, nombreTC);
            Cuenta cuenta = new Cuenta(CBU, salario, tipoCuenta, ultimoMovimiento);
            dgvCuenta.Rows.Add(CBU,nombreTC,salario);

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (!ValidarCliente())
            {
                MessageBox.Show("Verifique los campos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cliente.Nombre = txtNombre.Text;
            cliente.Apellido = txtApellido.Text;
            cliente.DNI = Convert.ToInt32(txtDNI.Text);
            if (Existe())
            {
                MessageBox.Show("El usuario ya existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (helper.AltaCuenta(cliente))
                MessageBox.Show("Se agregó con exito!", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Limpiar();

        }

        private void dgvCuenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCuenta.CurrentCell.ColumnIndex == 3)
                if (dgvCuenta.CurrentCell.RowIndex != dgvCuenta.RowCount-1)
                {
                    cliente.EliminarCuenta(dgvCuenta.CurrentRow.Index);
                    dgvCuenta.Rows.Remove(dgvCuenta.CurrentRow);
                }
        }
    }
}
