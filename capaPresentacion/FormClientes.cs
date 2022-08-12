// Obtenemos el acceso a todas las clases que esten en la capaEntidad
using capaEntidad;
using capaNegocio;

namespace capaPresentacion
{
    public partial class FormClientes : Form
    {
        CNCliente CNCliente = new CNCliente();

        public FormClientes()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarForm();
        }

        private void LimpiarForm()
        {
            // Resetear campos del form
            numBoxID.Value = 0;
            txtBoxNombre.Text = string.Empty;
            txtBoxApellido.Text = string.Empty;
            picBoxFoto.Image = null;
        }

        private void linkFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Inicializar campo de archivo en vacio
            openFileDialogFoto.FileName = string.Empty;
            // Abrir ventana de seleccion de archivos
            // Validar si el usuario subio un archivo o canceló
            if (openFileDialogFoto.ShowDialog() == DialogResult.OK)
            {
                picBoxFoto.Load(openFileDialogFoto.FileName);
            }

            openFileDialogFoto.FileName = string.Empty;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool result;

            // Instanciar la clase
            CECliente cliente = new CECliente();
            // Asignar los valores que tengamos en el form
            cliente.Id = (int)numBoxID.Value;
            cliente.Nombre = txtBoxNombre.Text;
            cliente.Apellido = txtBoxApellido.Text;
            cliente.Foto = picBoxFoto.ImageLocation;

            // Validamos los datos
            result = CNCliente.ValidarDatos(cliente);

            if (result == false)
            {
                return;
            }

            if(cliente.Id == 0)
            {
                CNCliente.CrearCliente(cliente);
            }
            else
            {
                CNCliente.EditarCliente(cliente);
            }

            CargarDatos();
            LimpiarForm();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Prueba conexión base de datos
            // CNCliente.PruebaMySQL();

            bool resultQuestion;

            if (numBoxID.Value == 0) return;

            resultQuestion = MessageBox.Show("¿Deseas eliminar el registro?", "Titulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

            if (resultQuestion)
            {
                CECliente cECliente = new CECliente();
                cECliente.Id = (int)numBoxID.Value;
                CNCliente.EliminarCliente(cECliente);
                CargarDatos();
                LimpiarForm();
            }


        }


        // Load, evento que se ejecuta al iniciar el form
        private void FormClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            dataGridDatos.DataSource = CNCliente.ObtenerDatos().Tables["table_items"];
        }

        private void dataGridDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            numBoxID.Value = (int)dataGridDatos.CurrentRow.Cells["id"].Value;
            txtBoxNombre.Text = dataGridDatos.CurrentRow.Cells["nombre"].Value.ToString();
            txtBoxApellido.Text = dataGridDatos.CurrentRow.Cells["apellido"].Value.ToString();
            picBoxFoto.Load(dataGridDatos.CurrentRow.Cells["foto"].Value.ToString());
        }
    }
}