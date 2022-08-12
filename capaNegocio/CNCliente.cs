using capaEntidad;
using capaDatos;
using System.Data;

namespace capaNegocio
{
    public class CNCliente
    {
        CDCliente CDCliente = new CDCliente();

        // Desde esta capa vamos a validar la información que viene desde capaPresentacion
        public bool ValidarDatos(CECliente CECliente)
        {
            bool result = true;

            if(CECliente.Nombre == string.Empty)
            {
                result = false;
                MessageBox.Show("El nombre es obligatorio");
            }

            if (CECliente.Apellido == string.Empty)
            {
                result = false;
                MessageBox.Show("El apellido es obligatorio");
            }

            if (CECliente.Foto == null)
            {
                result = false;
                MessageBox.Show("La foto es obligatoria");
            }

            return result;
        }

        public void PruebaMySQL()
        {
            CDCliente.PruebaConexion();
        }

        public void CrearCliente(CECliente CECliente)
        {
            CDCliente.Crear(CECliente);
        }

        public void EditarCliente(CECliente CECliente)
        {
            CDCliente.Editar(CECliente);
        }

        public void EliminarCliente(CECliente CECliente)
        {
            CDCliente.Eliminar(CECliente);
        }

        public DataSet ObtenerDatos()
        {
            return CDCliente.Listar();
        }
    }
}