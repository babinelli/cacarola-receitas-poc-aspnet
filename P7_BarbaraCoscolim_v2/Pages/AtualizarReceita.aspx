<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AtualizarReceita.aspx.cs" Inherits="P7_BarbaraCoscolim_v2.Pages.AtualizarReceita" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Caçarola Receitas</title>
    <link href="../Styles/Styles.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>

    <form id="FormAtualizarReceita" runat="server">
        <div>
            <header>
                <a href="Homepage.html" title="Ca&ccedil;arola Receitas">
                    <img src="../Content/Imagem1.png" width="180" height="180" alt="Logotipo Ca&ccedil;arola Receitas" id="logotipo" />
                </a>
            </header>
        </div>

        <div>
            <!-- Navigation controls -->
            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" SiteMapProvider="SiteMap01" />

            <asp:Menu ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1"></asp:Menu>

            <br />
            <br />

            <!-- Pedir para utilizador escolher o ID da receita antes de mostrar/preencher o formulário abaixo -->
            <table>
                <tr>
                    <td colspan="2">
                        <p class="pTitle">ATUALIZAR RECEITA</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelReceitaIDAtualizar" runat="server" Text="Receita para atualizar:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListReceitaIDAtualizar" runat="server" OnSelectedIndexChanged="DropDownListReceitaIDAtualizar_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelNomeReceita" runat="server" Text="Nome da receita:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNomeReceita" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelCategoria" runat="server" Text="Categoria:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListCategoria" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelModoPreparo" runat="server" Text="Modo de preparo:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxModoPreparo" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelDificuldade" runat="server" Text="Dificuldade:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListDificuldade" runat="server" Enabled="false"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelTempoPreparo" runat="server" Text="Tempo de preparo:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxTempoPreparo" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="LabelDataRegisto" runat="server" Text="Data de registo:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Calendar ID="CalendarDataRegisto" SelectedDate="" runat="server" Enabled="false"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label>&nbsp;</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="ButtonUpdate" runat="server" Text="Atualizar" OnClick="ButtonUpdate_Click" Enabled="false" />
                        <asp:Button ID="ButtonTrocarSelecaoReceita" runat="server" Text="Selecionar outra receita" OnClick="ButtonTrocarSelecaoReceita_Click" Enabled=" false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label>&nbsp;</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="LabelMensagemSucesso" runat="server" CssClass="FontColorGreen" Text=""></asp:Label>
                        <br />
                        <asp:Label ID="LabelMensagemErro" runat="server" CssClass="FontColorRed" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <footer>
            <div class="footer">
                <p></p>
                <p>
                    <a href="mailto:contact@cacarolareceitas.com?subject=Contact%20us">contact@cacarolareceitas.com </a>
                </p>
                <p>&copy; Ca&ccedil;arola Receitas</p>
            </div>
        </footer>
    </form>
</body>
</html>
