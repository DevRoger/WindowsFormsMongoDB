using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsMongoDB.Services;
using WindowsFormsMongoDB.Models;

namespace WindowsFormsMongoDB
{
    public partial class FormCiudades : Form
    {
        public FormCiudades()
        {
            InitializeComponent();
        }

        private void Ciudades_Load(object sender, EventArgs e)
        {
            List<Ciudad> ciudades = CiudadesCollection.GetAll();

            ActualizarDataGridView();

        }

        private void añadirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCiudad f = new FormCiudad();
            f.ShowDialog();
            ActualizarDataGridView();
        }

        public void ActualizarDataGridView()
        {
            List<Ciudad> ciudades = CiudadesCollection.GetAll();
            dataGridViewCiudades.DataSource = null;
            dataGridViewCiudades.DataSource = ciudades;

            dataGridViewCiudades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridViewCiudades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Ciudad ciudad = dataGridViewCiudades.CurrentRow.DataBoundItem as Ciudad;
            FormCiudad f = new FormCiudad(ciudad);
            f.ShowDialog();
            ActualizarDataGridView();

        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ciudad ciudad = dataGridViewCiudades.CurrentRow.DataBoundItem as Ciudad;

            // Preguntar confirmación al usuario
            DialogResult resultado = MessageBox.Show(
                "¿Estás seguro de que deseas borrar la ciudad seleccionada?",
                "Confirmar borrado",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                CiudadesCollection.Delete(ciudad);
                ActualizarDataGridView();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
