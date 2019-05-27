using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace OCFG.Data
{
    public class AdministratorData
    {
        string connectionString = "Server=163.178.173.148; Database=OCFG_DataBase; Uid=lenguajesap; Pwd=lenguajesap;";
        Employee employee;

        public void updateEmployee(Employee employee, Canton canton)
        {
            using (SqlConnection sqlConnection = getConnection()) {
                sqlConnection.Open();

                string queryEmployee = "UPDATE Employee SET date_out " + employee.DateOut + "";
                string queryCanton = "UPDATE Cantons SET name " + canton.Name + " " +
                                     " WHERE id_employee = " + employee.Id + "";

                SqlCommand sqlEmployee = new SqlCommand(queryEmployee, sqlConnection);
                sqlEmployee.ExecuteNonQuery();
                SqlCommand sqlCanton = new SqlCommand(queryCanton, sqlConnection);
                sqlCanton.ExecuteNonQuery();
            }
        }

        public Employee getEmployeeByIdCard(int idCardEmployee)
        {
            Employee association = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT e.name_employee, e.last_name, e.id_card, phone " +
                               " FROM Employee e" +
                               " WHERE c.id_employee = e.id_employee" +
                               " AND e.id_card = " + idCardEmployee + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Employee*/
                        string name_employee = reader.GetString(0);
                        string last_name = reader.GetString(1);
                        string id_card = reader.GetString(2);
                        string phone = reader.GetString(3);
                        DateTime date_in = reader.GetDateTime(4);
                 
                        employee = new Employee();

                    }
                    sqlConnection.Close();
                }
                return association;
            }
        }

        private SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}