using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using QuanLySuKienVaBanVe.Common;
using QuanLySuKienVaBanVe.DTO;

namespace QuanLySuKienVaBanVe.GUI.Forms
{
    /// <summary>MDI Parent: menu được phân quyền theo VaiTro trong CSDL.</summary>
    public class FrmMain : Form
    {
        private readonly TaiKhoanDTO _account;
        private readonly MenuStrip _menu;
        private readonly ToolStrip _toolbar;
        private readonly StatusStrip _status;
        private readonly ToolStripStatusLabel _userStatus;
        private readonly ToolStripStatusLabel _timeStatus;

        public FrmMain(TaiKhoanDTO account)
        {
            if (account == null) throw new ArgumentNullException("account");
            _account = account;

            Text = "Quản lý sự kiện và bán vé";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            IsMdiContainer = true;
            BackColor = Color.FromArgb(245, 247, 251);
            Font = new Font("Segoe UI", 9.5f);
            MinimumSize = new Size(1000, 700);

            _menu = new MenuStrip { BackColor = Color.White, ForeColor = Color.FromArgb(29, 42, 65), Padding = new Padding(8, 5, 8, 5) };
            _toolbar = new ToolStrip { BackColor = Color.FromArgb(250, 250, 253), GripStyle = ToolStripGripStyle.Hidden, Padding = new Padding(6, 4, 6, 4) };
            _status = new StatusStrip { BackColor = Color.White, SizingGrip = false };
            _userStatus = new ToolStripStatusLabel();
            _timeStatus = new ToolStripStatusLabel { Spring = true, TextAlign = ContentAlignment.MiddleRight };
            _status.Items.Add(_userStatus);
            _status.Items.Add(new ToolStripStatusLabel("  |  "));
            _status.Items.Add(_timeStatus);

            BuildMenus();
            BuildToolbar();
            Controls.Add(_status);
            Controls.Add(_toolbar);
            Controls.Add(_menu);
            MainMenuStrip = _menu;
            UpdateStatus();
            FormClosed += delegate { AppSession.SignOut(); };
        }

        private void BuildMenus()
        {
            var system = new ToolStripMenuItem("Hệ thống");
            system.DropDownItems.Add(CreateMenuItem("Đổi mật khẩu", delegate { OpenChangePassword(); }));
            system.DropDownItems.Add(new ToolStripSeparator());
            system.DropDownItems.Add(CreateMenuItem("Đăng xuất", delegate { Logout(); }));
            system.DropDownItems.Add(CreateMenuItem("Thoát ứng dụng", delegate
            {
                if (MessageBox.Show(this, "Bạn có chắc muốn thoát chương trình?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Application.Exit();
            }));

            var admin = new ToolStripMenuItem("Quản trị");
            admin.DropDownItems.Add(CreateModuleItem("Quản lý sự kiện", new[] { "FrmQuanLySuKien", "FrmSuKien" }));
            admin.DropDownItems.Add(CreateModuleItem("Quản lý suất diễn", new[] { "FrmQuanLySuatDien", "FrmSuatDien" }));
            admin.DropDownItems.Add(CreateModuleItem("Khu vực / hạng vé", new[] { "FrmQuanLyKhuVuc", "FrmKhuVuc" }));
            admin.DropDownItems.Add(CreateModuleItem("Quản lý ghế", new[] { "FrmGhe" }));
            admin.DropDownItems.Add(new ToolStripSeparator());
            admin.DropDownItems.Add(CreateModuleItem("Quản lý tài khoản", new[] { "FrmTaiKhoan" }));
            admin.DropDownItems.Add(CreateModuleItem("Nhật ký thao tác", new[] { "FrmNhatKy" }));
            admin.DropDownItems.Add(CreateModuleItem("Báo cáo doanh thu", new[] { "FrmBaoCao", "FrmBaoCaoThongKe" }));
            admin.Visible = AppSession.IsManager;

            var operations = new ToolStripMenuItem("Nghiệp vụ");
            operations.DropDownItems.Add(CreateModuleItem("Bán vé tại quầy", new[] { "FrmBanVe" }));
            operations.DropDownItems.Add(CreateModuleItem("Tra cứu / hủy / đổi vé", new[] { "FrmQuanLyDonVe", "FrmDonVe" }));
            operations.DropDownItems.Add(CreateModuleItem("Khách hàng", new[] { "FrmKhachHang" }));
            operations.DropDownItems.Add(CreateModuleItem("In vé", new[] { "FrmInVe" }));

            _menu.Items.Add(system);
            _menu.Items.Add(admin);
            _menu.Items.Add(operations);
        }

        private void BuildToolbar()
        {
            AddToolbarButton("🎟 Bán vé", delegate { OpenModule(new[] { "FrmBanVe" }, "Bán vé tại quầy"); });
            if (AppSession.IsManager)
                AddToolbarButton("📅 Sự kiện", delegate { OpenModule(new[] { "FrmQuanLySuKien", "FrmSuKien" }, "Quản lý sự kiện"); });
            AddToolbarButton("🔐 Đổi mật khẩu", delegate { OpenChangePassword(); });
            AddToolbarButton("↩ Đăng xuất", delegate { Logout(); });
        }

        private void AddToolbarButton(string text, EventHandler handler)
        {
            var button = new ToolStripButton(text)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                ForeColor = Color.FromArgb(33, 48, 75),
                Font = new Font("Segoe UI", 9.5f),
                Margin = new Padding(3, 0, 8, 0)
            };
            button.Click += handler;
            _toolbar.Items.Add(button);
        }

        private ToolStripMenuItem CreateMenuItem(string text, EventHandler handler)
        {
            var item = new ToolStripMenuItem(text);
            item.Click += handler;
            return item;
        }

        private ToolStripMenuItem CreateModuleItem(string text, string[] candidateNames)
        {
            return CreateMenuItem(text, delegate { OpenModule(candidateNames, text); });
        }

        private void OpenChangePassword()
        {
            using (var form = new FrmDoiMatKhau()) form.ShowDialog(this);
        }

        private void OpenModule(string[] candidateNames, string displayName)
        {
            Type moduleType = FindFormType(candidateNames);
            if (moduleType != null)
            {
                foreach (Form child in MdiChildren)
                {
                    if (child.GetType() == moduleType)
                    {
                        child.WindowState = FormWindowState.Normal;
                        child.Activate();
                        return;
                    }
                }

                try
                {
                    var child = (Form)Activator.CreateInstance(moduleType);
                    child.MdiParent = this;
                    child.Show();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Không mở được form " + moduleType.Name + ". Chi tiết: " + ex.Message,
                        "Lỗi mở màn hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Form của TV2/TV3 chưa được merge thì hiển thị placeholder, không giả vờ rằng nghiệp vụ đã hoàn tất.
            foreach (Form child in MdiChildren)
            {
                FrmModulePlaceholder placeholder = child as FrmModulePlaceholder;
                if (placeholder != null && placeholder.ModuleName == displayName)
                {
                    child.Activate();
                    return;
                }
            }
            var pending = new FrmModulePlaceholder(displayName) { MdiParent = this };
            pending.Show();
        }

        private static Type FindFormType(string[] candidateNames)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;
                try { types = assembly.GetTypes(); }
                catch (ReflectionTypeLoadException ex) { types = ex.Types.Where(t => t != null).ToArray(); }
                catch { continue; }

                Type match = types.FirstOrDefault(t => t != null && typeof(Form).IsAssignableFrom(t) &&
                    candidateNames.Contains(t.Name, StringComparer.Ordinal));
                if (match != null) return match;
            }
            return null;
        }

        private void UpdateStatus()
        {
            _userStatus.Text = "Đăng nhập: " + _account.TenDangNhap + "  |  Vai trò: " + _account.TenVaiTro;
            _timeStatus.Text = "Thời gian: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void Logout()
        {
            DialogResult answer = MessageBox.Show(this, "Bạn muốn đăng xuất khỏi hệ thống?", "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                AppSession.SignOut();
                Close();
            }
        }
    }

    internal class FrmModulePlaceholder : Form
    {
        public string ModuleName { get; private set; }

        public FrmModulePlaceholder(string moduleName)
        {
            ModuleName = moduleName;
            Text = moduleName;
            Size = new Size(760, 480);
            MinimumSize = new Size(520, 320);
            BackColor = Color.White;
            StartPosition = FormStartPosition.CenterParent;

            var title = new Label
            {
                Text = moduleName,
                Font = new Font("Segoe UI", 22f, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 52, 79),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 110
            };
            var message = new Label
            {
                Text = "Màn hình này đang chờ thành viên phụ trách tích hợp.\nMenu MDI đã được nối sẵn theo tên form trong kế hoạch.",
                Font = new Font("Segoe UI", 12f),
                ForeColor = Color.FromArgb(111, 122, 143),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            Controls.Add(message);
            Controls.Add(title);
        }
    }
}
