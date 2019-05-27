using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OCFG.Data
{
    public class LoginData
    {
        String connectionString = "Server=163.178.173.148; Database=OCFG_DataBase; Uid=lenguajesap; Pwd=lenguajesap;";

        private SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        public string getRolEmployee(string username, string password) {
            string rol = "";

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                String query = "select rol from Officer where user_name = '"+username+ "' AND password_officer = '" + password + "';";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rol = reader.GetString(0);
                    }
                    sqlConnection.Close();
                }
            }

            return rol;

        }
    }
}