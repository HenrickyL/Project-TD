public interface IGameState
{
    void Enter();   // Método chamado ao entrar no estado
    void Update();  // Método para atualização do estado
    void Exit();    // Método chamado ao sair do estado
}
