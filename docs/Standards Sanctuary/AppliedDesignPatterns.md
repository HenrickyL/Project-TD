# Boas Práticas e Arquitetura do Project TD

Este documento aborda as melhores práticas e a arquitetura do **Project TD**, um jogo de Tower Defense em desenvolvimento. O objetivo é estabelecer uma base sólida que siga os princípios de **SOLID** e mantenha a complexidade gerenciável, adequada para um projeto indie.


## 1. Estrutura do Projeto em Unity

[Estrutura do Projeto](./FolderArch.md)

### 1.1. Nomeação de Classes e Scripts

- **Classes** devem seguir a convenção de nomenclatura PascalCase (ex: `GameManager`, `ResourceManager`).
- **Métodos** devem usar PascalCase (ex: `StartGame()`, `UpdateResources()`).
- **Variáveis** devem ser descritivas e usar camelCase (ex: `currentResources`, `enemySpawnRate`).
  
## 2. Padrões de Design

### 2.1. Máquina de Estados (State Pattern)

A ideia é usar uma máquina de estados para estruturar tanto o fluxo do jogo quanto a IA. A máquina de estados pode ser usada em várias camadas:

* **Fluxo do Jogo**: Estados como Menu, Pause, Play, etc.
* **Modos de Jogo:** Sobrevivência, Campanha, etc.
* **IA de NPCs:** Patrulhar, Perseguir, Atacar, etc.

O jogo será estruturado como uma máquina de estados, onde diferentes estados podem ser gerenciados de forma modular. Abaixo está um exemplo básico de implementação de uma máquina de estados.


#### 2.1.1. IGameState Interface

```csharp
public interface IGameState
{
    void Enter();  // Chamado quando o estado é ativado
    void Update(); // Atualiza a lógica do estado
    void Exit();   // Chamado quando o estado é desativado
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
```

#### 2.1.2. GameManager (Singleton Pattern)
Usaremos o padrão Singleton para o gerenciamento do fluxo do jogo. Isso garante que só exista uma instância do GameManager, que centraliza as transições entre os estados e controla o comportamento global.


```c#
public class GameManager : MonoBehaviour
{
    private IGameState currentState;
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }
}

```

#### 2.1.3. Factory Pattern

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

### 2.2. Implementação de Estados

#### 2.2.1. Exemplo de Estado de Menu
```c#
public class MenuState : IGameState
{
    public void Enter()
    {
        Debug.Log("Entrando no Menu Principal");
        // Inicialização do menu
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameManager.Instance.ChangeState(new PlayState());
        }
    }

    public void Exit()
    {
        Debug.Log("Saindo do Menu Principal");
        // Limpeza do menu
    }
}
```

### 2.3. Padrões SOLID
#### 2.3.1. Princípio da Responsabilidade Única (Single Responsibility Principle - SRP)

Cada classe deve ter uma única responsabilidade. Por exemplo, ResourceManager deve apenas gerenciar recursos, e TowerManager deve cuidar da construção e melhoria das torres.

```c#
public class ResourceManager
{
    private int currentResources;

    public void AddResources(int amount)
    {
        currentResources += amount;
    }

    public int GetResources()
    {
        return currentResources;
    }
}
```

#### 2.3.2. Princípio Aberto/Fechado (Open/Closed Principle - OCP)
As classes devem ser abertas para extensão, mas fechadas para modificação. Isso pode ser feito através de herança ou interfaces. Por exemplo, diferentes tipos de torres podem ser criadas a partir de uma classe base.

```c#
public abstract class Tower
{
    public abstract void Attack();
}

public class FastAttackTower : Tower
{
    public override void Attack()
    {
        // Lógica de ataque rápido
    }
}

public class FreezeTower : Tower
{
    public override void Attack()
    {
        // Lógica de ataque que congela inimigos
    }
}
```

### 2.4. Sistema de IA

A inteligência artificial dos inimigos pode ser gerenciada com uma máquina de estados, seguindo uma abordagem similar à do gerenciamento de estados do jogo.

#### 2.4.1. Exemplo de IA de Inimigo
```c#
public interface IEnemyState
{
    void Enter();
    void Update();
    void Exit();
}

public class PatrolState : IEnemyState
{
    public void Enter() { } // Lógica de patrulha 
    public void Update() { } // Atualização de patrulha
    public void Exit() { } // Lógica de saída da patrulha
}

public class EnemyAI : MonoBehaviour
{
    private IEnemyState currentState;

    public void ChangeState(IEnemyState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }
}
```

## 4. Conclusão
Este documento apresenta uma visão geral das boas práticas e arquitetura para o Project TD. A implementação de padrões de design como a máquina de estados e os princípios SOLID garantirá que o jogo tenha uma base sólida, facilitando o desenvolvimento e a manutenção ao longo do tempo. A estrutura proposta é adequada para um projeto indie e permite escalabilidade conforme o jogo evolui.
