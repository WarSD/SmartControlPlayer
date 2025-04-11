using BepInEx.Configuration;
using BepInEx.Logging;
using System.Collections.Generic;

namespace SmartControlPlayer
{
    public class MapImmunitySystem
    {
        public ConfigFile ConfigFile { get; private set; }
        public ManualLogSource Logger { get; private set; }
        public Dictionary<string, ConfigEntry<bool>> MapImmunityBinds = new Dictionary<string, ConfigEntry<bool>>();

        public MapImmunitySystem(ConfigFile config, ManualLogSource logger)
        {
            ConfigFile = config;
            Logger = logger;
        }

        public void Initialize()
        {
            RegisterMap("Arena");
            RegisterMap("Lobby");
            RegisterMap("Shop");
            Logger.LogInfo("[MapImmunitySystem] Mapas registrados: " + string.Join(", ", MapImmunityBinds.Keys));
        }

        public void RegisterMap(string mapName)
        {
            if (!MapImmunityBinds.ContainsKey(mapName))
            {
                var entry = ConfigFile.Bind("MapImmunity", $"{mapName}_Immune", false, $"Determina se o mapa {mapName} deve ignorar as configurações do plugin.");
                MapImmunityBinds.Add(mapName, entry);
            }
        }

        public bool IsMapImmune(string mapName)
        {
            if (MapImmunityBinds.TryGetValue(mapName, out var entry))
                return entry.Value;
            return false;
        }
    }
}
