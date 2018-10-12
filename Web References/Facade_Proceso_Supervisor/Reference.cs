﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.431
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.431.
// 
#pragma warning disable 1591

namespace SIGE.Facade_Proceso_Supervisor {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="Facade_Proceso_SupervisorSoap", Namespace="http://tempuri.org/")]
    public partial class Facade_Proceso_Supervisor : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback Get_ProductoDuplicadoOperationCompleted;
        
        private System.Threading.SendOrPostCallback Get_ConsultarAsignacionPDVOperationCompleted;
        
        private System.Threading.SendOrPostCallback Get_ConsultarAsignacionPRODUCTOOperationCompleted;
        
        private System.Threading.SendOrPostCallback Get_ConsultarAsignacionPRODUCTOPDV_XINFORMEOperationCompleted;
        
        private System.Threading.SendOrPostCallback Get_ConsultarInfoActividadComercioOperationCompleted;
        
        private System.Threading.SendOrPostCallback Get_ConsultarInfoActividadPropiaOperationCompleted;
        
        private System.Threading.SendOrPostCallback Get_PuntoVentaAsignadoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Facade_Proceso_Supervisor() {
            this.Url = global::SIGE.Properties.Settings.Default.SIGE_Facade_Proceso_Supervisor_Facade_Proceso_Supervisor;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event Get_ProductoDuplicadoCompletedEventHandler Get_ProductoDuplicadoCompleted;
        
        /// <remarks/>
        public event Get_ConsultarAsignacionPDVCompletedEventHandler Get_ConsultarAsignacionPDVCompleted;
        
        /// <remarks/>
        public event Get_ConsultarAsignacionPRODUCTOCompletedEventHandler Get_ConsultarAsignacionPRODUCTOCompleted;
        
        /// <remarks/>
        public event Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompletedEventHandler Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompleted;
        
        /// <remarks/>
        public event Get_ConsultarInfoActividadComercioCompletedEventHandler Get_ConsultarInfoActividadComercioCompleted;
        
        /// <remarks/>
        public event Get_ConsultarInfoActividadPropiaCompletedEventHandler Get_ConsultarInfoActividadPropiaCompleted;
        
        /// <remarks/>
        public event Get_PuntoVentaAsignadoCompletedEventHandler Get_PuntoVentaAsignadoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_ProductoDuplicado", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Get_ProductoDuplicado(int iid_ProductsPlanning, int iPerson_id, int iid_Planning) {
            object[] results = this.Invoke("Get_ProductoDuplicado", new object[] {
                        iid_ProductsPlanning,
                        iPerson_id,
                        iid_Planning});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Get_ProductoDuplicadoAsync(int iid_ProductsPlanning, int iPerson_id, int iid_Planning) {
            this.Get_ProductoDuplicadoAsync(iid_ProductsPlanning, iPerson_id, iid_Planning, null);
        }
        
        /// <remarks/>
        public void Get_ProductoDuplicadoAsync(int iid_ProductsPlanning, int iPerson_id, int iid_Planning, object userState) {
            if ((this.Get_ProductoDuplicadoOperationCompleted == null)) {
                this.Get_ProductoDuplicadoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_ProductoDuplicadoOperationCompleted);
            }
            this.InvokeAsync("Get_ProductoDuplicado", new object[] {
                        iid_ProductsPlanning,
                        iPerson_id,
                        iid_Planning}, this.Get_ProductoDuplicadoOperationCompleted, userState);
        }
        
        private void OnGet_ProductoDuplicadoOperationCompleted(object arg) {
            if ((this.Get_ProductoDuplicadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_ProductoDuplicadoCompleted(this, new Get_ProductoDuplicadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_ConsultarAsignacionPDV", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Get_ConsultarAsignacionPDV(int iid_Planning, int iPerson_id) {
            object[] results = this.Invoke("Get_ConsultarAsignacionPDV", new object[] {
                        iid_Planning,
                        iPerson_id});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Get_ConsultarAsignacionPDVAsync(int iid_Planning, int iPerson_id) {
            this.Get_ConsultarAsignacionPDVAsync(iid_Planning, iPerson_id, null);
        }
        
        /// <remarks/>
        public void Get_ConsultarAsignacionPDVAsync(int iid_Planning, int iPerson_id, object userState) {
            if ((this.Get_ConsultarAsignacionPDVOperationCompleted == null)) {
                this.Get_ConsultarAsignacionPDVOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_ConsultarAsignacionPDVOperationCompleted);
            }
            this.InvokeAsync("Get_ConsultarAsignacionPDV", new object[] {
                        iid_Planning,
                        iPerson_id}, this.Get_ConsultarAsignacionPDVOperationCompleted, userState);
        }
        
        private void OnGet_ConsultarAsignacionPDVOperationCompleted(object arg) {
            if ((this.Get_ConsultarAsignacionPDVCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_ConsultarAsignacionPDVCompleted(this, new Get_ConsultarAsignacionPDVCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_ConsultarAsignacionPRODUCTO", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Get_ConsultarAsignacionPRODUCTO(int iid_Planning, int iPerson_id) {
            object[] results = this.Invoke("Get_ConsultarAsignacionPRODUCTO", new object[] {
                        iid_Planning,
                        iPerson_id});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Get_ConsultarAsignacionPRODUCTOAsync(int iid_Planning, int iPerson_id) {
            this.Get_ConsultarAsignacionPRODUCTOAsync(iid_Planning, iPerson_id, null);
        }
        
        /// <remarks/>
        public void Get_ConsultarAsignacionPRODUCTOAsync(int iid_Planning, int iPerson_id, object userState) {
            if ((this.Get_ConsultarAsignacionPRODUCTOOperationCompleted == null)) {
                this.Get_ConsultarAsignacionPRODUCTOOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_ConsultarAsignacionPRODUCTOOperationCompleted);
            }
            this.InvokeAsync("Get_ConsultarAsignacionPRODUCTO", new object[] {
                        iid_Planning,
                        iPerson_id}, this.Get_ConsultarAsignacionPRODUCTOOperationCompleted, userState);
        }
        
        private void OnGet_ConsultarAsignacionPRODUCTOOperationCompleted(object arg) {
            if ((this.Get_ConsultarAsignacionPRODUCTOCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_ConsultarAsignacionPRODUCTOCompleted(this, new Get_ConsultarAsignacionPRODUCTOCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_ConsultarAsignacionPRODUCTOPDV_XINFORME", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Get_ConsultarAsignacionPRODUCTOPDV_XINFORME(int iid_Report, int iid_Planning, int iPerson_id, int iid_MPOSPlanning) {
            object[] results = this.Invoke("Get_ConsultarAsignacionPRODUCTOPDV_XINFORME", new object[] {
                        iid_Report,
                        iid_Planning,
                        iPerson_id,
                        iid_MPOSPlanning});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Get_ConsultarAsignacionPRODUCTOPDV_XINFORMEAsync(int iid_Report, int iid_Planning, int iPerson_id, int iid_MPOSPlanning) {
            this.Get_ConsultarAsignacionPRODUCTOPDV_XINFORMEAsync(iid_Report, iid_Planning, iPerson_id, iid_MPOSPlanning, null);
        }
        
        /// <remarks/>
        public void Get_ConsultarAsignacionPRODUCTOPDV_XINFORMEAsync(int iid_Report, int iid_Planning, int iPerson_id, int iid_MPOSPlanning, object userState) {
            if ((this.Get_ConsultarAsignacionPRODUCTOPDV_XINFORMEOperationCompleted == null)) {
                this.Get_ConsultarAsignacionPRODUCTOPDV_XINFORMEOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_ConsultarAsignacionPRODUCTOPDV_XINFORMEOperationCompleted);
            }
            this.InvokeAsync("Get_ConsultarAsignacionPRODUCTOPDV_XINFORME", new object[] {
                        iid_Report,
                        iid_Planning,
                        iPerson_id,
                        iid_MPOSPlanning}, this.Get_ConsultarAsignacionPRODUCTOPDV_XINFORMEOperationCompleted, userState);
        }
        
        private void OnGet_ConsultarAsignacionPRODUCTOPDV_XINFORMEOperationCompleted(object arg) {
            if ((this.Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompleted(this, new Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_ConsultarInfoActividadComercio", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Get_ConsultarInfoActividadComercio(int iid_Planning) {
            object[] results = this.Invoke("Get_ConsultarInfoActividadComercio", new object[] {
                        iid_Planning});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Get_ConsultarInfoActividadComercioAsync(int iid_Planning) {
            this.Get_ConsultarInfoActividadComercioAsync(iid_Planning, null);
        }
        
        /// <remarks/>
        public void Get_ConsultarInfoActividadComercioAsync(int iid_Planning, object userState) {
            if ((this.Get_ConsultarInfoActividadComercioOperationCompleted == null)) {
                this.Get_ConsultarInfoActividadComercioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_ConsultarInfoActividadComercioOperationCompleted);
            }
            this.InvokeAsync("Get_ConsultarInfoActividadComercio", new object[] {
                        iid_Planning}, this.Get_ConsultarInfoActividadComercioOperationCompleted, userState);
        }
        
        private void OnGet_ConsultarInfoActividadComercioOperationCompleted(object arg) {
            if ((this.Get_ConsultarInfoActividadComercioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_ConsultarInfoActividadComercioCompleted(this, new Get_ConsultarInfoActividadComercioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_ConsultarInfoActividadPropia", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Get_ConsultarInfoActividadPropia(int iid_Planning) {
            object[] results = this.Invoke("Get_ConsultarInfoActividadPropia", new object[] {
                        iid_Planning});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Get_ConsultarInfoActividadPropiaAsync(int iid_Planning) {
            this.Get_ConsultarInfoActividadPropiaAsync(iid_Planning, null);
        }
        
        /// <remarks/>
        public void Get_ConsultarInfoActividadPropiaAsync(int iid_Planning, object userState) {
            if ((this.Get_ConsultarInfoActividadPropiaOperationCompleted == null)) {
                this.Get_ConsultarInfoActividadPropiaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_ConsultarInfoActividadPropiaOperationCompleted);
            }
            this.InvokeAsync("Get_ConsultarInfoActividadPropia", new object[] {
                        iid_Planning}, this.Get_ConsultarInfoActividadPropiaOperationCompleted, userState);
        }
        
        private void OnGet_ConsultarInfoActividadPropiaOperationCompleted(object arg) {
            if ((this.Get_ConsultarInfoActividadPropiaCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_ConsultarInfoActividadPropiaCompleted(this, new Get_ConsultarInfoActividadPropiaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Get_PuntoVentaAsignado", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Get_PuntoVentaAsignado(int iid_MPOSPlanning, int iid_Planning) {
            object[] results = this.Invoke("Get_PuntoVentaAsignado", new object[] {
                        iid_MPOSPlanning,
                        iid_Planning});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Get_PuntoVentaAsignadoAsync(int iid_MPOSPlanning, int iid_Planning) {
            this.Get_PuntoVentaAsignadoAsync(iid_MPOSPlanning, iid_Planning, null);
        }
        
        /// <remarks/>
        public void Get_PuntoVentaAsignadoAsync(int iid_MPOSPlanning, int iid_Planning, object userState) {
            if ((this.Get_PuntoVentaAsignadoOperationCompleted == null)) {
                this.Get_PuntoVentaAsignadoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGet_PuntoVentaAsignadoOperationCompleted);
            }
            this.InvokeAsync("Get_PuntoVentaAsignado", new object[] {
                        iid_MPOSPlanning,
                        iid_Planning}, this.Get_PuntoVentaAsignadoOperationCompleted, userState);
        }
        
        private void OnGet_PuntoVentaAsignadoOperationCompleted(object arg) {
            if ((this.Get_PuntoVentaAsignadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Get_PuntoVentaAsignadoCompleted(this, new Get_PuntoVentaAsignadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_ProductoDuplicadoCompletedEventHandler(object sender, Get_ProductoDuplicadoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_ProductoDuplicadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_ProductoDuplicadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_ConsultarAsignacionPDVCompletedEventHandler(object sender, Get_ConsultarAsignacionPDVCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_ConsultarAsignacionPDVCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_ConsultarAsignacionPDVCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_ConsultarAsignacionPRODUCTOCompletedEventHandler(object sender, Get_ConsultarAsignacionPRODUCTOCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_ConsultarAsignacionPRODUCTOCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_ConsultarAsignacionPRODUCTOCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompletedEventHandler(object sender, Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_ConsultarAsignacionPRODUCTOPDV_XINFORMECompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_ConsultarInfoActividadComercioCompletedEventHandler(object sender, Get_ConsultarInfoActividadComercioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_ConsultarInfoActividadComercioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_ConsultarInfoActividadComercioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_ConsultarInfoActividadPropiaCompletedEventHandler(object sender, Get_ConsultarInfoActividadPropiaCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_ConsultarInfoActividadPropiaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_ConsultarInfoActividadPropiaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Get_PuntoVentaAsignadoCompletedEventHandler(object sender, Get_PuntoVentaAsignadoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Get_PuntoVentaAsignadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Get_PuntoVentaAsignadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataTable Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591