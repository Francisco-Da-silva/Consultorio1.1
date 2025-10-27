Consultorio 1.0 — Gestión de Turnos

Sistema Web Forms (.NET Framework 4.8) para gestionar pacientes y turnos de un consultorio (rol Médico / Secretaría), con autenticación por formularios, grillas y procedimientos almacenados en SQL Server.

🧱 Tech stack

ASP.NET Web Forms (.NET Framework 4.8)

C#, ADO.NET (SqlClient)

SQL Server (SQLExpress en desarrollo)

Bootstrap 5 (CDN)

IIS Express / IIS

🧭 Navegación principal

Turnos (About.aspx): ver, reservar, cancelar.

Pacientes (Pacientes.aspx): administración de pacientes.

Nuevo Paciente (NuevoPaciente.aspx): alta con ObraSocial.

Login (Login.aspx).

🧩 Puntos clave de funcionalidad

Reservar turno: busca paciente por DNI → valida solapamiento (fecha+hora) → inserta turno.

Cancelar turno: botón asociado a la fila seleccionada (usa DataKeyNames="id_turno").

Mensajes al usuario: Literal con alertas Bootstrap.

Grillas: GridView con actualización tras inserciones y cancelaciones.

☁️ Deploy a producción
Opción A: App Service + Azure SQL (recomendada)

Publicar Web a Azure App Service.

Migrar DB a Azure SQL (bacpac / DAC / script).

Cambiar connectionStrings para apuntar a Azure SQL (usuario/clave SQL).

Opción B: App Service + SQL on-premise

Exponer tu SQL local (VPN/Firewall) → no recomendado por seguridad/latencia.

🧪 Pruebas rápidas

Login

Médico: medico1 / 1234

Secretaría: secretaria1 / abcd

Alta paciente

Cargar Nombre/Apellido/DNI/ObraSocial.

Turno

Ingresar DNI, elegir fecha y hora → no permite duplicar (fecha+hora).

🛠️ Troubleshooting

401 / 403 / 500.19: revisar web.config por secciones duplicadas (sessionState, authentication, etc.).

No se cargó tipo Global: verificar Global.asax y Global.asax.cs (anidados y Inherits="Consultorio1._0.Global").

CodeDom provider: instalar paquete NuGet
Microsoft.CodeDom.Providers.DotNetCompilerPlatform y ajustar <system.codedom>.

Timeout SQL: verificar instancia .\SQLEXPRESS, servicio levantado y cadena de conexión.

📜 Licencia

Este proyecto se distribuye con fines educativos. Adaptalo libremente a tus necesidades.
