﻿using gui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gui.Gui
{
    public partial class CompanyView : System.Web.UI.Page
    {
        List<Company> companies = new List<Company>();
        override protected void OnInit(EventArgs e)
        {
            this.Load += new System.EventHandler(this.Page_Load);
            DB db = new DB();
            db.IsConnect();
            companies = db.GetAllComapny();
            foreach (Company company in companies)
            {
                TableCell id = new TableCell();
                id.Text = company.Company_ID.ToString();

                TableCell Name = new TableCell();
                Name.Text = company.Company_Name;

                TableCell Address = new TableCell();
                Address.Text = company.Company_Address;

                TableCell ContectName = new TableCell();
                ContectName.Text = company.Company_Contact_Name;

                TableCell ContectEmail = new TableCell();
                ContectEmail.Text = company.Company_Contact_Email;

                TableCell ContectPhone = new TableCell();
                ContectPhone.Text = company.Company_Contact_phone;

                TableCell Edit = new TableCell();
                Button Editbtn = new Button();
                Editbtn.Text = "צפייה";
                Editbtn.CssClass = "btn btn-default";
                Edit.Controls.Add(Editbtn);

                TableRow TableRow = new TableRow();
                TableRow.Cells.Add(id);
                TableRow.Cells.Add(Name);
                TableRow.Cells.Add(Address);
                TableRow.Cells.Add(ContectName);
                TableRow.Cells.Add(ContectPhone);
                TableRow.Cells.Add(ContectEmail);

                TableRow.Cells.Add(Edit);

                companyTable.Rows.Add(TableRow);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void newComapnyWorkshopBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewCompanyForm.aspx", false);
        }
        
    }
}