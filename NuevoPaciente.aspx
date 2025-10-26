<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NuevoPaciente.aspx.cs" Inherits="WebApplication1.NuevoPaciente" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <asp:Literal ID="mensajeLiteral" runat="server"></asp:Literal>

    <div class="col-md-6">
        <h3 class="text-primary">Registrar nuevo paciente</h3>
        <div class="card p-4 bg-light shadow-sm mb-4">
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtApellido" class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtDniPaciente" class="form-label">DNI</label>
                <asp:TextBox ID="txtDniPaciente" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label for="txtTelefono" class="form-label">Teléfono</label>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label for="txtObraSocial" class="form-label">Obra Social</label>
                <asp:TextBox ID="txtObraSocial" runat="server" CssClass="form-control" placeholder="Ej: OSDE, IOMA, PAMI"></asp:TextBox>

            </div>
        

            <asp:Button ID="BtnAgregarPaciente" runat="server" Text="Agregar Paciente"
                        CssClass="btn btn-success w-100" OnClick="BtnAgregarPaciente_Click" />
        </div>

            <asp:Button ID="btnVolver" runat="server" Text="Volver a la Agenda"
                         CssClass="btn btn-secondary w-100" OnClick="btnVolver_Click" />
    </div>

</asp:Content>


