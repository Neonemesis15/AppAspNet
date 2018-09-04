<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComboList.ascx.cs" Inherits="SIGE.UsersControls.ComboList" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<%@ Register TagPrefix="cbo" Namespace="OboutInc.Combobox" Assembly="obout_Combobox_Net" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>

<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.OleDb" %>
<%@ Import Namespace="System.Data.SqlClient" %>

<script language="C#" runat="server">
	void Page_load(object sender, EventArgs e)		
	{		
		if(!Page.IsPostBack) {
			CreateGrid();
		}

	}
	void CreateGrid()
	{
		OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/Northwind.mdb"));

		OleDbCommand myComm = new OleDbCommand("SELECT * FROM Suppliers", myConn);
		myConn.Open();		
		OleDbDataReader myReader = myComm.ExecuteReader();

		grid1.DataSource = myReader;
		grid1.DataBind();

		myConn.Close();	
	}	
		
	void DeleteRecord(object sender, GridRecordEventArgs e)
	{/*
		OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/Northwind.mdb"));
		myConn.Open();

        OleDbCommand myComm = new OleDbCommand("DELETE FROM Suppliers WHERE SupplierID = @SupplierID", myConn);

        myComm.Parameters.Add("@SupplierID", OleDbType.Integer).Value = e.Record["SupplierID"];
		
        myComm.ExecuteNonQuery();
		myConn.Close();*/
	}
	void UpdateRecord(object sender, GridRecordEventArgs e)
	{/*
		OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/Northwind.mdb"));
		myConn.Open();

        OleDbCommand myComm = new OleDbCommand("UPDATE Suppliers SET CompanyName = @CompanyName, Address = @Address, Country=@Country, HomePage=@HomePage WHERE SupplierID = @SupplierID", myConn);

        myComm.Parameters.Add("@CompanyName", OleDbType.VarChar).Value = e.Record["CompanyName"];
        myComm.Parameters.Add("@Address", OleDbType.VarChar).Value = e.Record["Address"];
        myComm.Parameters.Add("@Country", OleDbType.VarChar).Value = e.Record["Country"];
        myComm.Parameters.Add("@HomePage", OleDbType.VarChar).Value = e.Record["HomePage"];
        myComm.Parameters.Add("@SupplierID", OleDbType.Integer).Value = e.Record["SupplierID"];
        
        myComm.ExecuteNonQuery();
		myConn.Close();*/
	}
	void InsertRecord(object sender, GridRecordEventArgs e)
	{/*
		OleDbConnection myConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("../App_Data/Northwind.mdb"));
		myConn.Open();        		

        OleDbCommand myComm = new OleDbCommand("INSERT INTO Suppliers (CompanyName, Address, Country, HomePage) VALUES(@CompanyName, @Address, @Country, @HomePage)", myConn);

        myComm.Parameters.Add("@CompanyName", OleDbType.VarChar).Value = e.Record["CompanyName"];
        myComm.Parameters.Add("@Address", OleDbType.VarChar).Value = e.Record["Address"];
        myComm.Parameters.Add("@Country", OleDbType.VarChar).Value = e.Record["Country"];
        myComm.Parameters.Add("@HomePage", OleDbType.VarChar).Value = e.Record["HomePage"];
        
        myComm.ExecuteNonQuery();
		myConn.Close();*/
	}
	void RebindGrid(object sender, EventArgs e)
	{
		CreateGrid();
	}

</script>		

<html>
<head>
	<script type="text/javascript">
  function onGridSelect(arrSelectedRecords)
    {
        cbo1.setValue(arrSelectedRecords[0].SupplierID);
        cbo1.setText(arrSelectedRecords[0].CompanyName);
        //alert(arrSelectedRecords[0].SupplierID + "  " + arrSelectedRecords[0].CompanyName)
		cbo1.close();

    }
	/*

    function onload(){
		try{
			var grid = document.getElementById('grid_tmp_cont');
			var cont = document.getElementById('grid_inside_option');
			cont.appendChild(grid);
			grid.style.display="";
		}catch(ex){ };
        
    }
	function addLoadEvent(func) {
	  var oldonload = window.onload;
	  if (typeof window.onload != 'function') {
		window.onload = func;
		func();
	  } else {
		window.onload = function() {
		  if (oldonload) {
			oldonload();
		  }
		  func();
		}
	  }
	
	}
	addLoadEvent(onload);
	*/
	var flag = false;
	function onClientOpen() {
		try{
			if (!flag)
			{
				var grid = document.getElementById('grid_tmp_cont');
				var cont = document.getElementById('grid_inside_option');
				cont.appendChild(grid);
				grid.style.display="";
				flag = true;
			}
		}catch(ex){ alert(ex); };
	}
	</script>
</head>
<body>
<form id="Form1" runat="server">
		<br />
		<span class="h3">Using Custom Options.</span><br /><br />

		<br /><br />
		<br /><br />Select Supplier:
		<cbo:Combobox runat="server" ID="cbo1" Width="300" InnerWidth="570"	
			FolderStyle="styles/gray" OnClientOpen="onClientOpen" Validate="false" AlignContainer="left">
			<CustomOptions>
				<cbo:Custom ID="Option1" runat="server">
                    <div id="grid_inside_option"> </div>
				</cbo:Custom>
			</CustomOptions>
		</cbo:Combobox>	

		<div id="grid_tmp_cont" style="display:none;">
				<obout:Grid id="grid1" runat="server" CallbackMode="true" Serialize="true"  AllowRecordSelection="true" EnableRecordHover="true"
					 FolderStyle="../grid/styles/style_5" AutoGenerateColumns="false" AllowAddingRecords="false" AllowPageSizeSelection="false" AllowPaging="false" AllowSorting="false" 
					 OnRebind="RebindGrid" OnInsertCommand="InsertRecord"  OnDeleteCommand="DeleteRecord" OnUpdateCommand="UpdateRecord" >
					<ClientSideEvents OnClientSelect="onGridSelect"/>
					<Columns>
						<obout:Column ID="Column1" DataField="SupplierID" ReadOnly="true" HeaderText="ID" Width="60" runat="server"/>				
						<obout:Column ID="Column2" DataField="CompanyName" HeaderText="Company Name" Width="250" runat="server"/>	
						<obout:Column ID="Column3" DataField="Country" HeaderText="Country" Width="150" runat="server" />
						<obout:Column ID="Column4" DataField="HomePage" HeaderText="Has WebSite" TemplateID="TemplateHasWebsite"  Width="105" runat="server" />
					</Columns>
					<Templates>				
						<obout:GridTemplate runat="server" ID="TemplateHasWebsite" UseQuotes="true">
							<Template>
								<%# (Container.Value == "true" ? "yes" : "no") %>
							</Template>
						</obout:GridTemplate>
					</Templates>
				</obout:Grid>
		</div>
		<br />
		<br /><br />

</form>
</body>
</html>



