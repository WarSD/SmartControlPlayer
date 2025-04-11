using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace SmartControlPlayer
{
    [BepInPlugin("xBR.War_SD.SmartControlPlayer", "SmartControlPlayer", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LogSource;

        // Módulos principais
        public static SmartConfigManager ConfigManager;
        public static MapImmunitySystem MapImmunity;
        public static TranslationHandler Translation;
        public static DevModeUI DevUI;
        public static UpdateChecker Updater;
        public static MiniIA IA; // Opcional, desativado por padrão

        private Harmony harmony;

        private void Awake()
        {
            LogSource = Logger;
            // Inicializa o log, limpando o arquivo antigo
            LogUtil.InitializeLog("%APPDATA%/Local/Repo/SmartControlPlayer/smartlog.log");

            // Inicializa os módulos
            ConfigManager = new SmartConfigManager(Config, LogSource);
            MapImmunity = new MapImmunitySystem(Config, LogSource);
            Translation = new TranslationHandler(LogSource);
            DevUI = new DevModeUI(LogSource);
            Updater = new UpdateChecker(LogSource);
            IA = new MiniIA(LogSource);

            // Carrega traduções; o idioma padrão é determinado pelo bind de ConfigManager
            Translation.Load(ConfigManager.GetString("Language", "en"));

            ConfigManager.Initialize();
            MapImmunity.Initialize();
            DevUI.Initialize();
            Updater.Initialize();

            // Aplica patches com Harmony (adicione outros patches conforme necessário)
            harmony = new Harmony("xBR.War_SD.SmartControlPlayer");
            harmony.PatchAll();

            LogSource.LogInfo(Translation.T("LOG_PLUGIN_LOADED", "SmartControlPlayer", ConfigManager.GetString("Language", "en")));
        }

        private void Update()
        {
            DevUI.HandleInput();
            ConfigManager.Revalidate();
        }
    }
}
