
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResumeMenuUI : AbstractMenuUI
{
    [SerializeField] private TMP_Text menuTitleText;

    private AbstractSubMenuUI subMenu;

    public override void Initialize()
    {
        CreateMenuButtons();
    }

    public void AddSubMenu(AbstractSubMenuUI submenu)
    {
        subMenu = submenu;
        submenu.AddParent(this);
    }

    private void CreateMenuButtons()
    {
        ButtonConfig setup = new ButtonConfig() { IsToUpper = true };
        buttons = new ButtonResponse[3];
        buttons[0] = ButtonFactory.CreateButton(LocalizationFields.Resume, buttonPrefab, ResumeGame, body, setup);
        buttons[1] = ButtonFactory.CreateButton(LocalizationFields.Options, buttonPrefab, OpenOptions, body, setup);
        buttons[2] = ButtonFactory.CreateButton(LocalizationFields.QuitToMainMenu, buttonPrefab, GoToMainMenu, body, setup);

        RegisterLocalizationInButtons();

        AdjustButtonPositions();
    }

    private void CloseMenu() {
        UIManager.Instance.Hide();
        GameManager.Instance.ChangeToGameState();
    }


    private void ResumeGame()
    {
        CloseMenu();
        Debug.Log("Resume Game");
    }

    private void OpenOptions()
    {
        Hide();
        subMenu.Show();
        Debug.Log("Open Options");
    }

    private void GoToMainMenu()
    {
        CloseMenu();
        UIManager.Instance.OnEnable = false;
        MenuManager.Instance?.Show();
        SceneManager.LoadScene(ScenesEnum.MainMenuScene.ToString());
        Debug.Log("Exit To Main Menu");
    }
}

