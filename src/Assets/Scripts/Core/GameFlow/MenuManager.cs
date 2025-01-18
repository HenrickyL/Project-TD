using Perikan.Infra.Localization;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private OptionsMenuUI optionsMenuUI;

    private static bool OnInitialize { get; set; } = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }
   
    private void Initialize() {

        if (!OnInitialize) {
            LocalizationManager.RegisterObserver(mainMenuUI);
            LocalizationManager.RegisterObserver(optionsMenuUI);

            mainMenuUI.AddSubMenu(optionsMenuUI);

            mainMenuUI.Initialize();
            optionsMenuUI.Initialize();
            OnInitialize = true;
        }
        // Exibe o menu principal inicialmenteW
        mainMenuUI.Show();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
