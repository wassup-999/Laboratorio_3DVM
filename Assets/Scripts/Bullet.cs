using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float BulletForce = 2f;
    public Rigidbody rb;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    
    void Update()
    {
        BulletImpulse();
    }
    public void BulletImpulse()
    {
        Vector3 BulletDistance = (Vector3.up * BulletForce * transform.position.y).normalized;

        rb.AddForce(BulletDistance * this.BulletForce , ForceMode.Impulse);
    }
}
