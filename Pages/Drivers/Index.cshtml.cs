using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Transporte.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        public List<DriversInfo> ListDrivers { get; } = new List<DriversInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string sql = "SELECT * FROM Drivers";
                using SqlCommand command = new SqlCommand(sql, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DriversInfo driversInfo = new DriversInfo()
                    {
                        Id = reader.GetInt32(0).ToString(),
                        Name = reader.GetString(1),
                        CNH = reader.GetString(2),
                        Endereco = reader.GetString(3),
                        Contato = reader.GetString(4)
                    };

                    ListDrivers.Add(driversInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class DriversInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CNH { get; set; }
        public string Endereco { get; set; }
        public string Contato { get; set; }

    }
}
