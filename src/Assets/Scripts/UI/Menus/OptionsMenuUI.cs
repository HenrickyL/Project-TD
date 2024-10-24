using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OptionsMenuUI : MonoBehaviour, IUISubMenu
{
    [SerializeField] private GameObject panelOptions;
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private TMP_Text titleText;
    
    private TMP_Text backButtonText;
    private IUIManager parent;


    public void Initialize()
    {
        this.Hide();
        CreateLanguageOptions(ChangeLanguage);
        SetupBackButton(Hide);
    }

    public void AddParent(IUIManager submenu)
    {
        parent = submenu;
    }

    /*--------------------------*/

    public void ChangeLanguage(int lang)
    {
        if (LocalizationManager.SetLanguage((SupportedLanguages)lang)) { 
            parent.UpdateTexts();
            this.UpdateTexts();
        }
        Debug.Log("Changed Language");
    }

    /*--------------------------*/

    private void CreateLanguageOptions(System.Action<int> onChangeLanguage)
    {
        List<string> options = new List<string>();
        for (int i = 0; i <= (int)SupportedLanguages.Portuguese; i++)
        {
            SupportedLanguages op = (SupportedLanguages)i;
            options.Add(op.ToString());
        }

        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(options);
        languageDropdown.onValueChanged.AddListener(delegate { onChangeLanguage(languageDropdown.value); });
    }

    private void SetupBackButton(System.Action onClose)
    {
        Button backButton = panelOptions.GetComponentInChildren<Button>();
        backButton.onClick.AddListener(() => onClose.Invoke());

        backButtonText = backButton.GetComponentInChildren<TMP_Text>();
    }



    /*--------------------------*/

    public void Show()
    {
        panelOptions.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("Close Options");
        panelOptions.SetActive(false);
        parent?.Show();
    }

    private string GetLocalizadValue(LocalizationFields key)
    {
        return LocalizationManager.GetLocalizadMenuValue(key);
    }

    public void UpdateTexts()
    {
        titleText.text = GetLocalizadValue(LocalizationFields.OptionMenu).ToUpper();
        backButtonText.text = GetLocalizadValue(LocalizationFields.Back).ToUpper();
    }
}
