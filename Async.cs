using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;

namespace SIGE
{
    public class Async
    {
        ReportViewer reporteExecutiveSumary;
        BackgroundWorker worker;
        private int iidcompany;
        private string sidcanal;

        private string sidaño;
        private string sidmes;
        private string sidperiodo;
        private int icadena;
        private string sioficina;
        private string sidcategoria;
        int iservicio;
        string canal;
        int report;
        ReportViewer Reporte_v2_Wholesalers1;
        public Async ()
        {
            
        }

        public Async(int iidcompany, int iservicio, string canal, int report, string sidcategoria, int icadena, string sidaño, string sidmes, string sioficina, ReportViewer Reporte_v2_Wholesalers1)
        {
            this.iidcompany=iidcompany;
            this.iservicio = iservicio;
            this.canal = canal;
            this.report = report;
            this.sidcategoria = sidcategoria;
            this.icadena = icadena;
            this.sidaño = sidaño;
            this.sidmes = sidmes;
            this.sioficina = sioficina;
            this.Reporte_v2_Wholesalers1 = Reporte_v2_Wholesalers1;
        }
        public bool IsReusable { get { return false; } }


        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)
        {
            //context.Response.Write("<p>Begin IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");
            ReportExecutiveSummary(iidcompany, iservicio, canal, report, sidcategoria, icadena, sidaño, sidmes, sioficina, Reporte_v2_Wholesalers1);
            AsynchOperation asynch = new AsynchOperation(cb, context, extraData);
            asynch.StartAsyncWork();
            return asynch;
        }
        public void Asyncr1()
        {
             Action<int> EjecucionServicio = (int millis) =>
                {
                    System.Threading.Thread.Sleep(millis);
                };

             AsyncCallback CallbackMethod = (IAsyncResult ar) =>
             {
                 try
                 {
                     AsyncResult result = (AsyncResult)ar;
                     Action<int> callerDelegate = (Action<int>)result.AsyncDelegate;

                     callerDelegate.EndInvoke(ar);

                     Action update = () =>
                     {
                         //waitDialog.Close();
                         //this.Effect = null;
                         ReportExecutiveSummary(iidcompany, iservicio, canal, report, sidcategoria, icadena, sidaño, sidmes, sioficina, Reporte_v2_Wholesalers1);
                     };

                     //this.Dispatcher.Invoke(DispatcherPriority.Normal, update);
                 }
                 catch (Exception ex)
                 {
                     // TODO: controlar exception
                 }
             };
        }

        public  void ReportExecutiveSummary(Int32 iidcompany, Int32 iservicio, String canal, Int32 Report, String sidcategoria, Int32 icadena, String sidaño, String sidmes, String sioficina, ReportViewer Reporte_v2_Wholesalers1)
        {
            //iidcompany = Convert.ToInt32(this.Session["companyid"]);
            //iservicio = Convert.ToInt32(this.Session["Service"]);
            //canal = this.Session["Canal"].ToString().Trim();
           //Report = Convert.ToInt32(this.Session["Reporte"]);  

            try
            {

                reporteExecutiveSumary = (ReportViewer)(Reporte_v2_Wholesalers1.FindControl("ReportWholessalersGrafics"));
                reporteExecutiveSumary.Visible = true;
                reporteExecutiveSumary.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.FullPage;
                reporteExecutiveSumary.ServerReport.ReportPath = "/Reporte_Precios_V1/Reporte_PresenciaMayColgateGraficos";

                String strConnection = ConfigurationManager.AppSettings["SERVIDOR_REPORTING_SERVICES"];
                reporteExecutiveSumary.ServerReport.ReportServerUrl = new Uri(strConnection);
                reporteExecutiveSumary.ServerReport.ReportServerCredentials = new CFG.Tools.ReportServerNetCredentials();
                List<Microsoft.Reporting.WebForms.ReportParameter> parametros = new List<Microsoft.Reporting.WebForms.ReportParameter>();
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CLIENTE", Convert.ToString(iidcompany)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("SERVICIO", Convert.ToString(iservicio)));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CANAL", canal));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CADENA", icadena.ToString()));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CATEGORIA", sidcategoria));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("AÑO", sidaño));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("MES", sidmes));
                //parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("PERIODO", ));
                parametros.Add(new Microsoft.Reporting.WebForms.ReportParameter("CIUDAD", sioficina));
                reporteExecutiveSumary.ServerReport.SetParameters(parametros);
            }
            catch (Exception ex)
            {

                Exception mensaje = ex;
                //Label mensajeusu = new Label();
                //mensajeusu.Visible = true;
                //mensajeusu.Text = "Se ha perdido la Comunicación con Nuestro Servidor Disculpe las molestias";


            }

        }

        public void EndProcessRequest(IAsyncResult result)
        {
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new InvalidOperationException();
        }

    }


    class AsynchOperation : IAsyncResult
    {
        private bool _completed;
        private Object _state;
        private AsyncCallback _callback;
        private HttpContext _context;

        bool IAsyncResult.IsCompleted { get { return _completed; } }
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }
        Object IAsyncResult.AsyncState { get { return _state; } }
        bool IAsyncResult.CompletedSynchronously { get { return false; } }

        public AsynchOperation(AsyncCallback callback, HttpContext context, Object state)
        {
            _callback = callback;
            _context = context;
            _state = state;
            _completed = false;
        }

        public void StartAsyncWork()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartAsyncTask), null);
        }

        private void StartAsyncTask(Object workItemState)
        {
            //_context.Response.Write("<p>Completion IsThreadPoolThread is " + Thread.CurrentThread.IsThreadPoolThread + "</p>\r\n");

            //_context.Response.Write("Hello World from Async Handler!");
            //_completed = true;
            //_callback(this);
        }
    }

}