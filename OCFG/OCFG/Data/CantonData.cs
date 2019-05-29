using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;

namespace OCFG.Data
{
    public class CantonData
    {
        String connectionString = "Server=163.178.173.148; Database=OCFG_DataBase; Uid=lenguajesap; Pwd=lenguajesap;";

        public CantonData()
        {

        }

        private SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        public List<Canton> getCantonWithoutAssociation()
        {
            List<Canton> list = new List<Canton>();

            using (SqlConnection sqlCon = getConnection())
            {
                sqlCon.Open();
                String query = "Select name_canton from Canton where id_employee= null;";

                SqlCommand sqlSelect = new SqlCommand(query, sqlCon);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    Canton canton = null;
                    while (reader.Read())
                    {
                        canton = new Canton();
                        canton.Name = reader.GetString(0);

                        list.Add(canton);

                    }
                    sqlCon.Close();
                }
            }
            return list;

        }
    }
}

