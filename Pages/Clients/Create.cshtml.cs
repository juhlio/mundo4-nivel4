using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using TransportadoraLTDA.Pages.Clientes;

namespace Transporte.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientInfo clientInfo { get; set; } = new ClientInfo();
        public string errorMessage { get; set; } = "";
        public string successMessage { get; set; } = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            clientInfo.Id = Request.Form["Id"];
            clientInfo.Name = Request.Form["Name"];
            clientInfo.Empresa = Request.Form["Empresa"];
            clientInfo.Endereco = Request.Form["Endereco"];
            clientInfo.Contato = Request.Form["Contato"];

            if (clientInfo.Id.Length == 0 || clientInfo.Name.Length == 0 || clientInfo.Empresa.Length == 0 ||
                clientInfo.Endereco.Length == 0 || clientInfo.Contato.Length == 0)
            {
                errorMessage = "Preencha todos os campos";
                return;
            }

            //

            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "INSERT INTO Clients (ClientID, Nome, Empresa, Endereco, Contato) VALUES (@ClientID, @Nome, @Empresa, @Endereco, @Contato);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ClientID", clientInfo.Id);
                        command.Parameters.AddWithValue("@Nome", clientInfo.Name);
                        command.Parameters.AddWithValue("@Empresa", clientInfo.Empresa);
                        command.Parameters.AddWithValue("@Endereco", clientInfo.Endereco);
                        command.Parameters.AddWithValue("@Contato", clientInfo.Contato);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Novo Cliente Adicionado com sucesso";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
    }
}
