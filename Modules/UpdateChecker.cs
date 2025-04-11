using BepInEx.Logging;
using UnityEngine;

namespace SmartControlPlayer
{
    public class UpdateChecker
    {
        public ManualLogSource Logger { get; private set; }

        public UpdateChecker(ManualLogSource logger)
        {
            Logger = logger;
        }

        public void Initialize()
        {
            Logger.LogInfo("[UpdateChecker] Checking for updates...");
            string onlineVersion = GetOnlineVersion();
            if (Version.Parse(PluginVersion.modVersion) < Version.Parse(onlineVersion))
            {
                Logger.LogWarning($"Update available! Download: {GetDownloadLink()}");
                // Aqui você pode exibir uma notificação na UI se desejar
            }
            else
            {
                Logger.LogInfo("[UpdateChecker] Plugin is up-to-date.");
            }
        }

        private string GetOnlineVersion()
        {
            // Placeholder para requisição HTTP (aqui retornamos uma versão fixa)
            return "1.0.1";
        }

        private string GetDownloadLink()
        {
            return "https://gist.githubusercontent.com/WarSD/6f30a4cfc5b9107660f979444be52b73/raw/download.txt?noCache=" + Random.Range(1000, 9999);
        }
    }
}
