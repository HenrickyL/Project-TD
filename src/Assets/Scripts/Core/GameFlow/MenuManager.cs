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

    private void Awake()
    {
        this.SetupLocalizationManager();
        currentPanel = panelMainMenu.transform;
        this.CreateMenuButtons();
    }
    /*--------------------------------*/

    public void PlayGame() {
        Debug.Log("Play");
        SceneManager.LoadScene(gameScene.name);

    }

    public void OpenOptions()
    {
        Debug.Log("Open");
    }

    public void CloseOptions()
    {
        Debug.Log("Close");
    }

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    /*--------------------------------*/

    private void SetupLocalizationManager()
    {

        if (localizationManager == null)
        {
            Debug.LogError("LocalizationManager não encontrado!");
        }
        localizationManager.SetLanguage(SupportedLanguages.English);
        localizationManager.LoadLocalizedText(LocalizationCategory.Menus);
        Debug.Log("Setup localizationManager");
    }


    private void CreateMenuButtons()
    {
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
            newButton.name = menuItem.ToString();
            //newButton.transform.SetParent(buttonParent, false);

            newButton.transform.localPosition = new Vector3(0, y, 0);
            y-= offsetY;

            TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = localizationManager.GetLocalizedValue(menuItem);

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
