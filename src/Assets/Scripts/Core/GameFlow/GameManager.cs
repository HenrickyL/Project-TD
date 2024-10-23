using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
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
    
}
