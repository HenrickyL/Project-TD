using Perikan.Infra.Localization;

namespace Perikan.Infra.Menu { 
    public interface IUIManager : ILocalizateObject
    {
        void Initialize();  // Inicializa o Main Menu
        void Show();
        void Hide();
        //void AddSubMenu(IUIManager submenu);
    }

    public interface IUISubMenu
    {
        void AddParent(IUIManager submenu);
    }
}