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

namespace SIGE.Facade_Menu_strategy {
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
    [System.Web.Services.WebServiceBindingAttribute(Name="Facade_MPlanningSoap", Namespace="http://tempuri.org/")]
    public partial class Facade_MPlanning : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback MenuOperationCompleted;
        
        private System.Threading.SendOrPostCallback Menu2OperationCompleted;
        
        private System.Threading.SendOrPostCallback Menu3OperationCompleted;
        
        private System.Threading.SendOrPostCallback Menu4OperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Facade_MPlanning() {
            this.Url = global::SIGE.Properties.Settings.Default.SIGE_Facade_Menu_strategy_Facade_MPlanning;
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
        public event MenuCompletedEventHandler MenuCompleted;
        
        /// <remarks/>
        public event Menu2CompletedEventHandler Menu2Completed;
        
        /// <remarks/>
        public event Menu3CompletedEventHandler Menu3Completed;
        
        /// <remarks/>
        public event Menu4CompletedEventHandler Menu4Completed;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Menu", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Menu(string spresupuesto) {
            object[] results = this.Invoke("Menu", new object[] {
                        spresupuesto});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void MenuAsync(string spresupuesto) {
            this.MenuAsync(spresupuesto, null);
        }
        
        /// <remarks/>
        public void MenuAsync(string spresupuesto, object userState) {
            if ((this.MenuOperationCompleted == null)) {
                this.MenuOperationCompleted = new System.Threading.SendOrPostCallback(this.OnMenuOperationCompleted);
            }
            this.InvokeAsync("Menu", new object[] {
                        spresupuesto}, this.MenuOperationCompleted, userState);
        }
        
        private void OnMenuOperationCompleted(object arg) {
            if ((this.MenuCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.MenuCompleted(this, new MenuCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Menu2", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Menu2(int icodStrategy) {
            object[] results = this.Invoke("Menu2", new object[] {
                        icodStrategy});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Menu2Async(int icodStrategy) {
            this.Menu2Async(icodStrategy, null);
        }
        
        /// <remarks/>
        public void Menu2Async(int icodStrategy, object userState) {
            if ((this.Menu2OperationCompleted == null)) {
                this.Menu2OperationCompleted = new System.Threading.SendOrPostCallback(this.OnMenu2OperationCompleted);
            }
            this.InvokeAsync("Menu2", new object[] {
                        icodStrategy}, this.Menu2OperationCompleted, userState);
        }
        
        private void OnMenu2OperationCompleted(object arg) {
            if ((this.Menu2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Menu2Completed(this, new Menu2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Menu3", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Menu3(int icodStrategy) {
            object[] results = this.Invoke("Menu3", new object[] {
                        icodStrategy});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Menu3Async(int icodStrategy) {
            this.Menu3Async(icodStrategy, null);
        }
        
        /// <remarks/>
        public void Menu3Async(int icodStrategy, object userState) {
            if ((this.Menu3OperationCompleted == null)) {
                this.Menu3OperationCompleted = new System.Threading.SendOrPostCallback(this.OnMenu3OperationCompleted);
            }
            this.InvokeAsync("Menu3", new object[] {
                        icodStrategy}, this.Menu3OperationCompleted, userState);
        }
        
        private void OnMenu3OperationCompleted(object arg) {
            if ((this.Menu3Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Menu3Completed(this, new Menu3CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Menu4", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataTable Menu4(int idcodpoint) {
            object[] results = this.Invoke("Menu4", new object[] {
                        idcodpoint});
            return ((System.Data.DataTable)(results[0]));
        }
        
        /// <remarks/>
        public void Menu4Async(int idcodpoint) {
            this.Menu4Async(idcodpoint, null);
        }
        
        /// <remarks/>
        public void Menu4Async(int idcodpoint, object userState) {
            if ((this.Menu4OperationCompleted == null)) {
                this.Menu4OperationCompleted = new System.Threading.SendOrPostCallback(this.OnMenu4OperationCompleted);
            }
            this.InvokeAsync("Menu4", new object[] {
                        idcodpoint}, this.Menu4OperationCompleted, userState);
        }
        
        private void OnMenu4OperationCompleted(object arg) {
            if ((this.Menu4Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Menu4Completed(this, new Menu4CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    public delegate void MenuCompletedEventHandler(object sender, MenuCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class MenuCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal MenuCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void Menu2CompletedEventHandler(object sender, Menu2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Menu2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Menu2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void Menu3CompletedEventHandler(object sender, Menu3CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Menu3CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Menu3CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    public delegate void Menu4CompletedEventHandler(object sender, Menu4CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Menu4CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Menu4CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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