<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListarReceitas.aspx.cs" Inherits="P7_BarbaraCoscolim_v2.Pages.ListarReceitas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Caçarola Receitas</title>
    <link href="../Styles/Styles.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <form id="FormListarReceitas" runat="server">
        <div>
            <header>
                <a href="Homepage.html" title="Ca&ccedil;arola Receitas">
                    <img src="../Content/Imagem1.png" width="180" height="180" alt="Logotipo Ca&ccedil;arola Receitas" id="logotipo" />
                </a>
            </header>
        </div>

        <div>
            <p class="pTitle">LISTA DE RECEITAS</p>
            <!-- Navigation controls -->
            <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" SiteMapProvider="SiteMap01" />

            <asp:Menu ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1"></asp:Menu>
            <!-- Colocar tree view para exibir receitas por categoria -->

            <br />
            <br />
            <br />
            <asp:Label ID="LabelMensagemErro" runat="server" CssClass="label" Text=""></asp:Label>
            <!-- Grid view -->
            <asp:GridView ID="GridViewReceitas" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="NomeReceita" HeaderText="Receita" />
                    <asp:BoundField DataField="NomeCategoria" HeaderText="Categoria" />
                    <asp:BoundField DataField="ModoPreparo" HeaderText="Modo de preparo" />
                    <asp:BoundField DataField="Dificuldade" HeaderText="Dificuldade" />
                    <asp:BoundField DataField="TempoPreparo" HeaderText="Tempo de Preparo" />
                    <asp:BoundField DataField="DataRegisto" HeaderText="Data de registo" DataFormatString="{0:dd-MM-yyy}" SortExpression="Date" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButtonDetalhe" runat="server" CommandArgument='<%#Eval("ReceitaID")%>' OnClick="LinkButtonDetalhe_Click">Ver detalhes</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <br />

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
