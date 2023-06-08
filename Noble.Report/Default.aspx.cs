using Newtonsoft.Json;
using Noble.Report.NobleDefaultServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Noble.Report {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (Request.HttpMethod == "POST")
            {
                var myData = new ModuleWiseClaimsLookupModel();
                var jsonString = new StreamReader(Request.InputStream).ReadToEnd();
                myData = JsonConvert.DeserializeObject<ModuleWiseClaimsLookupModel>(jsonString);
                var clientCompanyId = myData.CompanyId;
                var serverPath = myData.TokenName;

                string connectionString = ConfigurationManager.ConnectionStrings["ServerString"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))

                {
                    connection.Open();
                    {
                        var token1 = JsonConvert.SerializeObject(myData);

                        string sqlQueryUpdate = $"Update ReportT SET Token='{token1}', [ServerName]='{serverPath}' Where CompanyId='{clientCompanyId}'";
                        using (SqlCommand command12 = new SqlCommand(sqlQueryUpdate, connection))
                        {
                            int rowsAffected = command12.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                connection.Close();
                            }
                            else
                            {
                                connection.Close();

                            }

                        }

                    }
                    connection.Open();
                    string sqlQuery = "SELECT * FROM ReportT Where CompanyId='" + clientCompanyId + "'";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {

                            string companyId = reader["CompanyId"].ToString();

                            if (clientCompanyId == Guid.Parse(companyId))
                            {


                                string token = reader["Token"].ToString();

                                myData = JsonConvert.DeserializeObject<ModuleWiseClaimsLookupModel>(token);

                            }
                            else
                            {
                                var token1 = JsonConvert.SerializeObject(myData);
                                string sqlQuery1 = "INSERT INTO ReportT (CompanyId, Token) VALUES (@CompanyId, @Token)";

                                using (SqlCommand command1 = new SqlCommand(sqlQuery1, connection))
                                {
                                    // Add parameters to the command
                                    command1.Parameters.AddWithValue("@CompanyId", clientCompanyId);
                                    command1.Parameters.AddWithValue("@ServerName", serverPath);
                                    command1.Parameters.AddWithValue("@Token", token1);

                                    // Execute the query
                                    int rowsAffected = command1.ExecuteNonQuery();

                                    // Check if the insertion was successful
                                    if (rowsAffected > 0)
                                    {
                                        // Data saved successfully
                                    }
                                    else
                                    {
                                        // Failed to save data
                                    }
                                }

                            }
                            // ...
                        }

                        if (!reader.HasRows)
                        {
                            connection.Close();
                            var token12 = JsonConvert.SerializeObject(myData);
                            string sqlQuery12 = "INSERT INTO ReportT (CompanyId, Token,ServerName) VALUES (@CompanyId, @Token,@ServerName)";
                            //using (SqlConnection sqlConnection = new SqlConnection(connectionString))

                            {

                                connection.Open();

                                using (SqlCommand command12 = new SqlCommand(sqlQuery12, connection))
                                {
                                    // Add parameters to the command
                                    command12.Parameters.AddWithValue("@CompanyId", clientCompanyId);
                                    command12.Parameters.AddWithValue("@ServerName", serverPath);
                                    command12.Parameters.AddWithValue("@Token", token12);

                                    // Execute the query
                                    int rowsAffected = command12.ExecuteNonQuery();

                                    // Check if the insertion was successful
                                    if (rowsAffected > 0)
                                    {
                                        //sqlConnection.Close();
                                        // Data saved successfully
                                    }
                                    else
                                    {
                                        //sqlConnection.Close();

                                        // Failed to save data
                                    }
                                }
                            }

                        }



                        // Close the reader
                        reader.Close();
                    }
                    connection.Close();
                }

                Session["Token"] = myData.Token;

            }

        }
    }
}