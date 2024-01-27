using BepInEx;

using HarmonyLib;
using BepInEx.Logging;
using BepInEx.Bootstrap;
using System;
using TMPro;
using Mono.Cecil;
using UnityEngine;
using System.IO;

namespace FontFixer
{
    public static class PluginInfo
    {
        public const string PLUGIN_ID = "FontFixer";
        public const string PLUGIN_NAME = "FontFixer";
        public const string PLUGIN_VERSION = "1.0.0";
        public const string PLUGIN_GUID = "com.elitemastereric.fontfixer";
    }

    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public const string IBM_3270_FONT_NAME = "IBM 3270";

        // IBM 3270 is the font used by several screens in the game, including the scanner and performance report.
        const string IBM_3270_TMP_PATH = "assets/_Custom/FontFix/IBM 3270.asset";
        const string IBM_3270_OTF_PATH = "assets/_Custom/FontFix/IBM 3270.otf";

        // GNU Unifont is a GPL-licensed font that supports an extremly wide range of Unicode characters.
        const string UNIFONT_TMP_PATH = "assets/_Custom/FontFix/Unifont.asset";
        const string UNIFONT_OTF_PATH = "assets/_Custom/FontFix/Unifont.otf";

        public static Plugin Instance { get; private set; }

        public ManualLogSource PluginLogger;

        public AssetBundle assetBundle;

        TMP_FontAsset font_IBM3270;
        TMP_FontAsset font_Unifont;
        
        private void Awake()
        {
            Instance = this;

            PluginLogger = Logger;

            // Apply Harmony patches (if any exist)
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            // Plugin startup logic
            PluginLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} ({PluginInfo.PLUGIN_GUID}) is loaded!");

            LoadAssetBundle();
            ApplyFallbackFont();
        }

        void LoadAssetBundle() {
            var assetBundlePath = Path.Combine(Path.GetDirectoryName(Info.Location), "fontfixer.bundle");
            assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
        }

        void ApplyFallbackFont() {
            var globalFallbackFont = GetGlobalFallbackFont();

            if (globalFallbackFont != null) {
                TMP_Settings.fallbackFontAssets.Add(globalFallbackFont);
            }
        }

        public TMP_FontAsset GetGlobalFallbackFont() {
            if (font_Unifont != null) {
                return font_Unifont;
            }

            if (assetBundle == null) {
                PluginLogger.LogError("Could not retrieve global fallback font, asset bundle is null");
                return null;
            }

            font_Unifont = assetBundle.LoadAsset<TMP_FontAsset>(UNIFONT_TMP_PATH);
            if (font_Unifont == null) {
                PluginLogger.LogError("Could not retrieve global fallback font, font asset is null");
                return null;
            }

            return font_Unifont;
        }

        public TMP_FontAsset GetFont_IBM3270() {
            if (font_IBM3270 != null) {
                return font_IBM3270;
            }

            if (assetBundle == null) {
                PluginLogger.LogError("Could not retrieve IBM 3270 font, asset bundle is null");
                return null;
            }

            font_IBM3270 = assetBundle.LoadAsset<TMP_FontAsset>(IBM_3270_TMP_PATH);
            if (font_IBM3270 == null) {
                PluginLogger.LogError("Could not retrieve IBM 3270 font, font asset is null");
                return null;
            }

            return font_IBM3270;
        }
    }
}
