using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using P7_BarbaraCoscolim_v2;

namespace P7_BarbaraCoscolim_v2.Pages
{
    public partial class InserirReceita : System.Web.UI.Page
    {
        // Instanciar a conexão
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-I5G5799\SQLEXPRESS;Initial Catalog=Receitas_P7_v2;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Carregar dropdown da Categoria (table) e da Dificuldade (enum)
                FazerDropDownCategoria();
                FazerDropDownDificuldade();

                // Colocar data atual como default no calendário
                CalendarDataRegisto.SelectedDate = DateTime.Today;

                Session["ultimoIdTextBox"] = 2;
            } 
            // Se é postback
            else
            {
                // Se a session já tiver tabela armazenada, atribuo-a à tabela do aspx, caso contrário, fica a tabela por default
                if (Session["tableIngredientes"] != null)
                {
                    TableIngredientes = Session["tableIngredientes"] as Table;
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            // Pegar dados da receita do formulário
            string nomeReceita = TextBoxNomeReceita.Text;
            string categoria = DropDownListCategoria.SelectedValue;
            string modoPreparo = TextBoxModoPreparo.Text;
            string dificuldade = DropDownListDificuldade.SelectedValue;
            string tempoPreparo = TextBoxTempoPreparo.Text;
            DateTime dataRegisto = CalendarDataRegisto.SelectedDate;

            // Invocar método para validar se todos os campos foram preenchidos
            bool formValido = Receita.ValidarCamposPreenchidos(nomeReceita, modoPreparo, tempoPreparo);

            // Invocar método para validar se o tempo de preparo inserido é numérico
            int tempoInt = Receita.ValidarTempoPreparo(tempoPreparo);

            bool ingredienteValido = true;

            if (formValido && tempoInt > 0)
            {
                // Pegar último ID das textboxes dos ingredientes para ver quantos existem
                int quantIngredientes = (int)Session["ultimoIdTextBox"];

                // Pega a tabela que está no placeholder em aspx
                Table t = Session["tableIngredientes"] as Table;
                // Table t = PlaceHolderIngredientes.Controls[0] as Table;

                // Lista de ingrediente da receita
                List<Ingrediente> listaIngredientes = new List<Ingrediente>();

                // Pegar dados do ingrediente
                for (int i = 0; i < quantIngredientes; i++)
                {
                    // Vai à linha correspondente ao i, encontra sua célula e pega a textbox
                    TextBox tbIngrediente = t.Rows[i+1].Cells[0].Controls[0] as TextBox;
                    TextBox tbQuantidade = t.Rows[i+1].Cells[1].Controls[0] as TextBox;

                    // Pega o texto das textboxes
                    string nomeIngrediente = tbIngrediente.Text;
                    string quantidadeIngrediente = tbQuantidade.Text;

                    // Valida se os campos não estão vazios
                    ingredienteValido = Ingrediente.ValidarCamposPreenchidos(nomeIngrediente, quantidadeIngrediente);

                    // Se forem válidos
                    if (ingredienteValido)
                    {
                        // Instancia objeto ingrediente
                        Ingrediente ingrediente = new Ingrediente(nomeIngrediente, quantidadeIngrediente);

                        // Adiciona  objeto à lista
                        listaIngredientes.Add(ingrediente);
                    }
                }

                // Converter categoria para int (ID) 
                int.TryParse(categoria, out int categoriaId);

                // Converter dificuldade (string) para EnumDificuldade
                Receita.EnumDificuldade dificuldadeEnum = (Receita.EnumDificuldade)Enum.Parse(typeof(Receita.EnumDificuldade), dificuldade);

                // Instanciar objeto receita
                Receita receita = new Receita(nomeReceita, categoriaId, modoPreparo, dificuldadeEnum, tempoInt, dataRegisto, listaIngredientes);

                // Invocar método para adicionar nova receita
                receita.InserirReceita(sqlConnection);

                // Limpa os campos do formulário
                ResetForm();

                // Verifica se foi realizada uma inserção ou atualização e coloca a mensagem adequada na label
                LabelMensagemSucesso.Text = "Inserido com sucesso!";
            }
            else if (!formValido || tempoInt <= 0 || ingredienteValido)
            {
                LabelMensagemSucesso.Text = string.Empty;
                LabelMensagemErro.Text = "ERRO: Preencha todos os campos corretamente!";
            }

        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            // Limpar campos do formulário
            ResetForm();
        }

        public void FazerDropDownCategoria()
        {
            // Caso a conexão esteja fechada, abre a conexão
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Cria um comando a partir da sp
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("uspCategoriaViewAll", sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Preenche a dataTable com as informações do dataAdapter
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            // Fecha a conexão
            sqlConnection.Close();

            // Pegar os dados da tabela e popular o dropdown
            DropDownListCategoria.DataSource = dataTable;
            DropDownListCategoria.DataTextField = "NomeCategoria";
            DropDownListCategoria.DataValueField = "CategoriaID";
            DropDownListCategoria.DataBind();

            // Adiciona texto por default
            DropDownListCategoria.Items.Insert(0, "Selecione...");
        }

        public void FazerDropDownDificuldade()
        {
            Array dificuldades = Enum.GetValues(typeof(Receita.EnumDificuldade));
            DropDownListDificuldade.Items.Insert(0, "Selecione...");
            foreach (Receita.EnumDificuldade item in dificuldades)
            {
                DropDownListDificuldade.Items.Add(new ListItem(item.ToString()));
            }
        }

        public void ResetForm()
        {
            HiddenFieldReceitaID.Value = string.Empty;
            TextBoxNomeReceita.Text = string.Empty;
            DropDownListCategoria.SelectedIndex = 0;
            TextBoxModoPreparo.Text = string.Empty;
            DropDownListDificuldade.SelectedIndex = 0;
            TextBoxTempoPreparo.Text = string.Empty;
            CalendarDataRegisto.SelectedDate = DateTime.Today;

            LabelMensagemSucesso.Text = string.Empty;
            LabelMensagemErro.Text = string.Empty;
        }

        protected void ButtonAddIngredient_Click(object sender, EventArgs e)
        {
            // Calculo o id atual
            int id = (int)Session["ultimoIdTextBox"] + 1;

            // Crio uma nova tabela
            Table tableIngredientes;

            // Se não houver tabela armazenada na session, uso a tabela por default, criada no aspx
            if (Session["tableIngredientes"] == null)
            {
                tableIngredientes = TableIngredientes;    
            } 
            // Se já houver tabela armazenada na session, eu a recupero
            else
            {
                tableIngredientes = Session["tableIngredientes"] as Table;
            }

            // Cria as textboxes
            TextBox tbNovoIngrediente = new TextBox();
            TextBox tbNovaQuantidade = new TextBox();

            // Adiciona o evento às textbox
            tbNovoIngrediente.TextChanged += new EventHandler(TextBoxIngredient_TextChanged);
            tbNovaQuantidade.TextChanged += new EventHandler(TextBoxIngredient_TextChanged);

            // Configura os Id da textboxes
            tbNovoIngrediente.ID = $"TextBoxIngrediente{id}";
            tbNovaQuantidade.ID = $"TextBoxQuantidade{id}";

            // Cria a linha e as células da tablea
            TableRow row = new TableRow();
            TableCell cellIngrediente = new TableCell();
            TableCell cellQuantidade = new TableCell();

            // Adiciona as textboxes às células
            cellIngrediente.Controls.Add(tbNovoIngrediente);
            cellQuantidade.Controls.Add(tbNovaQuantidade);

            // Adiciona as células às linhas
            row.Cells.Add(cellIngrediente);
            row.Cells.Add(cellQuantidade);

            // Adiciona a linha à tabela
            tableIngredientes.Rows.Add(row);

            // Remove o que já existe no placeholder e adiciona a tabela
            PlaceHolderIngredientes.Controls.RemoveAt(0);
            PlaceHolderIngredientes.Controls.Add(tableIngredientes);

            // Armazeno a tabela na session
            Session["tableIngredientes"] = tableIngredientes;

            // Armazeno o id na session
            Session["ultimoIdTextBox"] = id;
        }

        // ERRO --> EVENTO NUNCA DISPARA
        // O objetivo era atualizar a Session sempre que houvesse alteração nas texbox, para conseguir salvar os ingredientes inseridos nas textbox adicionadas dinamicamente
        // Sem conseguir isso, apenas os dois primeiros ingredientes (provenientes das textboxes adicionadas em aspx) são adicionados à base de dados.
        protected void TextBoxIngredient_TextChanged(object sender, EventArgs e)
        {
            if (Session["tableIngredientes"] == null) return;

            TextBox tb = sender as TextBox;
            
            Table t = Session["tableIngredientes"] as Table;

            for (int i = 1; i < t.Rows.Count; i++)
            {
                TextBox ingrediente = (t.Rows[i].Cells[0].Controls[0] as TextBox);
                TextBox quantidade = (t.Rows[i].Cells[1].Controls[0] as TextBox);

                if (ingrediente.ID == tb.ID)
                {
                    ingrediente.Text = tb.Text;
                }

                if (quantidade.ID == tb.ID)
                {
                    quantidade.Text = tb.Text;
                }
            }
        }
    }
}