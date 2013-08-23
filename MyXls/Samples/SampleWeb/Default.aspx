<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>MyXls Samples page - in2bits.org</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="font-size: 32pt"><strong><em><a href="/">MyXls</a></em></strong></span><br />
        Examples:<br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/cs/Simplest.aspx">Simplest</asp:HyperLink>
        (<asp:HyperLink ID="hyperLinkCodeBehindSimplestCS" runat="server" NavigateUrl="~/cs/Simplest.aspx.cs.html" Target="_blank">C#</asp:HyperLink> or 
        <asp:HyperLink ID="hyperLinkCodeBehindSimplestVB" runat="server" NavigateUrl="~/vb/Simplest.aspx.vb.html" Target="_blank">VB</asp:HyperLink> code-behind)
        - The very simplest MyXLS scenario.&nbsp; Creates and sends a Workbook
        with 1 Worksheet and no data.&nbsp; The File/Workbook name defaults to "Book1.xls"
        and when no worksheets are explicitly added, by default you get one named "Sheet1".&nbsp;
        Sound familiar?<br />
        This demonstrates the barebones code necessary to place this on a page.&nbsp; It
        shows the using statement to make things easier, the instantiation of the XlsDocument
        object, and calling the Send method on that XlsDocument object.&nbsp; The Send method
        handles checking the HttpResponse for buffering, clearing the buffer, writing the
        file, and flushing the buffer.&nbsp; You won't see anything on the web page, just
        get a popup prompt to download your file.<br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/cs/HelloWorld.aspx">Hello, World!</asp:HyperLink>
        (<asp:HyperLink ID="hyperLinkCodeBehindHelloWorldCS" runat="server" NavigateUrl="~/cs/HelloWorld.aspx.cs.html" Target="_blank">C#</asp:HyperLink> or 
        <asp:HyperLink ID="hyperLinkCodeBehindHelloWorldVB" runat="server" NavigateUrl="~/vb/HelloWorld.aspx.vb.html" Target="_blank">VB</asp:HyperLink> code-behind) - An approximation of a classic programming example.&nbsp; One Workbook
        "MyXLS_Hello_World.xls" with one Worksheet "Hello, World!" and cell A1 says "Hello,
        World!".&nbsp; This just adds three lines to the Simplest scenario: naming the file
        the Excel file generated and sent to the user, adding a Sheet with a specific name,
        and writing a value to a cell.<br />
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/vb/Wacky.aspx">Wacky</asp:HyperLink>
        (<asp:HyperLink ID="hyperLinkCodeBehindWackyCS" runat="server" NavigateUrl="~/cs/Wacky.aspx.cs.html" Target="_blank">C#</asp:HyperLink> or 
        <asp:HyperLink ID="hyperLinkCodeBehindWackyVB" runat="server" NavigateUrl="~/vb/Wacky.aspx.vb.html" Target="_blank">VB</asp:HyperLink> code-behind) - The key components of this are the lines that get the new XF object
        and initialize it to the desired formatting. It's not immediately obvious,
        but IntelliSense should help you figure out what how to do what you want.&nbsp;
        This is pretty ridiculous, really, but demonstrates that MyXLS can indeed do genuine
        formatting beyond what you could accomplish with .csv files, pre-2007 Excel XML
        or HTML, or other free methods that don't generate an actual binary Excel file.&nbsp; <span style="text-decoration: underline"><strong><em>
            This sample demonstrates how to add metadata visible in the Excel File -&gt; Properties
            menu. </em></strong></span>&nbsp;
        Certainly you all will suggest many improvements and request additional documentation.&nbsp;
        We're only two guys so far, though, so maybe you could help out?&nbsp; Otherwise,
        we'll get to it when we can, but meanwhile are focused on filling out the featureset
        of MyXLS.&nbsp;
    
    </div>
    </form>
</body>
</html>
