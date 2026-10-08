using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento de la cinta")]
    [SerializeField] private float scrollSpeed = 0.5f;
    
    private Renderer rend;

    void Start()
    {
        // Guardamos la referencia al Renderer del objeto
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Calculamos el desplazamiento en el eje X en base al tiempo
        float xOffset = Time.time * scrollSpeed;

        // Aplicamos el desplazamiento solo a la X, manteniendo la Y en 0
        rend.material.mainTextureOffset = new Vector2(0f, xOffset);
    }
}
