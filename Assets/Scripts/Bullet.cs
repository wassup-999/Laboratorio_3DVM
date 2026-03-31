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
        Vector3 BulletForce = (transform.up * this.BulletForce * transform.position.y).normalized;

        rb.AddForce(BulletForce * this.BulletForce , ForceMode.Impulse);
    }
}
