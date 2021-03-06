﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolsView.aspx.cs" Inherits="gui.Gui.SchoolsView" %>

<%@ Register Src="../Documents/nav.ascx" TagName="nav" TagPrefix="uc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>

</head>
<body>

    <form id="form1" runat="server">

        <uc1:nav ID="nav1" runat="server" />
        <br />

       
        <div class="container">
            <h2>בתי ספר</h2>
            <hr />

            <div class="row">

                <!-- Filter -->
                <div class="col-md-4">
                    <div class="form-inline">
                    

                        <asp:DropDownList class="form-control" runat="server" ID="DropDownListAreas">
                            <asp:ListItem>אזור</asp:ListItem>
                        </asp:DropDownList>

                        <asp:Button runat="server" ID="filter" OnClick="filter_Click" class="btn btn-info" Text="סינון" />

                    </div>
                </div>
                <!-- Search-->
                <div class="col-md-8">
                    <div class="form-inline">
   
                        <asp:TextBox ID="nameText" runat="server" type="text" class="form-control" placeholder="שם" Width="200px"></asp:TextBox>
                        או 
                        <asp:TextBox ID="symbolText" runat="server"  type="number" class="form-control" placeholder="סמל מוסד"  Width="200px"></asp:TextBox>

                        <asp:Button runat="server" ID="search" class="btn btn-info" OnClick="search_Click" Text="חיפוש" />

                        <asp:Button runat="server" ID="Clear"  OnClick="Clear_Click" class="btn btn-info" Text="ניקוי" />
                    </div>
                    <br />
                </div>

            </div>
            <%--end row--%>
            <%--Table--%>
                <asp:Button runat="server" Visible="false" ID="expot"  class="btn btn-info" Text="ייצא לאקסל" OnClick="btnExportExcel_Click" />
            <br />
             <asp:Label runat="server" ID="Sum"></asp:Label>
              <br />

            <asp:Table ID="ScoolsTable" runat="server" CssClass="table table-hover">
                <asp:TableRow>
                    <asp:TableCell Font-Bold="true">מס'</asp:TableCell>
                     <asp:TableCell Font-Bold="true">
                        <asp:LinkButton ForeColor="Black" runat="server" ID="LinkButton1">שם בית ספר 
                            <span class="glyphicon glyphicon-sort"></span>
                        </asp:LinkButton>
                    </asp:TableCell>
                    <asp:TableCell Font-Bold="true"> סמל מוסד</asp:TableCell>
                    <asp:TableCell Font-Bold="true"> איזור </asp:TableCell>
                    <asp:TableCell Font-Bold="true"> כתובת</asp:TableCell>
                    <asp:TableCell Font-Bold="true"> שם איש קשר</asp:TableCell>
                    <asp:TableCell Font-Bold="true"> טלפון איש הקשר</asp:TableCell>
                    <asp:TableCell Font-Bold="true"> אימייל איש קשר</asp:TableCell>
                    <asp:TableCell Font-Bold="true"> פעולות</asp:TableCell>
                </asp:TableRow>

            </asp:Table>          
            
              <br />
        </div>
    </form>
    <script>   $(" .navbar a:contains('בתי ספר')").parent().addClass('active');</script>
</body>
</html>
