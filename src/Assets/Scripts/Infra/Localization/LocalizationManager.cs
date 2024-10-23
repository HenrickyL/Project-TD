using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class LocalizationManager : MonoBehaviour, ILocalizationManager
{
    private static Dictionary<LocalizationCategory, Dictionary<string, string>> localizedData;
    private static SupportedLanguages currentLanguage = SupportedLanguages.Portuguese;  // Idioma padrão

    private Dictionary<string, string> localizedText;

    public static Dictionary<LocalizationCategory, Dictionary<string, string>>  GetLocalizedData() {
        if (localizedData == null)
            localizedData = new();
        return localizedData;
    }

    // Carrega o texto localizado de uma categoria específica (Menus, Dialogues, etc.)
    public void LoadLocalizedText(LocalizationCategory category)
    {

        string currentLagCode = currentLanguage.GetLanguageInfo().Code;
        string filePath = Path.Combine(Application.streamingAssetsPath, "Localization",
            currentLagCode,
            category + ".json");

        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            localizedText = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataAsJson);
            if(GetLocalizedData().ContainsKey(category))
                localizedData.Remove(category);
            localizedData.Add(category, localizedText);
            if (localizedText == null) { 
                throw new Exception(ErrorMessages.LocalizationJsonInvalid);
            }

            Debug.Log("Loaded Localization " + category + " in "+ currentLagCode);
        }
        else
        {
            string error = string.Format(ErrorMessages.LocalizationFileNotFound, category);
            Debug.LogError(error);
            throw new Exception(error);
        }
    }

    public string GetLocalizedValue(LocalizationFields key)
    {
        string keyCode = key.GetDescription();
        if (localizedText.ContainsKey(keyCode))
        {
            return localizedText[keyCode];
        }
        return null;
    }

    // Permite alterar o idioma atual
    public void SetLanguage(SupportedLanguages languageCode)
    {
        currentLanguage = languageCode;
    }
}
