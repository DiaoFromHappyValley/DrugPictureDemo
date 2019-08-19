using MedDispense.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Demo.DisplayImages
{
    public partial class Form1 : Form
    {

        string Image_ExtensionName = ConfigurationManager.AppSettings["DrugImageType"];
        string Image_DefaultSize = ConfigurationManager.AppSettings["DrugImageDefaultSize"];

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var singleImageLoader = new ImageSingleLoader();
            var imageLocalDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            singleImageLoader.ImageLoadedCompletedNotify += (ImageObj imageObj) =>
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    Debug.WriteLine(string.Format("Image {0} Notified", imageObj.BillNumber));

                    var fileFullName = Path.Combine(imageLocalDir, string.Format("{0}.{1}", imageObj.BillNumber, Image_ExtensionName));

                    using (FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read))
                    {
                        pictureBox1.Image = Image.FromStream(fs);
                    }
                });
            };

            singleImageLoader.ImageLoaedExceptionNotify += (Exception ex, ImageObj imageObj) =>
              {
                  BeginInvoke((MethodInvoker)delegate
                {
                    if (imageObj != null)
                    {
                        richTextBox2.AppendText(string.Format("Load image by BillNumber({0}) exception: {1} {2}", imageObj.BillNumber, ex.Message, Environment.NewLine));
                    }
                });
              };

            singleImageLoader.LoadImageAsync(new ImageObj
            {
                BillNumber = txtBillNumber.Text.Trim(),
                ImageName = string.Format("{0}.{1}", txtBillNumber.Text.Trim(), Image_ExtensionName)
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            richTextBox2.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Red;

            btnLoadImage.Enabled = false;
            RefrehUI(false);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            btnInitializeControls.Enabled = false;

            InitializeUI();

            btnLoadImage.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            if (groupBox1.Controls != null
                && groupBox1.Controls.Count > 0
                && groupBox1.Controls[0].Controls != null)
            {
                foreach (var item in groupBox1.Controls[0].Controls)
                {
                    if (item is PictureBox)
                    {
                        (item as PictureBox).Image = null;
                    }
                }

            }

            groupBox1.Controls.Clear();
            profileControls.Clear();

            btnInitializeControls.Enabled = true;
            btnLoadImage.Enabled = false;

        }
        private void button6_Click(object sender, EventArgs e)
        {
            btnClearLocalImages.Enabled = false;

            var imageLocalDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            if (Directory.Exists(imageLocalDir))
            {
                Directory.Delete(imageLocalDir, true);
            }

            Directory.CreateDirectory(imageLocalDir);
            btnClearLocalImages.Enabled = true;
        }
        List<ProfileControl> profileControls = new List<ProfileControl>();
        private void InitializeUI()
        {
            groupBox1.Controls.Clear();
            profileControls.Clear();

            var count = 12;

            FlowLayoutPanel fp = new FlowLayoutPanel { Dock = DockStyle.Fill };

            for (int i = 1; i <= count; i++)
            {
                var pc = new ProfileControl();
                pc.Billnum.Text = i.ToString();
                profileControls.Add(pc);
                fp.Controls.Add(pc);
            }

            groupBox1.Controls.Add(fp);
        }

        /// <summary>
        /// 多任务picturebox起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            RefrehUI(false);

            Thread t = new Thread(new ParameterizedThreadStart(DoProcessThread_Pool));
            t.Start(profileControls.Count);
        }

        private void DoProcessThread_Pool(object objCount)
        {
            int count = (int)objCount;

            var obj = new ImageMultipleLoader(count);

            var imageLocalDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

            obj.ImageLoadedCompletedNotify += (ImageObj imageObj) =>
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    var pc = profileControls.FirstOrDefault(o => o.Billnum.Text == imageObj.BillNumber);
                    if (pc != null)
                    {
                        Debug.WriteLine(string.Format("Image {0} Notified", imageObj.BillNumber));

                        var fileFullName = Path.Combine(imageLocalDir, string.Format("{0}.{1}", imageObj.BillNumber, Image_ExtensionName));

                        using (FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read))
                        {
                            pc.picDrugInfo.Image = Image.FromStream(fs);
                        }
                    }
                });
            };

            obj.ImageLoaedExceptionNotify += (Exception ex, ImageObj imagObj) =>
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    richTextBox1.AppendText(string.Format("Image {0} of {1} exception: {2} {3}", imagObj.ImageName, imagObj.BillNumber, ex.Message, Environment.NewLine));
                });
            };


            try
            {
                for (int i = 1; i <= count; i++)
                {
                    obj.LoadImageAsync(new ImageObj
                    {
                        BillNumber = i.ToString(),
                        ImageName = string.Format("{0}.{1}", i, Image_ExtensionName)
                    });
                }

                obj.finishedEvent.WaitOne();


                BeginInvoke((MethodInvoker)delegate
                {
                    RefrehUI(true);
                });

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void RefrehUI(bool enabled)
        {
            btnClearControls.Enabled = enabled;
            btnClearLocalImages.Enabled = enabled;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDefaultImageSize.Text.Trim()))
            {
                defaultImageSize = int.Parse(txtDefaultImageSize.Text.Trim());
            }


            var imageLocalDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
            var imageName = string.Format("{0}.{1}", txtBillNumber.Text.Trim(), Image_ExtensionName);
            var fileFullName = Path.Combine(imageLocalDir, imageName);

            using (FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read))
            {
                MessageBox.Show(fs.Length.ToString());

                if (IsValidLocalImage(fileFullName))
                {
                    MessageBox.Show("Valid Image");
                }
            }
        }


        int? defaultImageSize;
        internal bool IsValidLocalImage(string fileFullName)
        {
            using (FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read))
            {
                if (defaultImageSize.HasValue)
                {
                    var actualValue = defaultImageSize.Value;
                    var length = fs.Length;
                    if (Math.Abs((actualValue - 500)) < length && length < (actualValue + 500))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
