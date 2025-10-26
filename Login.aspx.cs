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
  public partial class Login : Page
    {
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                lblMensaje.Text = "⚠️ Ingrese usuario y contraseña.";
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["cadenaConexion"].ConnectionString;

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                {
                    conexion.Open();

                    SqlCommand comando = new SqlCommand(
                        "SELECT rol FROM Usuarios WHERE usuario = @usuario AND contrasena = @contrasena",
                        conexion);
                    comando.Parameters.AddWithValue("@usuario", usuario);
                    comando.Parameters.AddWithValue("@contrasena", contrasena);

                    object rolObj = comando.ExecuteScalar();

                    if (rolObj == null)
                    {
                        lblMensaje.Text = "❌ Usuario o contraseña incorrectos.";
                        return;
                    }

                    string rol = rolObj.ToString();

                    // ✅ Guardar sesión
                    Session["Usuario"] = usuario;
                    Session["Rol"] = rol;

                    // 🔁 Redirigir según rol
                    if (rol == "Medico" || rol == "Secretaria")
                        Response.Redirect("~/About.aspx");
                    else
                        lblMensaje.Text = "⚠️ Rol desconocido. Verifique con el administrador.";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "❌ Error al conectar con la base de datos: " + ex.Message;
            }
        }
    }
}
