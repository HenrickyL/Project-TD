public interface ILocalizationManager
{
    void LoadLocalizedText(LocalizationCategory category);
    string GetLocalizedValue(LocalizationFields key);
    void SetLanguage(SupportedLanguages languageCode);
}
