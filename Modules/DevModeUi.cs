using BepInEx.Logging;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace SmartControlPlayer
{
    public class DevModeUI
    {
        public bool IsActive = false;
        public ManualLogSource Logger { get; private set; }
        private GameObject devCanvas;
        private InputField inputField;
        private Text outputText;

        public DevModeUI(ManualLogSource logger)
        {
            Logger = logger;
        }

        public void Initialize()
        {
            devCanvas = new GameObject("DevModeCanvas");
            Canvas canvas = devCanvas.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            devCanvas.AddComponent<CanvasScaler>();
            devCanvas.AddComponent<GraphicRaycaster>();

            GameObject panel = new GameObject("DevPanel");
            panel.transform.SetParent(devCanvas.transform);
            Image panelImage = panel.AddComponent<Image>();
            panelImage.color = new Color(0f, 0f, 0f, 0.6f);
            RectTransform panelRect = panel.GetComponent<RectTransform>();
            panelRect.anchorMin = new Vector2(0f, 0.8f);
            panelRect.anchorMax = new Vector2(1f, 1f);
            panelRect.offsetMin = Vector2.zero;
            panelRect.offsetMax = Vector2.zero;

            GameObject inputGO = new GameObject("CommandInput");
            inputGO.transform.SetParent(panel.transform);
            inputField = inputGO.AddComponent<InputField>();
            Text placeholder = CreateUIText("Placeholder", "Enter command...", inputGO.transform, Color.gray);
            inputField.placeholder = placeholder;
            Text inputText = CreateUIText("InputText", "", inputGO.transform, Color.white);
            inputField.textComponent = inputText;
            RectTransform inputRect = inputGO.GetComponent<RectTransform>();
            inputRect.anchorMin = new Vector2(0.05f, 0.6f);
            inputRect.anchorMax = new Vector2(0.95f, 0.8f);
            inputRect.offsetMin = Vector2.zero;
            inputRect.offsetMax = Vector2.zero;

            GameObject outputGO = new GameObject("OutputText");
            outputGO.transform.SetParent(panel.transform);
            outputText = CreateUIText("OutputText", "DevMode Output", outputGO.transform, Color.green);
            RectTransform outputRect = outputGO.GetComponent<RectTransform>();
            outputRect.anchorMin = new Vector2(0.05f, 0.05f);
            outputRect.anchorMax = new Vector2(0.95f, 0.55f);
            outputRect.offsetMin = Vector2.zero;
            outputRect.offsetMax = Vector2.zero;

            devCanvas.SetActive(false);
            Logger.LogInfo("[DevModeUI] Initialized.");
        }

        private Text CreateUIText(string name, string content, Transform parent, Color color)
        {
            GameObject go = new GameObject(name);
            go.transform.SetParent(parent);
            Text text = go.AddComponent<Text>();
            text.text = content;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.color = color;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            return text;
        }

        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.F10))
            {
                IsActive = !IsActive;
                devCanvas.SetActive(IsActive);
            }
            if (!IsActive) return;
            if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(inputField.text))
            {
                ProcessCommand(inputField.text);
                inputField.text = "";
            }
        }

        private void ProcessCommand(string command)
        {
            Logger.LogInfo($"[DevModeUI] Executing command: {command}");
            if (command.ToLower().StartsWith("test langfiles"))
            {
                string output = string.Join("\n", TranslationHandlerStatic.Instance.Dict.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
                outputText.text = output;
            }
            else if (command.ToLower().StartsWith("simulate profile"))
            {
                outputText.text = "Simulating profile... (dry run)";
            }
            else if (command.ToLower().StartsWith("reload configs"))
            {
                SmartControlPlayerPlugin.ConfigManager.Initialize();
                outputText.text = "Configurations reloaded.";
            }
            else if (command.ToLower().StartsWith("exec "))
            {
                outputText.text = "Exec command executed (simulate).";
            }
            else
            {
                outputText.text = "Command not recognized.";
            }
        }
    }
}
