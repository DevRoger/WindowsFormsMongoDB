using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Actualizamos el objeto ciudad con el valor del TextBox
            ciudad.Nombre = textBoxNombre.Text;

            // Ejecutamos la operación correspondiente (insertar o actualizar)
            if (insert)
            {
                CiudadesCollection.Insert(ciudad);
            }
            else
            {
                CiudadesCollection.Update(ciudad);
            }

            this.Close();  // Cerramos el formulario
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