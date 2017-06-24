using SFML.Graphics;
using System;

namespace UnforgottenRealms.Core.Utils
{
    public static class FontExtensions
    {
        public static Font Arial { get; }

        static FontExtensions()
        {
            string fontsDirPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            string arialPath = fontsDirPath + @"\" + @"arial.ttf";

            Arial = new Font(arialPath);
        }
    }
}
