using System;

public enum SupportedLanguages
{
    [LanguageInfo("en", "English")]
    English,
    [LanguageInfo("pt", "Portuguese")]
    Portuguese

}

[AttributeUsage(AttributeTargets.Field)]
public class LanguageInfoAttribute : Attribute
{
    public string Code { get; }
    public string Text { get; }

    public LanguageInfoAttribute(string code, string text)
    {
        Code = code;
        Text = text;
    }
}
