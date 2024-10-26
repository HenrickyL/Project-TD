
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

        AdjustButtonPositions();
    }

    private void CloseMenu() {
        UIManager.Instance.Hide();
        GameManager.Instance.ChangeToGameState();
    }


    public void ResumeGame()
    {
        CloseMenu();
        Debug.Log("Resume Game");
    }

    public void OpenOptions()
    {
        Hide();
        subMenu.Show();
        Debug.Log("Open Options");
    }

    public void GoToMainMenu()
    {
        CloseMenu();
        SceneManager.LoadScene(ScenesEnum.MainMenuScene.ToString());
        UIManager.Instance.OnEnable = false;
        Debug.Log("Exit To Main Menu");
    }

}

