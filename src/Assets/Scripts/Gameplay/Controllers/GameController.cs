using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Inicializa o jogo e gera o mapa
    public void InitializeGame()
    {
        StartCoroutine(MapGenerator.GenerateMap());

        SetupGameElements();  // Configura elementos adicionais, como defesas e inimigos

        Debug.Log("GameController Initalized");

    }

    // Configura outros elementos do jogo
    private void SetupGameElements()
    {
        // Configura elementos do jogo, como defesas, inimigos, etc.
        Debug.Log("Configuring game elements.");
    }

    // Loop principal do jogo
    public void UpdateGame()
    {
        // Atualizações do loop principal do jogo
        // Exemplo: Atualizar a lógica dos inimigos e defesas
    }
}
