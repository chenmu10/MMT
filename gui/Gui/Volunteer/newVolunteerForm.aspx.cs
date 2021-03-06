﻿using gui.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace gui
{
    public partial class Default : System.Web.UI.Page
    {
      // bug in Send_Click. 

        DB db;
        public bool FormOkToDB = true;
        DateTime LastPostRequest;
      

        override protected void OnInit(EventArgs e)
        {
            LastPostRequest = DateTime.Now;
            this.Load += new System.EventHandler(this.Page_Load);
            db = new DB();
            db.IsConnect();
            GetAreasFromDB();

            bool check = true;

            if (check)
            {
                Firstname.Text = "חן";
                Lastname.Text = "מ";
                Firstnameeng.Text = "chen";
                Lastnameeng.Text = "mu";
                Email.Text = "chenmu10@gmail.com";
                Phone.Text = "050-0000000";
                Employer.Text = "google";
                otherRef.Text = "";
                DropDownListOccupation.SelectedIndex = 2; // occupation
                DropDownListReference.SelectedIndex = 2; // reference

            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            FormOkToDB = !FormOkToDB;
            if (Page.IsPostBack && !IsEmptyFields() && FormOkToDB)
            {
                LastPostRequest = DateTime.Now;
                InsertVolunteerToDB();
            }

        }


        public void GetAreasFromDB()
        {
            // Areas: activity is multiple choice. training is only one choise. from same list of areas
            List<ListItem> Areas = db.GetAllAreas();
            if (CheckBoxListAreas.Items.Count < 1)
            {
                for (int i = 0; i < Areas.Count; i++)
                {
                    CheckBoxListAreas.Items.Add(new ListItem(Areas[i].Text, i.ToString()));
                    DropDownListTraining.Items.Add(new ListItem(Areas[i].Text, i.ToString()));

                }

            }

        }

        private void ClearForm()
        {
            AreaErrorMsg.Text = "";
            Firstname.Text = "";
            Lastname.Text = "";
            Firstnameeng.Text = "";
            Lastnameeng.Text = "";
            Email.Text = "";
            Phone.Text = "";
            Employer.Text = "";
            otherRef.Text = "";
            DropDownListOccupation.SelectedIndex = 0; // occupation
            DropDownListReference.SelectedIndex = 0; // reference
            DropDownListTraining.SelectedIndex = 0;

            foreach (ListItem item in CheckBoxListAreas.Items) // activityAreas
            {
                if (item.Selected)
                {
                    item.Selected = false;
                }

            }
        }

        private void InsertVolunteerToDB()
        {
            // get all values from fields
            string firstnamevalue = Firstname.Text.ToString();
            string firstnameEngValue = Firstnameeng.Text.ToString();
            string lastnamevalue = Lastname.Text.ToString();
            string lastnameEngValue = Lastnameeng.Text.ToString();
            string emailvalue = Email.Text.ToString();
            string phonevalue = Phone.Text.ToString();
            string employervalue = Employer.Text.ToString();
            string occupation = DropDownListOccupation.SelectedValue.ToString();
            int trainingArea = Convert.ToInt32(DropDownListTraining.SelectedValue)+1;
            string reference;

            // check "other" field
            if (DropDownListReference.SelectedValue == "אחר")
            {
                reference = otherRef.Text.ToString();
            }
            else
            {
                reference = DropDownListReference.SelectedValue.ToString();
            }

            // get all selected from checkbox
            List<int> SelectedAreas = new List<int>();
            foreach (ListItem item in CheckBoxListAreas.Items)
            {
                if (item.Selected)
                {
                    SelectedAreas.Add(int.Parse(item.Value)+1);
                }
            }

            Volunteer NewVolunteer = new Volunteer(firstnamevalue, firstnameEngValue, lastnamevalue, lastnameEngValue, emailvalue, phonevalue, occupation, reference, SelectedAreas, employervalue, 0, trainingArea);

            if (db.IsVolunteerExist(NewVolunteer))
            {
                Response.Write("<script>alert('מתנדבת קיימת. צרי קשר במקרה של שינוי/בעיה');</script>");
            }
            else if (db.InsertNewVolunteer(NewVolunteer,false))
            {
                ClearForm();
                //Response.Write("<script>alert('נוספת בהצלחה למאגר'); window.location.href = '../Documents/SuccessNewVolunteer.aspx'; </script>");
                Response.Write("<script>alert('נוספת בהצלחה למאגר');</script>");
                //Thread.Sleep(50);
                Response.Redirect("../Documents/SuccessNewVolunteer.aspx", false);
            }
            else
            {
                Response.Write("<script>alert('שגיאה בגישה למאגר');</script>");
            }

        }

        private bool CheckAreaCheckList()
        {
            bool result = false;
            foreach (ListItem item in CheckBoxListAreas.Items)
            {
                if (item.Selected)
                {
                    result = true;
                }

            }
            return result;
        }


        private bool IsEmptyFields()
        {
            if (string.IsNullOrWhiteSpace(Firstname.Text) ||
                 string.IsNullOrWhiteSpace(Lastname.Text) ||
                 string.IsNullOrWhiteSpace(Email.Text) ||
                 string.IsNullOrWhiteSpace(Phone.Text) ||
                 DropDownListOccupation.SelectedIndex == 0 ||
                 string.IsNullOrWhiteSpace(Employer.Text) ||
                 DropDownListReference.SelectedIndex == 0)
            {
                Response.Write("<script>alert('נא למלא את כל השדות');</script>");
                return true;
            }

            if (DropDownListTraining.SelectedValue == "x")
            {
                Response.Write("<script>alert('בעיה עם הכשרה');</script>");
                return true;
            }

            if (!IsAreaChecked())
            {
                Response.Write("<script>alert('לא נבחר אזור פעילות');</script>");
                AreaErrorMsg.Text = "חובה לבחור לפחות אזור אחד";
                return true;
            }

            if (DropDownListReference.SelectedValue == "אחר")
            {
                if (string.IsNullOrWhiteSpace(otherRef.Text))
                {
                    Response.Write("<script>alert('חסר שדה 'אחר'');</script>");
                    return true;
                }
            }
            return false;
        }

        private bool IsAreaChecked() // did user choose at least one area
        {
            foreach (ListItem item in CheckBoxListAreas.Items)
            {
                if (item.Selected)
                {
                    return true;
                }

            }
            return false;
        }

    }
}






