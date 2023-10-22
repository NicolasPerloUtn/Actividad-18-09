using PracticaParcial2.Datos;
using PracticaParcial2.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaParcial2
{
    public partial class Form1 : Form
    {
        private OrdenRetiro nueva;
        private Gestor gestor;
        public Form1()
        {
            InitializeComponent();
            nueva = new OrdenRetiro();
            gestor = new Gestor();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarCombo();
        }

        public void CargarCombo()
        {
            DataTable dt = gestor.ListarMateriales();
            cboMaterial.DataSource = dt;
            cboMaterial.DisplayMember = "nombre";
            cboMaterial.ValueMember = "codigo";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtResponsable.Text == string.Empty)
            {
                MessageBox.Show("Debe ingresar un Responsable", "Controls", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtResponsable.Focus();
                return;
            }
            nueva.Fecha = dtpFecha.Value;
            nueva.Responsable = txtResponsable.Text;
            if (gestor.EjecutarInsert(nueva))
            {
                MessageBox.Show("Orden guardada");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("No se pudo guardar la orden");
            }
        }

        private void LimpiarCampos()
        {
            dtpFecha.Text = DateTime.Now.ToString();
            txtResponsable.Text = "";
            cboMaterial.SelectedIndex = -1;
            nudCantidad.Value = 1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();

            }
            else
            {
                return;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboMaterial.Text.Equals(string.Empty))
            {
                MessageBox.Show("Debe seleccionar un Material", "Controls", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(nudCantidad.Text) || !int.TryParse(nudCantidad.Text, out _))
            {
                MessageBox.Show("Debe ingresar una cantidad válida", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["Material"].Value.ToString().Equals(cboMaterial.Text))
                {
                    MessageBox.Show("Este ingrediente ya está cargado.", "Control", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            DataRowView item = (DataRowView)cboMaterial.SelectedItem;
            int mat = Convert.ToInt32(item.Row.ItemArray[0]);
            string nom = item.Row.ItemArray[1].ToString();
            int stock = Convert.ToInt32(item.Row.ItemArray[2]);

            Material m = new Material(mat, nom, stock);
            int cant = Convert.ToInt32(nudCantidad.Value);
            DetalleOrden detalle = new DetalleOrden(m, cant);

            nueva.AgregarOrden(detalle);
            dgvDetalles.Rows.Add(new object[] { detalle.Material.Codigo, detalle.Material.Nombre, detalle.Material.Stock ,detalle.Cantidad , "Quitar" });

        }

    }
}
