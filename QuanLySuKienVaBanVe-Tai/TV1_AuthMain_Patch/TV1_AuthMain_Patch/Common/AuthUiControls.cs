using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLySuKienVaBanVe.Common
{
    public class GradientGridPanel : Panel
    {
        public GradientGridPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Width <= 0 || Height <= 0) return;
            using (var brush = new LinearGradientBrush(ClientRectangle,
                Color.FromArgb(255, 246, 253), Color.FromArgb(255, 48, 159), 0f))
                e.Graphics.FillRectangle(brush, ClientRectangle);

            using (var gridPen = new Pen(Color.FromArgb(45, Color.White), 1f))
            {
                const int cell = 100;
                for (int x = 0; x < Width; x += cell) e.Graphics.DrawLine(gridPen, x, 0, x, Height);
                for (int y = 0; y < Height; y += cell) e.Graphics.DrawLine(gridPen, 0, y, Width, y);
            }
        }
    }

    public class RoundedPanel : Panel
    {
        public int CornerRadius { get; set; }

        public RoundedPanel()
        {
            CornerRadius = 18;
            DoubleBuffered = true;
            BackColor = Color.White;
            ResizeRedraw = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            using (GraphicsPath path = CreateRoundPath(new Rectangle(0, 0, Math.Max(1, Width), Math.Max(1, Height)), CornerRadius))
                Region = new Region(path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(Color.FromArgb(235, 235, 241), 1f))
            using (GraphicsPath path = CreateRoundPath(new Rectangle(0, 0, Width - 1, Height - 1), CornerRadius))
                e.Graphics.DrawPath(pen, path);
        }

        internal static GraphicsPath CreateRoundPath(Rectangle rect, int radius)
        {
            int d = Math.Max(1, radius * 2);
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

    public class HoverBounceButton : Button
    {
        private Color _normalColor = Color.FromArgb(255, 193, 232);
        private Color _hoverColor = Color.FromArgb(255, 105, 190);

        public Color NormalColor
        {
            get { return _normalColor; }
            set { _normalColor = value; BackColor = value; }
        }
        public Color HoverColor { get { return _hoverColor; } set { _hoverColor = value; } }

        public HoverBounceButton()
        {
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            UseVisualStyleBackColor = false;
            Cursor = Cursors.Hand;
            BackColor = _normalColor;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            MouseEnter += delegate { BackColor = _hoverColor; };
            MouseLeave += delegate { BackColor = _normalColor; };
        }

    }

    public class AuthTextBoxField : Panel
    {
        private readonly Label _icon;
        private readonly Label _placeholderLabel;
        private readonly Button _revealButton;
        private bool _passwordMode;
        private bool _revealed;
        private string _placeholder;
        private Color _outlineColor = Color.Transparent;

        public TextBox Input { get; private set; }
        public string Placeholder
        {
            get { return _placeholder; }
            set { _placeholder = value ?? string.Empty; _placeholderLabel.Text = _placeholder; UpdatePlaceholder(); }
        }
        public bool IsPassword
        {
            get { return _passwordMode; }
            set
            {
                _passwordMode = value;
                _revealButton.Visible = value;
                Input.UseSystemPasswordChar = value && !_revealed;
                RelayoutChildren();
            }
        }

        public AuthTextBoxField(string iconText, string placeholder, bool passwordMode)
        {
            Height = 56;
            BackColor = Color.FromArgb(242, 244, 248);
            DoubleBuffered = true;
            ResizeRedraw = true;
            _placeholder = placeholder;

            _icon = new Label
            {
                Text = iconText,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(100, 113, 138),
                Font = new Font("Segoe UI Symbol", 16f, FontStyle.Regular),
                Width = 40,
                Height = 52
            };
            Input = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11f),
                BackColor = BackColor,
                ForeColor = Color.FromArgb(28, 39, 59),
                TabStop = true
            };
            _placeholderLabel = new Label
            {
                Text = placeholder,
                ForeColor = Color.FromArgb(145, 157, 179),
                BackColor = BackColor,
                Font = new Font("Segoe UI", 11f),
                AutoSize = false,
                Cursor = Cursors.IBeam
            };
            _revealButton = new Button
            {
                Text = "◉",
                Width = 38,
                Height = 44,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = BackColor,
                ForeColor = Color.FromArgb(100, 113, 138),
                Font = new Font("Segoe UI Symbol", 13f),
                Cursor = Cursors.Hand,
                Visible = false,
                TabStop = false
            };
            _revealButton.Click += delegate
            {
                _revealed = !_revealed;
                Input.UseSystemPasswordChar = _passwordMode && !_revealed;
                Input.Focus();
            };
            _placeholderLabel.Click += delegate { Input.Focus(); };
            Input.TextChanged += delegate { UpdatePlaceholder(); Invalidate(); };
            Input.Enter += delegate { _outlineColor = Color.FromArgb(88, 143, 255); Invalidate(); UpdatePlaceholder(); };
            Input.Leave += delegate { _outlineColor = Color.Transparent; Invalidate(); UpdatePlaceholder(); };
            Controls.Add(_icon);
            Controls.Add(Input);
            Controls.Add(_placeholderLabel);
            Controls.Add(_revealButton);
            IsPassword = passwordMode;
            RelayoutChildren();
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            RelayoutChildren();
            using (GraphicsPath path = RoundedPanel.CreateRoundPath(new Rectangle(0, 0, Math.Max(1, Width), Math.Max(1, Height)), 10))
                Region = new Region(path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = RoundedPanel.CreateRoundPath(new Rectangle(0, 0, Width - 1, Height - 1), 10))
            using (var pen = new Pen(_outlineColor, _outlineColor == Color.Transparent ? 0.5f : 1.5f))
                e.Graphics.DrawPath(pen, path);
        }

        private void RelayoutChildren()
        {
            if (Input == null) return;
            _icon.Location = new Point(8, 2);
            _icon.Height = Math.Max(1, Height - 4);
            int rightSpace = _passwordMode ? 46 : 14;
            Input.Location = new Point(48, 19);
            Input.Width = Math.Max(40, Width - 48 - rightSpace);
            _placeholderLabel.Location = new Point(Input.Left + 1, Input.Top - 1);
            _placeholderLabel.Size = new Size(Math.Max(40, Input.Width), Input.Height + 3);
            _revealButton.Location = new Point(Math.Max(Width - 43, 0), 6);
            _revealButton.Height = Math.Max(1, Height - 12);
        }

        private void UpdatePlaceholder()
        {
            if (_placeholderLabel == null || Input == null) return;
            _placeholderLabel.Visible = Input.TextLength == 0 && !Input.Focused;
            _placeholderLabel.Text = _placeholder;
        }
    }

    public abstract class AuthFormBase : Form
    {
        protected GradientGridPanel BackgroundSurface;
        protected RoundedPanel Card;
        protected HoverBounceButton LanguageButton;
        private ContextMenuStrip _languageMenu;
        private int _cardWidth;
        private int _cardHeight;

        protected AuthFormBase(string title, int cardWidth, int cardHeight)
        {
            Text = title;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(900, 700);
            ClientSize = new Size(1120, 760);
            Font = new Font("Segoe UI", 10f);
            KeyPreview = true;
            _cardWidth = cardWidth;
            _cardHeight = cardHeight;

            BackgroundSurface = new GradientGridPanel { Dock = DockStyle.Fill };
            Controls.Add(BackgroundSurface);
            Card = new RoundedPanel
            {
                Size = new Size(cardWidth, cardHeight),
                BackColor = Color.White,
                CornerRadius = 20,
                Anchor = AnchorStyles.None
            };
            BackgroundSurface.Controls.Add(Card);

            LanguageButton = new HoverBounceButton
            {
                Text = "🇻🇳 VN  ▾",
                Size = new Size(112, 42),
                Location = new Point(ClientSize.Width - 128, 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                NormalColor = Color.White,
                HoverColor = Color.FromArgb(247, 239, 247),
                ForeColor = Color.FromArgb(23, 34, 55),
                Font = new Font("Segoe UI", 10f, FontStyle.Regular)
            };
            BackgroundSurface.Controls.Add(LanguageButton);
            BuildLanguageMenu();
            LanguageButton.Click += delegate
            {
                if (_languageMenu != null) _languageMenu.Show(LanguageButton, new Point(0, LanguageButton.Height));
            };
            Resize += delegate { LayoutCard(); };
            UiLanguage.LanguageChanged += LanguageChanged;
            FormClosed += delegate { UiLanguage.LanguageChanged -= LanguageChanged; };
            LayoutCard();
        }

        protected abstract void ApplyLanguage();

        protected Label AddLabel(string text, int x, int y, int width, int height, float size,
            FontStyle style = FontStyle.Regular, ContentAlignment align = ContentAlignment.MiddleLeft)
        {
            var label = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                Font = new Font("Segoe UI", size, style),
                ForeColor = Color.FromArgb(22, 35, 58),
                TextAlign = align,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            Card.Controls.Add(label);
            return label;
        }

        protected AuthTextBoxField AddField(string icon, string placeholder, bool password, int top)
        {
            var field = new AuthTextBoxField(icon, placeholder, password)
            {
                Location = new Point(50, top),
                Width = Card.ClientSize.Width - 100,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            Card.Controls.Add(field);
            return field;
        }

        protected HoverBounceButton AddPrimaryButton(string text, int top)
        {
            var button = new HoverBounceButton
            {
                Text = text,
                Location = new Point(50, top),
                Size = new Size(Card.ClientSize.Width - 100, 52),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                NormalColor = Color.FromArgb(255, 185, 226),
                HoverColor = Color.FromArgb(255, 104, 190),
                ForeColor = Color.White
            };
            Card.Controls.Add(button);
            return button;
        }

        protected Button AddLinkButton(string text, int x, int y, int width, int height)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.White,
                ForeColor = Color.FromArgb(25, 103, 210),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            button.MouseEnter += delegate { button.ForeColor = Color.FromArgb(255, 74, 170); };
            button.MouseLeave += delegate { button.ForeColor = Color.FromArgb(25, 103, 210); };
            Card.Controls.Add(button);
            return button;
        }

        protected void LayoutCard()
        {
            if (Card == null || BackgroundSurface == null) return;
            Card.Location = new Point(Math.Max(20, (BackgroundSurface.ClientSize.Width - _cardWidth) / 2),
                Math.Max(16, (BackgroundSurface.ClientSize.Height - _cardHeight) / 2));
            LanguageButton.Location = new Point(Math.Max(10, BackgroundSurface.ClientSize.Width - LanguageButton.Width - 18), 14);
        }

        private void BuildLanguageMenu()
        {
            _languageMenu = new ContextMenuStrip();
            var vietnamese = new ToolStripMenuItem("🇻🇳  Tiếng Việt (VN)");
            var english = new ToolStripMenuItem("🇬🇧  English (EN)");
            vietnamese.Click += delegate { UiLanguage.SetEnglish(false); };
            english.Click += delegate { UiLanguage.SetEnglish(true); };
            _languageMenu.Items.Add(vietnamese);
            _languageMenu.Items.Add(english);
        }

        private void LanguageChanged(object sender, EventArgs e)
        {
            LanguageButton.Text = UiLanguage.English ? "🇬🇧 EN  ▾" : "🇻🇳 VN  ▾";
            ApplyLanguage();
        }
    }
}
