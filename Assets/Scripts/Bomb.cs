using UnityEngine;

public class Bomb : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject,3);
    }

    
    void Update()
    {
        
    }
    public void Explote()
    {
        transform.localScale *= 2;
    }
}
