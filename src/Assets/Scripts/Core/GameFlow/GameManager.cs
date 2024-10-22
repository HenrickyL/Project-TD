using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LocalizationManager localizationManager;

    private void Awake()
    {
        this.SetupLocalizationManager();
    }

    private void Start()
    {
        // Start the game in the MenuState
        //ChangeState(new MenuState());
    }

    // Method to change states
    public void ChangeState(IGameState newState)
    {
        //currentState?.Exit();
        //currentState = newState;
        //currentState.Enter();
    }



    /*--------------------------------*/

    private void SetupLocalizationManager() {

        if (localizationManager == null)
        {
            Debug.LogError("LocalizationManager não encontrado!");
        }
        localizationManager.SetLanguage(SupportedLanguages.Portuguese); 
        localizationManager.LoadLocalizedText(LocalizationCategory.Menus);
        Debug.Log("Setup localizationManager");
    }
}
