using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class LocalizationManager : MonoBehaviour, ILocalizationManager
{
    private Dictionary<string, string> localizedText;
    private SupportedLanguages currentLanguage = SupportedLanguages.Portuguese;  // Idioma padrão

    // Carrega o texto localizado de uma categoria específica (Menus, Dialogues, etc.)
    public void LoadLocalizedText(LocalizationCategory category)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "Localization", 
            currentLanguage.GetLanguageInfo().Code,
            category + ".json");

        Debug.Log(filePath);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            localizedText = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataAsJson);
            if (localizedText == null) { 
                throw new Exception(ErrorMessages.LocalizationJsonInvalid);
            }

            Debug.Log("Loaded Localization " + category + " in "+ currentLanguage.GetLanguageInfo().Code);
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
