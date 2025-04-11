using System;
using System.IO;
using UnityEngine;

namespace SmartControlPlayer
{
    public static class LogUtil
    {
        private static string logPath = "%APPDATA%/Local/Repo/SmartControlPlayer/smartlog.log";

        public static void InitializeLog(string path)
        {
            logPath = Environment.ExpandEnvironmentVariables(path);
            try
            {
                if (File.Exists(logPath))
                    File.Delete(logPath);
                Directory.CreateDirectory(Path.GetDirectoryName(logPath));
                File.WriteAllText(logPath, $"[Log iniciado {DateTime.Now}]\n");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SmartControlPlayer][LogUtil] Error initializing log: {ex}");
            }
        }

        public static void Log(string message)
        {
            string line = $"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {message}\n";
            try
            {
                File.AppendAllText(logPath, line);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SmartControlPlayer][LogUtil] Error writing log: {ex}");
            }
            Debug.Log("[SmartControlPlayer] " + message);
        }
    }
}
