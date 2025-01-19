using Perikan.Infra.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Perikan.Infra.Factory { 
public abstract class AbstractGameAssetFactory : ScriptableObject
{
    Scene scene;
    protected T CreateGameAssetInstance<T>(T prefab) where T : GameElement
        {
        if (!scene.isLoaded)
        {
            if (Application.isEditor)
            {
                scene = SceneManager.GetSceneByName(name);
                if (!scene.isLoaded)
                {
                    scene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                scene = SceneManager.CreateScene(name);
            }
        }
        T instance = Instantiate(prefab) as T;
        //SceneManager.MoveGameObjectToScene(instance.gameObject, scene);
        return instance;
    }


    protected T Get<T>(T prefab) where T : GameElement
        {
        if (!(prefab is T))
            throw new System.InvalidCastException($"O prefab configurado não é do tipo esperado: {typeof(T)}");

        T instance = CreateGameAssetInstance(prefab as T);
        instance.OriginFactory = this;
        return instance;
    }


    public virtual void Reclaim(GameElement content)
    {
        Debug.Assert(content.OriginFactory == this, "Wrong factory reclaimed!");
        Destroy(content.gameObject);
    }
}
}