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

        public void insertEmployee(Employee employee)
        {


            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction transaction = null;
                string query1;
                string query2;

                string user = employee.Name.Substring(0, 3) + employee.LastName.Substring(0, 3) + employee.PhoneNumber.Substring(0, 3);
                string password = employee.PhoneNumber.Substring(0, 3) + employee.Name.Substring(0, 1) + employee.IdCard.Substring(0, 3) + employee.LastName.Substring(0, 1);
                string rol = "Empleado";

                query2 = "Insert into Officer(user_name, password_officer, rol) " +
                "values ('" + user + "','" + password + "','" + rol + "')";

                sqlConnection.Open();
                SqlCommand sqlSelect2 = new SqlCommand(query2, sqlConnection);
                sqlSelect2.ExecuteNonQuery();
                int idOfficer = getIdOfficer(user);
                string formatted = employee.DateIn.ToString("dd/M/yyyy");

                query1 = "Insert into Employee(name_employee, last_name, id_card, phone_number,date_in, email, id_officer) " +
                "values (" + "'" + employee.Name + "','" + employee.LastName + "','" + employee.IdCard + "','" + employee.PhoneNumber + "','"
                + formatted + "','" + employee.Email + "'," + idOfficer + ")";
                SqlCommand sqlSelect1 = new SqlCommand(query1, sqlConnection);
                sqlSelect1.ExecuteNonQuery();
                int idEmployee = getIdEmployee(employee.IdCard);


                for (int i = 0; i < employee.Canton.Count; i++)
                {
                    string query3 = "Update Canton set id_employee=" + idEmployee + " where name_canton ='" + employee.Canton[i].Name + "'";
                    SqlCommand sqlSelect3 = new SqlCommand(query3, sqlConnection);
                    sqlSelect3.ExecuteNonQuery();
                }

                try
                {

                    sqlConnection.Close();
                    transaction.Commit();
                }

                catch (SqlException ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    throw ex;
                }
                finally
                {
                    if (sqlConnection != null)
                    {
                        sqlConnection.Close();
                    }
                }
            }

        }
        private int getIdEmployee(string idCard)
        {
            int idEmployee = 0;
            using (SqlConnection sqlConnection = getConnection())
            {

                sqlConnection.Open();
                string query = "Select id_employee from Employee " + "where ( id_card =" + "'" + idCard + "')";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idEmployee = reader.GetInt32(0);
                    }

                }
                sqlConnection.Close();
            }
            return idEmployee;
        }

        public int getIdOfficer(String user)
        {
            int idOfficer = 0;
            using (SqlConnection sqlConnection = getConnection())
            {

                sqlConnection.Open();
                string query = "Select id_officer from Officer " + "where ( user_name =" + "'" + user + "')";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        idOfficer = reader.GetInt32(0);
                    }

                }
                sqlConnection.Close();
            }
            return idOfficer;
        }
    

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