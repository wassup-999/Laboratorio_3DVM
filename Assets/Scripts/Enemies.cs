using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int Range = 10;
    public bool VerifyArea = false;
    public Transform Player;
    public float speed = 1f;
    void Start()
    {
        GetComponent<SphereCollider>().radius = Range;
        GetComponent<Player>();
    }

    
    void Update()
    {
        //FollowPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VerifyArea = true;
            Player = other.gameObject.transform;         
            Debug.Log("Detected");
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VerifyArea= false;
            Player = null;
            Debug.Log(" Not Detected");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision");
            Destroy(gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,Range);
        Gizmos.color = Color.green;
        if (VerifyArea)
        {
            Gizmos.DrawLine(transform.position, Player.transform.position);
            Vector3 dir = (Player.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }        
    }
    public void FollowPlayer()
    {
        /* (VerifyArea)
        {
            Vector3 dir = (Player.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }*/
    }    
}
