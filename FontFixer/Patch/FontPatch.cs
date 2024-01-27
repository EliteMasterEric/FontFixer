using HarmonyLib;
using TMPro;

namespace FontFixer.Patch {
	[HarmonyPatch(typeof(TextMeshProUGUI), "Awake")]
	internal class LagScanPatch
	{
		[HarmonyPostfix]
		public static void Postfix(TextMeshProUGUI __instance)
		{
			if (__instance.font.faceInfo.familyName == Plugin.IBM_3270_FONT_NAME)
			{
                // Replace any instances of the IBM 3270 font with the custom fixed version.
                // The global fallback font is already applied.
                Plugin.Instance.PluginLogger.LogInfo($"Applying custom font to {__instance.name}.");
                var targetFont = Plugin.Instance.GetFont_IBM3270();
                __instance.font = targetFont;
			} else {
                // Otherwise, attempt to apply our global fallback font.
                var fallbackFont = Plugin.Instance.GetGlobalFallbackFont();

                Plugin.Instance.PluginLogger.LogInfo($"Injecting fallback font to {__instance.name}.");
                if (__instance.font.fallbackFontAssetTable == null) {
                    // No fallback font table, create one.
                    __instance.font.fallbackFontAssetTable = new() { fallbackFont };
                } else {
                    // Append our fallback font to the existing table if it isn't already present.
                    if (!__instance.font.fallbackFontAssetTable.Contains(fallbackFont)) {
                        __instance.font.fallbackFontAssetTable.Add(fallbackFont);
                    }
                }
            }
		}
	}
}