<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="WebApplication1.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <asp:Literal ID="mensajeLiteral" runat="server"></asp:Literal>



    <h2 class="text-center text-primary mb-4">Gestión de Turnos</h2>

    <!-- Grilla de Turnos -->
    <div class="mb-4">


        <asp:GridView ID="gvTurnos" runat="server"  AutoGenerateColumns ="false"  CssClass="table table-striped" DataKeyNames="Id_turno" 
                      OnSelectedIndexChanged  ="gvTurnos_SelectedIndexChanged">

            <Columns>
                <asp:BoundField DataField="Id_turno" HeaderText="N° Turno" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="Dni" HeaderText="DNI" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Hora" HeaderText="Hora" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                
                <asp:TemplateField HeaderText="Elegir">
                <ItemTemplate>
                <asp:CheckBox ID="chkSeleccionar" runat="server" 
                 AutoPostBack="true" 
                 OnCheckedChanged="chkSeleccionar_CheckedChanged" />
               </ItemTemplate>
               </asp:TemplateField>


            </Columns>
        </asp:GridView>
    

    



        <div class="row mt-4">

    <!--  Columna izquierda: Formulario Reservar Turno -->
    <div class="col-md-6">
        <h3 class="text-primary">Reservar nuevo turno</h3>
        <div class="card p-4 shadow-sm bg-light mb-4">
            <div class="mb-3">
                <label for="txtDni" class="form-label">DNI del paciente</label>
                <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" placeholder="Ingrese el DNI del paciente"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtFecha" class="form-label">Fecha del turno</label>
                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtHora" class="form-label">Hora del turno</label>
                <asp:TextBox ID="txtHora" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
            </div>

            <asp:Button ID="btnReservar" runat="server" Text="Reservar Turno"
                        CssClass="btn btn-primary w-100" OnClick="btnReservar_Click" />
        </div>
    </div>
            
    <!--  Columna derecha: Botones en tarjetas -->
    <div class="col-md-5 offset-md-1">

          <h3 class="text-primary">Nuevo Paciente/ Cancelar Turno</h3>

        <!-- Nuevo Paciente -->
        <div class="card p-4 shadow-sm bg-light mb-4 text-center">
            <h4 class="text-success mb-3">Nuevo Paciente</h4>
            <p class="text-muted">Registrar un nuevo paciente en el sistema</p>
            <asp:Button ID="Button1" runat="server" Text="Agregar Nuevo Paciente"
                        CssClass="btn btn-success w-100" OnClick="btnNuevoPaciente_Click" />
        </div>

        <!--  Cancelar Turno -->
        <div class="card p-4 shadow-sm bg-light text-center">
            <h4 class="text-danger mb-3">Cancelar Turno</h4>
            <p class="text-muted">Anular el turno seleccionado</p>
            <asp:Button ID="Button2" runat="server" Text="Cancelar Turno Seleccionado"
                        CssClass="btn btn-danger w-100" OnClick="btnCancelarTurno_Click" />
        </div>
    </div>

</div>
        

        </div>
    
</asp:Content>
