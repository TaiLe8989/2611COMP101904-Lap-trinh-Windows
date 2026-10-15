using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace COMP1019_Lab08_SimplePaint
{
    public class FrmPaint : Form
    {
        // ===== Controls =====
        private readonly FlowLayoutPanel topPanel = new FlowLayoutPanel();
        private readonly DrawSurface canvas = new DrawSurface();
        private readonly ComboBox cboMode = new ComboBox();
        private readonly NumericUpDown nudWidth = new NumericUpDown();
        private readonly Button btnColor = new Button();
        private readonly Button btnBackColor = new Button();
        private readonly Button btnUndo = new Button();
        private readonly Button btnRedo = new Button();
        private readonly Button btnClear = new Button();
        private readonly Button btnOpenImage = new Button();
        private readonly Button btnRemoveImage = new Button();
        private readonly Button btnSave = new Button();
        private readonly Label lblStatus = new Label();

        // ===== Dữ liệu vẽ =====
        private readonly List<DrawItem> items = new List<DrawItem>();
        private DrawItem? currentItem;
        private bool isDrawing = false;
        private Color currentColor = Color.Black;

        // ===== Nền canvas =====
        private Color canvasBackColor = Color.White;
        private Bitmap? backgroundImage;
        private Point backgroundLocation = Point.Empty;

        // ===== Lịch sử để lấy lại hình (Redo) sau Undo / Clear =====
        private readonly Stack<RedoEntry> redoStack = new Stack<RedoEntry>();

        /// <summary>
        /// Một bước có thể lấy lại: một nét vừa Undo (1 phần tử),
        /// hoặc toàn bộ nét vẽ + ảnh nền vừa bị Clear.
        /// </summary>
        private sealed class RedoEntry
        {
            public List<DrawItem> Items { get; }
            public Bitmap? Image { get; }
            public Point ImageLocation { get; }

            public RedoEntry(List<DrawItem> items, Bitmap? image, Point imageLocation)
            {
                Items = items;
                Image = image;
                ImageLocation = imageLocation;
            }
        }

        public FrmPaint()
        {
            Text = "COMP1019 - Simple Paint";
            Width = 1100;
            Height = 700;
            MinimumSize = new Size(700, 400);
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true; // để bắt phím tắt

            BuildUserInterface();
            RegisterEvents();
            UpdateButtons();
        }

        // ============================ Giao diện ============================
        private void BuildUserInterface()
        {
            topPanel.Dock = DockStyle.Top;
            topPanel.AutoSize = true;
            topPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            topPanel.WrapContents = true;
            topPanel.Padding = new Padding(8, 4, 8, 4);
            topPanel.BackColor = Color.FromArgb(238, 247, 247);

            Label lblMode = new Label { Text = "Mode", AutoSize = true, Margin = new Padding(4, 11, 0, 0) };
            Label lblWidth = new Label { Text = "Width", AutoSize = true, Margin = new Padding(4, 11, 0, 0) };

            cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMode.Items.AddRange(new object[] { "Pen", "Line", "Rectangle", "Ellipse" });
            cboMode.SelectedIndex = 0;
            cboMode.Width = 110;
            cboMode.Margin = new Padding(4, 8, 12, 4);

            nudWidth.Minimum = 1;
            nudWidth.Maximum = 20;
            nudWidth.Value = 3;
            nudWidth.Width = 60;
            nudWidth.Margin = new Padding(4, 8, 12, 4);

            ConfigureButton(btnColor, "Color", 80);
            ConfigureButton(btnBackColor, "BG Color", 80);
            ConfigureButton(btnUndo, "Undo", 70);
            ConfigureButton(btnRedo, "Redo", 70);
            ConfigureButton(btnClear, "Clear", 70);
            ConfigureButton(btnOpenImage, "Open Image", 95);
            ConfigureButton(btnRemoveImage, "Remove Image", 110);
            ConfigureButton(btnSave, "Save", 70);
            ApplyColorToButton();

            topPanel.Controls.Add(lblMode);
            topPanel.Controls.Add(cboMode);
            topPanel.Controls.Add(lblWidth);
            topPanel.Controls.Add(nudWidth);
            topPanel.Controls.Add(btnColor);
            topPanel.Controls.Add(btnBackColor);
            topPanel.Controls.Add(btnUndo);
            topPanel.Controls.Add(btnRedo);
            topPanel.Controls.Add(btnClear);
            topPanel.Controls.Add(btnOpenImage);
            topPanel.Controls.Add(btnRemoveImage);
            topPanel.Controls.Add(btnSave);

            lblStatus.Dock = DockStyle.Bottom;
            lblStatus.Height = 26;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblStatus.Padding = new Padding(8, 0, 0, 0);

            // Thứ tự Add quan trọng với Dock: canvas (Fill) thêm trước, sau đó Bottom, Top.
            Controls.Add(canvas);
            Controls.Add(lblStatus);
            Controls.Add(topPanel);
        }

        private static void ConfigureButton(Button button, string text, int width)
        {
            button.Text = text;
            button.Width = width;
            button.Height = 28;
            button.Margin = new Padding(4, 5, 4, 5);
        }

        private void RegisterEvents()
        {
            canvas.Paint += Canvas_Paint;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseUp += Canvas_MouseUp;

            btnColor.Click += BtnColor_Click;
            btnBackColor.Click += BtnBackColor_Click;
            btnUndo.Click += BtnUndo_Click;
            btnRedo.Click += BtnRedo_Click;
            btnClear.Click += BtnClear_Click;
            btnOpenImage.Click += BtnOpenImage_Click;
            btnRemoveImage.Click += BtnRemoveImage_Click;
            btnSave.Click += BtnSave_Click;

            KeyDown += FrmPaint_KeyDown;
        }

        // ============================ Thuộc tính / tiện ích ============================
        private DrawMode CurrentMode
        {
            get
            {
                string? mode = cboMode.SelectedItem?.ToString();
                return Enum.TryParse(mode, out DrawMode result) ? result : DrawMode.Pen;
            }
        }

        private void ApplyColorToButton()
        {
            btnColor.BackColor = currentColor;
            double luminance = (0.299 * currentColor.R + 0.587 * currentColor.G + 0.114 * currentColor.B) / 255;
            btnColor.ForeColor = luminance > 0.5 ? Color.Black : Color.White;
        }

        /// <summary>Bật/tắt các nút theo trạng thái dữ liệu hiện tại.</summary>
        private void UpdateButtons()
        {
            btnUndo.Enabled = items.Count > 0;
            btnRedo.Enabled = redoStack.Count > 0;
            btnClear.Enabled = items.Count > 0 || backgroundImage != null;
            btnRemoveImage.Enabled = backgroundImage != null;
        }

        /// <summary>Hủy lịch sử Redo (khi có nét vẽ mới) và giải phóng ảnh nền đang giữ trong đó.</summary>
        private void ClearRedoHistory()
        {
            foreach (RedoEntry entry in redoStack)
                entry.Image?.Dispose();

            redoStack.Clear();
        }

        // ============================ Màu vẽ / màu nền ============================
        private void BtnColor_Click(object? sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog();
            dialog.Color = currentColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = dialog.Color;
                ApplyColorToButton();
            }
        }

        private void BtnBackColor_Click(object? sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog();
            dialog.Color = canvasBackColor;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                canvasBackColor = dialog.Color;
                canvas.BackColor = canvasBackColor;
                canvas.Invalidate();
            }
        }

        // ============================ Undo / Redo / Clear ============================
        private void BtnUndo_Click(object? sender, EventArgs e)
        {
            UndoLastItem();
        }

        private void BtnRedo_Click(object? sender, EventArgs e)
        {
            RedoLastAction();
        }

        private void UndoLastItem()
        {
            if (items.Count == 0) return;

            DrawItem last = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            redoStack.Push(new RedoEntry(new List<DrawItem> { last }, null, Point.Empty));

            canvas.Invalidate();
            UpdateButtons();
        }

        private void RedoLastAction()
        {
            if (redoStack.Count == 0) return;

            RedoEntry entry = redoStack.Pop();
            items.AddRange(entry.Items);

            if (entry.Image != null)
            {
                if (backgroundImage == null)
                {
                    backgroundImage = entry.Image;
                    backgroundLocation = entry.ImageLocation;
                }
                else
                {
                    // Người dùng đã mở ảnh nền khác sau khi Clear: giữ ảnh hiện tại.
                    entry.Image.Dispose();
                }
            }

            canvas.Invalidate();
            UpdateButtons();
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            if (items.Count == 0 && backgroundImage == null) return;

            DialogResult result = MessageBox.Show(
                "Xóa toàn bộ vùng vẽ? (Có thể lấy lại bằng nút Redo)",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            // Lưu lại toàn bộ để Redo có thể khôi phục
            redoStack.Push(new RedoEntry(new List<DrawItem>(items), backgroundImage, backgroundLocation));

            items.Clear();
            backgroundImage = null; // quyền sở hữu ảnh chuyển sang RedoEntry
            canvas.Invalidate();
            UpdateButtons();
        }

        // ============================ Ảnh nền / lưu ảnh ============================
        private void BtnOpenImage_Click(object? sender, EventArgs e)
        {
            using OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Chọn ảnh nền",
                Filter = "Image files|*.png;*.jpg;*.jpeg;*.bmp;*.gif|All files|*.*"
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                using Image source = Image.FromFile(dialog.FileName);
                Bitmap fitted = FitImageToCanvas(source, out Point location);

                backgroundImage?.Dispose();
                backgroundImage = fitted;
                backgroundLocation = location;

                canvas.Invalidate();
                UpdateButtons();
                lblStatus.Text = $"Đã mở ảnh nền: {Path.GetFileName(dialog.FileName)}";
            }
            catch (Exception ex) when (ex is OutOfMemoryException || ex is ArgumentException
                                       || ex is IOException || ex is UnauthorizedAccessException
                                       || ex is ExternalException)
            {
                MessageBox.Show("Không thể mở ảnh này. Vui lòng chọn file ảnh hợp lệ.\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Co ảnh vừa khít canvas (giữ tỉ lệ, căn giữa). Ảnh được co một lần khi mở
        /// để vị trí của các nét vẽ luôn khớp với ảnh, kể cả khi resize Form.
        /// </summary>
        private Bitmap FitImageToCanvas(Image source, out Point location)
        {
            Size area = canvas.ClientSize;
            if (area.Width <= 0 || area.Height <= 0)
                area = source.Size;

            double scale = Math.Min((double)area.Width / source.Width, (double)area.Height / source.Height);
            int width = Math.Max(1, (int)Math.Round(source.Width * scale));
            int height = Math.Max(1, (int)Math.Round(source.Height * scale));

            Bitmap fitted = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(fitted))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(source, 0, 0, width, height);
            }

            location = new Point((area.Width - width) / 2, (area.Height - height) / 2);
            return fitted;
        }

        private void BtnRemoveImage_Click(object? sender, EventArgs e)
        {
            if (backgroundImage == null) return;

            backgroundImage.Dispose();
            backgroundImage = null;
            canvas.Invalidate();
            UpdateButtons();
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            SaveDrawing();
        }

        private void SaveDrawing()
        {
            Size size = canvas.ClientSize;
            if (size.Width <= 0 || size.Height <= 0) return;

            using SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Lưu ảnh",
                Filter = "PNG (*.png)|*.png|JPEG (*.jpg)|*.jpg|Bitmap (*.bmp)|*.bmp",
                FileName = "paint.png",
                DefaultExt = "png",
                AddExtension = true,
                OverwritePrompt = true
            };

            if (dialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                using Bitmap bitmap = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    RenderCanvas(g);
                }

                ImageFormat format = Path.GetExtension(dialog.FileName).ToLowerInvariant() switch
                {
                    ".jpg" or ".jpeg" => ImageFormat.Jpeg,
                    ".bmp" => ImageFormat.Bmp,
                    _ => ImageFormat.Png
                };

                bitmap.Save(dialog.FileName, format);
                lblStatus.Text = $"Đã lưu ảnh: {dialog.FileName}";
            }
            catch (Exception ex) when (ex is ExternalException || ex is IOException
                                       || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("Không thể lưu ảnh.\n\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================ Phím tắt ============================
        private void FrmPaint_KeyDown(object? sender, KeyEventArgs e)
        {
            if (!e.Control) return;

            switch (e.KeyCode)
            {
                case Keys.Z when e.Shift:   // Ctrl+Shift+Z = Redo
                    RedoLastAction();
                    break;
                case Keys.Z:                // Ctrl+Z = Undo
                    UndoLastItem();
                    break;
                case Keys.Y:                // Ctrl+Y = Redo
                    RedoLastAction();
                    break;
                case Keys.S:                // Ctrl+S = Lưu ảnh
                    SaveDrawing();
                    break;
                default:
                    return;
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        // ============================ Xử lý chuột ============================
        private void Canvas_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            isDrawing = true;

            currentItem = new DrawItem
            {
                Mode = CurrentMode,
                Start = e.Location,
                End = e.Location,
                Color = currentColor,
                Width = (float)nudWidth.Value
            };

            if (currentItem.Mode == DrawMode.Pen)
                currentItem.Points.Add(e.Location);
        }

        private void Canvas_MouseMove(object? sender, MouseEventArgs e)
        {
            lblStatus.Text = $"X = {e.X}, Y = {e.Y}, Items = {items.Count}";

            if (!isDrawing || currentItem == null) return;

            currentItem.End = e.Location;

            if (currentItem.Mode == DrawMode.Pen)
                currentItem.Points.Add(e.Location);

            canvas.Invalidate();
        }

        private void Canvas_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (!isDrawing || currentItem == null) return;

            currentItem.End = e.Location;

            if (currentItem.Mode == DrawMode.Pen)
                currentItem.Points.Add(e.Location);

            items.Add(currentItem);
            ClearRedoHistory(); // có nét vẽ mới thì không còn nhánh Redo cũ

            currentItem = null;
            isDrawing = false;
            canvas.Invalidate();
            UpdateButtons();
        }

        // ============================ Vẽ ============================
        private void Canvas_Paint(object? sender, PaintEventArgs e)
        {
            RenderCanvas(e.Graphics);
        }

        /// <summary>
        /// Vẽ toàn bộ nội dung canvas: màu nền, ảnh nền, các nét đã vẽ và nét đang vẽ.
        /// Dùng chung cho sự kiện Paint và chức năng lưu ảnh để kết quả giống hệt nhau.
        /// </summary>
        private void RenderCanvas(Graphics g)
        {
            g.Clear(canvasBackColor);

            if (backgroundImage != null)
                g.DrawImage(backgroundImage, new Rectangle(backgroundLocation, backgroundImage.Size));

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (DrawItem item in items)
                DrawOneItem(g, item);

            if (currentItem != null)
                DrawOneItem(g, currentItem);
        }

        private void DrawOneItem(Graphics g, DrawItem item)
        {
            using Pen pen = new Pen(item.Color, item.Width);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;

            Rectangle rect = GetRectangle(item.Start, item.End);

            switch (item.Mode)
            {
                case DrawMode.Pen:
                    DrawFreePen(g, pen, item);
                    break;
                case DrawMode.Line:
                    g.DrawLine(pen, item.Start, item.End);
                    break;
                case DrawMode.Rectangle:
                    g.DrawRectangle(pen, rect);
                    break;
                case DrawMode.Ellipse:
                    g.DrawEllipse(pen, rect);
                    break;
            }
        }

        private void DrawFreePen(Graphics g, Pen pen, DrawItem item)
        {
            List<Point> points = item.Points;

            if (points.Count == 0) return;

            // Chỉ click một điểm: vẽ chấm tròn
            if (points.Count == 1)
            {
                using SolidBrush brush = new SolidBrush(item.Color);
                float r = item.Width / 2f;
                g.FillEllipse(brush, points[0].X - r, points[0].Y - r, item.Width, item.Width);
                return;
            }

            for (int i = 1; i < points.Count; i++)
            {
                g.DrawLine(pen, points[i - 1], points[i]);
            }
        }

        /// <summary>Chuẩn hóa hình chữ nhật để kéo chuột theo mọi hướng đều hợp lệ.</summary>
        private Rectangle GetRectangle(Point start, Point end)
        {
            int x = Math.Min(start.X, end.X);
            int y = Math.Min(start.Y, end.Y);
            int width = Math.Abs(start.X - end.X);
            int height = Math.Abs(start.Y - end.Y);

            return new Rectangle(x, y, width, height);
        }

        // ============================ Giải phóng tài nguyên ============================
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            backgroundImage?.Dispose();
            ClearRedoHistory();
            base.OnFormClosed(e);
        }
    }
}
