<%@ Page Title="Pacientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="WebApplication1.Pacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center text-primary mb-4">Listado de Pacientes</h2>

    <asp:Literal ID="mensajeLiteral" runat="server"></asp:Literal>

    <asp:GridView ID="gvPacientes" runat="server"
        CssClass="table table-bordered table-striped shadow-sm"
        AutoGenerateColumns="False" EmptyDataText="No hay pacientes registrados">
        <Columns>
            <asp:BoundField DataField="id_paciente" HeaderText="ID" />
            <asp:BoundField DataField="dni" HeaderText="DNI" />
            <asp:BoundField DataField="nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="telefono" HeaderText="Teléfono" />
            
            <asp:BoundField DataField="ObraSocial" HeaderText="Obra Social" />
        </Columns>
    </asp:GridView>

    <div class="text-center mt-4">
        <asp:Button ID="btnVolver" runat="server" Text="Volver a la Agenda"
            CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
    </div>
</asp:Content>
