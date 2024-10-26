using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public record TextResponse {
    public TMP_Text Text { get; set; }
    public LocalizationFields Key { get; set; }
}

public abstract class AbstractMenuUI : MonoBehaviour, IUIManager
{
    [SerializeField] protected GameObject panel;
    [SerializeField] protected GameObject buttonPrefab;
    [SerializeField] protected Transform body;


    //private IUISubMenu subMenu;
    protected ButtonResponse[] buttons;
    protected List<TextResponse> texts = new();

    protected float offset = 10;


    public virtual void Initialize()
    {
        throw new System.NotImplementedException();
    }


    public void UpdateTexts()
    {
        foreach (TextResponse item in texts) {
            item.Text.text = GetLocalizadValue(item.Key).ToUpper();
        }
    }
    public void Show()
    {
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    /*-----------------------------*/

    protected void AdjustButtonPositions()
    {
        RectTransform rectTransform = body.GetComponent<RectTransform>();
        float posY = rectTransform.transform.localPosition.y;
        float offsetY = buttons[0].Height + offset;

        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i].Button;
            RectTransform buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.localPosition = new Vector3(0, posY - i * offsetY, 0);
        }
    }

    protected string GetLocalizadValue(LocalizationFields key)
    {
        return LocalizationManager.GetLocalizadMenuValue(key);
    }

}


public abstract class AbstractSubMenuUI : AbstractMenuUI, IUISubMenu
{
    protected IUIManager parent;

    public void AddParent(IUIManager submenu)
    {
        parent = submenu;
    }
}
