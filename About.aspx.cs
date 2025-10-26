using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebApplication1
{
    public partial class About : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1️⃣ Verificar si hay sesión
            if (Session["Rol"] == null)
            {
                Session["error"] = "Debes iniciar sesión.";
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // 2️⃣ Verificar si el rol tiene permiso
            string rol = Session["Rol"].ToString();

            // Solo Medico y Secretaria pueden entrar
            if (rol != "Medico" && rol != "Secretaria")
            {
                Session["error"] = "Acceso denegado.";
                Response.Redirect("~/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // 3️⃣ Cargar contenido solo si es la primera carga
            if (!IsPostBack)
            {
                cargarGrilla();

                if (Request.QueryString["nuevo"] == "1")
                    mostrarMensaje("✅ Se agregó un nuevo paciente correctamente.", "success");
            }
        }
        private void cargarGrilla()
        {
            try
            {
                ClinicaNegocio negocio = new ClinicaNegocio();
                DataTable tabla = negocio.ListarTurnosSP();

                gvTurnos.DataSource = tabla;
                gvTurnos.DataBind();
            }
            catch (Exception ex)
            {
                mostrarMensaje("❌ Error al cargar los turnos: " + ex.Message, "danger");
            }
        }

        // 💬 Mostrar mensajes Bootstrap
        private void mostrarMensaje(string mensaje, string tipo)
        {
            string html = $@"
        <div class='alert alert-{tipo} alert-dismissible fade show mt-3' role='alert'>
            {mensaje}
            <button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button>
        </div>";
            mensajeLiteral.Text = html;
        }

        // 🎯 Evento para reservar un turno
        protected void btnReservar_Click(object sender, EventArgs e)
        {
            string dni = txtDni.Text.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                mostrarMensaje("Debe ingresar el DNI del paciente.", "warning");
                return;
            }

            try
            {
                DateTime fecha = DateTime.Parse(txtFecha.Text);
                TimeSpan hora = TimeSpan.Parse(txtHora.Text);

                ClinicaNegocio negocio = new ClinicaNegocio();
                int? idPaciente = negocio.BuscarPacientePorDni(dni);

                if (idPaciente == null)
                {
                    mostrarMensaje("El paciente no existe en el sistema.", "danger");
                    return;
                }

                // Si el método incluye obra social (puede ser opcional)
         /*       string obraSocial = "Sin especificar";*/ //  TextBox 
                negocio.AgregarTurno(idPaciente.Value, fecha, hora, "Reservado");

             
                mostrarMensaje("✅ Turno agregado correctamente.", "success");
                cargarGrilla();
            }
            catch (FormatException)
            {
                mostrarMensaje("⚠️ Formato de fecha u hora inválido. Verifique los campos.", "warning");
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000) // Error RAISERROR del SP
                    mostrarMensaje(ex.Message, "warning");
                else
                    mostrarMensaje("❌ Error al registrar el turno: " + ex.Message, "danger");
            }
        }



        protected void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            // Redirige a la página de pacientes
            Response.Redirect("NuevoPaciente.aspx");
        }


        protected void gvTurnos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelarTurno")
            {
                int idTurno = Convert.ToInt32(e.CommandArgument);

                try
                {
                    ClinicaNegocio negocio = new ClinicaNegocio();
                    negocio.EliminarTurno(idTurno); // 🔹 Llama al SP Eliminar_Turno

                    mostrarMensaje("🗑️ Turno cancelado correctamente.", "success");
                    cargarGrilla(); // 🔄 Refresca la grilla
                }
                catch (Exception ex)
                {
                    mostrarMensaje("❌ Error al cancelar el turno: " + ex.Message, "danger");
                }
            }
        }
        protected void btnCancelarTurno_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya un turno seleccionado
                if (ViewState["TurnoSeleccionado"] == null)

                {
                    mostrarMensaje("⚠️ Debe seleccionar un turno de la grilla para cancelar.", "warning");
                    return;
                }

                int idTurno = Convert.ToInt32(ViewState["TurnoSeleccionado"]);


                ClinicaNegocio negocio = new ClinicaNegocio();
                negocio.EliminarTurno(idTurno); // 🔹 Llama al SP Eliminar_Turno

                mostrarMensaje("🗑️ Turno cancelado correctamente.", "success");
                cargarGrilla(); // 🔄 Refresca la grilla
            }
            catch (Exception ex)
            {
                mostrarMensaje("❌ Error al cancelar el turno: " + ex.Message, "danger");
            }
        }

        protected void gvTurnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Obtener el ID del turno seleccionado
                int idTurno = Convert.ToInt32(gvTurnos.SelectedDataKey.Value);

                // Obtener los valores de la fila seleccionada
                GridViewRow fila = gvTurnos.SelectedRow;

                string nombre = fila.Cells[2].Text;
                string apellido = fila.Cells[3].Text;
                string dni = fila.Cells[4].Text;
                string fecha = fila.Cells[5].Text;
                string hora = fila.Cells[6].Text;
                string estado = fila.Cells[7].Text;
                string ObraSocial = fila.Cells[8].Text;

                // Mostrar un resumen en pantalla (podés también llenar los TextBox)
                mostrarMensaje($"📋 Turno seleccionado: {nombre} {apellido} - {fecha} {hora} ({estado})", "info");

                // Ejemplo: cargar en los TextBox si querés modificar
                txtDni.Text = dni;
                txtFecha.Text = DateTime.Parse(fecha).ToString("yyyy-MM-dd");
                txtHora.Text = hora;
            }
            catch (Exception ex)
            {
                mostrarMensaje("❌ Error al seleccionar el turno: " + ex.Message, "danger");
            }
        }


        protected void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow fila = (GridViewRow)chk.NamingContainer;

            // Deseleccionamos otros checks (solo uno activo a la vez)
            foreach (GridViewRow row in gvTurnos.Rows)
            {
                CheckBox otroChk = (CheckBox)row.FindControl("chkSeleccionar");
                if (otroChk != chk)
                    otroChk.Checked = false;
            }

            // Aplicamos efecto visual
            if (chk.Checked)
            {
                fila.BackColor = System.Drawing.Color.LightCoral;
                fila.Font.Bold = true;

                int idTurno = Convert.ToInt32(gvTurnos.DataKeys[fila.RowIndex].Value);
                ViewState["TurnoSeleccionado"] = idTurno;

                mostrarMensaje("⚠️ Turno seleccionado para cancelación.", "warning");
            }
            else
            {
                fila.BackColor = System.Drawing.Color.White;
                fila.Font.Bold = false;
                ViewState["TurnoSeleccionado"] = null;
            }
        }




    }



}
