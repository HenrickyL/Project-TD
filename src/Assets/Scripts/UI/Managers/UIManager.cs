using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour, IUIManager
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject buttonPrefab; // Prefab do botão
    [SerializeField] private Transform body; // Local onde os botões serão instanciados

    private Canvas canvas;
    private bool isResumeSelected = false;
    private bool isMainMenuSelected = false;

    private (LocalizationFields, Button)[] buttons;
    private TMP_Text[] buttonTexts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        Initialize();
    }

    public void Initialize()
    {
        CreateMenuButtons();
        Hide();
    }

    public void Show()
    {
        canvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        canvas.gameObject.SetActive(false);
    }

    public void UpdateTexts()
    {
        throw new System.NotImplementedException();
    }

    /*-----------------------------*/

    // Atualiza o estado do botão "Resume" quando clicado
    private void OnResumeSelected()
    {
        isResumeSelected = true;
        Hide(); // Esconde o menu de pausa
        GameManager.Instance.ChangeToGameState();
    }

    // Atualiza o estado do botão "Main Menu" quando clicado
    private void OnMainMenuSelected()
    {
        isMainMenuSelected = true;
        //Hide(); // Esconde o menu de pausa e retorna ao menu principal
        // Aqui você pode adicionar a lógica para voltar ao menu principal, se necessário
    }
    private void OnQuitSelected()
    {
        GameManager.Instance.GoToMainMenu();
        Debug.Log("Quitting the Game");
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
    private string GetLocalizadValue(LocalizationFields key)
    {
        return LocalizationManager.GetLocalizadMenuValue(key);
    }

    
    private void CreateMenuButtons()
    {
        buttons = new (LocalizationFields, Button)[3];
        buttons[0] = (LocalizationFields.StartGame, CreateButton(LocalizationFields.Resume, OnResumeSelected));
        buttons[1] = (LocalizationFields.Options, CreateButton(LocalizationFields.Options, OnMainMenuSelected));
        buttons[2] = (LocalizationFields.Quit, CreateButton(LocalizationFields.Quit, OnQuitSelected));

        this.AdjustButtonPositions();
    }

    private Button CreateButton(LocalizationFields textKey, System.Action onClickAction)
    {
        GameObject newButton = Instantiate(buttonPrefab, body);
        TMP_Text textComponent = newButton.GetComponentInChildren<TMP_Text>();
        textComponent.text = GetLocalizadValue(textKey).ToUpper();

        Button button = newButton.GetComponent<Button>();
        button.onClick.AddListener(() => onClickAction.Invoke());

        return button;
    }

    /*-----------------------------*/

    // Retorna o estado do botão "Resume" e o redefine para false
    public bool IsResumeSelected()
    {
        bool wasSelected = isResumeSelected;
        isResumeSelected = false; // Redefine o estado
        return wasSelected;
    }

    // Retorna o estado do botão "Main Menu" e o redefine para false
    public bool IsMainMenuSelected()
    {
        bool wasSelected = isMainMenuSelected;
        isMainMenuSelected = false; // Redefine o estado
        return wasSelected;
    }
   
}
