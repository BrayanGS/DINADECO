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

        public void updateEmployee(DateTime dateIn, string phoneNumber, string id)
        {
            string formatted = dateIn.ToString("dd/M/yyyy");
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                string queryEmployee = "UPDATE Employee SET date_in = '" +formatted + "', " +
                                       " phone_number = '" + phoneNumber + "', status=" + 0 + " WHERE id_card = '" + id + "';";

                SqlCommand sqlEmployee = new SqlCommand(queryEmployee, sqlConnection);
                sqlEmployee.ExecuteNonQuery();
            }
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

                string queryEmployee = "UPDATE Employee SET date_out = '" + employee.DateOut + "', " +
                                       " phone_number = '" + employee.PhoneNumber+ "' WHERE id_card = '"+canton.Employee.IdCard+"';";

                int idEmployee = getIdEmployeeByIdCard(canton.Employee.IdCard);

                string queryCanton = "UPDATE Canton SET id_employee = " + idEmployee + " " +
                                     " WHERE id_canton = " + canton.Id + ";";

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

                String query = "SELECT e.name_employee, e.last_name, e.id_card, e.phone_number, e.date_in , e.email, e.address, e.date_out" +
                               " FROM Employee e" +
                               " WHERE e.id_card = '" + idCardEmployee + "';";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /** Employee **/
                        employee = new Employee();
                        employee.Name = reader.GetString(0);
                        employee.LastName = reader.GetString(1);
                        employee.IdCard = reader.GetString(2);
                        employee.PhoneNumber = reader.GetString(3);
                        employee.DateIn = reader.GetDateTime(4);
                        employee.Email = reader.GetString(5);
                        employee.Address = reader.GetString(6);
                        employee.DateOut = reader.GetDateTime(7);
                    }
                    sqlConnection.Close();
                }
                return employee;
            }
        }

        public Canton getCantonByIdEmployee(string idEmployee)
        {
            Canton canton = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT c.name_canton" +
                               " FROM Canton c" +
                               " WHERE c.id_employee = '" + idEmployee+ "';";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /** Employee **/
                        canton = new Canton();
                        canton.Name = reader.GetString(0);
                    }
                    sqlConnection.Close();
                }
                return canton;
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
                    String query = "SELECT name_employee, last_name, id_card, phone_number, email, status FROM Employee" +
                                   " WHERE name_employee like '" + search.Substring(0, 1).ToUpper()+ search.Substring(1) + "%"+ "' " +
                                   " OR last_name like'" + search.Substring(0, 1).ToUpper()+ search.Substring(1)+ "%" + "' " +
                                   " OR id_card like '" + search + "%" + "'";

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
                            employee.Status = (int)reader[5];
                            employees.Add(employee);
                        }
                        sqlConnection.Close();
                    }
                }
            return employees;
        }

        public int getIdEmployeeByIdCard(string idCard) {

            int idEmployee = 0;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT id_employee" +
                               " FROM Employee e" +
                               " WHERE e.id_card = '" + idCard + "';";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idEmployee = reader.GetInt32(0);

                    }
                    sqlConnection.Close();
                }
                return idEmployee;
            }

        }

        public void deleteEmployee(string idCard, DateTime dateOut) {
            int idEmployee = getIdEmployeeByIdCard(idCard);
            string formatted = dateOut.ToString("dd/M/yyyy");
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                
                String query1 = "delete from Officer where id_officer = (select id_officer from Employee where id_card = '"+ idCard + "');";
                String query2 = "update canton set id_employee = NULL where id_employee = " + idEmployee + ";";
                String query3 = "update Employee set status = "+ 1 + ", date_out ='" + formatted + "' where id_card = '"+ idCard +"'; ";

                SqlCommand sqlOfficer = new SqlCommand(query1, sqlConnection);
                sqlOfficer.ExecuteNonQuery();
                SqlCommand sqlCanton = new SqlCommand(query2, sqlConnection);
                sqlCanton.ExecuteNonQuery();
                SqlCommand sqlEmployee = new SqlCommand(query3, sqlConnection);
                sqlEmployee.ExecuteNonQuery();
            }
        }


        private string validateRol(string search)
        {

            return (search);
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