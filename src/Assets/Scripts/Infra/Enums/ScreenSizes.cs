using UnityEngine;

public static class ScreenSizes
{
    private static readonly Vector2 Small = new Vector2(640, 480); // Exemplo: 640x480
    private static readonly Vector2 Medium = new Vector2(1280, 720); // Exemplo: 1280x720
    private static readonly Vector2 Large = new Vector2(1920, 1080); // Exemplo: 1920x1080
    private static readonly Vector2 ExtraLarge = new Vector2(2560, 1440); // Exemplo: 2560x1440

    public static Vector2 GetScreenSize(ScreenSizeCategory category)
    {
        return category switch
        {
            ScreenSizeCategory.Small => Small,
            ScreenSizeCategory.Medium => Medium,
            ScreenSizeCategory.Large => Large,
            ScreenSizeCategory.ExtraLarge => ExtraLarge,
            _ => Medium, // Padrão
        };
    }
}

public enum ScreenSizeCategory
{
    Small,
    Medium,
    Large,
    ExtraLarge
}
