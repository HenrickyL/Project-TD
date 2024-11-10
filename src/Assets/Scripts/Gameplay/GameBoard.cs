using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    Transform ground = default;

    Vector2Int size;

    public void Initialize(Vector2Int size) { 
        this.size = size;
        ground.localScale = new Vector3(size.x, size.y, 1f);
    }

    private void OnValidate()
    {
        if (size.x < 2)
        {
            size.x = 2;
        }
        if (size.y < 2)
        {
            size.y = 2;
        }
    }
} 
