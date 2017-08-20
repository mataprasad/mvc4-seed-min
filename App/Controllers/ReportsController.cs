using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using System.Net;

namespace App.Controllers
{
    [HandleError]
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/



        public FileContentResult Index(String format = null)
        {
            var sr = new ServerReport();
            sr.ReportServerCredentials = new CustomReportCredentials("administrator", "mypass", "domena");

            /* ReportViewer1.Width = 800;
        ReportViewer1.Height = 600;
        ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        IReportServerCredentials irsc = new CustomReportCredentials("administrator", "mypass", "domena");
        ReportViewer1.ServerReport.ReportServerCredentials = irsc;
        ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://192.168.0.1/ReportServer/");
        ReportViewer1.ServerReport.ReportPath = "/autonarudzba/listanarudzbi";
        ReportViewer1.ServerReport.Refresh();
             * 
             * 
             */ 
            
            ReportParameter[] parameters = new ReportParameter[1];
            List<BalanceSheatRptData> data = new List<BalanceSheatRptData>();
            for (int i = 0; i < 100; i++)
            {
                data.Add(new BalanceSheatRptData()
                {
                    AmountDue = "0",
                    AmountPaid = "100",
                    AmountPayable = "100",
                    FatherName = "B.P. Chauhan",
                    Name = "M.P. Chauhan"
                });
            }
            data.Add(new BalanceSheatRptData()
            {
                AmountDue = "50",
                AmountPaid = "50",
                AmountPayable = "100",
                FatherName = "B.P. Chauhan",
                Name = "Renu. Chauhan"
            });

            BalanceSheatRptParam rptParams = new BalanceSheatRptParam()
            {
                ClassCode = "XII PCM-A",
                GTotalDue = "50",
                GTotalPaid = "150",
                GTotalPayable = "200",
                RptDate = "02-10-2013"
            };

            var localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath(@"~/Content/BalanceSheatReport.rdlc");
            ReportParameter[] reportParams = new ReportParameter[2];
            reportParams[0] = new ReportParameter("ClassCode", rptParams.ClassCode);
            reportParams[1] = new ReportParameter("RptDate", String.Format("Date : {0}", rptParams.RptDate));
            localReport.SetParameters(reportParams);
            localReport.DataSources.Clear();
            localReport.DataSources.Add(new ReportDataSource("RptData", data));
            //http://www.codeproject.com/Articles/609580/Prototype-MVC4-Razor-ReportViewer-RDLC
            string reportType = "Image";
            string mimeType;
            string encoding;
            string fileNameExtension;
            //The DeviceInfo settings should be changed based on the reportType            
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx            
            string deviceInfo = "<DeviceInfo>" +
                "  <OutputFormat>jpeg</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>1in</MarginLeft>" +
                "  <MarginRight>1in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension); 
            if (format == null)
            {
                return File(renderedBytes, "image/jpeg");
            }
            else if (format == "PDF")
            {
                return File(renderedBytes, "pdf");
            }
            else
            {
                return File(renderedBytes, "image/jpeg");
            }

        }

        public void Index1()
        {

            List<BalanceSheatRptData> data = new List<BalanceSheatRptData>();
            data.Add(new BalanceSheatRptData()
            {
                AmountDue = "0",
                AmountPaid = "100",
                AmountPayable = "100",
                FatherName = "B.P. Chauhan",
                Name = "M.P. Chauhan"
            });
            for (int i = 0; i < 200; i++)
            {
                data.Add(new BalanceSheatRptData()
                {
                    AmountDue = "50",
                    AmountPaid = "50",
                    AmountPayable = "100",
                    FatherName = "B.P. Chauhan",
                    Name = "Renu. Chauhan"
                });
            }

            BalanceSheatRptParam rptParams = new BalanceSheatRptParam()
            {
                ClassCode = "XII PCM-A",
                GTotalDue = "50",
                GTotalPaid = "150",
                GTotalPayable = "200",
                RptDate = "02-10-2013"
            };

            var localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath(@"~/Content/BalanceSheatReport.rdlc");
            ReportParameter[] reportParams = new ReportParameter[2];
            reportParams[0] = new ReportParameter("ClassCode", rptParams.ClassCode);
            reportParams[1] = new ReportParameter("RptDate", String.Format("Date : {0}", rptParams.RptDate));
            localReport.SetParameters(reportParams);
            localReport.DataSources.Clear();
            localReport.DataSources.Add(new ReportDataSource("RptData", data));
            //string reportType = "Image";
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo = "<DeviceInfo>" +
               "  <OutputFormat>jpeg</OutputFormat>" +
               "  <PageWidth>8.5in</PageWidth>" +
               "  <PageHeight>11in</PageHeight>" +
               "  <MarginTop>0.5in</MarginTop>" +
               "  <MarginLeft>1in</MarginLeft>" +
               "  <MarginRight>1in</MarginRight>" +
               "  <MarginBottom>0.5in</MarginBottom>" +
               "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report            
            renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.Clear();
            //Response.ContentType = "image/jpeg";
            Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers.jpeg");
            Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers.pdf");
            Response.BinaryWrite(renderedBytes);
            Response.End();
        }

    }


    public class BalanceSheatRptData
    {
        public String AmountDue { get; set; }
        public String AmountPayable { get; set; }
        public String AmountPaid { get; set; }
        public String Name { get; set; }
        public String FatherName { get; set; }
    }

    public class BalanceSheatRptParam
    {
        public String ClassCode { get; set; }
        public String RptDate { get; set; }
        public String GTotalDue { get; set; }
        public String GTotalPaid { get; set; }
        public String GTotalPayable { get; set; }
    }

    public class CustomReportCredentials : IReportServerCredentials
    {
        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        public CustomReportCredentials(string UserName, string PassWord, string DomainName)
        {
            _UserName = UserName;
            _PassWord = PassWord;
            _DomainName = DomainName;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public ICredentials NetworkCredentials
        {
            get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user,
         out string password, out string authority)
        {
            authCookie = null;
            user = password = authority = null;
            return false;
        }
    }
}
