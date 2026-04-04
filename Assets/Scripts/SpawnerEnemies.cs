using JetBrains.Annotations;
using UnityEngine;

public class SpawnerEnemies : MonoBehaviour
{
    public GameObject EnemyPrefab; 
    public int NumberOfEnemies = 0;
    public int MaxEnemies = 5;
    public float spawnInterval = 2f;
    public float Range = 10f;
    public bool EnableSpawn = false;
    public float counter;
    void Start()
    {
        GetComponent<SphereCollider>().radius = Range;
        
    }

    
    void Update()
    {
        IsEnableSpawn();
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, Range);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableSpawn = true;
            Debug.Log("Player Entered");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnableSpawn = false;
            Debug.Log("Player Exited");
        }
    }
    public void IsEnableSpawn()
    {
        if (EnableSpawn)
        {
            counter += Time.deltaTime;
            if (counter >= spawnInterval)
            {
                SpawnEnemies();
                counter = 0f;
            }
        }
        for (int i = 0; i <= NumberOfEnemies; i++)
        {
            if (NumberOfEnemies >= MaxEnemies)
            {
                EnableSpawn = false;
            }
        }
    }  
    public void SpawnEnemies()
    {
        NumberOfEnemies += 1;
        GameObject obj = Instantiate(EnemyPrefab, transform);
        obj.transform.position = new(0, 0, 0);
        Vector3 origin = transform.position;
        Vector3 dir = new Vector3(Random.Range(1f,-1f) , 0 , (Random.Range(1f,-1f))).normalized;
        Vector3 FinalPosition = origin + dir * Random.Range(0, Range);
        obj.transform.position += FinalPosition;
    }
}
