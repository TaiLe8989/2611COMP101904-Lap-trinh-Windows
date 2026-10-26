using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ShopManagerMini.Models;

namespace ShopManagerMini.Forms
{
    
    public class FrmMain : Form
    {
        private MenuStrip menuMain;
        private ToolStripMenuItem mnuSystem, mnuLogout, mnuExit;
        private ToolStripMenuItem mnuCatalog, mnuProduct, mnuCustomer;
        private ToolStripMenuItem mnuHelp, mnuAbout;
        private StatusStrip statusMain;
        private ToolStripStatusLabel lblUser, lblRole, lblTime;
        private Timer timerClock;

        private User _currentUser;
        private bool _skipExitConfirm = false;

      
        private readonly BindingList<Product> _products = new BindingList<Product>
        {
            new Product("SP001", "Nước suối Aquafina", "Đồ uống", 6000, 100),
            new Product("SP002", "Mì gói Hảo Hảo", "Thực phẩm", 4500, 200),
            new Product("SP003", "Bút bi Thiên Long", "Văn phòng phẩm", 5000, 150)
        };

        private readonly BindingList<Customer> _customers = new BindingList<Customer>
        {
            new Customer("KH001", "Nguyễn Văn An", "0901234567", "Thường"),
            new Customer("KH002", "Trần Thị Bình", "0912345678", "VIP")
        };

       
        public FrmMain(User user)
        {
            _currentUser = user;
            InitializeUI();
            ShowUserInfo();
        }

        private void InitializeUI()
        {
            Text = "ShopManagerMini - Quản lý cửa hàng mini";
            Font = new Font("Segoe UI", 10F);
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Size = new Size(1000, 650);
            StartPosition = FormStartPosition.CenterScreen;
            IsMdiContainer = true; 

        
            mnuLogout = new ToolStripMenuItem("Đăng xuất") { Name = "mnuLogout" };
            mnuExit = new ToolStripMenuItem("Thoát") { Name = "mnuExit" };
            mnuSystem = new ToolStripMenuItem("Hệ thống") { Name = "mnuSystem" };
            mnuSystem.DropDownItems.AddRange(new ToolStripItem[] { mnuLogout, mnuExit });

            mnuProduct = new ToolStripMenuItem("Quản lý sản phẩm") { Name = "mnuProduct" };
            mnuCustomer = new ToolStripMenuItem("Quản lý khách hàng") { Name = "mnuCustomer" };
            mnuCatalog = new ToolStripMenuItem("Danh mục") { Name = "mnuCatalog" };
            mnuCatalog.DropDownItems.AddRange(new ToolStripItem[] { mnuProduct, mnuCustomer });

            mnuAbout = new ToolStripMenuItem("Giới thiệu") { Name = "mnuAbout" };
            mnuHelp = new ToolStripMenuItem("Trợ giúp") { Name = "mnuHelp" };
            mnuHelp.DropDownItems.Add(mnuAbout);

            menuMain = new MenuStrip { Name = "menuMain" };
            menuMain.Items.AddRange(new ToolStripItem[] { mnuSystem, mnuCatalog, mnuHelp });
            MainMenuStrip = menuMain;

          
            lblUser = new ToolStripStatusLabel { Name = "lblUser" };
            lblRole = new ToolStripStatusLabel { Name = "lblRole" };
            lblTime = new ToolStripStatusLabel
            {
                Name = "lblTime",
                Spring = true,
                TextAlign = ContentAlignment.MiddleRight
            };
            statusMain = new StatusStrip { Name = "statusMain" };
            statusMain.Items.AddRange(new ToolStripItem[] { lblUser, lblRole, lblTime });

            Controls.Add(statusMain);
            Controls.Add(menuMain);

       
            timerClock = new Timer { Interval = 1000 };
            timerClock.Tick += (s, e) => lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            timerClock.Start();
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

         
            mnuLogout.Click += mnuLogout_Click;
            mnuExit.Click += (s, e) => Close(); // FormClosing sẽ hỏi xác nhận
            mnuProduct.Click += (s, e) => OpenChild(() => new FrmProduct(_products));
            mnuCustomer.Click += (s, e) => OpenChild(() => new FrmCustomer(_customers));
            mnuAbout.Click += (s, e) =>
            {
                using (FrmAbout about = new FrmAbout()) about.ShowDialog(this);
            };
            FormClosing += FrmMain_FormClosing;
        }

       
        private void ShowUserInfo()
        {
            lblUser.Text = "Người dùng: " + _currentUser.FullName;
            lblRole.Text = "| Vai trò: " + _currentUser.Role;
        }

        private void OpenChild<T>(Func<T> createForm) where T : Form
        {
            foreach (Form child in MdiChildren)
            {
                if (child is T)
                {
                    if (child.WindowState == FormWindowState.Minimized)
                        child.WindowState = FormWindowState.Normal;
                    child.Activate();
                    return;
                }
            }

            T frm = createForm();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            foreach (Form child in MdiChildren) child.Close();
            Hide();

            using (FrmLogin login = new FrmLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    _currentUser = login.CurrentUser;
                    ShowUserInfo();
                    Show();
                }
                else
                {
                    _skipExitConfirm = true;
                    Close();
                }
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_skipExitConfirm) return;

            DialogResult confirm = MessageBox.Show("Bạn có chắc muốn thoát chương trình?", "Xác nhận thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) e.Cancel = true;
        }
    }
}
