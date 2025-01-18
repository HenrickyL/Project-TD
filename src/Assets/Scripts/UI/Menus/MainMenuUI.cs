using Perikan.Infra.Menu;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : AbstractMenuUI
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



    /*--------------------------*/

    private void PlayGame()
    {
        SceneManager.LoadScene(ScenesEnum.GameScene.ToString());
        GameManager.Instance?.Initialize();
        MenuManager.Instance?.Hide();
        Debug.Log("Play");
    }

    private void OpenOptions()
    {
        Debug.Log("Open Options");
        this.Hide();
        subMenu.Show();
    }

    private void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    /*--------------------------*/

    private void CreateMenuButtons()
    {
        ButtonConfig setup = new ButtonConfig() { IsToUpper = true };
        buttons = new ButtonResponse[3];
        buttons[0] = ButtonFactory.CreateButton(LocalizationFields.StartGame, buttonPrefab, PlayGame, body, setup);
        buttons[1] = ButtonFactory.CreateButton(LocalizationFields.Options, buttonPrefab, OpenOptions, body, setup);
        buttons[2] = ButtonFactory.CreateButton(LocalizationFields.Quit, buttonPrefab, ExitGame, body, setup);

        RegisterLocalizationInButtons();

        menuTitleText.text = GetLocalizadValue(LocalizationFields.MainMenu).ToUpper();
        texts.Add(new TextResponse()
        {
            Text = menuTitleText,
            Key = LocalizationFields.MainMenu
        });

        AdjustButtonPositions();
    }

    

}
