using Perikan.Infra.Localization;
using Perikan.Infra.Menu;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    public static UIManager Instance { get; private set; }
    private Canvas canvas;

    [SerializeField] private ResumeMenuUI resumeMenu;
    [SerializeField] private OptionsMenuUI optionsMenuUI;

    public bool OnEnable { get; set; } = false;
    public bool OnInitalize { get; set; } = false;


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
        Initialize();
    }


    public void Initialize()
    {
        if (!OnInitalize) { 
            canvas = canvas ?? GetComponentInParent<Canvas>();
            if (canvas != null)
            {
                DontDestroyOnLoad(canvas.gameObject);
            }
            LocalizationManager.RegisterObserver(resumeMenu);
            LocalizationManager.RegisterObserver(optionsMenuUI);

            resumeMenu.AddSubMenu(optionsMenuUI);
            resumeMenu.Initialize();
            optionsMenuUI.Initialize();
            OnInitalize = true;
        }
        optionsMenuUI.Hide();
    }

    public void UpdateTexts()
    {
        throw new System.NotImplementedException();
    }

    public void Show()
    {
        canvas.gameObject.SetActive(true);
        resumeMenu.Show();
    }

    public void Hide()
    {
        canvas.gameObject.SetActive(false);
        optionsMenuUI?.Hide();
    }
}
