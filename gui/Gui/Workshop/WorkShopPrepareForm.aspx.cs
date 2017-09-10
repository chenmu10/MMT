﻿using gui.Models;
using System;
using System.Web.UI;

namespace gui.Gui
{
    //TODO db.updateWorkshopByID

    public partial class WorkShopPrepareForm : System.Web.UI.Page
    {
        DB db;
        DateTime LastPostRequest;


        override protected void OnInit(EventArgs e)
        {
            LastPostRequest = DateTime.Now;
            this.Load += new System.EventHandler(this.Page_Load);
            db = new DB();
            db.IsConnect();

            string workshopID = Request.QueryString["workshopID"];
            string IsCompany = Request.QueryString["IsCompany"];

            FillWorkshopData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
 
        }

        private void FillWorkshopData()
        {
            string workshopID = Request.QueryString["workshopID"];
            PrepareForm selectedPrePare = new PrepareForm();

            if (workshopID != null){
                // check if prepare form exist
                selectedPrePare = db.getPrePareFormByWorkshopID(int.Parse(workshopID));
                if (selectedPrePare.Workshop_School_Workshop_ID != 0)
                {
                    // yes
                    WorkshopJoin workshopInfo = db.GetJoinWorkShopByID(selectedPrePare.Workshop_School_Workshop_ID);
                    School school = db.GetSchoolByID(int.Parse(workshopID));
                    //PrePare Form Is Ok 
                    _workshopID.Text = workshopID;
                    _schoolName.Text = workshopInfo.School_Name;
                    _WorkshopDate.Text = workshopInfo.WorkShop_Date;
                    _schoolAddress.Text = school.School_Address;
                    // TODO db.updateSchoolParkingBySchoolID
                }

                else
                {

                    //PrePare Form not created!
                }
            }


           
        }

        private bool IsFormValid()
        {
            if (estimatedParticipants == null ||
                numOfCompWithEmulator == null ||
                String.IsNullOrWhiteSpace(schoolparking.Text) ||
                RadioButtonListDidPrepare.SelectedIndex == -1 ||
                RadioButtonListProjectOrControl.SelectedIndex == -1 ||
                RadioButtonListSeniors.SelectedIndex == -1 ||
                RadioButtonListShowVideo.SelectedIndex == -1 ||
                String.IsNullOrWhiteSpace(teacherName.Text) ||
                String.IsNullOrWhiteSpace(teacherEmail.Text) ||
                String.IsNullOrWhiteSpace(teacherPhone.Text))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

     

        private void ClearForm()
        {
            estimatedParticipants.Text = String.Empty;
            numOfCompWithEmulator.Text = String.Empty;
            schoolparking.Text = String.Empty;
            RadioButtonListDidPrepare.SelectedIndex = -1;
            RadioButtonListProjectOrControl.SelectedIndex = -1;
            RadioButtonListSeniors.SelectedIndex = -1;
            RadioButtonListShowVideo.SelectedIndex = -1;
            teacherName.Text = String.Empty;
            teacherEmail.Text = String.Empty;
            teacherPhone.Text = String.Empty;
        }

        protected void UpdatePrepareToWorkshop(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                LastPostRequest = DateTime.Now;

                int schoolWorkshopID = Convert.ToInt32(Request.QueryString["workshopID"]);
                PrepareForm prepareInfo = db.getPrePareFormByWorkshopID(schoolWorkshopID);
                prepareInfo.WorkShop_Number_Of_Final_Student = Convert.ToInt32(estimatedParticipants.Text.ToString()); // check diff between num_of_student and amount
                prepareInfo.WorkShop_Is_Projector = Convert.ToBoolean(RadioButtonListProjectOrControl.SelectedValue);
                prepareInfo.WorkShop_Is_Seniors_Coming = Convert.ToBoolean(RadioButtonListSeniors.SelectedValue);
                prepareInfo.WorkShop_Did_Preparation = Convert.ToBoolean(RadioButtonListDidPrepare.SelectedValue);
                prepareInfo.WorkShop_Teacher_Name = teacherName.Text.ToString();
                prepareInfo.WorkShop_Teacher_Email = teacherEmail.Text.ToString();
                prepareInfo.WorkShop_Teacher_phone = teacherPhone.Text.ToString();
                prepareInfo.WorkShop_Comments = prepareComment.Text;
                prepareInfo.WorkShop_Is_Video_possible = Convert.ToBoolean(RadioButtonListShowVideo.SelectedValue);
                prepareInfo.WorkShop_Number_Of_emulator_Computer = Convert.ToInt32(numOfCompWithEmulator.Text.ToString());

                string schoolparkingValue = schoolparking.Text.ToString(); // MISSING db.update school parking by schoolID

                msg.InnerText = "עוד לא בוצע update";

                //if (db.UpdatePrepare(prepareInfo)) 
                //{
                //    Response.Redirect("SuccessForm.aspx", false);
                //}
                //else
                //{
                //    Response.Write("<script>alert('שגיאה בהכנסה ל-DB');</script>");
                //}
            }
        }
    }




}