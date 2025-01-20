namespace Perikan.Infra.Game { 
    public interface IGameState
    {
        void Enter();   // Método chamado ao entrar no estado
        void UpdateGame();  // Método para atualização do estado
        void Exit();    // Método chamado ao sair do estado
    }
}