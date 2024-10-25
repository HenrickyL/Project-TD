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

    private int maxWidth = 500;
    private int maxHeight = 200;
    private int minWidth = 200;
    private int minHeight = 100;
    private float heightPercentage = 0.3f;
    private float widthPercentage = 0.5f;



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
        //TODO: Use Factory to generate a button
        RectTransform rectTransform = body.GetComponent<RectTransform>();
        //float width = rectTransform.rect.width;
        //float height = rectTransform.rect.height;
        float posY = rectTransform.transform.localPosition.y;

        //float defaultWidth = buttonRect.rect.width;
        //float defaultHeight = buttonRect.rect.height;

        // Definir largura e altura proporcional com um máximo e mínimo
        //float proportionalWidth = defaultWidth * widthPercentage;
        //float proportionalHeight = defaultHeight * heightPercentage;
        int offsetY = 100;//Mathf.Clamp(proportionalHeight, minHeight, maxHeight);
        //float startY = height / 2 - offsetY / 2;

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i].Item2;
            RectTransform buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.localPosition = new Vector3(0, posY - i * offsetY, 0);
        }
    }

    private Button CreateButton(LocalizationFields textKey, System.Action onClickAction)
    {
        GameObject newButton = Instantiate(buttonPrefab, body);
        RectTransform buttonRect = newButton.GetComponent<RectTransform>();

        //// Ajuste os anchors do botão para centralizar
        //buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        //buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        //buttonRect.pivot = new Vector2(0.5f, 0.5f);

        // Tamanho padrão do prefab
        float defaultWidth = buttonRect.rect.width;
        float defaultHeight = buttonRect.rect.height;

        // Definir largura e altura proporcional com um máximo e mínimo
        float proportionalWidth = defaultWidth * widthPercentage;
        float proportionalHeight = defaultHeight * heightPercentage;

        buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Clamp(proportionalWidth, minWidth, maxWidth));
        buttonRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Clamp(proportionalHeight, minHeight, maxHeight));


        TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = GetLocalizadValue(textKey).ToUpper();

        // Modificar a fonte e tamanho
        buttonText.fontSize = 24; // Ajuste conforme necessário
        buttonText.enableAutoSizing = true;
        buttonText.fontSizeMin = 18;
        buttonText.fontSizeMax = 36;

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
