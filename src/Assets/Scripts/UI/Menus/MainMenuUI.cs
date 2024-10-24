using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuUI : MonoBehaviour, IUIManager
{
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private TMP_Text menuTitleText;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform body;
    [SerializeField] private SceneAsset gameScene;

    private IUISubMenu subMenu;
    private (LocalizationFields,Button)[] buttons;


    public void Initialize()
    {
        CreateMenuButtons(PlayGame, OpenOptions, ExitGame);
        this.Show();
    }

    public void AddSubMenu(IUISubMenu submenu) {
        subMenu = submenu;
        submenu.AddParent(this);
    }

    /*--------------------------*/

    public void PlayGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(gameScene.name);
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

    /*--------------------------*/

    private void CreateMenuButtons(System.Action onStartGame, System.Action onOpenOptions, System.Action onExitGame)
    {
        buttons = new (LocalizationFields,Button)[3];
        buttons[0] = (LocalizationFields.StartGame, CreateButton(LocalizationFields.StartGame, onStartGame));
        buttons[1] = (LocalizationFields.Options, CreateButton(LocalizationFields.Options, onOpenOptions));
        buttons[2] = (LocalizationFields.Quit, CreateButton(LocalizationFields.Quit, onExitGame));

        this.AdjustButtonPositions();
    }

    private void AdjustButtonPositions()
    {
        RectTransform rectTransform = body.GetComponent<RectTransform>();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        const int offsetY = 250;
        float startY = height / 2 - offsetY / 2;

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i].Item2;
            RectTransform buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.localPosition = new Vector3(0, startY - i * offsetY, 0);
        }
    }

    private Button CreateButton(LocalizationFields textKey, System.Action onClickAction)
    {
        GameObject newButton = Instantiate(buttonPrefab, body);
        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = GetLocalizadValue(textKey).ToUpper();

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => onClickAction.Invoke());

        return button;
    }

   

    private string GetLocalizadValue(LocalizationFields key) {
        return LocalizationManager.GetLocalizadMenuValue(key);
    }

    
    /*--------------------------*/
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

        foreach (var (key,button) in buttons) { 
            TMP_Text startText = button.GetComponentInChildren<TMP_Text>();
            startText.text = GetLocalizadValue(key).ToUpper();
        }
    }
}
