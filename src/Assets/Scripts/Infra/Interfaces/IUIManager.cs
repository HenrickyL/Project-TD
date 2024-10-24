public interface IUIManager
{
    void Initialize();  // Inicializa o Main Menu
    void UpdateTexts();  // Atualiza todos os textos na UI
    void Show();
    void Hide();
    //void AddSubMenu(IUIManager submenu);
}

public interface IUISubMenu : IUIManager
{
    void AddParent(IUIManager submenu);
}