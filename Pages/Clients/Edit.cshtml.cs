using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using TransportadoraLTDA.Pages.Clientes;

namespace Transporte.Pages.Clients
{
    public class EditModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet(string clientID)
        {
            // Verifica se o ID do cliente foi fornecido na URL
            if (string.IsNullOrEmpty(clientID))
            {
                errorMessage = "ID do cliente não fornecido.";
                return;
            }

            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Clients WHERE ClientID=@ClientID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ClientID", clientID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Preenche as informações do cliente
                                clientInfo.Id = reader.GetInt32(0).ToString();
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Empresa = reader.GetString(2);
                                clientInfo.Endereco = reader.GetString(3);
                                clientInfo.Contato = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            // Recebe os dados do formulário
            clientInfo.Id = Request.Form["Id"];
            clientInfo.Name = Request.Form["Name"];
            clientInfo.Empresa = Request.Form["Empresa"];
            clientInfo.Endereco = Request.Form["Endereco"];
            clientInfo.Contato = Request.Form["Contato"];

            // Valida se todos os campos foram preenchidos
            if (string.IsNullOrEmpty(clientInfo.Id) || string.IsNullOrEmpty(clientInfo.Name) || string.IsNullOrEmpty(clientInfo.Empresa) ||
               string.IsNullOrEmpty(clientInfo.Endereco) || string.IsNullOrEmpty(clientInfo.Contato))
            {
                errorMessage = "Preencha todos os campos";
                return;
            }

            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=DArio123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Clients " +
                        "SET Nome=@Nome, Empresa=@Empresa, Endereco=@Endereco, Contato=@Contato " +
                        "WHERE ClientID=@ClientID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Define os parâmetros da consulta SQL
                        command.Parameters.AddWithValue("@Nome", clientInfo.Name);
                        command.Parameters.AddWithValue("@Empresa", clientInfo.Empresa);
                        command.Parameters.AddWithValue("@Endereco", clientInfo.Endereco);
                        command.Parameters.AddWithValue("@Contato", clientInfo.Contato);
                        command.Parameters.AddWithValue("@ClientID", clientInfo.Id);

                        // Executa a consulta SQL
                        command.ExecuteNonQuery();
                    }
                }

                // Define a mensagem de sucesso
                successMessage = "Cliente atualizado com sucesso!";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            // Redireciona para a página de índice após a edição
            Response.Redirect("/Clients/Index");
        }
    }
}
