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
                string fileName = Path.GetTempFileName() + ".doc";
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@fileName, FileMode.Create));
                doc.Open();

                // Creamos una tabla de encabezado
                PdfPTable tblPrueba = new PdfPTable(assotiations.Count);
                tblPrueba.WidthPercentage = 100;

                for (int i = 0; i < assotiations.Count; i++)
                {
                    //Association
                    PdfPCell idAssociation = new PdfPCell();
                    idAssociation.AddElement(new Phrase(assotiations[i].Id));
                    tblPrueba.AddCell(idAssociation);

                    PdfPCell registryCode = new PdfPCell();
                    registryCode.AddElement(new Phrase(assotiations[i].RegistryCode));
                    tblPrueba.AddCell(registryCode);

                    PdfPCell nameAssociation = new PdfPCell();
                    nameAssociation.AddElement(new Phrase(assotiations[i].Name));
                    tblPrueba.AddCell(nameAssociation);

                    PdfPCell region = new PdfPCell();
                    region.AddElement(new Phrase(assotiations[i].RegistryCode));
                    tblPrueba.AddCell(region);

                    PdfPCell canton = new PdfPCell();
                    canton.AddElement(new Phrase(assotiations[i].Canton));
                    tblPrueba.AddCell(canton);

                    PdfPCell statusAssociation = new PdfPCell();
                    statusAssociation.AddElement(new Phrase(assotiations[i].Id));
                    tblPrueba.AddCell(statusAssociation);

                    //work plan 
                    PdfPCell idWork = new PdfPCell();
                    idWork.AddElement(new Phrase(assotiations[i].WorkPlan.Id));
                    tblPrueba.AddCell(idWork);

                    PdfPCell workDate = new PdfPCell();
                    workDate.AddElement(new Phrase(assotiations[i].WorkPlan.AssemblyDate));
                    tblPrueba.AddCell(workDate);

                    PdfPCell workStatus = new PdfPCell();
                    workStatus.AddElement(new Phrase(assotiations[i].WorkPlan.Status));
                    tblPrueba.AddCell(workStatus);

                    //Economic Report
                    PdfPCell economicId = new PdfPCell();
                    economicId.AddElement(new Phrase(assotiations[i].EconomicReport.Id));
                    tblPrueba.AddCell(economicId);

                    PdfPCell economicDate = new PdfPCell();
                    economicDate.AddElement(new Phrase("" + assotiations[i].EconomicReport.DateReceived.Month + "" + assotiations[i].EconomicReport.DateReceived.Day));
                    tblPrueba.AddCell(economicDate);

                    PdfPCell status = new PdfPCell();
                    economicId.AddElement(new Phrase(assotiations[i].EconomicReport.Status));
                    tblPrueba.AddCell(status);

                    //Concrete Liquidation
                    PdfPCell concreteId = new PdfPCell();
                    concreteId.AddElement(new Phrase(assotiations[i].ConcreteLiquidation.Id));
                    tblPrueba.AddCell(concreteId);

                    PdfPCell concreteDate = new PdfPCell();
                    concreteDate.AddElement(new Phrase("" + assotiations[i].ConcreteLiquidation.DateReceived.Month + "" + assotiations[i].ConcreteLiquidation.DateReceived.Day));
                    tblPrueba.AddCell(concreteDate);

                    PdfPCell concreteStatus = new PdfPCell();
                    concreteStatus.AddElement(new Phrase(assotiations[i].ConcreteLiquidation.Status));
                    tblPrueba.AddCell(concreteStatus);

                    doc.Add(tblPrueba);
                    doc.Close();

                    SqlCommand command = new SqlCommand("Call insertDocument", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@doc", 1);

                }

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
                        assotiation.Settlement.Year = reader.GetString(12);
                        assotiation.Settlement.Status = reader.GetChar(13);

                        //economic report assotiation
                        assotiation.EconomicReport.Id = reader.GetInt32(14);
                        assotiation.EconomicReport.DateReceived = reader.GetDateTime(15);
                        assotiation.EconomicReport.Year = reader.GetString(16);
                        assotiation.EconomicReport.Status = reader.GetChar(17);

                        //concrete
                        assotiation.ConcreteLiquidation.Id = reader.GetInt32(18);
                        assotiation.ConcreteLiquidation.DateReceived = reader.GetDateTime(15);
                        assotiation.ConcreteLiquidation.Year = reader.GetString(16);
                        assotiation.ConcreteLiquidation.Status = reader.GetChar(17);

                        assotiations.Add(assotiation);
                    }

                }
                return assotiations;
            }

            
        }

        public Document getDocument()
        {
            Document document = null;
            using (SqlConnection conn = GetConnection())
            {
                SqlCommand commandGetDocument = new SqlCommand("Call getDocument", conn);
                commandGetDocument.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader reader = commandGetAssotiation.ExecuteReader())
                {
                    Association assotiation = null;
                    while (reader.Read())
                    {
                        
                    }

                }
            }

        }
    }
}