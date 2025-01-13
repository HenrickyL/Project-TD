using UnityEngine;

public class Tower : GameTileContent
{
    public override void GameUpdate()
    {
        base.GameUpdate();
        Debug.Log("Searching for target...");
    }
}
