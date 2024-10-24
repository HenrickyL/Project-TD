using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class LocalizationManager
{
    private static Dictionary<LocalizationCategory, Dictionary<string, string>> localizedData
        = new Dictionary<LocalizationCategory, Dictionary<string, string>>();

    private static SupportedLanguages currentLanguage = SupportedLanguages.Portuguese;  // Idioma padrão

    public static string GetLocalizedValue(LocalizationCategory category, LocalizationFields key)
    {
        if (localizedData.Count == 0) {
            LoadLocalizedText(category);
        }

        if (localizedData.TryGetValue(category, out var localizedText))
        {
            string keyCode = key.GetDescription();
            if (localizedText.TryGetValue(keyCode, out var value))
            {
                return value;
            }
        }
        return null;
    }

    public static bool SetLanguage(SupportedLanguages language)
    {
        if (currentLanguage != language)
        {
            currentLanguage = language;
            localizedData.Clear();
            Debug.Log($"Language set to {language}. Localization data cleared.");
            return true;
        }
        return false;
    }

    public static string GetLocalizadMenuValue(LocalizationFields key) {
        return GetLocalizedValue(LocalizationCategory.Menus, key);
    }



    /*---------------------*/
    private static void LoadLocalizedText(LocalizationCategory category)
    {
        string currentLangCode = currentLanguage.GetLanguageInfo().Code;
        string filePath = Path.Combine(Application.streamingAssetsPath, "Localization", currentLangCode, category + ".json");

        Debug.Log($"Loading localization file from: {filePath}");

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Dictionary<string, string> localizedText = JsonConvert.DeserializeObject<Dictionary<string, string>>(dataAsJson);

            if (localizedText == null)
                throw new Exception(ErrorMessages.LocalizationJsonInvalid);

            // Substitui a categoria existente no cache
            localizedData.Add(category, localizedText);

            Debug.Log($"Loaded Localization {category} in {currentLangCode}");
        }
        else
        {
            string error = string.Format(ErrorMessages.LocalizationFileNotFound, category);
            Debug.LogError(error);
            throw new FileNotFoundException(error);
        }
    }

    public static SupportedLanguages GetCurrentLanguage() {
        return currentLanguage;
    }

    public static List<SupportedLanguages> GetOrderedLanguages(){
        Array languages = Enum.GetValues(typeof(SupportedLanguages));
        SupportedLanguages first = LocalizationManager.GetCurrentLanguage();

        List<SupportedLanguages> options = new();
        options.Add(first);

        foreach (SupportedLanguages lang in languages)
        {
            if (lang != first) options.Add(lang);
        }
        return options;
    }
}
