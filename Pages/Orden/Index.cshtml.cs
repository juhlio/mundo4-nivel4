using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Transporte.Pages.Orden
{
    public class IndexModel : PageModel
    {
        public List<OrdenInfo> ListOrden { get; set; }

        public void OnGet()
        {
            ListOrden = new List<OrdenInfo>();

            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Orders";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                OrdenInfo ordenInfo = new OrdenInfo
                                {
                                    Id = reader.GetInt32(0).ToString(),
                                    IdCliente = reader.GetInt32(1).ToString(),
                                    IdDriver = reader.GetInt32(2).ToString(),
                                    Detalhes = reader.GetString(3),
                                    Data = reader.GetDateTime(4).ToString("yyyy-MM-dd"),
                                    Status = reader.GetString(5)
                                };

                                ListOrden.Add(ordenInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                // Você pode querer lançar uma exceção ou lidar com o erro de outra forma aqui
            }
        }
    }

    public class OrdenInfo
    {
        public string Id { get; set; }
        public string IdCliente { get; set; }
        public string IdDriver { get; set; }
        public string Detalhes { get; set; }
        public string Data { get; set; }
        public string Status { get; set; }
    }
}
