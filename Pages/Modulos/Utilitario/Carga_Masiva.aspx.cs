using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Lucky.Data;

namespace SIGE.Pages.Modulos.Utilitario
{
    public partial class Carga_Masiva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if ((FileUp.PostedFile != null) && (FileUp.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUp.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Archivos") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUp.FileName.ToLower().EndsWith(".xlsx"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUp.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Presencia"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {
                           
                                oDa.Fill(oDs);

                                dt = oDs.Tables[0];
                                int numcol = 13; //determina el número de columnas para el datatable
                                if (dt.Columns.Count == numcol)
                                {
                                    dt.Columns[0].ColumnName = "Person";
                                    dt.Columns[1].ColumnName = "id_perfil";
                                    dt.Columns[2].ColumnName = "id_Equipo";
                                    dt.Columns[3].ColumnName = "id_Cliente";
                                    dt.Columns[4].ColumnName = "ID_PTOVENTA";
                                    dt.Columns[5].ColumnName = "ID_CATEGORIA";
                                    dt.Columns[6].ColumnName = "ID_MARCA";
                                    dt.Columns[7].ColumnName = "ID_OPCIONPRESENCIA";
                                    dt.Columns[8].ColumnName = "FEC_REG_CEL";
                                    dt.Columns[9].ColumnName = "TIPO_CANAL";
                                    dt.Columns[10].ColumnName = "COMENTARIO";
                                    dt.Columns[11].ColumnName = "ID_ELEMENTO";
                                    dt.Columns[12].ColumnName = "VALOR_ELEMENTO";




                                    ConnectionStringSettings settingconection;
                                    settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky_Tmp"];
                                    string oSqlConnIN = settingconection.ConnectionString;

                                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                    {
                                        bulkCopy.DestinationTableName = "TBL_Reporte_Presencia_CargaMasiva";
                                        //carga los SKU's temporalmente para hacer el procedimiento a través de un SP
                                        bulkCopy.WriteToServer(dt);
                                    }
                                    Conexion cn = new Conexion(2);
                                   // cn.ejecutarDataTable("STP_JVM_VERIFICAR_INSERTAR_MATERIAL_APOYO_MASIVO");




                                    int cargados = 0;
                                    int duplicados = 0;


                                    string Person="";
                                    string id_perfil = "";
                                    string id_Equipo = "";
                                    string id_Cliente = "";
                                    string ID_PTOVENTA = "";
                                    string cod_Categoria = "";

                                    string ID_MARCA = "";
                                    string cod_OPCIONPRESENCIA = "";
                                    string FEC_REG_CEL = "";
                                    string FEC_REG_BD = "";
                                    string latitud = "";
                                    string LONGITUD = "";


                                    string origen = "";
                                    string tipo_canal = "";
                                    string COMENTARIO = "";
                                    string ID_REG_PRESENCIA = "";
                                    string ID_POP = "";
                                    string VALOR_POP = "";

                                    DataSet ds = new DataSet();
                                  

                                    string mensaje = "";
                                    int valor = 1;

  //                                  for (int i = 0; i <= dt.Rows.Count - 1; i++)
  //                                  {

  //                                      if (dt.Rows[i]["ID_OPCIONPRESENCIA"].ToString().Trim() == "03" )
  //                                      {
  //                                          DataTable dtl = cn.ejecutarDataTable("STP_JVM_VERIFICAR_MATERIAL_APOYO", dt.Rows[i]["id_Equipo"].ToString().Trim(), dt.Rows[i]["id_Cliente"].ToString().Trim(), dt.Rows[i]["ID_PTOVENTA"].ToString(),  dt.Rows[i]["ID_OPCIONPRESENCIA"].ToString().Trim(),
  //dt.Rows[i]["FEC_REG_CEL"].ToString().Trim(), dt.Rows[i]["ID_POP"].ToString().Trim());
  //                                          if (dtl.Rows[0]["EXISTE_CAB"].ToString() == "1" && dtl.Rows[0]["EXISTE_MATERIAL_APOYO"].ToString() == "0")
  //                                          {
  //                                              //insertar Detalle
  //                                              cn.ejecutarDataSet("STP_JVM_INSERTAR_PRESENCIA_02_DETALLE", Convert.ToInt32(dtl.Rows[0]["ID_REG_PRESENCIA"].ToString()), dt.Rows[i]["ID_POP"].ToString().Trim(), dt.Rows[i]["VALOR_POP"].ToString().Trim(),
  //                                                       null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

  //                                              cargados = cargados + 1;

  //                                          }
  //                                          else if (dtl.Rows[0]["EXISTE_CAB"].ToString() == "0" && dtl.Rows[0]["EXISTE_MATERIAL_APOYO"].ToString() == "0")
  //                                          {
  //                                              //Registrar Cabecera y Detalle
  //                                              Person = dt.Rows[i]["Person"].ToString().Trim();
  //                                              id_perfil = dt.Rows[i]["id_perfil"].ToString().Trim();
  //                                              id_Equipo = dt.Rows[i]["id_Equipo"].ToString().Trim();
  //                                              id_Cliente = dt.Rows[i]["id_Cliente"].ToString().Trim();
  //                                              ID_PTOVENTA = dt.Rows[i]["ID_PTOVENTA"].ToString().Trim();
  //                                              cod_Categoria = dt.Rows[i]["ID_CATEGORIA"].ToString().Trim();


  //                                              ID_MARCA = dt.Rows[i]["ID_MARCA"].ToString().Trim();
  //                                              cod_OPCIONPRESENCIA = dt.Rows[i]["ID_OPCIONPRESENCIA"].ToString().Trim();
  //                                              FEC_REG_CEL = dt.Rows[i]["FEC_REG_CEL"].ToString().Trim();

  //                                              FEC_REG_BD = dt.Rows[i]["FEC_REG_BD"].ToString().Trim();
  //                                              latitud = dt.Rows[i]["LATITUD"].ToString().Trim();
  //                                              LONGITUD = dt.Rows[i]["LONGITUD"].ToString().Trim();


  //                                              origen = dt.Rows[i]["ORIGEN"].ToString().Trim();
  //                                              tipo_canal = dt.Rows[i]["TIPO_CANAL"].ToString().Trim();
  //                                              COMENTARIO = dt.Rows[i]["COMENTARIO"].ToString().Trim();
  //                                              ID_POP = dt.Rows[i]["ID_POP"].ToString().Trim();
  //                                              VALOR_POP = dt.Rows[i]["VALOR_POP"].ToString().Trim();


  //                                              ID_REG_PRESENCIA = cn.ejecutarretornodeOUTPUT("STP_JVM_INSERTAR_PRESENCIA_02", 14, Convert.ToInt32(Person), id_perfil,
  //      id_Equipo, id_Cliente, ID_PTOVENTA, cod_Categoria, ID_MARCA, cod_OPCIONPRESENCIA,
  //      FEC_REG_CEL, latitud, LONGITUD, origen, tipo_canal, COMENTARIO, ID_REG_PRESENCIA);

  //                                              cn.ejecutarDataSet("STP_JVM_INSERTAR_PRESENCIA_02_DETALLE", Convert.ToInt32(ID_REG_PRESENCIA), ID_POP, VALOR_POP,
  //                                                                 null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

  //                                              cargados = cargados + 1;
  //                                          }
  //                                          else
  //                                          {
  //                                              valor = valor * 0;
  //                                          }
  //                                      }

                                  
  //                                  }


                                   
                                    LblVacio.Text = "El archivo fue cargado correctamente" ;
                                    LblVacio.Visible=true;


                                }
                                else
                                {
                                    //Pmensaje.CssClass = "MensajesSupervisor";
                                    //lblencabezado.Text = "Sr. Usuario";
                                    //lblmensajegeneral.Text = "El archivo debe contener 29 campos. Por favor verifique.";
                                    //Mensajes_Usuario();
                                }
                            

                        }
                        catch (Exception ex)
                        {
                            //Pmensaje.CssClass = "MensajesSupervisor";
                            //lblencabezado.Text = "Sr. Usuario";
                            //lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de puntos de venta válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Puntos_Venta";
                            //Mensajes_Usuario();
                        }
                        oConn1.Close();
                    }
                    else
                    {
                        //Pmensaje.CssClass = "MensajesSupervisor";
                        //lblencabezado.Text = "Sr. Usuario";
                        //lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                        //Mensajes_Usuario();
                    }
                }
            }
            else
            {
                //Pmensaje.CssClass = "MensajesSupervisor";
                //lblencabezado.Text = "Sr. Usuario";
                //lblmensajegeneral.Text = "Es indispensable seleccionar un presupuesto y un archivo.";
                LblVacio.Text = "Por favor seleccione un archivo.";
            }
        }

        protected void btnCargarPresencia_Click(object sender, EventArgs e)
        {
            if ((FileUpPresencia.PostedFile != null) && (FileUpPresencia.PostedFile.ContentLength > 0))
            {
                string fn = System.IO.Path.GetFileName(FileUpPresencia.PostedFile.FileName);
                string SaveLocation = Server.MapPath("Archivos") + "\\" + fn;

                if (SaveLocation != string.Empty)
                {
                    if (FileUpPresencia.FileName.ToLower().EndsWith(".xlsx"))
                    {
                        // string Destino = Server.MapPath(null) + "\\PDV_Planning\\" + Path.GetFileName(FileUpPDV.PostedFile.FileName);
                        OleDbConnection oConn1 = new OleDbConnection();
                        OleDbCommand oCmd = new OleDbCommand();
                        OleDbDataAdapter oDa = new OleDbDataAdapter();
                        DataSet oDs = new DataSet();
                        DataTable dt = new DataTable();

                        FileUpPresencia.PostedFile.SaveAs(SaveLocation);

                        // oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\"";
                        oConn1.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + SaveLocation + ";Extended Properties=\"Excel 12.0; HDR=YES;\"";
                        oConn1.Open();
                        oCmd.CommandText = ConfigurationManager.AppSettings["CargaMasiva_Presencia"];
                        oCmd.Connection = oConn1;
                        oDa.SelectCommand = oCmd;

                        try
                        {

                            oDa.Fill(oDs);

                            dt = oDs.Tables[0];
                            int numcol = 13; //determina el número de columnas para el datatable
                            if (dt.Columns.Count == numcol)
                            {
                                dt.Columns[0].ColumnName = "Person";
                                dt.Columns[1].ColumnName = "id_perfil";
                                dt.Columns[2].ColumnName = "id_Equipo";
                                dt.Columns[3].ColumnName = "id_Cliente";
                                dt.Columns[4].ColumnName = "ID_PTOVENTA";
                                dt.Columns[5].ColumnName = "ID_CATEGORIA";
                                dt.Columns[6].ColumnName = "ID_MARCA";
                                dt.Columns[7].ColumnName = "ID_OPCIONPRESENCIA";
                                dt.Columns[8].ColumnName = "FEC_REG_CEL";
                                dt.Columns[9].ColumnName = "TIPO_CANAL";
                                dt.Columns[10].ColumnName = "COMENTARIO";
                                dt.Columns[11].ColumnName = "ID_ELEMENTO";
                                dt.Columns[12].ColumnName = "VALOR_ELEMENTO";




                                ConnectionStringSettings settingconection;
                                settingconection = ConfigurationManager.ConnectionStrings["ConectaDBLucky_Tmp"];
                                string oSqlConnIN = settingconection.ConnectionString;

                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(oSqlConnIN))
                                {
                                    bulkCopy.DestinationTableName = "TBL_Reporte_Presencia_CargaMasiva";
                                    //carga los SKU's temporalmente para hacer el procedimiento a través de un SP
                                    bulkCopy.WriteToServer(dt);
                                }
                                Conexion cn = new Conexion(2);
                              // cn.ejecutarDataTable("STP_JVM_VERIFICAR_INSERTAR_PRESENCIA_COLGATE_Y_COMPETENCIA");




                                int cargados = 0;
                                int duplicados = 0;


                                string Person = "";
                                string id_perfil = "";
                                string id_Equipo = "";
                                string id_Cliente = "";
                                string ID_PTOVENTA = "";
                                string cod_Categoria = "";

                                string ID_MARCA = "";
                                string cod_OPCIONPRESENCIA = "";
                                string FEC_REG_CEL = "";
                                string FEC_REG_BD = "";
                                string latitud = "";
                                string LONGITUD = "";


                                string origen = "";
                                string tipo_canal = "";
                                string COMENTARIO = "";
                                string ID_REG_PRESENCIA = "";
                                string ID_POP = "";
                                string VALOR_POP = "";

                                DataSet ds = new DataSet();


                                string mensaje = "";
                                int valor = 1;

                                //                                  for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                //                                  {

                                //                                      if (dt.Rows[i]["ID_OPCIONPRESENCIA"].ToString().Trim() == "03" )
                                //                                      {
                                //                                          DataTable dtl = cn.ejecutarDataTable("STP_JVM_VERIFICAR_MATERIAL_APOYO", dt.Rows[i]["id_Equipo"].ToString().Trim(), dt.Rows[i]["id_Cliente"].ToString().Trim(), dt.Rows[i]["ID_PTOVENTA"].ToString(),  dt.Rows[i]["ID_OPCIONPRESENCIA"].ToString().Trim(),
                                //dt.Rows[i]["FEC_REG_CEL"].ToString().Trim(), dt.Rows[i]["ID_POP"].ToString().Trim());
                                //                                          if (dtl.Rows[0]["EXISTE_CAB"].ToString() == "1" && dtl.Rows[0]["EXISTE_MATERIAL_APOYO"].ToString() == "0")
                                //                                          {
                                //                                              //insertar Detalle
                                //                                              cn.ejecutarDataSet("STP_JVM_INSERTAR_PRESENCIA_02_DETALLE", Convert.ToInt32(dtl.Rows[0]["ID_REG_PRESENCIA"].ToString()), dt.Rows[i]["ID_POP"].ToString().Trim(), dt.Rows[i]["VALOR_POP"].ToString().Trim(),
                                //                                                       null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                                //                                              cargados = cargados + 1;

                                //                                          }
                                //                                          else if (dtl.Rows[0]["EXISTE_CAB"].ToString() == "0" && dtl.Rows[0]["EXISTE_MATERIAL_APOYO"].ToString() == "0")
                                //                                          {
                                //                                              //Registrar Cabecera y Detalle
                                //                                              Person = dt.Rows[i]["Person"].ToString().Trim();
                                //                                              id_perfil = dt.Rows[i]["id_perfil"].ToString().Trim();
                                //                                              id_Equipo = dt.Rows[i]["id_Equipo"].ToString().Trim();
                                //                                              id_Cliente = dt.Rows[i]["id_Cliente"].ToString().Trim();
                                //                                              ID_PTOVENTA = dt.Rows[i]["ID_PTOVENTA"].ToString().Trim();
                                //                                              cod_Categoria = dt.Rows[i]["ID_CATEGORIA"].ToString().Trim();


                                //                                              ID_MARCA = dt.Rows[i]["ID_MARCA"].ToString().Trim();
                                //                                              cod_OPCIONPRESENCIA = dt.Rows[i]["ID_OPCIONPRESENCIA"].ToString().Trim();
                                //                                              FEC_REG_CEL = dt.Rows[i]["FEC_REG_CEL"].ToString().Trim();

                                //                                              FEC_REG_BD = dt.Rows[i]["FEC_REG_BD"].ToString().Trim();
                                //                                              latitud = dt.Rows[i]["LATITUD"].ToString().Trim();
                                //                                              LONGITUD = dt.Rows[i]["LONGITUD"].ToString().Trim();


                                //                                              origen = dt.Rows[i]["ORIGEN"].ToString().Trim();
                                //                                              tipo_canal = dt.Rows[i]["TIPO_CANAL"].ToString().Trim();
                                //                                              COMENTARIO = dt.Rows[i]["COMENTARIO"].ToString().Trim();
                                //                                              ID_POP = dt.Rows[i]["ID_POP"].ToString().Trim();
                                //                                              VALOR_POP = dt.Rows[i]["VALOR_POP"].ToString().Trim();


                                //                                              ID_REG_PRESENCIA = cn.ejecutarretornodeOUTPUT("STP_JVM_INSERTAR_PRESENCIA_02", 14, Convert.ToInt32(Person), id_perfil,
                                //      id_Equipo, id_Cliente, ID_PTOVENTA, cod_Categoria, ID_MARCA, cod_OPCIONPRESENCIA,
                                //      FEC_REG_CEL, latitud, LONGITUD, origen, tipo_canal, COMENTARIO, ID_REG_PRESENCIA);

                                //                                              cn.ejecutarDataSet("STP_JVM_INSERTAR_PRESENCIA_02_DETALLE", Convert.ToInt32(ID_REG_PRESENCIA), ID_POP, VALOR_POP,
                                //                                                                 null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                                //                                              cargados = cargados + 1;
                                //                                          }
                                //                                          else
                                //                                          {
                                //                                              valor = valor * 0;
                                //                                          }
                                //                                      }


                                //                                  }



                                LblVacio.Text = "El archivo fue cargado correctamente";
                                LblVacio.Visible = true;


                            }
                            else
                            {
                                //Pmensaje.CssClass = "MensajesSupervisor";
                                //lblencabezado.Text = "Sr. Usuario";
                                //lblmensajegeneral.Text = "El archivo debe contener 29 campos. Por favor verifique.";
                                //Mensajes_Usuario();
                            }


                        }
                        catch (Exception ex)
                        {
                            //Pmensaje.CssClass = "MensajesSupervisor";
                            //lblencabezado.Text = "Sr. Usuario";
                            //lblmensajegeneral.Text = "El archivo seleccionado no corresponde a un archivo de puntos de venta válido. Por favor verifique que el nombre de la hoja donde estan los datos sea Puntos_Venta";
                            //Mensajes_Usuario();
                        }
                        oConn1.Close();
                    }
                    else
                    {
                        //Pmensaje.CssClass = "MensajesSupervisor";
                        //lblencabezado.Text = "Sr. Usuario";
                        //lblmensajegeneral.Text = "Solo se permite cargar archivos en formato Excel 2003. Por favor verifique.";
                        //Mensajes_Usuario();
                    }
                }
            }
            else
            {
                //Pmensaje.CssClass = "MensajesSupervisor";
                //lblencabezado.Text = "Sr. Usuario";
                //lblmensajegeneral.Text = "Es indispensable seleccionar un presupuesto y un archivo.";
                LblVacio.Text = "Por favor seleccione un archivo.";
            }
        }
    }
}