using System.Reflection;
using System;
using System.ComponentModel;
using Perikan.Infra.Localization;

public static class EnumExtensions
{
    public static (string Code, string Text) GetLanguageInfo(this SupportedLanguages language)
    {
        var type = language.GetType();
        var memberInfo = type.GetMember(language.ToString())[0];
        var attributes = memberInfo.GetCustomAttributes(typeof(LanguageInfoAttribute), false);
        var attribute = (LanguageInfoAttribute)attributes[0];

        return (attribute.Code, attribute.Text);
    }

    public static string GetDescription(this Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}