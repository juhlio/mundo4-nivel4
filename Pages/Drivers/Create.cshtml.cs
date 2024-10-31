using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Transporte.Pages.Drivers;

namespace Transporte.Pages.Drivers
{
    public class CreateModel : PageModel
    {
        public DriversInfo DriversInfo { get; set; } = new DriversInfo();
        public string ErrorMessage { get; set; } = "";
        public string SuccessMessage { get; set; } = "";

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DriversInfo.Id = Request.Form["Id"];
            DriversInfo.Name = Request.Form["Name"];
            DriversInfo.CNH = Request.Form["CNH"];
            DriversInfo.Endereco = Request.Form["Endereco"];
            DriversInfo.Contato = Request.Form["Contato"];

            if (string.IsNullOrEmpty(DriversInfo.Id) || string.IsNullOrEmpty(DriversInfo.Name) || string.IsNullOrEmpty(DriversInfo.CNH) ||
                string.IsNullOrEmpty(DriversInfo.Endereco) || string.IsNullOrEmpty(DriversInfo.Contato))
            {
                ErrorMessage = "Preencha todos os campos";
                return Page();
            }

            try
            {
                string connectionString = "Server=tcp:banco.database.windows.net,1433;Initial Catalog=banco;Persist Security Info=False;User ID=azbanco;Password=dariojunior123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Drivers (DriverID, Nome, CNH, Endereco, Contato) VALUES (@DriverID, @Nome, @CNH, @Endereco, @Contato);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DriverID", DriversInfo.Id);
                        command.Parameters.AddWithValue("@Nome", DriversInfo.Name);
                        command.Parameters.AddWithValue("@CNH", DriversInfo.CNH);
                        command.Parameters.AddWithValue("@Endereco", DriversInfo.Endereco);
                        command.Parameters.AddWithValue("@Contato", DriversInfo.Contato);

                        command.ExecuteNonQuery();
                    }
                }

                SuccessMessage = "Novo Motorista adicionado com sucesso";
                return RedirectToPage("/Drivers/Index"); // Redireciona para a página de lista de motoristas
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
