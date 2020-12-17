<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="P7_BarbaraCoscolim_v2.Pages.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Caçarola Receitas</title>
    <link href="../Styles/Styles.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
</head>
<body>
    <!-- Navigation controls -->
    <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" SiteMapProvider="SiteMap01" />

    <form id="FormHome" runat="server">
        <div>
            <header>
                <a href="Homepage.html" title="Ca&ccedil;arola Receitas">
                    <img src="../Content/Imagem1.png" width="180" height="180" alt="Logotipo Ca&ccedil;arola Receitas" id="logotipo" />
                </a>
            </header>
        </div>

        <div class="DivMenu">
            <asp:Menu ID="Menu1" runat="server" DataSourceID="SiteMapDataSource1"></asp:Menu>
            <br />
            <br />
            <br />
        </div>

        <div class="DivContent">
            <img src="../Content/bacalhau-rei-do-natal.jpg" class="home-img" alt="Bacalhau de natal"/>
            <p class="bottomleft">Bacalhau de natal</p>
            <p id="pHomeContent">Os pratos de bacalhau podem ser feitos com lombos (sua parte mais nobre), postas (todo o restante do peixe) ou lascas (partes boas que sobram nos cortes das postas e são vendidas por preços muito mais baixos), em receitas com poucos acompanhamentos ou naquelas que quase dispensam outros pratos à mesa. Uma coisa é certa: ele é garantia de sucesso com a imensa maioria dos convidados.</p>
        </div>

        <footer>
            <div class="footer">

                <a href="mailto:contact@cacarolareceitas.com?subject=Contact%20us">contact@cacarolareceitas.com </a>

                <p>&copy; Ca&ccedil;arola Receitas</p>
            </div>
        </footer>
    </form>
</body>
</html>
