using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace WebApplication1
{
    public partial class Pacientes : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 🔐 Verificar sesión activa
            if (Session["Rol"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            // 🔄 Cargar datos solo la primera vez
            if (!IsPostBack)
            {
                CargarPacientes();

                // Si venimos de agregar un paciente, mostrar alerta
                if (Request.QueryString["nuevo"] == "1")
                    MostrarMensaje("✅ Paciente agregado correctamente.", "success");
            }
        }

        private void CargarPacientes()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;

                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    string query = "SELECT id_paciente, dni, nombre, apellido,telefono ,ObraSocial FROM Pacientes ORDER BY apellido, nombre";
                    SqlCommand comando = new SqlCommand(query, conexion);

                    conexion.Open();

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        DataTable tabla = new DataTable();
                        tabla.Load(lector);

                        gvPacientes.DataSource = tabla;
                        gvPacientes.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("❌ Error al cargar los pacientes: " + ex.Message, "danger");
            }
        }


        private void MostrarMensaje(string mensaje, string tipo)
        {
            string script = $@"
                <div class='alert alert-{tipo} alert-dismissible fade show mt-3' role='alert'>
                    {mensaje}
                    <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>
                </div>";
            mensajeLiteral.Text = script;
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("About.aspx");
        }
    }
}

