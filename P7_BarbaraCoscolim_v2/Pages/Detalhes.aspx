<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalhes.aspx.cs" Inherits="P7_BarbaraCoscolim_v2.Pages.Detalhes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Caçarola Receitas</title>
    <link href="../Styles/Styles.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="FormDeletarReceita" runat="server">
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

            <table>
                <tr>
                    <td>
                        <asp:Label ID="LabelNomeReceita" runat="server" Text="Receita:"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelNomepReceita" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelCategoria" runat="server" Text="Categoria:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelpCategoria" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelModoPreparo" runat="server" Text="Modo de preparo:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelpModoPreparo" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelDificuldade" runat="server" Text="Dificuldade:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelpDificuldade" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelTempoPreparo" runat="server" Text="Tempo de preparo:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelpTempoPreparo" runat="server" Text=""></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelDataRegisto" runat="server" Text="Data de registo:"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelpDataRegisto" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label>&nbsp;</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label>&nbsp;</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelMensagemErro" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>

            <asp:GridView ID="GridViewIngredientes" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="NomeIngrediente" HeaderText="Ingrediente" />
                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                </Columns>
            </asp:GridView>

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
