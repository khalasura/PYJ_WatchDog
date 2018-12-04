using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PYJ_WatchDog.Common
{
    public class ThemeHelper
    {
        public static void ApplyBase(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        public static void ApplyPrimary(ThemeColor name)
        {
            new PaletteHelper().ReplacePrimaryColor(name.ToString().ToLower());
        }

        public static void ApplyAccent(ThemeColor name)
        {
            new PaletteHelper().ReplaceAccentColor(name.ToString().ToLower());
        }
    }

    public enum ThemeColor
    {
        Yellow,
        Amber,
        DeepOrange,
        LightBlue,
        Teal,
        Cyan,
        Pink,
        Green,
        DeepPurple,
        Indigo,
        LightGreen,
        Blue,
        Lime,
        Red,
        Orange,
        Purple,
        Bluegrey,
        Grey,
        Brown,
    }
}
