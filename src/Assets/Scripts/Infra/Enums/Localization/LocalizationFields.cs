using System.ComponentModel;

namespace Perikan.Infra.Localization
{
    public enum LocalizationFields
    {
        [Description("start_game")]
        StartGame,
        [Description("options")]
        Options,
        [Description("back")]
        Back,
        [Description("quit")]
        Quit,
        [Description("resume")]
        Resume,
        [Description("main_menu")]
        MainMenu,
        [Description("option_menu")]
        OptionMenu,
        [Description("pause")]
        Pause,
        [Description("quit_to_main_menu")]
        QuitToMainMenu
    }
}
