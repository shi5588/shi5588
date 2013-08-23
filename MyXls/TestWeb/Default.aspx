<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="org.in2bits.MyXls._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>MyXLS - Test Page</title>
	<meta name="Keywords" content="sf.net,aspbiff8,aspole2,vbscript,asp,ole2,excel,generate,multiple worksheets,multiple ranges,no com,erickson" />
	<meta name="Description" content="ASPBIFF8 is a SourceForge.net project developing an ASP VBScript class to generate and stream Excel files supporting multiples ranges/sheets/formats from a webserver without COM (the Excel object)" />
</head>
<body>
    <h2>MyXLS Excel File Writer Demo</h2>
<p>This page allows you to see some of the functionality of the <a href="http://myxls.sourceforge.net">Code</a> 
	in action.  Select your parameters (Sheet name, # Rows, # Columns, Topmost Row #, Leftmost Column #) and select 'Go!' and you will 
	shortly receive an Excel Workbook (named testing.xls) containing as many worksheets as you have named, populated with a dataset of 
	the dimensions you specified.  If you do not specify any sheet names (removing the default as well), your workbook will consist of 
	one empty sheet named Sheet1.  If you have any problems or feedback, please notify Manmoth via the 
	<a href="http://sourceforge.net/projects/myxls">MyXLS Sourceforge.net project page</a>.</p>
    <form id="form1" runat="server">
    <div>
<table border="0" cellspacing="5">
	<thead>
		<tr><th colspan="5"><b>Select parameters for up to 5 Worksheets for generated file:</b></th></tr>
		<tr><th>Sheet Name</th><th>Rows (0-5000)</th><th>Cols (1-50)</th><th>Row1 (1-32767)</th><th>Col1 (1-100)</th></tr></thead>
	<tbody>
		<tr><td><asp:TextBox ID="txtSheet1" MaxLength="35" Text="ASPBIFF8_Sheet1" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRows1" MaxLength="5" Text="10" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtCols1" MaxLength="3" Text="5" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRowMin1" MaxLength="5" Text="1" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtColMin1" MaxLength="3" Text="1" runat="server"></asp:TextBox></td></tr>
		<tr><td><asp:TextBox ID="txtSheet2" MaxLength="35" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRows2" MaxLength="5" Text="10" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtCols2" MaxLength="3" Text="5" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRowMin2" MaxLength="5" Text="1" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtColMin2" MaxLength="3" Text="1" runat="server"></asp:TextBox></td></tr>
		<tr><td><asp:TextBox ID="txtSheet3" MaxLength="35" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRows3" MaxLength="5" Text="10" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtCols3" MaxLength="3" Text="5" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRowMin3" MaxLength="5" Text="1" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtColMin3" MaxLength="3" Text="1" runat="server"></asp:TextBox></td></tr>
		<tr><td><asp:TextBox ID="txtSheet4" MaxLength="35" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRows4" MaxLength="5" Text="10" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtCols4" MaxLength="3" Text="5" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRowMin4" MaxLength="5" Text="1" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtColMin4" MaxLength="3" Text="1" runat="server"></asp:TextBox></td></tr>
		<tr><td><asp:TextBox ID="txtSheet5" MaxLength="35" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRows5" MaxLength="5" Text="10" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtCols5" MaxLength="3" Text="5" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtRowMin5" MaxLength="5" Text="1" runat="server"></asp:TextBox></td>
			<td><asp:TextBox ID="txtColMin5" MaxLength="3" Text="1" runat="server"></asp:TextBox></td></tr>
		<tr><td align="center" colspan="5"><asp:Button ID="buttonSubmit" runat="server" Text="Go!" PostBackUrl="~/DemoMyXLS.aspx" /></td></tr></tbody>
</table>
    </div>
    </form>
</body>
</html>
