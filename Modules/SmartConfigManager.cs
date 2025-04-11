using BepInEx.Configuration;
using BepInEx.Logging;
using System;
using System.Collections.Generic;

namespace SmartControlPlayer
{
    public class SmartConfigManager
    {
        public ConfigFile ConfigFile { get; private set; }
        public ManualLogSource Logger { get; private set; }

        // Exemplos de binds essenciais
        public ConfigEntry<bool> EnableDevMode;
        public ConfigEntry<string> Language;
        public ConfigEntry<float> GlobalScaling;
        public ConfigEntry<int> MinExtraction;
        public ConfigEntry<int> ExtractionLimit;
        public ConfigEntry<int> MapSizeLimit;
        public ConfigEntry<bool> ForceMapSize;
        public ConfigEntry<bool> EnableMiniIA;
        public ConfigEntry<bool> EnableUpdateChecker;

        public SmartConfigManager(ConfigFile config, ManualLogSource logger)
        {
            ConfigFile = config;
            Logger = logger;
        }

        public void Initialize()
        {
            EnableDevMode = ConfigFile.Bind("Debug", "EnableDevMode", false, "Ativa o DevMode UI (F10)");
            Language = ConfigFile.Bind("General", "Language", "en", "Idioma para traduções");
            GlobalScaling = ConfigFile.Bind("General", "GlobalScaling", 1f, new ConfigDescription("Multiplicador global para escalonamento", new AcceptableValueRange<float>(0.3f, 10f)));
            MinExtraction = ConfigFile.Bind("Extraction", "MinExtraction", 2, new ConfigDescription("Extração mínima", new AcceptableValueRange<int>(0, 20)));
            ExtractionLimit = ConfigFile.Bind("Extraction", "ExtractionLimit", 5, new ConfigDescription("Extração máxima", new AcceptableValueRange<int>(0, 20)));
            MapSizeLimit = ConfigFile.Bind("Map", "MapSizeLimit", 10, new ConfigDescription("Tamanho máximo do mapa", new AcceptableValueRange<int>(3, 10)));
            ForceMapSize = ConfigFile.Bind("Map", "ForceMapSize", false, "Força o tamanho do mapa para o valor de MapSizeLimit");
            EnableMiniIA = ConfigFile.Bind("AI", "EnableMiniIA", false, "Ativa o mini módulo de IA auxiliar (desativado por padrão)");
            EnableUpdateChecker = ConfigFile.Bind("System", "EnableUpdateChecker", true, "Verifica atualizações do plugin");
            Logger.LogInfo("[SmartConfigManager] Configurações iniciais carregadas.");
        }

        public void Revalidate()
        {
            if (MinExtraction.Value > ExtractionLimit.Value)
            {
                Logger.LogWarning("MinExtraction é maior que ExtractionLimit. Ajustando...");
                MinExtraction.Value = ExtractionLimit.Value;
                ConfigFile.Save();
            }
        }

        public string GetString(string key, string defaultValue)
        {
            if (key.Equals("Language", StringComparison.InvariantCultureIgnoreCase))
                return Language.Value;
            return defaultValue;
        }
    }
}
