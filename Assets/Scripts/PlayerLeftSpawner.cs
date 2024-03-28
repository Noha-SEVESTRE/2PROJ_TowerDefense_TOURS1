using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeftSpawner : MonoBehaviour
{
    public GameObject melee; 
    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMelee()
    {
        Instantiate(melee, spawnPoint.position, Quaternion.identity);
    }

    public void SpawnAntiArmor()
    {

    }
}
