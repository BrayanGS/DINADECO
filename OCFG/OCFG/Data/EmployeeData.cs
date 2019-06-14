using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace OCFG.Data
{
    public class EmployeeData
    {
        String connectionString = "Server=163.178.173.148; Database=OCFG_DataBase; Uid=lenguajesap; Pwd=lenguajesap;";

        public EmployeeData()
        {

        }

        private SqlConnection getConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void insertEmployee(Employee employee)
        {


            using (SqlConnection sqlConnection = getConnection())
            {
                SqlTransaction transaction = null;
                string query1;
                string query2;

                string user = employee.Name.Substring(0, 3) + employee.LastName.Substring(0, 3) + employee.PhoneNumber.Substring(0, 3);
                string password = employee.PhoneNumber.Substring(0, 3) + employee.Name.Substring(0, 1) + employee.IdCard.Substring(0, 3) + employee.LastName.Substring(0, 1);
                generarCorreo(employee.Email, user, password, employee.Name + " " + employee.LastName);
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


                foreach (var item in employee.Canton2)
                {
                    string query3 = "Update Canton set id_employee=" + idEmployee + " where name_canton ='" + item + "'";
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

        public void generarCorreo(string email, string username, string password, string nameClient)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress(email);
            mail.Subject = "Correo DINADECO";
            mail.Body = "Buenas " + nameClient + " sus datos para ingresar a la plataforma DINADECO son: " +
                " USUARIO: " + username + " CONTRASEÑA: " + password + "";


            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("dinadecoprueba@gmail.com", "DINADECO123cartago");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                mail.Dispose();
            }
            catch (Exception ex)
            {

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

        public List<Association> getAssociationsByFilter(String search, String filter)
        {
            List<Association> associations = new List<Association>();
            if (!filter.Equals("cod"))
            {
                using (SqlConnection sqlConnection = getConnection())
                {
                    sqlConnection.Open();
                    String query = "SELECT registry_code, name_association, canton, region, " +
                                   "status,active,province FROM Association " +
                                   "WHERE name_association ='" + search + "' or canton='" + search + "' or region='" + search +
                                   "' or province='" + search + "'";

                    SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                    String varStatus;
                    string varActive;
                    using (SqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        Association association = null;
                        while (reader.Read())
                        {
                            association = new Association();
                            association.RegistryCode = (int)reader[0];
                            association.Name = (string)reader[1];
                            association.Canton = (string)reader[2];
                            association.Province = (string)reader[6];
                            association.Region = (string)reader[3];
                            association.Status = (string)reader[4];
                            association.Active = (string)reader[5];
                            if (association.Status.Equals("1"))
                            {
                                varStatus = "Al día";
                            }
                            else
                            {
                                varStatus = "Pendiente";
                            }
                            if (association.Active.Equals("Yes"))
                            {
                                varActive = "Activa";
                            }
                            else
                            {
                                varActive = "Inactiva";
                            }
                            association.Status = varStatus;
                            association.Active = varActive;

                            associations.Add(association);
                        }
                        sqlConnection.Close();
                    }
                }


            }
            else
            {
                using (SqlConnection sqlConnection = getConnection())
                {
                    sqlConnection.Open();
                    String query = "SELECT registry_code, name_association, canton, region, " +
                                   "status,active,province FROM Association " +
                                   "WHERE registry_code=" + search + "";

                    SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                    String varStatus;
                    string varActive;
                    using (SqlDataReader reader = sqlSelect.ExecuteReader())
                    {
                        Association association = null;
                        while (reader.Read())
                        {
                            association = new Association();
                            association.RegistryCode = (int)reader[0];
                            association.Name = (string)reader[1];
                            association.Canton = (string)reader[2];
                            association.Province = (string)reader[6];
                            association.Region = (string)reader[3];
                            association.Status = (string)reader[4];
                            association.Active = (string)reader[5];
                            if (association.Status.Equals("1"))
                            {
                                varStatus = "Al día";
                            }
                            else
                            {
                                varStatus = "Pendiente";
                            }
                            if (association.Active.Equals("Yes"))
                            {
                                varActive = "Activa";
                            }
                            else
                            {
                                varActive = "Inactiva";
                            }
                            association.Status = varStatus;
                            association.Active = varActive;

                            associations.Add(association);
                        }
                        sqlConnection.Close();
                    }
                }
            }
            return associations;
        }
    }
}

