using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace WebApplication1
{
    public partial class NuevoPaciente : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Rol"] == null)
            {
                Response.Redirect("~/Login.aspx");

                return;
            }
            if (!IsPostBack)
            {
                // cargar una grilla 
            }
        }

        protected void BtnAgregarPaciente_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string dni = txtDniPaciente.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string obraSocial = txtObraSocial.Text.Trim();
            //string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(dni))
            {
                MostrarMensaje("Complete los campos obligatorios (Nombre, Apellido y DNI).", "danger");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                conexion.Open();

                SqlCommand existe = new SqlCommand("SELECT COUNT(*) FROM Pacientes WHERE dni = @dni", conexion);
                existe.Parameters.AddWithValue("@dni", dni);
                int count = (int)existe.ExecuteScalar();

                if (count > 0)
                {
                    MostrarMensaje("El paciente ya está registrado.", "warning");
                    return;
                }

                SqlCommand insertar = new SqlCommand(


                    "INSERT INTO Pacientes (dni, nombre, apellido, telefono, ObraSocial) " + "VALUES (@dni, @nombre, @apellido, @telefono, @ObraSocial)", conexion);
                insertar.Parameters.AddWithValue("@dni", dni);
                insertar.Parameters.AddWithValue("@nombre", nombre);
                insertar.Parameters.AddWithValue("@apellido", apellido);
                insertar.Parameters.AddWithValue("@telefono", telefono);
                insertar.Parameters.AddWithValue("@ObraSocial", obraSocial);


                //insertar.Parameters.AddWithValue("@correo", email);

                insertar.ExecuteNonQuery();
            }

            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDniPaciente.Text = "";
            txtTelefono.Text = "";
            txtObraSocial.Text = "";

            // ✅ Notificación de éxito
            Response.Redirect("Pacientes.aspx?nuevo=1");


        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("About.aspx");
        }

        // 🔔 Método auxiliar para mostrar alertas de Bootstrap
        private void MostrarMensaje(string mensaje, string tipo)
        {
            string script = $@"
                <div class='alert alert-{tipo} alert-dismissible fade show mt-3' role='alert'>
                    {mensaje}
                    <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>
                </div>";
            mensajeLiteral.Text = script;
        }
    }
}
