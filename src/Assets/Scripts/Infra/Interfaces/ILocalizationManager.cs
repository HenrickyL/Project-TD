public interface ILocalizationManager
{
    void LoadLocalizedText(string category);
    string GetLocalizedValue(LocalizationFields key);
    void SetLanguage(SupportedLanguages languageCode);
}
