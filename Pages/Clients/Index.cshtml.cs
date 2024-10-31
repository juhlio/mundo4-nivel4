using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TransportadoraLTDA.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> ListClients { get; } = [];



        public void OnGet()
        {
            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;\r\n";

                using SqlConnection connection = new(connectionString);
                connection.Open();
                string sql = "SELECT * FROM Clients";
                using SqlCommand command = new(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClientInfo clientInfo = new()
                    {
                        Id = reader.GetInt32(0).ToString(),
                        Name = reader.GetString(1),
                        Empresa = reader.GetString(2),
                        Endereco = reader.GetString(3),
                        Contato = reader.GetString(4)
                    };

                    ListClients.Add(clientInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ClientInfo
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Empresa { get; set; }
        public string? Endereco { get; set; }
        public string? Contato { get; set; }
        public object? ClientID { get; internal set; }
    }
}
