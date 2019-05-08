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

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand commandGetAssotiation = new SqlCommand("getAllAssotiations", conn);
                
                using (SqlDataReader reader = commandGetAssotiation.ExecuteReader())
                {
                    assotiations = new List<Association>();
                    Association association = null;
                    WorkPlan workPlan = null;
                    Settlement settlement = null;
                    EconomicReport economicReport = null;
                    ConcreteLiquidation concreteLiquidation = null;

                        while (reader.Read())
                        {
                            
                            //Obtengo asociacion
                            int id = reader.GetInt32(0);
                            int registryCode = reader.GetInt32(1);
                            string name = reader.GetString(2);
                            string region = reader.GetString(3);
                            string canton = reader.GetString(4);
                            string status = reader.GetString(5);
                            string active = reader.GetString(6);
                            string province = reader.GetString(7);

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

                        association = new Association(id, registryCode,name,region,canton,status,active,province,
                            workPlan,settlement,economicReport,concreteLiquidation);

                        assotiations.Add(association);
                        }
                    conn.Close();

                }

                    return assotiations;
            }


        }
            public Document getDocument()
             {
                 Document document = null;
                 using (SqlConnection conn = GetConnection())
                 {
                     SqlCommand commandGetDocument = new SqlCommand("getDocument", conn);
                     
                     using (SqlDataReader reader = commandGetDocument.ExecuteReader())
                     {
                         
                         while (reader.Read())
                         {
                            document = (Document) reader.GetValue(0);
                             
                         }

                     }
                 }


                     return document;
                 }

             }
        
    }
}
