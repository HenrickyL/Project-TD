using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using System.Linq;

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
        CreateLanguageOptions();
        SetupBackButton();
    }

    public void AddParent(IUIManager submenu)
    {
        parent = submenu;
    }

    /*--------------------------*/

    public void ChangeLanguage(int lang)
    {
        var languages = LocalizationManager.GetOrderedLanguages();

        if (LocalizationManager.SetLanguage(languages[lang])) { 
            parent.UpdateTexts();
            this.UpdateTexts();
            CreateLanguageOptions();
        }
        Debug.Log("Changed Language");
    }

    /*--------------------------*/

    private void CreateLanguageOptions()
    {
        List<string> options = new();
        foreach (SupportedLanguages lang in LocalizationManager.GetOrderedLanguages())
        {
            options.Add(lang.ToString());
        }

        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(options);
        languageDropdown.onValueChanged.AddListener(delegate { ChangeLanguage(languageDropdown.value); });
    }

    private void SetupBackButton()
    {
        System.Action onClose = this.Hide;
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
