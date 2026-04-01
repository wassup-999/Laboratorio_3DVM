using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletForce = 10f;  
    public Rigidbody rb;    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 4f);
        
    }

    
    void Update()
    {
        BulletImpulse();
    }
    public void BulletImpulse()
    {     
        Vector3 BulletDistance = transform.up * BulletForce * Time.deltaTime;
        rb.AddForce(BulletDistance * BulletForce , ForceMode.Impulse);
        BulletForce -= 2 * Time.deltaTime ; 
        if(BulletForce <= 0)
        {
            BulletForce = 0;           
        }
    }
    
}
