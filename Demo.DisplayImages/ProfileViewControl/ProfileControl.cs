using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace MedDispense.UI
{
    /// <summary>
    /// Summary description for UserControl1.
    /// </summary>
    public class ProfileControl : System.Windows.Forms.UserControl
    {
        private System.Windows.Forms.Panel _panel1;
        private System.Windows.Forms.Label _lblBillnum;
        private System.Windows.Forms.Label _lblStrength;
        private System.Windows.Forms.Label _lblBrandName;
        private System.Windows.Forms.Label _lblGenericName;
        private System.Windows.Forms.Label _lblFreq;
        private static int _controlHeight;
        private static int _controlWidth;
        private bool _isInDispenseList;
        private int _OrderID;
        private PictureBox _picDrugInfo;
        private bool _hasClickEvent = false;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ProfileControl()
        {
            FP.ControlFP();
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            _controlHeight = _panel1.Height;
            _controlWidth = _panel1.Width;

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfileControl));
            this._panel1 = new System.Windows.Forms.Panel();
            this._picDrugInfo = new System.Windows.Forms.PictureBox();
            this._lblFreq = new System.Windows.Forms.Label();
            this._lblStrength = new System.Windows.Forms.Label();
            this._lblBrandName = new System.Windows.Forms.Label();
            this._lblGenericName = new System.Windows.Forms.Label();
            this._lblBillnum = new System.Windows.Forms.Label();
            this._panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._picDrugInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // _panel1
            // 
            this._panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._panel1.Controls.Add(this._picDrugInfo);
            this._panel1.Controls.Add(this._lblFreq);
            this._panel1.Controls.Add(this._lblStrength);
            this._panel1.Controls.Add(this._lblBrandName);
            this._panel1.Controls.Add(this._lblGenericName);
            this._panel1.Controls.Add(this._lblBillnum);
            this._panel1.Location = new System.Drawing.Point(0, 0);
            this._panel1.Name = "_panel1";
            this._panel1.Size = new System.Drawing.Size(271, 81);
            this._panel1.TabIndex = 4;
            this._panel1.Click += new System.EventHandler(this.ProfileControl_Click);
            // 
            // _picDrugInfo
            // 
            this._picDrugInfo.Image = ((System.Drawing.Image)(resources.GetObject("_picDrugInfo.Image")));
            this._picDrugInfo.InitialImage = ((System.Drawing.Image)(resources.GetObject("_picDrugInfo.InitialImage")));
            this._picDrugInfo.Location = new System.Drawing.Point(0, 0);
            this._picDrugInfo.Name = "_picDrugInfo";
            this._picDrugInfo.Size = new System.Drawing.Size(61, 58);
            this._picDrugInfo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._picDrugInfo.TabIndex = 9;
            this._picDrugInfo.TabStop = false;
            this._picDrugInfo.Click += new System.EventHandler(this.ProfileControl_Click);
            // 
            // _lblFreq
            // 
            this._lblFreq.BackColor = System.Drawing.SystemColors.ControlDark;
            this._lblFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblFreq.Location = new System.Drawing.Point(195, 61);
            this._lblFreq.Name = "_lblFreq";
            this._lblFreq.Size = new System.Drawing.Size(70, 15);
            this._lblFreq.TabIndex = 7;
            this._lblFreq.Click += new System.EventHandler(this.ProfileControl_Click);
            // 
            // _lblStrength
            // 
            this._lblStrength.BackColor = System.Drawing.SystemColors.GrayText;
            this._lblStrength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblStrength.Location = new System.Drawing.Point(0, 61);
            this._lblStrength.Name = "_lblStrength";
            this._lblStrength.Size = new System.Drawing.Size(192, 15);
            this._lblStrength.TabIndex = 6;
            this._lblStrength.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._lblStrength.Click += new System.EventHandler(this.ProfileControl_Click);
            // 
            // _lblBrandName
            // 
            this._lblBrandName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblBrandName.Location = new System.Drawing.Point(64, 33);
            this._lblBrandName.Name = "_lblBrandName";
            this._lblBrandName.Size = new System.Drawing.Size(201, 24);
            this._lblBrandName.TabIndex = 5;
            this._lblBrandName.Text = "This the long drug name that has Solution and mg/ml and other stuff to see if it " +
                "all fits in the space";
            this._lblBrandName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lblBrandName.Click += new System.EventHandler(this.ProfileControl_Click);
            // 
            // _lblGenericName
            // 
            this._lblGenericName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._lblGenericName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblGenericName.Location = new System.Drawing.Point(64, 0);
            this._lblGenericName.Name = "_lblGenericName";
            this._lblGenericName.Size = new System.Drawing.Size(201, 31);
            this._lblGenericName.TabIndex = 4;
            this._lblGenericName.Text = "This the long drug name that has Solution and mg/ml and other stuff to see if it " +
                "all fits in the space";
            this._lblGenericName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lblGenericName.Click += new System.EventHandler(this.ProfileControl_Click);
            // 
            // _lblBillnum
            // 
            this._lblBillnum.BackColor = System.Drawing.SystemColors.GrayText;
            this._lblBillnum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblBillnum.Location = new System.Drawing.Point(66, 61);
            this._lblBillnum.Name = "_lblBillnum";
            this._lblBillnum.Size = new System.Drawing.Size(106, 15);
            this._lblBillnum.TabIndex = 8;
            this._lblBillnum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._lblBillnum.Click += new System.EventHandler(this.ProfileControl_Click);
            // 
            // ProfileControl
            // 
            this.Controls.Add(this._panel1);
            this.Name = "ProfileControl";
            this.Size = new System.Drawing.Size(271, 81);
            this.Click += new System.EventHandler(this.ProfileControl_Click);
            this._panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._picDrugInfo)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void ProfileControl_Click(object sender, System.EventArgs e)
        {
        }

        public static int ControlHeight
        {
            get { return _controlHeight; }
        }

        public static int ControlWidth
        {
            get { return _controlWidth; }
        }

        public Label DrugName
        {
            get { return _lblGenericName; }
            set { _lblGenericName = value; }

        }

        public Label Brand
        {
            get { return _lblBrandName; }
            set { _lblBrandName = value; }

        }

        public Label Billnum
        {
            get { return _lblBillnum; }
            set { _lblBillnum = value; }

        }

        public Label Strength
        {
            get { return _lblStrength; }
            set { _lblStrength = value; }

        }

        public Label Freq
        {
            get { return _lblFreq; }
            set { _lblFreq = value; }

        }

        public PictureBox picDrugInfo
        {
            get { return _picDrugInfo; }
            set { _picDrugInfo = value; }
        }

        public System.Drawing.Color LabelBackColor
        {
            set { _lblGenericName.BackColor = value; }
        }

        public Panel Panel1
        {
            get { return _panel1; }
            set { _panel1 = value; }
        }

        String _imageNmae;
        public String ImageName
        {
            get { return _imageNmae; }
            set
            {
                _imageNmae = value;
                try
                {
                    _picDrugInfo.Image = Image.FromFile(_imageNmae);
                }
                catch
                {
                    //LOG
                }
            }
        }

        public void SetBackColor(Color c)
        {
            this.BackColor = c;
            _lblGenericName.BackColor = c;
            _lblBrandName.BackColor = c;
            _lblStrength.BackColor = c;
            _lblBillnum.BackColor = c;
            _lblFreq.BackColor = c;
        }

        public void SetColors(ProfileControl pc, int ProfileButtonColor)
        {
            switch (ProfileButtonColor)
            {
                case 0: // Teal
                    pc.DrugName.ForeColor = Color.Lime;
                    pc.Strength.ForeColor = Color.White;
                    pc.Freq.ForeColor = Color.White;
                    pc.Billnum.ForeColor = Color.White;
                    pc.Brand.ForeColor = Color.White;
                    pc.SetBackColor(Color.Teal);
                    pc.Strength.BackColor = Color.Black;

                    break;
                case 1: // Green
                    pc.DrugName.ForeColor = Color.Lime;
                    pc.Strength.ForeColor = Color.White;
                    pc.Freq.ForeColor = Color.White;
                    pc.Billnum.ForeColor = Color.White;
                    pc.Brand.ForeColor = Color.Yellow;
                    pc.Strength.BackColor = Color.Black;
                    pc.SetBackColor(Color.Green);
                    break;
                case 2: // White
                    pc.DrugName.ForeColor = Color.Black;
                    pc.Strength.ForeColor = Color.White;
                    pc.Freq.ForeColor = Color.Black;
                    pc.Billnum.ForeColor = Color.Black;
                    pc.Brand.ForeColor = Color.Black;
                    pc.SetBackColor(Color.White);
                    pc.Strength.BackColor = Color.Black;
                    break;
                case 3: // Silver
                    pc.DrugName.ForeColor = Color.Black;
                    pc.Strength.ForeColor = Color.White;
                    pc.Freq.ForeColor = Color.Black;
                    pc.Billnum.ForeColor = Color.Black;
                    pc.Brand.ForeColor = Color.Black;
                    pc.SetBackColor(Color.Silver);
                    pc.Strength.BackColor = Color.Black;
                    break;
                default:
                    break;
            }
            //pc.Strength.BackColor = Color.Black;
            _picDrugInfo.BackColor = Color.Transparent;
        }

        public void SetVisibleLabels(ProfileControl pc, int ProfileViewMode)
        {
            switch (ProfileViewMode)
            {
                case 0:
                    pc.Freq.Visible = false;
                    pc.Strength.Visible = false;
                    pc.Billnum.Visible = false;
                    pc.Brand.Visible = false;
                    break;
                case 1:
                    pc.Freq.Visible = false;
                    pc.Strength.Visible = false;
                    pc.Billnum.Visible = false;
                    pc.Brand.Visible = true;
                    break;
                case 2:
                    pc.Freq.Visible = true;
                    pc.Strength.Visible = false;
                    pc.Billnum.Visible = false;
                    pc.Brand.Visible = true;
                    break;
                case 3:
                    pc.Freq.Visible = true;
                    pc.Strength.Visible = true;
                    pc.Billnum.Visible = false;
                    pc.Brand.Visible = true;
                    break;
                case 4:
                    pc.Freq.Visible = true;
                    pc.Strength.Visible = true;
                    pc.Billnum.Visible = true;
                    pc.Brand.Visible = true;
                    break;
                case 5:
                    pc.Freq.Visible = true;
                    pc.Strength.Visible = true;
                    pc.Billnum.Visible = false;
                    pc.Brand.Visible = false;
                    break;
                default:
                    break;
            }
        }

        public bool IsInDispenseList
        {
            get { return _isInDispenseList; }
            set { _isInDispenseList = value; }
        }
        public bool hasClickEvent
        {
            get { return _hasClickEvent; }
            set { _hasClickEvent = value; }
        }
        public int OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

    }
}
