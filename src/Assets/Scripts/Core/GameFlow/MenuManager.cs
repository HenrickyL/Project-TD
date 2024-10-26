using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private OptionsMenuUI optionsMenuUI;

    private SupportedLanguages currentLanguage = SupportedLanguages.Portuguese;

    private void Awake()
    {
        LocalizationManager.SetLanguage(currentLanguage);

        mainMenuUI.AddSubMenu(optionsMenuUI);

        mainMenuUI.Initialize();
        optionsMenuUI.Initialize();

        // Exibe o menu principal inicialmente
        mainMenuUI.Show();
    }
}
