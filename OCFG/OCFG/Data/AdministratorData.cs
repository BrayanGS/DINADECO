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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="canton"></param>
        public void updateEmployee(Employee employee, Canton canton)
        {
            using (SqlConnection sqlConnection = getConnection()) {
                sqlConnection.Open();

                string queryEmployee = "UPDATE Employee SET date_out " + employee.DateOut + " " +
                                       " phone_number = " + employee.PhoneNumber+" ";
                string queryCanton = "UPDATE Cantons SET id_employee " + canton.Employee.Id + " " +
                                     " WHERE id_employee = " + employee.Id + "";

                SqlCommand sqlEmployee = new SqlCommand(queryEmployee, sqlConnection);
                sqlEmployee.ExecuteNonQuery();
                SqlCommand sqlCanton = new SqlCommand(queryCanton, sqlConnection);
                sqlCanton.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCardEmployee"></param>
        /// <returns></returns>
        public Employee getEmployeeByIdCard(string idCardEmployee)
        {
            Employee employee = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT e.name_employee, e.last_name, e.id_card, e.phone_number, e.date_in , e.email" +
                               " FROM Employee e" +
                               " WHERE e.id_card = '" + idCardEmployee+"';";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Employee*/
                        string name_employee = reader.GetString(0);
                        string last_name = reader.GetString(1);
                        string id_card = reader.GetString(2);
                        string phone_number = reader.GetString(3);
                        DateTime date_in = reader.GetDateTime(4);
                        string email = reader.GetString(5);

                        employee = new Employee(name_employee, last_name, id_card, phone_number, date_in, email);

                    }
                    sqlConnection.Close();
                }
                return employee;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<Employee> searchEmployeeByFilter(String search)
        {
            List<Employee> employees = new List<Employee>();
                using (SqlConnection sqlConnection = getConnection())
                {
                    sqlConnection.Open();
                    String query = "SELECT name, last_name, id_card, phone_number, email " +
                                   "WHERE name ='" + search + "' or last_name='" + search + "' or id_card = '" + search + "'";

                    SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                    using (SqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        Employee employee = null;
                        while (reader.Read())
                        {
                            employee = new Employee();
                            employee.Name = (string)reader[0];
                            employee.LastName = (string)reader[1];
                            employee.IdCard = (string)reader[2];
                            employee.PhoneNumber = (string)reader[3];
                            employee.Email = (string)reader[4];
                            employees.Add(employee);
                        }
                        sqlConnection.Close();
                    }
                }
            return employees;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}