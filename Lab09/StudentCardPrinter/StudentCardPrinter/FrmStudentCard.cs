using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace StudentCardPrinter
{
    public partial class FrmStudentCard : Form
    {
        private TextBox txtStudentId;
        private TextBox txtFullName;
        private ComboBox cboFaculty;    
        private TextBox txtCourse;      
        private ComboBox cboClass;      
        private DateTimePicker dtpBirthDate;
        private ComboBox cboGender;
        
        private PictureBox picAvatar;
        private Button btnChoosePhoto;
        private Panel pnlCardPreview;
        
        private Button btnClear;
        private Button btnPreviewPrint;
        private Button btnPrint;
        private Button btnExit;

        private PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
        private PrintDialog printDialog1;

        public FrmStudentCard()
        {
            InitializeComponentCustom();
            InitializeData();
            RegisterEvents();
        }

        private void InitializeComponentCustom()
        {
            this.Text = "Thiết kế và in thẻ sinh viên";
            this.Size = new Size(1000, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

         
            GroupBox grbInfo = new GroupBox();
            grbInfo.Text = "Thông tin sinh viên";
            grbInfo.Location = new Point(12, 12);
            grbInfo.Size = new Size(460, 380);
            this.Controls.Add(grbInfo);

           
            Label lblStudentId = new Label { Text = "Mã sinh viên *:", Location = new Point(15, 30), AutoSize = true };
            txtStudentId = new TextBox { Location = new Point(120, 27), Size = new Size(320, 23) };
            grbInfo.Controls.AddRange(new Control[] { lblStudentId, txtStudentId });

         
            Label lblFullName = new Label { Text = "Họ tên *:", Location = new Point(15, 70), AutoSize = true };
            txtFullName = new TextBox { Location = new Point(120, 67), Size = new Size(320, 23) };
            grbInfo.Controls.AddRange(new Control[] { lblFullName, txtFullName });

           
            Label lblFaculty = new Label { Text = "Khoa:", Location = new Point(15, 110), AutoSize = true };
            cboFaculty = new ComboBox { Location = new Point(120, 107), Size = new Size(320, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            grbInfo.Controls.AddRange(new Control[] { lblFaculty, cboFaculty });

           
            Label lblCourse = new Label { Text = "Khóa học:", Location = new Point(15, 150), AutoSize = true };
            txtCourse = new TextBox { Location = new Point(120, 147), Size = new Size(320, 23) };
            grbInfo.Controls.AddRange(new Control[] { lblCourse, txtCourse });

            
            Label lblClass = new Label { Text = "Lớp:", Location = new Point(15, 190), AutoSize = true };
            cboClass = new ComboBox { Location = new Point(120, 187), Size = new Size(320, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            grbInfo.Controls.AddRange(new Control[] { lblClass, cboClass });

           
            Label lblBirthDate = new Label { Text = "Ngày sinh:", Location = new Point(15, 230), AutoSize = true };
            dtpBirthDate = new DateTimePicker { Location = new Point(120, 227), Size = new Size(320, 23), Format = DateTimePickerFormat.Short };
            grbInfo.Controls.AddRange(new Control[] { lblBirthDate, dtpBirthDate });

           
            Label lblGender = new Label { Text = "Giới tính:", Location = new Point(15, 270), AutoSize = true };
            cboGender = new ComboBox { Location = new Point(120, 267), Size = new Size(320, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            grbInfo.Controls.AddRange(new Control[] { lblGender, cboGender });


            GroupBox grbAvatar = new GroupBox();
            grbAvatar.Text = "Ảnh đại diện";
            grbAvatar.Location = new Point(12, 405);
            grbAvatar.Size = new Size(460, 210);
            this.Controls.Add(grbAvatar);

            picAvatar = new PictureBox();
            picAvatar.Location = new Point(15, 25);
            picAvatar.Size = new Size(130, 165);
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            grbAvatar.Controls.Add(picAvatar);

            btnChoosePhoto = new Button();
            btnChoosePhoto.Text = "Chọn ảnh...";
            btnChoosePhoto.Location = new Point(160, 40);
            btnChoosePhoto.Size = new Size(110, 35);
            grbAvatar.Controls.Add(btnChoosePhoto);

            Label lblNoteImg = new Label();
            lblNoteImg.Text = "Hỗ trợ: JPG, JPEG, PNG, BMP";
            lblNoteImg.Location = new Point(160, 85);
            lblNoteImg.AutoSize = true;
            grbAvatar.Controls.Add(lblNoteImg);


          
            GroupBox grbPreview = new GroupBox();
            grbPreview.Text = "Xem trước thẻ sinh viên";
            grbPreview.Location = new Point(485, 12);
            grbPreview.Size = new Size(485, 603);
            this.Controls.Add(grbPreview);

            pnlCardPreview = new Panel();
            pnlCardPreview.Location = new Point(15, 30);
            pnlCardPreview.Size = new Size(455, 555);
            pnlCardPreview.BackColor = Color.WhiteSmoke;
            grbPreview.Controls.Add(pnlCardPreview);


           
            btnClear = new Button { Text = "Làm mới", Location = new Point(12, 630), Size = new Size(90, 35) };
            btnPreviewPrint = new Button { Text = "Xem trước", Location = new Point(115, 630), Size = new Size(90, 35) };
            btnPrint = new Button { Text = "In", Location = new Point(220, 630), Size = new Size(90, 35) };
            btnExit = new Button { Text = "Thoát", Location = new Point(382, 630), Size = new Size(90, 35) };

            this.Controls.AddRange(new Control[] { btnClear, btnPreviewPrint, btnPrint, btnExit });

            printDocument1 = new PrintDocument();
            printPreviewDialog1 = new PrintPreviewDialog();
            printDialog1 = new PrintDialog();
        }

        private void InitializeData()
        {
            cboFaculty.Items.AddRange(new object[] { "Công nghệ thông tin", "Ngôn ngữ Pháp" });
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });

            if (cboFaculty.Items.Count > 0) cboFaculty.SelectedIndex = 0;
            if (cboGender.Items.Count > 0) cboGender.SelectedIndex = 0;

            txtCourse.Text = "K51";
            UpdateClassList();
        }

        private void RegisterEvents()
        {
            btnChoosePhoto.Click += btnChoosePhoto_Click;
            btnPreviewPrint.Click += btnPreviewPrint_Click;
            btnPrint.Click += btnPrint_Click;
            btnClear.Click += btnClear_Click;
            btnExit.Click += (s, e) => this.Close();

            pnlCardPreview.Paint += pnlCardPreview_Paint;
            printDocument1.PrintPage += printDocument1_PrintPage;

            txtStudentId.TextChanged += InputChanged;
            txtFullName.TextChanged += InputChanged;
            txtCourse.TextChanged += (s, e) => { UpdateClassList(); InputChanged(s, e); };
            cboFaculty.SelectedIndexChanged += (s, e) => { UpdateClassList(); InputChanged(s, e); };
            cboClass.SelectedIndexChanged += InputChanged;
            cboGender.SelectedIndexChanged += InputChanged;
            dtpBirthDate.ValueChanged += InputChanged;
        }

        private void UpdateClassList()
        {
            string faculty = cboFaculty.SelectedItem?.ToString() ?? "";
            string rawCourse = txtCourse.Text.Trim();
            string normalizedCourse = NormalizeCourse(rawCourse);

            cboClass.Items.Clear();

            if (faculty == "Công nghệ thông tin")
            {
                cboClass.Items.AddRange(new object[] {
                    $"{normalizedCourse}.CNTT.A",
                    $"{normalizedCourse}.CNTT.B",
                    $"{normalizedCourse}.CNTT.C",
                    $"{normalizedCourse}.CNTT.D"
                });
            }
            else if (faculty == "Ngôn ngữ Pháp")
            {
                cboClass.Items.AddRange(new object[] {
                    $"{normalizedCourse}.NNP.A",
                    $"{normalizedCourse}.NNP.B",
                    $"{normalizedCourse}.NNP.C"
                });
            }

            if (cboClass.Items.Count > 0)
            {
                cboClass.SelectedIndex = 0;
            }
        }

        private string NormalizeCourse(string input)
        {
            if (string.IsNullOrEmpty(input)) return "K51";
            string clean = input.Trim().ToUpper();
            if (clean.StartsWith("K"))
            {
                return clean;
            }
            else
            {
                return "K" + clean;
            }
        }

        private void btnChoosePhoto_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Chọn ảnh sinh viên";
            dlg.Filter = "Image files|*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using Image temp = Image.FromFile(dlg.FileName);
                    Image copy = new Bitmap(temp);
                    picAvatar.Image?.Dispose();
                    picAvatar.Image = copy;
                    pnlCardPreview.Invalidate();
                }
                catch
                {
                    MessageBox.Show("File được chọn không phải ảnh hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private StudentCardInfo GetStudentInfo()
        {
            string formattedCourse = NormalizeCourse(txtCourse.Text);

            return new StudentCardInfo
            {
                StudentId = txtStudentId.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                ClassName = cboClass.Text,
                Faculty = cboFaculty.Text,
                Course = formattedCourse,
                BirthDate = dtpBirthDate.Value,
                Gender = cboGender.Text,
                Photo = picAvatar.Image
            };
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtStudentId.Text))
            {
                MessageBox.Show("Vui lòng nhập mã sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentId.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }
            if (picAvatar.Image == null)
            {
                MessageBox.Show("Vui lòng chọn ảnh sinh viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void InputChanged(object? sender, EventArgs e)
        {
            pnlCardPreview.Invalidate();
        }

        private void pnlCardPreview_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle card = new Rectangle(15, 15, pnlCardPreview.Width - 30, pnlCardPreview.Height - 30);
            DrawStudentCard(e.Graphics, card, GetStudentInfo());
        }

        private void DrawStudentCard(Graphics g, Rectangle card, StudentCardInfo s)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using Font schoolFont = new Font("Arial", 10, FontStyle.Bold);
            using Font titleFont = new Font("Arial", 15, FontStyle.Bold);
            using Font labelFont = new Font("Arial", 10, FontStyle.Bold);
            using Font normalFont = new Font("Arial", 10, FontStyle.Regular);
            
            using Pen borderPen = new Pen(Color.Navy, 2);
            using SolidBrush blueBrush = new SolidBrush(Color.Navy);
            using SolidBrush bgBrush = new SolidBrush(Color.FromArgb(238, 247, 247));

            g.FillRectangle(bgBrush, card);
            g.DrawRectangle(borderPen, card);

            int x = card.Left;
            int y = card.Top;

            g.FillRectangle(blueBrush, new Rectangle(x, y, card.Width, 75));
            g.DrawString("TRƯỜNG ĐẠI HỌC SƯ PHẠM TP. HỒ CHÍ MINH", schoolFont, Brushes.White, x + 20, y + 12);
            g.DrawString("THẺ SINH VIÊN", titleFont, Brushes.White, x + 20, y + 38);

            Rectangle photoBox = new Rectangle(x + 25, y + 95, 125, 160);
            g.FillRectangle(Brushes.White, photoBox);
            g.DrawRectangle(Pens.Gray, photoBox);

            if (s.Photo != null)
            {
                DrawImageZoom(g, s.Photo, photoBox);
            }
            else
            {
                g.DrawString("Ảnh 3x4", normalFont, Brushes.Gray, photoBox.Left + 38, photoBox.Top + 70);
            }

            int infoX = x + 175;
            int infoY = y + 100;

            DrawInfoLine(g, "MSSV:", s.StudentId, labelFont, normalFont, infoX, infoY);
            DrawInfoLine(g, "Họ tên:", s.FullName, labelFont, normalFont, infoX, infoY + 30);
            DrawInfoLine(g, "Lớp:", s.ClassName, labelFont, normalFont, infoX, infoY + 60);
            DrawInfoLine(g, "Khoa:", s.Faculty, labelFont, normalFont, infoX, infoY + 90);
            DrawInfoLine(g, "Khóa:", s.Course, labelFont, normalFont, infoX, infoY + 120);
            DrawInfoLine(g, "Ngày sinh:", s.BirthDate.ToString("dd/MM/yyyy"), labelFont, normalFont, infoX, infoY + 150);
            DrawInfoLine(g, "Giới tính:", s.Gender, labelFont, normalFont, infoX, infoY + 180);
        }

        private void DrawInfoLine(Graphics g, string label, string value, Font labelFont, Font normalFont, int x, int y)
        {
            g.DrawString(label, labelFont, Brushes.Black, x, y);
            g.DrawString(value, normalFont, Brushes.Black, x + 90, y);
        }

        private void DrawImageZoom(Graphics g, Image image, Rectangle box)
        {
            float ratio = Math.Min((float)box.Width / image.Width, (float)box.Height / image.Height);
            int width = (int)(image.Width * ratio);
            int height = (int)(image.Height * ratio);
            int x = box.Left + (box.Width - width) / 2;
            int y = box.Top + (box.Height - height) / 2;
            g.DrawImage(image, new Rectangle(x, y, width, height));
        }

        private void btnPreviewPrint_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Width = 900;
            printPreviewDialog1.Height = 700;
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object? sender, PrintPageEventArgs e)
        {
            StudentCardInfo info = GetStudentInfo();
            Rectangle card = new Rectangle(e.MarginBounds.Left, e.MarginBounds.Top, 455, 300);
            DrawStudentCard(e.Graphics, card, info);
            e.HasMorePages = false;
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            txtStudentId.Clear();
            txtFullName.Clear();
            txtCourse.Text = "K51";
            if (cboFaculty.Items.Count > 0) cboFaculty.SelectedIndex = 0;
            if (cboGender.Items.Count > 0) cboGender.SelectedIndex = 0;
            dtpBirthDate.Value = DateTime.Today;
            picAvatar.Image?.Dispose();
            picAvatar.Image = null;
            pnlCardPreview.Invalidate();
            txtStudentId.Focus();
        }
    }
}
