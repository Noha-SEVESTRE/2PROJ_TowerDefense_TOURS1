using UnityEngine;

public class ScrollWithMouse : MonoBehaviour
{
<<<<<<< Updated upstream
    public float scrollSpeed = 0.01f; 
    public float scrollThreshold = 0.05f; 
    public float minX = -6.2f; 
    public float maxX = 6.2f; 
    public float minZ = -10f; 
    public float maxZ = 10f; 
=======
    public float scrollSpeed = 0.01f; // Vitesse de défilement très lente
    public float scrollThreshold = 0.05f; // Seuil pour le déclenchement du défilement latéral
    public float minX = -6.2f; // Limite minimale pour la position X de la caméra
    public float maxX = 6.2f; // Limite maximale pour la position X de la caméra
    public float minZ = -10f; // Limite minimale pour la position Z de la caméra
    public float maxZ = 10f; // Limite maximale pour la position Z de la caméra
>>>>>>> Stashed changes

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scrollInput * scrollSpeed);

        
        float mouseX = Input.mousePosition.x / Screen.width; 
        if (mouseX < scrollThreshold || mouseX > 1 - scrollThreshold) 
        {
            float horizontalInput = (mouseX < scrollThreshold) ? -1 : 1; 
            transform.Translate(Vector3.right * horizontalInput * scrollSpeed);
        }

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        transform.position = newPosition;
    }
}
