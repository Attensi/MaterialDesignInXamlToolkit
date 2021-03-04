using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Effects;

namespace MaterialDesignThemes.Wpf.Converters
{
    internal static class ShadowInfo
    {
        private static readonly IDictionary<ShadowDepth, DropShadowEffect?> ShadowsDictionary;

        static ShadowInfo()
        {
            ShadowsDictionary = new Dictionary<ShadowDepth, DropShadowEffect?>
            {
                { ShadowDepth.Depth0, null },
                { ShadowDepth.Depth1, FindShadowEffect("MaterialDesignShadowDepth1") },
                { ShadowDepth.Depth2, FindShadowEffect("MaterialDesignShadowDepth2") },
                { ShadowDepth.Depth3, FindShadowEffect("MaterialDesignShadowDepth3") },
                { ShadowDepth.Depth4, FindShadowEffect("MaterialDesignShadowDepth4") },
                { ShadowDepth.Depth5, FindShadowEffect("MaterialDesignShadowDepth5") },
            };
        }

        public static DropShadowEffect? GetDropShadow(ShadowDepth depth)
        {
            return ShadowsDictionary[depth];
        }

        private static DropShadowEffect? FindShadowEffect(string key) => Application.Current.Resources[key] as DropShadowEffect;
    }
}