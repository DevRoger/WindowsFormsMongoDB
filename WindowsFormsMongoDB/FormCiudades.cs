using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WindowsFormsMongoDB.Services;
using System.Linq;
using WindowsFormsMongoDB.Models;

namespace WindowsFormsMongoDB
{
    public partial class FormCiudades : Form
    {
        // BindingSource para gestionar el DataGridView de forma eficiente
        private BindingSource bsCiudades = new BindingSource();

        // Lista original de ciudades sin filtrar
        private List<Ciudad> listaCiudades = new List<Ciudad>();

        public FormCiudades()
        {
            InitializeComponent();

            // Configuración inicial del DataGridView
            dataGridViewCiudades.DataSource = bsCiudades;
            dataGridViewCiudades.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Ciudades_Load(object sender, EventArgs e)
        {
            ActualizarDataGridView();
        }

        /// <summary>
        /// Abre el formulario para añadir una nueva ciudad.
        /// </summary>
        private void añadirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCiudad f = new FormCiudad();
            f.ShowDialog();
            ActualizarDataGridView();
        }

        /// <summary>
        /// Recupera la lista de ciudades y actualiza el DataGridView.
        /// Maneja excepciones para capturar errores de conexión o datos.
        /// </summary>
        public void ActualizarDataGridView()
        {
            try
            {
                // Obtener todas las ciudades de la base de datos y almacenarlas en la lista original
                listaCiudades = CiudadesCollection.GetAll();

                // Clonar la lista para evitar modificaciones directas sobre la original
                bsCiudades.DataSource = new List<Ciudad>(listaCiudades);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener la lista de ciudades: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Abre el formulario para editar la ciudad seleccionada en el DataGridView al hacer doble clic.
        /// </summary>
        private void dataGridViewCiudades_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewCiudades.CurrentRow != null)
            {
                if (dataGridViewCiudades.CurrentRow.DataBoundItem is Ciudad ciudad)
                {
                    FormCiudad f = new FormCiudad(ciudad);
                    f.ShowDialog();
                    ActualizarDataGridView();
                }
            }
        }

        /// <summary>
        /// Solicita confirmación antes de borrar la ciudad seleccionada.
        /// Maneja posibles errores durante el proceso de eliminación.
        /// </summary>
        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewCiudades.CurrentRow != null &&
                dataGridViewCiudades.CurrentRow.DataBoundItem is Ciudad ciudad)
            {
                // Preguntar al usuario si realmente desea eliminar la ciudad
                DialogResult resultado = MessageBox.Show(
                    "¿Estás seguro de que deseas borrar la ciudad seleccionada?",
                    "Confirmar borrado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        CiudadesCollection.Delete(ciudad);
                        ActualizarDataGridView();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al borrar la ciudad: {ex.Message}",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Cierra la aplicación.
        /// </summary>
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Filtra la lista de ciudades en tiempo real según el texto ingresado en txtBusqueda.
        /// Si el texto está vacío, restablece la lista original.
        /// </summary>
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtBusqueda.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(filtro))
            {
                // Si el campo de búsqueda está vacío, restaurar la lista original
                bsCiudades.DataSource = new List<Ciudad>(listaCiudades);
            }
            else
            {
                // Filtrar ciudades por nombre y actualizar la vista
                var ciudadesFiltradas = listaCiudades
                    .Where(c => c.Nombre.ToLower().Contains(filtro))
                    .ToList();
                bsCiudades.DataSource = ciudadesFiltradas;
            }
        }
    }
}
