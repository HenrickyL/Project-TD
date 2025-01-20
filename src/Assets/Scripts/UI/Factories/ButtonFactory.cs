using Perikan.Infra.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public record ButtonConfig
{
    public float WidthPercentage { get; init; } = 0.2f;
    public float HeightPercentage { get; init; } = 0.1f;
    public int MinWidth { get; init; } = 350;
    public int MaxWidth { get; init; } = 700;
    public int MinHeight { get; init; } = 50;
    public int MaxHeight { get; init; } = 200;
    public bool IsToUpper { get; init; } = false;
}

public record ButtonResponse
{
    public LocalizationFields KeyText { get; set; }
    public Button Button { get; set; }
    public TMP_Text Text { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}

public static class ButtonFactory
{
    public static ButtonResponse CreateButton(
        LocalizationFields textKey,
        GameObject buttonPrefab,
        System.Action onClickAction, 
        Transform parent, 
        ButtonConfig config = default
        )
    {
        // Instancia o prefab do botão no objeto pai especificado
        GameObject newButton = Object.Instantiate(buttonPrefab, parent);
        RectTransform buttonRect = newButton.GetComponent<RectTransform>();

        // Tamanho padrão do prefab
        float defaultWidth = buttonRect.rect.width;
        float defaultHeight = buttonRect.rect.height;

        // Define largura e altura proporcional com um máximo e mínimo
        float proportionalWidth = defaultWidth * config.WidthPercentage;
        float proportionalHeight = defaultHeight * config.HeightPercentage;

        //buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(proportionalWidth, config.MinWidth, config.MaxWidth));
        //buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(proportionalHeight, config.MinHeight, config.MaxHeight));

        Vector2 values = CalculateSize( config );

        buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, values.x);
        buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, values.y);

        // Configura o texto e evento do botão
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = GetText(textKey, config);

        // Configura fonte e tamanho de texto
        buttonText.enableAutoSizing = true;
        buttonText.fontSize = 24;
        buttonText.fontSizeMin = 18;
        buttonText.fontSizeMax = 36;

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => onClickAction?.Invoke());

        return new ButtonResponse(){ 
            KeyText = textKey,
            Button = button,
            Text = buttonText,
            Height = values.y,
            Width = values.x
        };
    }

    private static string GetText(LocalizationFields textKey, ButtonConfig config = default) { 
        string text = LocalizationManager.GetLocalizadMenuValue(textKey);
        return config.IsToUpper ? text.ToUpper() : text;
    }

    private static Vector2 CalculateSize(ButtonConfig config = default)
    {
        Vector2 percentage = new Vector2(config.WidthPercentage, config.HeightPercentage);
        Vector2 min = new Vector2(config.MinWidth, config.MinHeight);
        Vector2 max = new Vector2(config.MaxWidth, config.MaxHeight);

        // Calcula a largura e altura com base na porcentagem da tela
        float targetWidth = Screen.width * percentage.x;
        float targetHeight = Screen.height * percentage.y;

        // Restringe os valores dentro do mínimo e máximo definidos
        float finalWidth = Mathf.Clamp(targetWidth, min.x, max.x);
        float finalHeight = Mathf.Clamp(targetHeight, min.y, max.y);

        return new Vector2(finalWidth, finalHeight);
    }
}


namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}
