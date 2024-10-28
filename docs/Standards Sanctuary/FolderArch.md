# Folder Architecture

Explicação das Pastas:

* /Core: Onde ficam os scripts principais do gerenciamento do fluxo do jogo (GameManager, modos de jogo) e máquina de estados.
* /Gameplay: Scripts relacionados à lógica de gameplay, como gerência de produção, recursos e defesas (ex: torres).
* /AI: Scripts de controle de IA e seus estados.
* /UI: Scripts e componentes de interface gráfica, como menus e HUD.
* /Scenes: Cenas específicas do jogo (menu, níveis, etc.).
* /Resources: Configurações e assets globais que podem ser carregados em tempo de execução.

```
/Assets
    /Scripts
        /Core
            /GameState
                MenuState.cs
                PlayState.cs
                PauseState.cs
            /GameFlow
                GameManager.cs
                ModeManager.cs
        /Gameplay
            /Controllers
                GameController.cs
            /Map
                MapGenerator.cs
            /Production
                ResourceManager.cs
                ProductionManager.cs
            /Defense
                TowerManager.cs
                EnemySpawner.cs
        /AI
            /StateMachine
                IEnemyState.cs
                PatrolState.cs
                ChaseState.cs
                AttackState.cs
        /UI
            /Menus
                MainMenu.cs
                PauseMenu.cs
    /Infra
        /Interfaces
            IGameState.cs
            ILocationManager.cs
        /Localization
            LocationManager.cs
    /Prefabs
        /Towers
        /Enemies
        /UI
    /Art
        /Textures
        /Sprites
    /Scenes
        /MainMenu
        /Level1
        /SurvivalMode
    /Resources
        GameSettings.asset
    /Localization
        /en
            Menus.json
            Dialogues.json
            Cutscenes.json
        /pt
            Menus.json
            Dialogues.json
            Cutscenes.json

```