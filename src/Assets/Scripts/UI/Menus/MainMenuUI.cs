using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour, IUIManager
{
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private TMP_Text menuTitleText;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform body;

    private IUISubMenu subMenu;
    private ButtonResponse[] buttons;
    private float offset = 10;

    public void Initialize()
    {
        CreateMenuButtons(PlayGame, OpenOptions, ExitGame);
        this.Show();
    }

    public void AddSubMenu(IUISubMenu submenu)
    {
        subMenu = submenu;
        submenu.AddParent(this);
    }

    private void CreateMenuButtons(System.Action onStartGame, System.Action onOpenOptions, System.Action onExitGame)
    {
        ButtonConfig setup = new ButtonConfig() { IsToUpper = true };
        buttons = new ButtonResponse[3];
        buttons[0] = ButtonFactory.CreateButton(LocalizationFields.StartGame, buttonPrefab, onStartGame, body, setup);
        buttons[1] = ButtonFactory.CreateButton(LocalizationFields.Options, buttonPrefab, onOpenOptions, body, setup);
        buttons[2] = ButtonFactory.CreateButton(LocalizationFields.Quit, buttonPrefab, onExitGame, body, setup);

        AdjustButtonPositions();
    }

    private void AdjustButtonPositions()
    {
        RectTransform rectTransform = body.GetComponent<RectTransform>();
        float posY = rectTransform.transform.localPosition.y;
        float offsetY = buttons[0].Height + offset;

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i].Button;
            RectTransform buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.localPosition = new Vector3(0, posY - i * offsetY, 0);
        }
    }

    public void PlayGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(ScenesEnum.GameScene.ToString());
    }

    public void OpenOptions()
    {
        Debug.Log("Open Options");
        this.Hide();
        subMenu.Show();
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    private string GetLocalizadValue(LocalizationFields key)
    {
        return LocalizationManager.GetLocalizadMenuValue(key);
    }

    public void Show()
    {
        panelMainMenu.SetActive(true);
    }

    public void Hide()
    {
        panelMainMenu.SetActive(false);
    }

    public void UpdateTexts()
    {
        menuTitleText.text = LocalizationManager.GetLocalizadMenuValue(LocalizationFields.MainMenu).ToUpper();

        foreach (ButtonResponse item in buttons)
        {
            TMP_Text startText = item.Button.GetComponentInChildren<TMP_Text>();
            startText.text = GetLocalizadValue(item.KeyText).ToUpper();
        }
    }
}
