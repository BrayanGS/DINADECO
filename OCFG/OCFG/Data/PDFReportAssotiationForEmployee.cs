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

                    SqlCommand command = new SqlCommand("insertDocument", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@doc", 1);

                }

            }

        }

        public List<Association> getAllAssotiations()
        {

            List<Association> assotiations;

            using (SqlConnection conn = GetConnection())
            {
                SqlCommand commandGetAssotiation = new SqlCommand("getAllAssotiations", conn);
                commandGetAssotiation.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = commandGetAssotiation.ExecuteReader())
                {
                    assotiations = new List<Association>();
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
                        association.Settlement.Year = reader.GetString(12);
                        association.Settlement.Status = reader.GetChar(13);

                        //economic report assotiation
                        association.EconomicReport.Id = reader.GetInt32(14);
                        association.EconomicReport.DateReceived = reader.GetDateTime(15);
                        association.EconomicReport.Year = reader.GetString(16);
                        association.EconomicReport.Status = reader.GetChar(17);

                        //concrete
                        association.ConcreteLiquidation.Id = reader.GetInt32(18);
                        association.ConcreteLiquidation.DateReceived = reader.GetDateTime(15);
                        association.ConcreteLiquidation.Year = reader.GetString(16);
                        association.ConcreteLiquidation.Status = reader.GetChar(17);

                        association.Settlement.Id = reader.GetInt32(10);
                        association.Settlement.DateReceived = reader.GetDateTime(11);
                        association.Settlement.Year = reader.GetString(12);
                        association.Settlement.Status = reader.GetChar(13);

                        //economic report assotiation
                        association.EconomicReport.Id = reader.GetInt32(14);
                        association.EconomicReport.DateReceived = reader.GetDateTime(15);
                        association.EconomicReport.Year = reader.GetString(16);
                        association.EconomicReport.Status = reader.GetChar(17);

                        //concrete
                        association.ConcreteLiquidation.Id = reader.GetInt32(18);
                        association.ConcreteLiquidation.DateReceived = reader.GetDateTime(15);
                        association.ConcreteLiquidation.Year = reader.GetString(16);
                        association.ConcreteLiquidation.Status = reader.GetChar(17);


                        assotiations.Add(association);
                    }
                    conn.Close();

                }

                return assotiations;
            }


        }

        /** public Document getDocument()
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


                 return associations;
             }

         }**/

    }
}