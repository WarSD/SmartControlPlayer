using BepInEx.Logging;

namespace SmartControlPlayer
{
    public class MiniIA
    {
        public ManualLogSource Logger { get; private set; }
        public bool Enabled { get; private set; } = false; // Desativado por padrão

        public MiniIA(ManualLogSource logger)
        {
            Logger = logger;
        }

        public string GetSuggestion(string context)
        {
            if (!Enabled)
                return "MiniIA is disabled.";
            return $"[MiniIA] Suggestion for {context}: try lowering the offset.";
        }
    }
}
