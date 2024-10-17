# Applied Design Patterns

## Máquina de Estados (State Pattern) 

A ideia é usar uma máquina de estados para estruturar tanto o fluxo do jogo quanto a IA. A máquina de estados pode ser usada em várias camadas:

* **Fluxo do Jogo**: Estados como Menu, Pause, Play, etc.
* **Modos de Jogo:** Sobrevivência, Campanha, etc.
* **IA de NPCs:** Patrulhar, Perseguir, Atacar, etc.
* 
```c#
// Interface base para os estados do jogo
public interface IGameState
{
    void Enter();
    void Update();
    void Exit();
}

// Exemplo de estado do Menu Principal
public class MenuState : IGameState
{
    public void Enter() { /* Inicializa o Menu */ }
    public void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            // Transição para o estado de jogo
            GameManager.Instance.ChangeState(new PlayState());
        }
    }
    public void Exit() { /* Limpa o Menu */ }
}

// Gerenciador de estados do jogo
public class GameManager : MonoBehaviour
{
    private IGameState currentState;

    public void ChangeState(IGameState newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update() 
    {
        if (currentState != null)
            currentState.Update();
    }
}

```


## GameManager (Singleton Pattern)
Usaremos o padrão Singleton para o gerenciamento do fluxo do jogo. Isso garante que só exista uma instância do GameManager, que centraliza as transições entre os estados e controla o comportamento global.


```c#
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}

```

## Factory Pattern

Podemos aplicar o Factory Pattern para a criação de inimigos, torres ou qualquer elemento que exija diferentes variações com base em parâmetros específicos. Isso facilita a criação de novos tipos de objetos no futuro, mantendo o código modular.

```c#
public abstract class EnemyFactory
{
    public abstract Enemy CreateEnemy(string type);
}

public class BasicEnemyFactory : EnemyFactory
{
    public override Enemy CreateEnemy(string type)
    {
        switch (type)
        {
            case "Orc": return new OrcEnemy();
            case "Goblin": return new GoblinEnemy();
            default: return null;
        }
    }
}

```