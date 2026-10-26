using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;   // tránh mơ hồ với System.Threading.Timer khi project bật ImplicitUsings
using QuanLySuKienVaBanVe.BLL;
using QuanLySuKienVaBanVe.DTO;

namespace QuanLySuKienVaBanVe.GUI
{
    /// <summary>
    /// Form chính dạng MDI Parent: MenuStrip + ToolStrip + StatusStrip, menu ẩn/hiện theo vai trò.
    /// Các form con (TV2, TV3 làm) được mở bằng MoFormCon&lt;T&gt;() theo đúng "hợp đồng kỹ thuật" của nhóm.
    /// </summary>
    public class FrmMain : Form
    {
        /// <summary>true nếu người dùng bấm Đăng xuất (Program.cs sẽ hiện lại màn hình đăng nhập).</summary>
        public bool DangXuat { get; private set; }

        private MenuStrip menu;
        private ToolStrip tool;
        private StatusStrip status;
        private ToolStripStatusLabel lblUser, lblRole, lblTime;
        private readonly Timer clock = new Timer { Interval = 1000 };

        private readonly List<ToolStripItem> keyed = new List<ToolStripItem>();     // item có Tag = khóa Lang
        private readonly List<ToolStripItem> chiQuanLy = new List<ToolStripItem>(); // item chỉ Quản lý được thấy

        public FrmMain()
        {
            Font = new Font("Segoe UI", 9.5f);
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(900, 600);

            BuildUi();
            foreach (Control c in Controls)
                if (c is MdiClient) c.BackColor = Color.FromArgb(252, 240, 248);   // nền vùng MDI

            ApplyLanguage();
            ApplyPhanQuyen();

            Lang.Changed += OnLangChanged;
            clock.Tick += delegate { lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); };
            clock.Start();
            lblTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        // =================================================================
        //  MỞ FORM CON (hợp đồng kỹ thuật mục 7.5)
        // =================================================================
        /// <summary>Mở form con; nếu đã mở rồi thì chỉ đưa lên trước, không mở trùng.</summary>
        public void MoFormCon<T>() where T : Form, new()
        {
            foreach (Form f in MdiChildren)
                if (f is T) { f.Activate(); return; }
            T frm = new T();
            frm.MdiParent = this;
            frm.Show();
        }

        /// <summary>
        /// Tạm thời: form chưa có nên chỉ báo. Khi TV2/TV3 merge form xong, thay đúng 1 dòng:
        ///     MoSuKien:  ChuaCoForm("m_events")  ->  MoFormCon&lt;FrmQuanLySuKien&gt;();
        /// </summary>
        private void ChuaCoForm(string key)
        {
            MessageBox.Show(Lang.T("not_ready", Lang.T(key)), Lang.T("app_title"),
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ---- Mỗi chức năng 1 hàm: sau này chỉ sửa ở đây ----
        private void MoSuKien()   { ChuaCoForm("m_events"); }      // TODO: MoFormCon<FrmQuanLySuKien>();
        private void MoSuatDien() { ChuaCoForm("m_showtimes"); }   // TODO: MoFormCon<FrmQuanLySuatDien>();
        private void MoKhuVuc()   { ChuaCoForm("m_zones"); }       // TODO: MoFormCon<FrmQuanLyKhuVuc>();
        private void MoKhachHang(){ ChuaCoForm("m_customers"); }   // TODO: MoFormCon<FrmKhachHang>();
        private void MoBanVe()    { ChuaCoForm("m_pos"); }         // TODO: MoFormCon<FrmBanVe>();
        private void MoDonVe()    { ChuaCoForm("m_orders"); }      // TODO: MoFormCon<FrmQuanLyDonVe>();
        private void MoBaoCao()   { ChuaCoForm("m_revenue"); }     // TODO: MoFormCon<FrmBaoCao>();
        private void MoNhatKy()   { ChuaCoForm("m_audit"); }       // TODO: MoFormCon<FrmNhatKy>();  (TV1 tự làm tuần 3)
        private void MoDoiMatKhau(){ ChuaCoForm("m_changepw"); }   // TODO: FrmDoiMatKhau (PDF yêu cầu)

        // =================================================================
        //  DỰNG MENU / TOOLBAR / STATUSBAR
        // =================================================================
        private ToolStripMenuItem AddTop(string key)
        {
            ToolStripMenuItem it = new ToolStripMenuItem { Tag = key };
            menu.Items.Add(it);
            keyed.Add(it);
            return it;
        }

        private ToolStripMenuItem Add(ToolStripMenuItem parent, string key, Action action, bool onlyManager)
        {
            ToolStripMenuItem it = new ToolStripMenuItem { Tag = key };
            if (action != null) it.Click += delegate { action(); };
            parent.DropDownItems.Add(it);
            keyed.Add(it);
            if (onlyManager) chiQuanLy.Add(it);
            return it;
        }

        private ToolStripButton Btn(string key, Action action, bool onlyManager)
        {
            ToolStripButton b = new ToolStripButton { Tag = key, DisplayStyle = ToolStripItemDisplayStyle.Text };
            b.Click += delegate { action(); };
            tool.Items.Add(b);
            keyed.Add(b);
            if (onlyManager) chiQuanLy.Add(b);
            return b;
        }

        private void BuildUi()
        {
            menu = new MenuStrip { BackColor = Color.White };
            tool = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden, BackColor = Color.FromArgb(253, 238, 247), Padding = new Padding(6, 3, 6, 3) };
            status = new StatusStrip { BackColor = Color.FromArgb(255, 20, 147), ForeColor = Color.White };

            // ---- Hệ thống ----
            ToolStripMenuItem mSys = AddTop("m_system");
            Add(mSys, "m_changepw", MoDoiMatKhau, false);
            ToolStripMenuItem mLang = Add(mSys, "m_language", null, false);
            ToolStripMenuItem vi = new ToolStripMenuItem(Lang.T("lang_vi"));
            ToolStripMenuItem en = new ToolStripMenuItem(Lang.T("lang_en"));
            vi.Click += delegate { Lang.Set(Ngon.VN); };
            en.Click += delegate { Lang.Set(Ngon.EN); };
            mLang.DropDownItems.Add(vi);
            mLang.DropDownItems.Add(en);
            mLang.DropDownOpening += delegate { vi.Checked = Lang.Hien == Ngon.VN; en.Checked = Lang.Hien == Ngon.EN; };
            mSys.DropDownItems.Add(new ToolStripSeparator());
            Add(mSys, "m_logout", DangXuatClick, false);
            Add(mSys, "m_exit", delegate { Close(); }, false);

            // ---- Danh mục ----
            ToolStripMenuItem mCat = AddTop("m_catalog");
            Add(mCat, "m_events", MoSuKien, true);
            Add(mCat, "m_showtimes", MoSuatDien, true);
            Add(mCat, "m_zones", MoKhuVuc, true);
            Add(mCat, "m_customers", MoKhachHang, false);

            // ---- Bán vé ----
            ToolStripMenuItem mSale = AddTop("m_sales");
            Add(mSale, "m_pos", MoBanVe, false);
            Add(mSale, "m_orders", MoDonVe, false);

            // ---- Báo cáo (chỉ Quản lý) ----
            ToolStripMenuItem mRep = AddTop("m_reports");
            Add(mRep, "m_revenue", MoBaoCao, true);
            Add(mRep, "m_audit", MoNhatKy, true);

            // ---- Trợ giúp ----
            ToolStripMenuItem mHelp = AddTop("m_help");
            Add(mHelp, "m_about", delegate
            {
                MessageBox.Show(Lang.T("about_text"), Lang.T("m_about"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }, false);

            // ---- Toolbar (lối tắt) ----
            Btn("m_pos", MoBanVe, false);
            Btn("m_orders", MoDonVe, false);
            Btn("m_customers", MoKhachHang, false);
            Btn("m_revenue", MoBaoCao, true);
            ToolStripButton bOut = Btn("m_logout", DangXuatClick, false);
            bOut.Alignment = ToolStripItemAlignment.Right;

            // ---- Thanh trạng thái ----
            lblUser = new ToolStripStatusLabel();
            lblRole = new ToolStripStatusLabel();
            ToolStripStatusLabel spring = new ToolStripStatusLabel { Spring = true };
            lblTime = new ToolStripStatusLabel();
            status.Items.AddRange(new ToolStripItem[] { lblUser, new ToolStripStatusLabel("|"), lblRole, spring, lblTime });

            // Thứ tự Add quyết định thứ tự dock: status (đáy) -> tool -> menu (trên cùng)
            Controls.Add(status);
            Controls.Add(tool);
            Controls.Add(menu);
            MainMenuStrip = menu;
        }

        // =================================================================
        //  PHÂN QUYỀN + ĐA NGÔN NGỮ
        // =================================================================
        /// <summary>
        /// QuanLy thấy tất cả; NhanVien chỉ thấy Bán vé / Đơn vé / Khách hàng.
        /// Dùng .Available (không dùng .Visible: Visible của item trong menu xổ xuống luôn trả về false khi menu đang đóng).
        /// </summary>
        private void ApplyPhanQuyen()
        {
            bool ql = PhienDangNhap.LaQuanLy;
            foreach (ToolStripItem it in chiQuanLy) it.Available = ql;

            foreach (ToolStripMenuItem top in menu.Items)       // menu cha không còn mục con nào thì ẩn luôn
            {
                if (top.DropDownItems.Count == 0) continue;
                bool any = false;
                foreach (ToolStripItem c in top.DropDownItems) if (c.Available) { any = true; break; }
                top.Available = any;
            }

            TaiKhoanDTO tk = PhienDangNhap.TaiKhoan;
            lblUser.Text = Lang.T("s_user") + ": " + (tk != null ? tk.TenDangNhap : "?");
            lblRole.Text = Lang.T("s_role") + ": " + (tk != null ? Lang.T("role_" + tk.VaiTro) : "?");
        }

        private void ApplyLanguage()
        {
            Text = Lang.T("app_title");
            foreach (ToolStripItem it in keyed) it.Text = Lang.T((string)it.Tag);
        }

        private void OnLangChanged(object sender, EventArgs e)
        {
            ApplyLanguage();
            ApplyPhanQuyen();       // tên vai trò trên thanh trạng thái cũng đổi theo ngôn ngữ
        }

        // =================================================================
        //  ĐĂNG XUẤT / THOÁT
        // =================================================================
        private void DangXuatClick()
        {
            if (MessageBox.Show(Lang.T("confirm_logout"), Lang.T("app_title"),
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            DangXuat = true;
            PhienDangNhap.KetThuc();
            Close();                // Program.cs thấy DangXuat = true sẽ mở lại FrmDangNhap
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!DangXuat && e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show(Lang.T("confirm_exit"), Lang.T("app_title"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
            }
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            Lang.Changed -= OnLangChanged;
            clock.Stop();
            clock.Dispose();
            base.OnFormClosed(e);
        }
    }
}
