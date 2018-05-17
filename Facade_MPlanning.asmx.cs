using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Lucky.Data;
using Lucky.Data.Common.Application;
using Lucky.Business;
using Lucky.Business.Common.Application;
using Lucky.Entity.Common.Application;
using System.Web;
using System.Web.Services;
using Lucky.Entity;

namespace Facade_Planning
{
    /// <summary>
    /// Descripción breve de Facade_MPlanning
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]

    public class Facade_MPlanning : System.Web.Services.WebService
    {

        [WebMethod]
        public DataTable Menu(string spresupuesto)
        {
            Conexion oConn = new Conexion();
            DataTable dt = null;
            EEstrategy oesUsuario = new EEstrategy();
            dt = oConn.ejecutarDataTable("UP_WEBSIGE_PLALLENASERVICES", spresupuesto);
            if(dt!=null){
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        
                        oesUsuario.codStrategy = Convert.ToInt32(dt.Rows[i]["cod_Strategy"].ToString().Trim());
                        oesUsuario.StrategyName = dt.Rows[i]["Strategy_Name"].ToString().Trim();


                    }
                }
                return dt;
            }
            else
            {

                return null;


            }
        }

        [WebMethod]
        public DataTable Menu2(int icodStrategy)
        {
            Conexion oConn = new Conexion();
            DataTable dt = null;
            EReports oesUsuario = new EReports();
            dt = oConn.ejecutarDataTable("UP_WEB_CONSULTAREPORTESXESTRATEGIA", icodStrategy);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {

                    oesUsuario.reportId = Convert.ToInt32(dt.Rows[i]["Report_Id"].ToString().Trim());
                    oesUsuario.ReportNameReport = dt.Rows[i]["Report_NameReport"].ToString().Trim();
                  
                }

                return dt;
            }
            else
            {

                return null;

            }


        }


        [WebMethod]
        public DataTable Menu3(int icodStrategy)
        {

            Conexion oConn = new Conexion();
            DataTable dt = null;
            EStrategit_Points oesUsuario = new EStrategit_Points();
            
            dt = oConn.ejecutarDataTable("UP_WEB_CONSULTAITEMSSERVICES", icodStrategy);



            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {                    
                    
                    oesUsuario.idcodPoint = Convert.ToInt32(dt.Rows[i]["id_cod_Point"].ToString().Trim());
                    oesUsuario.codPoint = dt.Rows[i]["cod_Point"].ToString().Trim();
                    oesUsuario.DescriptionPoint = dt.Rows[i]["Description_Point"].ToString().Trim();
                }

                return dt;
            }
            else
            {

                return null;

            }
        }

        [WebMethod]
        public DataTable Menu4(int idcodpoint)
        {

            Conexion oConn = new Conexion();
            DataTable dt = null;
            EItemsPoint oesUsuario = new EItemsPoint();
            dt = oConn.ejecutarDataTable("UP_WEB_CONSULTAITEMPOINTS",idcodpoint);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    oesUsuario.id_cod_point = Convert.ToInt32(dt.Rows[i]["cod_Item"].ToString().Trim());
                    oesUsuario.cod_Point = dt.Rows[i]["cod_Point"].ToString().Trim();
                    oesUsuario.Item_Description = dt.Rows[i]["Item_Description"].ToString().Trim();
                }

                return dt;
            }
            else
            {

                return null;

            }
        }
    }
            


}
    

