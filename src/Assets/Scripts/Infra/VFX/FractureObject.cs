using System.Collections;
using UnityEngine;

public class FracturedObject : MonoBehaviour
{
    [SerializeField]private GameObject originalObject;
    [SerializeField]private GameObject fractureLevelsObject;
    [SerializeField]private Material fractureMaterial;
    [SerializeField]private float minForce = 0.1f;
    [SerializeField]private float maxForce = 0.5f;
    [SerializeField]private float forceRadius = 10f;
    [SerializeField]private float fragScaleFactor = 1;

    private int index = 0;
    private GameObject[] _fracturedLevelObj;
    private bool onGenerate = false;


    private void tart()
    {
        ApplyMaterialToObject(originalObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CrackGenerate();
            Crack();
        }
        else if (Input.GetKeyDown(KeyCode.R)) {
            originalObject.SetActive(true);
            onGenerate = false;
            index = 0;
        }
    }

    void CrackGenerate() {
        if (originalObject != null && !onGenerate)
        {
            originalObject.SetActive(false);
            if (fractureLevelsObject != null ) {
                int length = fractureLevelsObject.transform.childCount;
                _fracturedLevelObj = new GameObject[length];
                onGenerate = true;
                for (int i = 0; i < length; i++)
                {
                    Transform levelTransform = fractureLevelsObject.transform.GetChild(i);
                    _fracturedLevelObj[i] = Instantiate(levelTransform.gameObject, originalObject.transform.position, originalObject.transform.rotation);
                    //_fracturedLevelObj[i].SetActive(false);
                    foreach(Transform t in _fracturedLevelObj[i].transform)
                    {
                        ApplyMaterialToObject(t.gameObject);
                    }
                }
            }
        }
    }

    void Crack() {
        if (index < fractureLevelsObject.transform.childCount) {
            GameObject current = _fracturedLevelObj[index];
            //current.SetActive(true);
            foreach (Transform t in current.transform)
            {
                GameObject fragment = t.gameObject; // Cada pedaço da fratura

                // Adiciona Rigidbody ao fragmento
                Rigidbody rb = fragment.AddComponent<Rigidbody>();
                rb.mass = 0.3f;
                rb.useGravity = true;

                // Adiciona MeshCollider ao fragmento
                MeshCollider meshCol = fragment.AddComponent<MeshCollider>();
                meshCol.convex = true; // Necessário para física
                //AddBoxColliderToFragment(fragment);

                // Aplica uma força de explosão
                if (rb != null)
                {
                    rb.AddExplosionForce(Random.Range(minForce, maxForce), originalObject.transform.position, forceRadius);
                }
                StartCoroutine(Shrink(t, 2));
            }
            Destroy(current, 5);
            index += 1;
        }
    }

    void AddBoxColliderToFragment(GameObject fragment)
    {
        MeshRenderer meshRenderer = fragment.GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            // Obtém os limites do fragmento
            Bounds bounds = meshRenderer.bounds;

            // Adiciona um BoxCollider e ajusta seu tamanho e posição
            BoxCollider boxCol = fragment.AddComponent<BoxCollider>();
            boxCol.center = fragment.transform.InverseTransformPoint(bounds.center);
            boxCol.size = bounds.size;
        }
    }

    IEnumerator Shrink(Transform t, float delay) {
        yield return new WaitForSeconds(delay);

       
        Vector3 newScale = t.localScale;

        while(newScale.x >= 0)
        {
            newScale -= Vector3.one*fragScaleFactor;
            if (t == null)
            {
                yield break;
            }
            t.localScale = newScale;
            yield return new WaitForSeconds(0.05f);
        }
      
    }

    void ApplyMaterialToObject(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = fractureMaterial;
        }

        foreach (Transform child in obj.transform)
        {
            ApplyMaterialToObject(child.gameObject);
        }
    }
}
