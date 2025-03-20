using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsMongoDB.Models;
using WindowsFormsMongoDB.Services;

namespace WindowsFormsMongoDB
{
    /// <summary>
    /// Formulario para la creación y edición de ciudades
    /// </summary>
    public partial class FormCiudad : Form
    {
        // Instancia de la ciudad que se está manipulando
        private Ciudad ciudad;

        // Bandera para determinar si es una operación de inserción (true) o edición (false)
        private bool insert = true;

        /// <summary>
        /// Constructor para crear una nueva ciudad
        /// </summary>
        public FormCiudad()
        {
            InitializeComponent();
            ciudad = new Ciudad();
            insert = true;  // Modo inserción
        }

        /// <summary>
        /// Constructor para editar una ciudad existente
        /// </summary>
        /// <param name="ciudad">Ciudad a editar</param>
        public FormCiudad(Ciudad ciudad)
        {
            InitializeComponent();
            this.ciudad = ciudad;
            insert = false;  // Modo edición
        }

        private void FormCiudad_Load(object sender, EventArgs e)
        {
            // Si estamos en modo edición, cargamos los datos existentes
            if (!insert)
            {
                textBoxNombre.Text = ciudad.Nombre;
            }
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Aceptar
        /// </summary>
        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            // Validar que el nombre no esté vacío o contenga solo espacios en blanco
            if (string.IsNullOrWhiteSpace(textBoxNombre.Text))
            {
                MessageBox.Show("El nombre de la ciudad no puede estar vacío.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Salir sin continuar con la inserción o actualización
            }

            // Actualizamos el objeto ciudad con el valor del TextBox
            ciudad.Nombre = textBoxNombre.Text.Trim(); // Trim() para eliminar espacios en blanco al inicio y fin

            // Verificar si la ciudad ya existe
            if (CiudadExiste(ciudad.Nombre))
            {
                MessageBox.Show("Ya existe una ciudad con ese nombre. Intenta con otro nombre.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;  // No continuar con la inserción
            }

            try
            {
                // Ejecutamos la operación correspondiente: insertar o actualizar
                if (insert)
                {
                    CiudadesCollection.Insert(ciudad);  // Insertar nueva ciudad
                    MessageBox.Show($"La ciudad '{ciudad.Nombre}' se ha agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    CiudadesCollection.Update(ciudad);  // Actualizar ciudad existente
                }
                this.Close();  // Cerramos el formulario
            }
            catch (Exception ex)
            {
                // Manejo de errores: si ocurre algún problema al insertar o actualizar
                MessageBox.Show($"Error al guardar la ciudad: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Verifica si una ciudad con el nombre dado ya existe.
        /// </summary>
        /// <param name="nombreCiudad">Nombre de la ciudad a verificar</param>
        /// <returns>Verdadero si la ciudad existe, falso si no existe</returns>
        private bool CiudadExiste(string nombreCiudad)
        {
            // Buscar si el nombre de la ciudad ya existe en la base de datos o en la lista
            var ciudades = CiudadesCollection.GetAll(); // Obtener todas las ciudades (esto se puede optimizar si ya tienes la lista)
            return ciudades.Any(c => c.Nombre.Equals(nombreCiudad, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Maneja el evento de clic en el botón Cancelar
        /// </summary>
        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();  // Cierra el formulario sin guardar cambios
        }
    }
}
