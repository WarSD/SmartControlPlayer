using BepInEx.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SmartControlPlayer
{
    public class TranslationHandler
    {
        public Dictionary<string, string> Dict = new Dictionary<string, string>();
        public ManualLogSource Logger { get; private set; }
        public string LangPath { get; private set; }

        public TranslationHandler(ManualLogSource logger)
        {
            Logger = logger;
            LangPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Repo", "SmartControlPlayer", "Lang");
            if (!Directory.Exists(LangPath))
                Directory.CreateDirectory(LangPath);
        }

        public void Load(string LanguageCode)
        {
            string enPath = Path.Combine(LangPath, "en.json");
            string ptPath = Path.Combine(LangPath, "pt-br.json");

            if (!File.Exists(enPath))
            {
                var defaultEn = new Dictionary<string, string>()
                {
                    {"LOG_PLUGIN_LOADED", "Plugin {0} loaded! Language: {1}"},
                    {"LOG_LANGUAGE_CHANGED", "Language changed from {0} to {1}"},
                    // Adicione outras chaves conforme necessário...
                };
                File.WriteAllText(enPath, JsonConvert.SerializeObject(defaultEn, Formatting.Indented));
            }
            if (!File.Exists(ptPath))
            {
                var defaultPt = new Dictionary<string, string>()
                {
                    {"LOG_PLUGIN_LOADED", "Plugin {0} carregado! Idioma: {1}"},
                    {"LOG_LANGUAGE_CHANGED", "Idioma alterado de {0} para {1}"},
                    // Adicione outras chaves conforme necessário...
                };
                File.WriteAllText(ptPath, JsonConvert.SerializeObject(defaultPt, Formatting.Indented));
            }
            string chosenLang = LanguageCode.ToLower();
            string chosenPath = Path.Combine(LangPath, $"{chosenLang}.json");
            if (!File.Exists(chosenPath))
                chosenPath = enPath;
            try
            {
                string json = File.ReadAllText(chosenPath);
                Dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                Logger.LogInfo($"[TranslationHandler] Loaded language file: {chosenPath}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"[TranslationHandler] Error loading language file: {ex}");
                Dict = new Dictionary<string, string>();
            }
        }

        public string T(string key, params object[] args)
        {
            if (Dict.TryGetValue(key, out string value))
                return (args != null && args.Length > 0) ? string.Format(value, args) : value;
            return key;
        }
    }
}
