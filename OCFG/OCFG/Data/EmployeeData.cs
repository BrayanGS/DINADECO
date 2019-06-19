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
        WorkPlan workPlan;
        EconomicReport economicReport;
        Settlement settlement;
        ConcreteLiquidation concreteLiquidation;

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

        /*FOR SEARCH ASSOCIATION*/

        public Association getAssociationId(int idAssociation)
        {
            Association association = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "SELECT a.registry_code, ISNULL(a.type,0), ISNULL(a.name_association, 'No disponible'), ISNULL(a.region, 'No disponible')," 
                                + "ISNULL(a.canton, 'No disponible'), ISNULL(a.province, 'No disponible'), ISNULL(a.status, '0'), ISNULL(a.active, 'No'),"
                                + "ISNULL(a.adequacy, 'No'), ISNULL(a.affidavit, 'No'), ISNULL(a.legal_document, 'No disponible'), ISNULL(a.superavit, 'No'),"
                                + "ISNULL(a.id_economic, 0), ISNULL(a.id_settlement, 0), ISNULL(a.id_work, 0), ISNULL(a.id_concrete, 0)"
                                + " from Association a where a.registry_code = " + idAssociation + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                string varStatus;
                string varActive;
                string varAdequacy;
                string varAffidavit;
                string varSuperavit;

                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*Association*/
                        int registryCode = reader.GetInt32(0);
                        int type = reader.GetInt32(1);
                        string name = reader.GetString(2);
                        string region = reader.GetString(3);
                        string canton = reader.GetString(4);
                        string province = reader.GetString(5);
                        string status = reader.GetString(6);
                        string active = reader.GetString(7);
                        string adequacy = reader.GetString(8);
                        string affidavit = reader.GetString(9);
                        string legalDocument = reader.GetString(10);
                        string superavit = reader.GetString(11);
                        int idEconomic = reader.GetInt32(12);
                        int idSettlement = reader.GetInt32(13);
                        int idWork = reader.GetInt32(14);
                        int idConcrete = reader.GetInt32(15);

                        /*Status*/
                        if (status.Equals("1"))
                        {
                            varStatus = "Al Día";
                        }
                        else
                        {
                            varStatus = "Pendiente";
                        }
                         /*Active*/
                        if (active.Equals("YES"))
                        {
                            varActive = "Activa";
                        }
                        else
                        {
                            varActive = "Inactiva";
                        }

                        /*Adequacy*/
                        if (adequacy.Equals("YES"))
                        {
                            varAdequacy = "Si";
                        }
                        else
                        {
                            varAdequacy = "No";
                        }

                        /*Affidavit*/
                        if (affidavit.Equals("YES"))
                        {
                            varAffidavit = "Si";
                        }
                        else
                        {
                            varAffidavit = "No";
                        }

                        /*Superavit*/
                        if (superavit.Equals("YES"))
                        {
                            varSuperavit = "Si";
                        }
                        else
                        {
                            varSuperavit = "No";
                        }

                        status = varStatus;
                        active = varActive;
                        adequacy = varAdequacy;
                        affidavit = varAffidavit;
                        superavit = varSuperavit;

                        /* WorkPlan */
                        workPlan = getWorkPlanById(idWork);
                        workPlan.Id = idWork;

                        /*EconomicReport*/
                        economicReport = getEconomicReportById(idEconomic);
                        economicReport.Id = idEconomic;

                        /*Settlement*/
                        settlement = getSettlementById(idSettlement);
                        settlement.Id = idSettlement;

                        /* ConcreteLiquitation */
                        concreteLiquidation = getConcreteById(idConcrete);
                        concreteLiquidation.Id = idConcrete;

                        association = new Association(registryCode, name, region, canton, status,
                        active, province, legalDocument, superavit, adequacy, affidavit,
                        type, workPlan, settlement, economicReport, concreteLiquidation);

                    }
                    sqlConnection.Close();
                }
                return association;
            }
        }

        /*GET DIFERENTS DOCUMENTS BY EMPLOYEE*/
        public WorkPlan getWorkPlanById(int idWork)
        {
            WorkPlan workPlan = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select w.assembly_date, w.status from WorkPlan w where w.id_work = " + idWork + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        /*WorkPlan*/
                        workPlan = new WorkPlan();
                        workPlan.AssemblyDate = reader.GetString(0);
                        workPlan.Status = reader.GetString(1);

                    }
                    sqlConnection.Close();
                }
                return workPlan;
            }
        }

        public EconomicReport getEconomicReportById(int idReport)
        {
            EconomicReport economicReport = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select e.date_received, e.year, e.balance, e.status  from EconomicReport e where e.id_economic = " + idReport + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        economicReport = new EconomicReport();
                        economicReport.DateReceived = reader.GetDateTime(0);
                        economicReport.Year = reader.GetString(1);
                        string statusEconomic = reader.GetString(3);
                        char[] cadEconomic = statusEconomic.ToCharArray();
                        economicReport.Status = cadEconomic[0];

                    }
                    sqlConnection.Close();
                }
                return economicReport;
            }
        }

        public Settlement getSettlementById(int idSettlement)
        {
            Settlement settlement = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select s.date_received, s.year, s.status  from Settlement s where s.id_settlement = " + idSettlement + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        settlement = new Settlement();
                        settlement.DateReceived = reader.GetDateTime(0);
                        settlement.Year = reader.GetString(1);
                        string statusSettlement = reader.GetString(2);
                        char[] cadSettlement = statusSettlement.ToCharArray();
                        settlement.Status = cadSettlement[0];

                    }
                    sqlConnection.Close();
                }
                return settlement;
            }
        }

        public ConcreteLiquidation getConcreteById(int idConcrete)
        {
            ConcreteLiquidation concreteLiquidation = null;

            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();

                String query = "select c.date_received, c.year, c.status from ConcreteLiquidation c where c.id_concrete = " + idConcrete + ";";

                SqlCommand sqlSelect = new SqlCommand(query, sqlConnection);
                using (SqlDataReader reader = sqlSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        concreteLiquidation = new ConcreteLiquidation();
                        concreteLiquidation.DateReceived = reader.GetDateTime(0);
                        concreteLiquidation.Year = reader.GetString(1);
                        string statusConcrete = reader.GetString(2);
                        char[] cadConcrete = statusConcrete.ToCharArray();
                        concreteLiquidation.Status = cadConcrete[0];

                    }
                    sqlConnection.Close();
                }
                return concreteLiquidation;
            }
        }

        /*UPDATE DOCUMENTS BY EMPLOYEE*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryCode"></param>
        public void updateWorkPlan(WorkPlan workPlan)
        {
            string queryWork = null;
            Association association = new Association();
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                queryWork = "UPDATE WorkPlan SET assembly_date = '" + workPlan.AssemblyDate + "'" +
                                   "WHERE WorkPlan.id_work = " + workPlan.Id;

                SqlCommand sqlWork = new SqlCommand(queryWork, sqlConnection);
                sqlWork.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateEconomicReport(EconomicReport economicReport)
        {
            string queryEconomic = null;
            Association association = new Association();
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                queryEconomic = "queryEconomic EconomicReport SET date_received = '" + economicReport.DateReceived + "', " +
                                       " year = '" + economicReport.Year + "', " +
                                       "balance = '" + economicReport.Balance + "'" +
                                       "WHERE EconomicReport.id_economic = " + economicReport.Id;

                SqlCommand sqlEconomic = new SqlCommand(queryEconomic, sqlConnection);
                sqlEconomic.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateSettlement(Settlement settlement)
        {
            string querySettlement = null;
            Association association = new Association();
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                querySettlement = "UPDATE Settlement SET date_received = '" + settlement.DateReceived + "', " +
                                        "year = '" + settlement.Year + "'" +
                                        "WHERE Settlement.id_settlement = " + settlement.Id;

                SqlCommand sqlSettlement = new SqlCommand(querySettlement, sqlConnection);
                sqlSettlement.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="association"></param>
        public void updateConcreteLiquidation(ConcreteLiquidation concreteLiquidation)
        {
            string queryConcrete = null;
            Association association = new Association();
            using (SqlConnection sqlConnection = getConnection())
            {
                sqlConnection.Open();
                queryConcrete = "UPDATE ConcreteLiquidation SET date_received = '" + concreteLiquidation.DateReceived + "', " +
                                        "year = '" + concreteLiquidation.Year + "'" +
                                        "WHERE ConcreteLiquidation.id_concrete = " + concreteLiquidation.Id;

                SqlCommand sqlConcrete = new SqlCommand(queryConcrete, sqlConnection);
                sqlConcrete.ExecuteNonQuery();
            }
        }
    }
}

