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
            List<Association> associations = new List<Association>();
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand commandGetAssotiation = new SqlCommand("Call getAllAssotiations", conn);
                commandGetAssotiation.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = commandGetAssotiation.ExecuteReader())
                {
                    Association association = null;
                    while (reader.Read())
                    {
                        association = new Association();

                        //Obtengo asociacion
                        association.Id = reader.GetInt32(1);
                        association.RegistryCode = reader.GetInt32(2);
                        association.Name = reader.GetString(3);
                        association.Region = reader.GetString(4);
                        association.Canton = reader.GetString(5);
                        association.Status = reader.GetString(6);

                        //work plan assotiation
                        association.WorkPlan.Id = reader.GetInt32(7);
                        association.WorkPlan.AssemblyDate = reader.GetString(8);// averiguar
                        association.WorkPlan.Status = reader.GetString(9);

                        //settlement association
                        association.Settlement.Id = reader.GetInt32(10);
                        association.Settlement.DateReceived = reader.GetDateTime(11);
                        association.Settlement.Year = reader.GetDateTime(12);
                        association.Settlement.Status = reader.GetChar(13);

                        //economic report assotiation
                        association.EconomicReport.Id = reader.GetInt32(14);
                        association.EconomicReport.DateReceived = reader.GetDateTime(15);
                        association.EconomicReport.Year = reader.GetDateTime(16);
                        association.EconomicReport.Status = reader.GetChar(17);

                        //concrete
                        association.ConcreteLiquidation.Id = reader.GetInt32(18);
                        association.ConcreteLiquidation.DateReceived = reader.GetDateTime(15);
                        association.ConcreteLiquidation.Year = reader.GetDateTime(16);
                        association.ConcreteLiquidation.Status = reader.GetChar(17);

                        associations.Add(association);
                    }

                }
                return associations;
            }
        }
    }
}