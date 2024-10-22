public interface ILocalizationManager
{
    void LoadLocalizedText(string category);
    string GetLocalizedValue(string key);
    void SetLanguage(string languageCode);
}
