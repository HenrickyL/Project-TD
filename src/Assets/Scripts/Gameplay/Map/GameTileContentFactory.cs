using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameTileContentFactory : ScriptableObject
{
    private Scene _contentScene;

    [SerializeField]
    GameTileContent destinationPrefab = default;

    [SerializeField]
    GameTileContent emptyPrefab = default;

    /* --------------------------------------------------------------------------------- */

    public void Reclaim(GameTileContent content)
    {
        Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
        Destroy(content.gameObject);
    }

    private GameTileContent Get(GameTileContent prefab)
    {
        GameTileContent instance = Instantiate(prefab);
        instance.OriginFactory = this;
        MoveToFactoryScene(instance.gameObject);
        return instance;
    }

    public GameTileContent Get(GameTileContentType type)
    {
        Vector3 a = destinationPrefab.transform.position;
        Vector3 b = emptyPrefab.transform.position;

        switch (type)
        {
            case GameTileContentType.Destination: return Get(destinationPrefab);
            case GameTileContentType.Empty: return Get(emptyPrefab);
        }
        Debug.Assert(false, "Unsupported type: " + type);
        return null;
    }

    /* The instance is moved to the content scene of the factory, which can be created on demand. If we're in the editor, 
      first check whether the scene does exist before creating it, in case we lost track of it during a hot reload. */
    private void MoveToFactoryScene(GameObject obj)
    {
        if (!_contentScene.isLoaded)
        {
            if (Application.isEditor)
            {
                _contentScene = SceneManager.GetSceneByName(name);
                if (!_contentScene.isLoaded)
                {
                    _contentScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                _contentScene = SceneManager.CreateScene(name);
            }
        }
        SceneManager.MoveGameObjectToScene(obj, _contentScene);
    }
}