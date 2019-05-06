using iTextSharp.text;
using iTextSharp.text.pdf;
using OCFG.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace OCFG.Data
{
    public class PDFReportAssotiationForEmployee
    {
        static String connectionString = "Server=163.178.173.148; Database=OCFG_DataBase; Uid=lenguajesap; Pwd=lenguajesap;";


        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void generateReport()
        {
            List<Association> assotiations = new List<Association>();
            using (SqlConnection conn = GetConnection())
            {
                Document doc = new Document();

                conn.Open();
                SqlCommand command = new SqlCommand("Call insertDocument", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@doc", 1);
                string fileName = Path.GetTempFileName() + ".doc";
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@fileName, FileMode.Create));
                doc.Open();

                //ya creo el pdf, quede aqui perrrrrros


            }

        }

        public List<Association> getAllAssotiations()
        {
            List<Association> assotiations = new List<Association>();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand commandGetAssotiation = new SqlCommand("Call getAllAssotiations", conn);
                commandGetAssotiation.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = commandGetAssotiation.ExecuteReader())
                {
                    Association assotiation = null;
                    while (reader.Read())
                    {
                        assotiation = new Association();

                        //Obtengo asociacion
                        assotiation.Id = reader.GetInt32(1);
                        assotiation.RegistryCode = reader.GetInt32(2);
                        assotiation.Name = reader.GetString(3);
                        assotiation.Region = reader.GetString(4);
                        assotiation.Canton = reader.GetString(5);
                        assotiation.Status = reader.GetString(6);

                        //work plan assotiation
                        assotiation.WorkPlan.Id = reader.GetInt32(7);
                        assotiation.WorkPlan.AssemblyDate = reader.GetString(8);// averiguar
                        assotiation.WorkPlan.Status = reader.GetString(9);

                        //settlement association
                        assotiation.Settlement.Id = reader.GetInt32(10);
                        assotiation.Settlement.DateReceived = reader.GetDateTime(11);
                        assotiation.Settlement.Year = reader.GetDateTime(12);
                        assotiation.Settlement.Status = reader.GetChar(13);

                        //economic report assotiation
                        assotiation.EconomicReport.Id = reader.GetInt32(14);
                        assotiation.EconomicReport.DateReceived = reader.GetDateTime(15);
                        assotiation.EconomicReport.Year = reader.GetDateTime(16);
                        assotiation.EconomicReport.Status = reader.GetChar(17);

                        //concrete
                        assotiation.ConcreteLiquidation.Id = reader.GetInt32(18);
                        assotiation.ConcreteLiquidation.DateReceived = reader.GetDateTime(15);
                        assotiation.ConcreteLiquidation.Year = reader.GetDateTime(16);
                        assotiation.ConcreteLiquidation.Status = reader.GetChar(17);

                        assotiations.Add(assotiation);
                    }

                }
                return assotiations;
            }
        }
    }
}