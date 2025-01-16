public class ATowerState : BaseState
{
    protected Tower tower => (Tower)Entity;
    public ATowerState(string name) : base(name)
    {}
}


