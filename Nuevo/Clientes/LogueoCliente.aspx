<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LogueoCliente.aspx.cs" Inherits="LogueoCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 361px;
        }
        .auto-style3 {
            width: 154px;
        }
        .auto-style4 {
            width: 260px;
        }
        .auto-style5 {
            text-align: center;
        }
        .auto-style6 {
            width: 499px;
        }
        .auto-style7 {
            width: 100%;
            margin-left: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style7">
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style5" colspan="2">LOGIN</td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">Nro. Pasaporte :</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtPasaporte" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">Contraseña:</td>
            <td class="auto-style4">
                <asp:TextBox ID="txtPass" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
            </td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td class="auto-style4">
                <asp:Button ID="BtnIniciar" runat="server" Text="Iniciar Sesión" OnClick="BtnIniciar_Click" />
            </td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
    </table>
</asp:Content>

