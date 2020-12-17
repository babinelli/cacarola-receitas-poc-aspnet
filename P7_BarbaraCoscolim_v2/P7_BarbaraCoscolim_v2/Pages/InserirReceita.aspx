<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InserirReceita.aspx.cs" Inherits="P7_BarbaraCoscolim_v2.Pages.InserirReceita" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Caçarola Receitas</title>
    <link href="../Styles/Styles.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="FormInserirReceita" runat="server">
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

            <!-- Hidden Field para ReceitaID -->
            <asp:HiddenField ID="HiddenFieldReceitaID" runat="server" />

            <table>
                <tr>
                    <td colspan="2">
                        <p class="pTitle">INSERIR RECEITA</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelNomeReceita" runat="server" Text="Nome da receita:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxNomeReceita" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelCategoria" runat="server" Text="Categoria:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListCategoria" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:PlaceHolder ID="PlaceHolderIngredientes" runat="server">
                            <asp:Table ID="TableIngredientes" runat="server">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell>Ingrediente:</asp:TableHeaderCell>
                                    <asp:TableHeaderCell>Quantidade:</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxIngrediente1" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxQuantidade1" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxIngrediente2" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="TextBoxQuantidade2" runat="server"></asp:TextBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="ButtonAddIngredient" runat="server" Text="Adicionar ingrediente" OnClick="ButtonAddIngredient_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelModoPreparo" runat="server" Text="Modo de preparo:"></asp:Label>
                    </td>
                    <td>

                        <asp:TextBox ID="TextBoxModoPreparo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelDificuldade" runat="server" Text="Dificuldade:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListDificuldade" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelTempoPreparo" runat="server" Text="Tempo de preparo:" ToolTip="Tempo em minutos"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxTempoPreparo" runat="server" ToolTip="Tempo em minutos"></asp:TextBox>
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
                        <asp:Calendar ID="CalendarDataRegisto" SelectedDate="" runat="server"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <label>&nbsp;</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="ButtonAdd" runat="server" Text="Adicionar" OnClick="ButtonAdd_Click" />
                        <asp:Button ID="ButtonClear" runat="server" Text="Limpar" OnClick="ButtonClear_Click" />
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
