using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // *************************************************************************
        // Properites for the entire Form - global variables for the form

        int meter1Start, meter1End, meter1Total, meter2Start, meter2End, meter2Total, meter3Start, meter3End, meter3Total;
        int priorBalance, meterCount;
        
        double totalKWH, kwhCharge, meterCharge, afterHours, missedAppt, totalDue;

        bool goodMeter1, goodMeter2, goodMeter3, goodPriorBalance, goodMeterCount, allIsGood;
        

        //***********************************************************************
        // Presentation Layer Features
        //       connects properties (variables) to the UI controls!
        //       ResetUI(), BindFromUI(), BindToUI(), SendCurserHome()

        // Clearing UI Controls
        private void ResetUI()
        {
            // Clearing all Text Boxes
            txtBxMeter1Start.Text = string.Empty;
            txtBxMeter1End.Text = string.Empty;
            txtBxMeter2Start.Text = string.Empty;
            txtBxMeter2End.Text = string.Empty;
            txtBxMeter3Start.Text = string.Empty;
            txtBxMeter3End.Text = string.Empty;
            txtBxPrior.Text = string.Empty;
            txtBxMeterCount.Text = string.Empty;

            // Resetting buttons
            btnAfterHours.Enabled = true;
            btnMissedAppt.Enabled = true;

            //Clearing all Labels
            lblMeter1.Text = "0";
            lblMeter2.Text = "0";
            lblMeter3.Text = "0";
            lblTotalKWH.Text = "0.00";
            lblKwhCharge.Text = "0.00";
            lblMeterCharge.Text = "0.00";
            lblAfterHours.Text = "0.00";
            lblMissedAppt.Text = "0.00";
            lblTotalDue.Text = "$0.00";
        }

        // Getting data From UI and Assigning to Variables
        private void BindFromUI()
        {
            goodMeter1 = int.TryParse(txtBxMeter1Start.Text, out meter1Start) && int.TryParse(txtBxMeter1End.Text, out meter1End);
            goodMeter2 = int.TryParse(txtBxMeter2Start.Text, out meter2Start) && int.TryParse(txtBxMeter2End.Text, out meter2End);
            goodMeter3 = int.TryParse(txtBxMeter3Start.Text, out meter3Start) && int.TryParse(txtBxMeter3End.Text, out meter3End);
            goodPriorBalance = int.TryParse(txtBxPrior.Text, out priorBalance);
            goodMeterCount = int.TryParse(txtBxMeterCount.Text, out meterCount);

            // bool for good data validation

            allIsGood = goodMeter1 && goodMeter2 && goodMeter3 && goodPriorBalance && goodMeterCount;
        }

        // Connecting the Variables to the UI Controls!
        private void BindToUI()
        {
            // Connecting variables to labels
            lblMeter1.Text = meter1Total.ToString();
            lblMeter2.Text = meter2Total.ToString();
            lblMeter3.Text = meter3Total.ToString();
            lblTotalKWH.Text = totalKWH.ToString();
            lblKwhCharge.Text = kwhCharge.ToString("F");
            lblMeterCharge.Text = meterCharge.ToString("F");
            lblAfterHours.Text = afterHours.ToString("F");
            lblMissedAppt.Text = missedAppt.ToString("F");
            lblTotalDue.Text = "$" + totalDue.ToString("F");
        }

        // Replacing Curser to start of UI
        private void SendCurserHome()
        {
            txtBxMeter1Start.Focus();
        }

        //**********************************************************************
        // Logic Layer
        //       does the work using only properties (variable)

        // Resetting the variables to 0
        private void ResetVariables()
        {
            // Resetting int Variables to 0
            meter1Start = 0;
            meter1End = 0;
            meter1Total = 0;
            meter2Start = 0;
            meter2End = 0;
            meter2Total = 0;
            meter3Start = 0;
            meter3End = 0;
            meter3Total = 0;
            priorBalance = 0;
            meterCount = 0;

            // Resetting double Variables to 0
            totalKWH = 0.0;
            kwhCharge = 0.0;
            meterCharge = 0.0;
            afterHours = 0.0;
            missedAppt = 0.0;
            totalDue = 0.0;
        }

        // Calling Reset Methods to Clear UI
        private void btnClear_Click_1(object sender, EventArgs e)
        {
            ResetUI();
            ResetVariables();
            SendCurserHome();
        }

        // Preforming the Variables Calculations
        private void btnReCalc_Click_1(object sender, EventArgs e)
        {
            BindFromUI();

            if (allIsGood)
            {
                totalDue = kwhCharge + meterCharge + afterHours + missedAppt + priorBalance;
            }

            else
            {
                MessageBox.Show("Some text fields have incorrect data. Please make sure that textboxes are not blank, and contain numeric data");
            }
            BindToUI();
        }

        // Apply After Hours Charge to Total
        private void btnAfterHours_Click_1(object sender, EventArgs e)
        {
            const double AFTER_HOURS = 50.00;
            afterHours = AFTER_HOURS;
            btnAfterHours.Enabled = false;
            BindToUI();
        }

        // Apply Missed Appt Charge to Total
        private void btnMissedAppt_Click_1(object sender, EventArgs e)
        {
            const double MISSED_APPT = 25.00;
            missedAppt = MISSED_APPT;
            btnMissedAppt.Enabled = false;
            BindToUI();
        }

        // Updating UI to display meter row total and current meter total 
        private void txtBxMeter1End_TextChanged(object sender, EventArgs e)
        {
            BindFromUI();
            meter1Total = meter1Start + meter1End;
            if (goodMeter1)
            {
                totalKWH = meter1Total + meter2Total + meter3Total;
                kwhCharge = 0.08199999999999999 * totalKWH;
            }
            else
            {
            }
            BindToUI();
        }

        // Updating UI to display meter row total and current meter total and  kwhCharge
        private void txtBxMeter2End_TextChanged(object sender, EventArgs e)
        {
            BindFromUI();
            meter2Total = meter2Start + meter2End;
            if (goodMeter2)
            {
                totalKWH = meter1Total + meter2Total + meter3Total;
                kwhCharge = 0.08199999999999999 * totalKWH;
            }

            else
            {
            }
            BindToUI();
        }

        // Updating UI to display meter row total and current meter total and  kwhCharge
        private void txtBxMeter3End_TextChanged(object sender, EventArgs e)
        {
            BindFromUI();
            meter3Total = meter3Start + meter3End;
            BindToUI();

            if (goodMeter3)
            {
                totalKWH = meter1Total + meter2Total + meter3Total;
                kwhCharge = 0.08199999999999999 * totalKWH;
            }
            else
            {
            }
            BindToUI();
        }

        // Updating UI to show meter charge
        private void txtBxMeterCount_TextChanged(object sender, EventArgs e)
        {
            BindFromUI();
            meterCharge = 25.00 * meterCount;
            BindToUI();
        }
    }
}
