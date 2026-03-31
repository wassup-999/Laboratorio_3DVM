using UnityEngine;

public class Canon : MonoBehaviour
{   
    public GameObject BulletPrefab;
    public Transform SpawnPoint;
    public float counter;
    public float SpawnInverval = 4;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        SpawnBull();
    }
    public void SpawnBull()
    {      
        counter += Time.deltaTime;
        if (counter >= SpawnInverval)
        {           
            Instantiate(BulletPrefab, SpawnPoint.transform.position, transform.rotation);
            counter = 0;           
        }       
    }
}
