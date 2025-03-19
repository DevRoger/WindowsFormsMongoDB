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
    public partial class FormCiudad : Form
    {
        Ciudad ciudad;
        bool insert = true;
        public FormCiudad()
        {
            InitializeComponent();
            ciudad = new Ciudad();
            insert = true;
        }

        public FormCiudad(Ciudad ciudad)
        {
            InitializeComponent();
            this.ciudad = ciudad;
            insert = false;
        }

        private void FormCiudad_Load(object sender, EventArgs e)
        {
            if (!insert)
            {
                textBoxNombre.Text = ciudad.Nombre;
            }
        }

        private void buttonAceptar_Click(object sender, EventArgs e)
        {
            ciudad.Nombre = textBoxNombre.Text;

            if (insert)
            {
                CiudadesCollection.Insert(ciudad);
            }
            else
            {
                CiudadesCollection.Update(ciudad);
            }

            this.Close();

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
