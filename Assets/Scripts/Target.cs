using UnityEngine;
using UnityEngine.InputSystem; 

public class Target : MonoBehaviour
{
    public bool isBadObject; 
    public int pointValue = 10; 
    
    // NUEVO: Variable para asignar el sonido en el Inspector
    public AudioClip sonidoDestruccion; 

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Destroy(gameObject, 5f); 
    }

    void Update()
    {
        // (Tu código de Raycast se mantiene exactamente igual aquí)
        if (gameManager.isGameActive && Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == this.transform)
                {
                    DestruirObjeto();
                }
            }
        }
    }

    void DestruirObjeto()
    {
        // NUEVO: Reproducir el sonido en la posición de la cámara principal para que se escuche fuerte y claro
        if (sonidoDestruccion != null)
        {
            AudioSource.PlayClipAtPoint(sonidoDestruccion, Camera.main.transform.position);
        }

        if (isBadObject)
        {
            gameManager.LoseLife();
        }
        else
        {
            gameManager.UpdateScore(pointValue);
        }
        
        Destroy(gameObject);
    }
}