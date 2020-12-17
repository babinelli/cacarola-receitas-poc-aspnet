using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P7_BarbaraCoscolim_v2.Pages
{
    public partial class AtualizarReceita : System.Web.UI.Page
    {
        // Instanciar a conexão
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-I5G5799\SQLEXPRESS;Initial Catalog=Receitas_P7_v2;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // Carregar dropdowns: escolha do ID (table), Categoria (table) e Dificuldade (enum)
                FazerDropDownReceitaID();
                FazerDropDownCategoria();
                FazerDropDownDificuldade();

                // Colocar data atual como default no calendário
                CalendarDataRegisto.SelectedDate = DateTime.Today;
            }
        }

        protected void DropDownListReceitaIDAtualizar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Desabilita seleção de receita
            DropDownListReceitaIDAtualizar.Enabled = false;

            // Habilita botão para trocar receita selecionada
            ButtonTrocarSelecaoReceita.Enabled = true;

            // Pegar id selecionado
            string receitaField = DropDownListReceitaIDAtualizar.SelectedValue;

            // Converter receitaId para int
            int.TryParse(receitaField, out int receitaId);

            // Buscar receita a partir do ID
            Receita receita = Receita.BuscarReceita(sqlConnection, receitaId);

            // Preencher formulário
            TextBoxNomeReceita.Text = receita.NomeReceita;
            DropDownListCategoria.SelectedValue = receita.CategoriaID.ToString();
            TextBoxModoPreparo.Text = receita.ModoPreparo;
            DropDownListDificuldade.SelectedValue = receita.Dificuldade.ToString();
            TextBoxTempoPreparo.Text = receita.TempoPreparo.ToString();
            CalendarDataRegisto.SelectedDate = receita.DataRegisto;

            // Habilitar formulário
            TextBoxNomeReceita.Enabled = true;
            DropDownListCategoria.Enabled = true;
            TextBoxModoPreparo.Enabled = true;
            DropDownListDificuldade.Enabled = true;
            TextBoxTempoPreparo.Enabled = true;
            CalendarDataRegisto.Enabled = true;

            // Habilitar botão atualizar
            ButtonUpdate.Enabled = true;
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            // Pegar dados do formulário
            string receitaField = DropDownListReceitaIDAtualizar.SelectedValue; // Nunca será vazio, pois carrego a instância da receita que já está na BD
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

            if (!formValido || tempoInt <= 0 || DropDownListReceitaIDAtualizar.SelectedIndex == 0)
            {
                LabelMensagemSucesso.Text = string.Empty;
                LabelMensagemErro.Text = "ERRO: Preencha todos os campos corretamente!";
            }
            else
            {
                int.TryParse(receitaField, out int receitaId);

                // Converter categoria para int (ID) 
                int.TryParse(categoria, out int categoriaId);

                // ESTÁ A SALVAR O ÍNDICE E NÃO O NOME DA DIFICULDADE
                // Converter dificuldade (string) para EnumDificuldade
                Receita.EnumDificuldade dificuldadeEnum = Receita.EnumDificuldade.Fácil;
                Array dificuldades = Enum.GetValues(typeof(Receita.EnumDificuldade));
                foreach (Receita.EnumDificuldade item in dificuldades)
                {
                    if (item.ToString() == dificuldade)
                    {
                        dificuldadeEnum = item;
                        break;
                    }
                }

                // Instanciar objeto receita
                Receita receita = new Receita(receitaId, nomeReceita, categoriaId, modoPreparo, dificuldadeEnum, tempoInt, dataRegisto);

                // Invocar método para adicionar nova receita
                receita.AtualizarReceita(sqlConnection);

                // Limpa os campos do formulário
                ResetForm();

                // Verifica se foi realizada uma inserção ou atualização e coloca a mensagem adequada na label
                LabelMensagemSucesso.Text = "Atualizado com sucesso!";
            }
        }

        protected void ButtonTrocarSelecaoReceita_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        public void FazerDropDownReceitaID()
        {
            // Caso a conexão esteja fechada, abre a conexão
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // Criar um comando a partir da sp
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("uspReceitaIDViewAll", sqlConnection);
            sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Preenche a dataTable com as informações do dataAdapter
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            // Fecha a conexão
            sqlConnection.Close();

            // Habilitar dropdown para escolha da receita (mais uma garantia)
            DropDownListReceitaIDAtualizar.Enabled = true;

            // Pegar os dados da tabela e popular o dropdown
            DropDownListReceitaIDAtualizar.DataSource = dataTable;
            DropDownListReceitaIDAtualizar.DataTextField = "NomeReceita";
            DropDownListReceitaIDAtualizar.DataValueField = "ReceitaID";
            DropDownListReceitaIDAtualizar.DataBind();

            // Adiciona texto por default
            DropDownListReceitaIDAtualizar.Items.Insert(0, "Selecione...");
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
            // Limpar campos do formulário
            DropDownListReceitaIDAtualizar.SelectedIndex = 0;
            TextBoxNomeReceita.Text = string.Empty;
            TextBoxModoPreparo.Text = string.Empty;
            TextBoxTempoPreparo.Text = string.Empty;
            CalendarDataRegisto.SelectedDate = DateTime.Today;
            LabelMensagemSucesso.Text = string.Empty;
            LabelMensagemErro.Text = string.Empty;

            // Habilita dropdown para escolha da receita
            DropDownListReceitaIDAtualizar.Enabled = true;

            // Desabilitar formulário
            TextBoxNomeReceita.Enabled = false;
            DropDownListCategoria.Enabled = false;
            TextBoxModoPreparo.Enabled = false;
            DropDownListDificuldade.Enabled = false;
            TextBoxTempoPreparo.Enabled = false;
            CalendarDataRegisto.Enabled = false;

            // Desabilitar botões
            ButtonUpdate.Enabled = false;
            ButtonTrocarSelecaoReceita.Enabled = false;
        }
    }
}