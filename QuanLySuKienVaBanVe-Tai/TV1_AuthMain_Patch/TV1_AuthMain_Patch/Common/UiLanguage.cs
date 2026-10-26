using System;

namespace QuanLySuKienVaBanVe.Common
{
    public static class UiLanguage
    {
        public static bool English { get; private set; }
        public static event EventHandler LanguageChanged;

        public static string Text(string vietnamese, string english)
        {
            return English ? english : vietnamese;
        }

        public static void SetEnglish(bool english)
        {
            if (English == english) return;
            English = english;
            EventHandler handler = LanguageChanged;
            if (handler != null) handler(null, EventArgs.Empty);
        }
    }
}
