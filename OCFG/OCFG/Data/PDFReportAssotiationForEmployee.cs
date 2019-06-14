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

            List<Association> assotiations = getAllAssotiations();

            using (SqlConnection conn = GetConnection())
            {
                Document doc = new Document();

                conn.Open();
                string fileName = @"Descargas\ReporteGeneral.pdf";
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@fileName, FileMode.Create));
                doc.Open();

                // Creamos una tabla de encabezado
                PdfPTable tblPrueba = new PdfPTable(assotiations.Count);
                tblPrueba.WidthPercentage = 100;

                for (int i = 0; i < assotiations.Count; i++)
                {
                    //Association
                    PdfPCell Association = new PdfPCell();
                    Association.AddElement(new Phrase(assotiations[i].Id));
                    Association.AddElement(new Phrase(assotiations[i].RegistryCode));
                    Association.AddElement(new Phrase(assotiations[i].Name));
                    Association.AddElement(new Phrase(assotiations[i].RegistryCode));
                    Association.AddElement(new Phrase(assotiations[i].Canton));
                    Association.AddElement(new Phrase(assotiations[i].Id));
                    Association.AddElement(new Phrase(assotiations[i].WorkPlan.Id));
                    Association.AddElement(new Phrase(assotiations[i].WorkPlan.AssemblyDate));
                    Association.AddElement(new Phrase(assotiations[i].WorkPlan.Status));
                    Association.AddElement(new Phrase(assotiations[i].EconomicReport.Id));
                    Association.AddElement(new Phrase("" + assotiations[i].EconomicReport.DateReceived.Month + "" + assotiations[i].EconomicReport.DateReceived.Day));
                    Association.AddElement(new Phrase(assotiations[i].EconomicReport.Status));
                    Association.AddElement(new Phrase(assotiations[i].ConcreteLiquidation.Id));
                    Association.AddElement(new Phrase("" + assotiations[i].ConcreteLiquidation.DateReceived.Month + "" + assotiations[i].ConcreteLiquidation.DateReceived.Day));
                    Association.AddElement(new Phrase(assotiations[i].ConcreteLiquidation.Status));
                    tblPrueba.AddCell(Association);

                    doc.Add(tblPrueba);
                    doc.Close();

                    String query = "insert into Document values('" + doc + "')";
                    SqlCommand command = new SqlCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }

            }

        }

        public List<Association> getAllAssotiations()
        {

            List<Association> assotiations;
            assotiations = new List<Association>();
            Association association;
            WorkPlan workPlan;
            Settlement settlement;
            EconomicReport economicReport;
            ConcreteLiquidation concreteLiquidation;

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand commandGetAssotiation = new SqlCommand("getAllAssotiations", conn);

                using (SqlDataReader reader = commandGetAssotiation.ExecuteReader())
                {


                    while (reader.Read())
                    {

                        //Obtengo asociacion
                        int id = reader.GetInt32(0);
                        int registryCode = reader.GetInt32(1);
                        string type = reader.GetString(2);
                        string name = reader.GetString(3);
                        string region = reader.GetString(4);
                        string canton = reader.GetString(5);
                        string province = reader.GetString(6);
                        string status = reader.GetString(7);
                        string active = reader.GetString(8);
                        string adequacy = reader.GetString(9);
                        string affiavit = reader.GetString(10);
                        string legalDocument = reader.GetString(15);
                        string superavit = reader.GetString(16);

                        workPlan = new WorkPlan();
                        //work plan assotiation
                        workPlan.Id = reader.GetInt32(8);
                        workPlan.AssemblyDate = reader.GetString(9);// averiguar
                        workPlan.Status = reader.GetString(10);

                        settlement = new Settlement();
                        //settlement association
                        settlement.Id = reader.GetInt32(11);
                        settlement.DateReceived = reader.GetDateTime(12);
                        settlement.Year = reader.GetString(13);
                        string statusSettlement = reader.GetString(14);
                        char[] cadSettlement = statusSettlement.ToCharArray();
                        settlement.Status = cadSettlement[0];

                        economicReport = new EconomicReport();
                        //economic report assotiation
                        economicReport.Id = reader.GetInt32(15);
                        economicReport.DateReceived = reader.GetDateTime(16);
                        economicReport.Year = reader.GetString(17);
                        economicReport.Balance = (float)reader.GetDouble(18);
                        string statusEconomic = reader.GetString(19);
                        char[] cadEconomic = statusEconomic.ToCharArray();
                        economicReport.Status = cadEconomic[0];

                        concreteLiquidation = new ConcreteLiquidation();
                        //concrete
                        concreteLiquidation.Id = reader.GetInt32(20);
                        concreteLiquidation.DateReceived = reader.GetDateTime(21);
                        concreteLiquidation.Year = reader.GetString(22);
                        string statusConcrete = reader.GetString(23);
                        char[] cadConcrete = statusConcrete.ToCharArray();
                        concreteLiquidation.Status = cadConcrete[0];

                        association = new Association(id, registryCode, name, region, canton, status, active, province, legalDocument,  superavit,  adequacy,  affiavit,  type,
                           workPlan, settlement, economicReport, concreteLiquidation);

                        assotiations.Add(association);
                    }
                    conn.Close();

                }

                return assotiations;
            }


        }
        /**public void getDocument()
         {
             Document document = null;
             using (SqlConnection conn = GetConnection())
             {
                conn.Open();
                 SqlCommand commandGetDocument = new SqlCommand("getDocument", conn);
                 
                 using (SqlDataReader reader = commandGetDocument.ExecuteReader())
                 {
                     
                     while (reader.Read())
                     {
                        document = (Document) reader.GetValue(0);
                         
                     }

                

                 }
                conn.Close();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(@"C:\DINADECO\OCFG\OCFG\Document\ReporteGeneral.pdf", FileMode.Create));
               }   
                    
                 
        }**/



    }
}

