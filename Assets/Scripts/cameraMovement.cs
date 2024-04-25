using UnityEngine;

public class ScrollWithMouse : MonoBehaviour
{
    public float scrollSpeed = 0.01f; 
    public float scrollThreshold = 0.05f; 
    public float minX = -9; 
    public float maxX = 9; 
    public float minZ = -10f; 
    public float maxZ = 10f; 

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
