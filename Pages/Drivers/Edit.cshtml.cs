using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Transporte.Pages.Drivers;

namespace Transporte.Pages.Drivers
{
    public class EditModel : PageModel
    {
        public DriversInfo driversInfo = new DriversInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet(string driverID)
        {
            if (string.IsNullOrEmpty(driverID))
            {
                errorMessage = "ID do motorista não fornecido.";
                return;
            }

            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Drivers WHERE DriverID=@DriverID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", driverID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                driversInfo.Id = reader.GetInt32(0).ToString();
                                driversInfo.Name = reader.GetString(1);
                                driversInfo.CNH = reader.GetString(2);
                                driversInfo.Endereco = reader.GetString(3);
                                driversInfo.Contato = reader.GetString(4);
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

        public IActionResult OnPost()
        {
            driversInfo.Id = Request.Form["Id"];
            driversInfo.Name = Request.Form["Name"];
            driversInfo.CNH = Request.Form["CNH"];
            driversInfo.Endereco = Request.Form["Endereco"];
            driversInfo.Contato = Request.Form["Contato"];

            if (string.IsNullOrEmpty(driversInfo.Id) || string.IsNullOrEmpty(driversInfo.Name) || string.IsNullOrEmpty(driversInfo.CNH) ||
                string.IsNullOrEmpty(driversInfo.Endereco) || string.IsNullOrEmpty(driversInfo.Contato))
            {
                errorMessage = "Preencha todos os campos";
                return Page();
            }

            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "UPDATE Drivers " +
                        "SET Nome=@Nome, CNH=@CNH, Endereco=@Endereco, Contato=@Contato " +
                        "WHERE DriverID=@DriverID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", driversInfo.Name);
                        command.Parameters.AddWithValue("@CNH", driversInfo.CNH);
                        command.Parameters.AddWithValue("@Endereco", driversInfo.Endereco);
                        command.Parameters.AddWithValue("@Contato", driversInfo.Contato);
                        command.Parameters.AddWithValue("@DriverID", driversInfo.Id);

                        command.ExecuteNonQuery();
                    }
                }

                successMessage = "Motorista atualizado com sucesso!";
                return RedirectToPage("/Drivers/Index");
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return Page();
            }
        }
    }
}
