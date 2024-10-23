using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private LocalizationManager localizationManager;
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelOptions;
    private Transform currentPanel;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private SceneAsset gameScene;
    [SerializeField] private TMP_Text menuTitleText;

    private SupportedLanguages currentLanguage = SupportedLanguages.English;


    private void Awake()
    {
        this.SetupLocalizationManager(currentLanguage);

        currentPanel = panelMainMenu.transform;
        panelMainMenu.SetActive(true);
        panelOptions.SetActive(false);

        this.CreateMenuButtons();
        this.CreateOptionMenu();
    }
    /*--------------------------------*/

    public void PlayGame() {
        Debug.Log("Play");
        SceneManager.LoadScene(gameScene.name);

    }

    public void OpenOptions()
    {
        Debug.Log("Open");
        SwapMenu(false);
    }

    public void CloseOptions()
    {
        Debug.Log("Close");
        SwapMenu(true);
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    /*--------------------------------*/

    private void SetupLocalizationManager(SupportedLanguages language)
    {

        if (localizationManager == null)
        {
            Debug.LogError("LocalizationManager não encontrado!");
        }
        localizationManager.SetLanguage(language);
        localizationManager.LoadLocalizedText(LocalizationCategory.Menus);
        Debug.Log("Setup localizationManager");
    }

    private void SwapMenu(bool toMain) {

        if (toMain)
        {
            panelMainMenu.SetActive(true);
            panelOptions.SetActive(false);
            currentPanel = panelMainMenu.transform;
        }
        else
        {
            panelMainMenu.SetActive(false);
            panelOptions.SetActive(true);
            currentPanel = panelOptions.transform;
        }

    }

    private void CreateOptionMenu() {
        LocalizationFields titleField = LocalizationFields.OptionMenu;
        LocalizationFields backField = LocalizationFields.Back;

        TMP_Text titleText = panelOptions.GetComponentInChildren<TMP_Text>();
        titleText.text = localizationManager.GetLocalizedValue(titleField).ToUpper();

        Button backBt = panelOptions.GetComponentInChildren<Button>();
        backBt.GetComponentInChildren<TMP_Text>().text = localizationManager.GetLocalizedValue(backField).ToUpper();

        backBt.onClick.AddListener(CloseOptions);
    }
    private void CreateMenuButtons()
    {
        LocalizationFields titleFiels = LocalizationFields.MainMenu;
        menuTitleText.text = localizationManager.GetLocalizedValue(titleFiels).ToUpper();

        // Defina os itens do menu com base no enum
        LocalizationFields[] menuItems = {
            LocalizationFields.StartGame,
            LocalizationFields.Options,
            LocalizationFields.Quit
        };
        const int offsetY = 250;
        int y = ((int)menuItems.Count() / 2)* offsetY;

        foreach (var menuItem in menuItems)
        {
            GameObject newButton = Instantiate(buttonPrefab, currentPanel);
            newButton.name = menuItem.ToString().ToUpper();
            //newButton.transform.SetParent(buttonParent, false);

            newButton.transform.localPosition = new Vector3(0, y, 0);
            y-= offsetY;

            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = localizationManager.GetLocalizedValue(menuItem).ToUpper();

            Button button = newButton.GetComponent<Button>();
            switch (menuItem)
            {
                case LocalizationFields.StartGame:
                    button.onClick.AddListener(PlayGame);
                    break;
                case LocalizationFields.Options:
                    button.onClick.AddListener(OpenOptions);
                    break;
                case LocalizationFields.Quit:
                    button.onClick.AddListener(ExitGame);
                    break;
            }
        }
    }

}
