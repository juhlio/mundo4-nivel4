using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Transporte.Pages.Orden
{
    public class CreateModel : PageModel
    {
        private readonly string _connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public List<ClienteInfo> ListClientes { get; set; }
        public List<DriverInfo> ListDrivers { get; set; }

        public void OnGet()
        {
            ListClientes = LoadClientes("");
            ListDrivers = LoadDrivers("");
        }

        public IActionResult OnPost(string detalhesPedido, DateTime dataEntrega, string status, int clienteID, int driverID)
        {
            try
            {
                SalvarOrdem(detalhesPedido, dataEntrega, status, clienteID, driverID);
                return RedirectToPage("/Orden/Index");
            }
            catch (Exception ex)
            {
                // Tratar exceção aqui, se necessário
                return RedirectToPage("/Error");
            }
        }

        private void SalvarOrdem(string detalhesPedido, DateTime dataEntrega, string status, int clienteID, int driverID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Orders (DetalhesPedido, DataEntrega, Status, ClientID, DriverID) VALUES (@DetalhesPedido, @DataEntrega, @Status, @ClientID, @DriverID)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@DetalhesPedido", detalhesPedido);
                    command.Parameters.AddWithValue("@DataEntrega", dataEntrega);
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@ClientID", clienteID);
                    command.Parameters.AddWithValue("@DriverID", driverID);

                    command.ExecuteNonQuery();
                }
            }
        }

        private List<ClienteInfo> LoadClientes(string searchTerm)
        {
            var clientes = new List<ClienteInfo>();

            string sql = "SELECT ClientID, Nome, Empresa, Endereco, Contato FROM Clients WHERE Nome LIKE @SearchTerm";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ClienteInfo cliente = new ClienteInfo
                            {
                                ClientID = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Empresa = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Endereco = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Contato = reader.IsDBNull(4) ? null : reader.GetString(4)
                            };
                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }

        private List<DriverInfo> LoadDrivers(string searchTerm)
        {
            var drivers = new List<DriverInfo>();

            string sql = "SELECT DriverID, Nome, CNH, Endereco, Contato FROM Drivers WHERE Nome LIKE @SearchTerm";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DriverInfo driver = new DriverInfo
                            {
                                DriverID = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                CNH = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Endereco = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Contato = reader.IsDBNull(4) ? null : reader.GetString(4)
                            };
                            drivers.Add(driver);
                        }
                    }
                }
            }

            return drivers;
        }
    }

    public class ClienteInfo
    {
        public int ClientID { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public string Endereco { get; set; }
        public string Contato { get; set; }
    }

    public class DriverInfo
    {
        public int DriverID { get; set; }
        public string Nome { get; set; }
        public string CNH { get; set; }
        public string Endereco { get; set; }
        public string Contato { get; set; }
    }
}
