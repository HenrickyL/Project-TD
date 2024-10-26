
using TMPro;
using UnityEngine;

public class ResumeMenuUI : AbstractMenuUI
{
    [SerializeField] private TMP_Text menuTitleText;

    private AbstractSubMenuUI subMenu;

    public override void Initialize()
    {
        CreateMenuButtons();
        this.Show();
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


    public void ResumeGame()
    {
        Debug.Log("Resume Game");
    }

    public void OpenOptions()
    {
        Debug.Log("Open Options");
        this.Hide();
        subMenu.Show();
    }

    public void GoToMainMenu()
    {
        Debug.Log("Exit To Main Menu");
    }
    
}

