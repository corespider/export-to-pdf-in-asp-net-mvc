using ExportPDFMVC.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExportPDFMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeRepository _emprepo = new EmployeeRepository();
        // GET: Customer
        public async Task<ActionResult> Index()
        {
            List<Employee> _empList =await _emprepo.GetEmployeeList() as List<Employee>;
            return View(_empList);
        }
        [HttpPost]
        public async Task<ActionResult> Export()
        {
            List<Employee> _empList =await _emprepo.GetEmployeeList() as List<Employee>;
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    GridView gridview = new GridView();
                    gridview.DataSource = _empList;
                    gridview.DataBind();

                    gridview.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Export.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            
                return View();
            }
        }
    }
}