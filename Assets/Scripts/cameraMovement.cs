using UnityEngine;

public class ScrollWithMouse : MonoBehaviour
{
    public float scrollSpeed = 0.01f; // Vitesse de défilement très lente
    public float scrollThreshold = 0.05f; // Seuil pour le déclenchement du défilement latéral
    public float minX = -6.2f; // Limite minimale pour la position X de la caméra
    public float maxX = 6.2; // Limite maximale pour la position X de la caméra
    public float minZ = -10f; // Limite minimale pour la position Z de la caméra
    public float maxZ = 10f; // Limite maximale pour la position Z de la caméra

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(Vector3.forward * scrollInput * scrollSpeed);

        // Défilement horizontal basé sur la position de la souris près des bords de l'écran
        float mouseX = Input.mousePosition.x / Screen.width; // Normalisation de la position X de la souris
        if (mouseX < scrollThreshold || mouseX > 1 - scrollThreshold) // Si la souris est près du bord
        {
            float horizontalInput = (mouseX < scrollThreshold) ? -1 : 1; // Déterminer la direction du défilement
            transform.Translate(Vector3.right * horizontalInput * scrollSpeed);
        }

        // Limiter la position de la caméra dans les limites spécifiées
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
        transform.position = newPosition;
    }
}
