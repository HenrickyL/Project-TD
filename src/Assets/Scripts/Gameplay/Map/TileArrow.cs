using UnityEngine;

public class TileArrow : MonoBehaviour
{
    [SerializeField]
    private Direction _direction = Direction.North;

    private Renderer arrowRenderer;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private float rotationProgress;
    public float Angle { get { return _direction.ToAngle(); } }
    private bool isActive = false;


    private void Awake()
    {
        transform.localRotation = Quaternion.Euler(90, 0, Angle);
        arrowRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (rotationProgress < 1)
        {
            rotationProgress += Time.deltaTime * 0.25f; // Interpolação suave da rotação
            transform.localRotation = Quaternion.Lerp(initialRotation, targetRotation, rotationProgress);
        }
    }

    private void Start()
    {
        ResetRotation();
    }

    private void ResetRotation()
    {
        initialRotation = transform.localRotation;
        targetRotation = Quaternion.Euler(90, 0, Angle);
        rotationProgress = 0.0f;
    }

    /* ---------------------------------------------- */
    public void RotateTo(Direction dir)
    {
        _direction = dir;
        ResetRotation();
    }


    public void SetActive(bool value) { 
        isActive = value;
        Material currentMaterial = isActive ? GameController.ArrowMaterial : GameController.ArrowDisableMaterial;
        arrowRenderer.material = currentMaterial;
    }
}
