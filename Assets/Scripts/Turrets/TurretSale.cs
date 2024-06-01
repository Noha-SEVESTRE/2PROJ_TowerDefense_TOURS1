using UnityEngine;

public class TurretSale : MonoBehaviour
{
    private bool saleEnabled = false;
    public Texture2D saleCursorTexture; 

    public void EnableSale()
    {
        saleEnabled = true;
        Debug.Log("Sale enabled");
        Cursor.SetCursor(saleCursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void DisableSale()
    {
        saleEnabled = false;
        Debug.Log("Sale disabled");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        if (saleEnabled && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("TurretPlayer1"))
                {
                    Turret turret = hit.collider.GetComponent<Turret>();
                    if (turret != null)
                    {
                        int price = turret.GetSellingPrice();
                        PlayerStats.money += price;
                        Destroy(hit.collider.gameObject);
                        saleEnabled = false;
                        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    }
                }
                else
                {
                    Debug.Log("No turret found!");
                }
            }
            else
            {
                saleEnabled = false;
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}