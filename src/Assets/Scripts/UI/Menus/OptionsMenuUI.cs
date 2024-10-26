using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionsMenuUI : AbstractSubMenuUI
{
    [SerializeField] private TMP_Dropdown languageDropdown;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform backPos;


    private TMP_Text backButtonText;


    public override void Initialize()
    {
        if (titleText != null) { 
            texts.Add(new TextResponse()
            {
                Text = titleText,
                Key = LocalizationFields.OptionMenu
            });
        }
        panel.SetActive(false);
        CreateLanguageOptions();
        SetupBackButton();
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

    private void OnBackToMainMenu() {
        this.Hide();
        parent.Show();
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
        ButtonConfig setup = new ButtonConfig() { IsToUpper = true };
        buttons = new ButtonResponse[1];
        buttons[0] = ButtonFactory.CreateButton(LocalizationFields.Back, buttonPrefab, OnBackToMainMenu, backPos, setup);

        foreach (ButtonResponse item in buttons)
        {
            texts.Add(new TextResponse()
            {
                Text = item.Text,
                Key = item.KeyText
            });
        }
    }

}
