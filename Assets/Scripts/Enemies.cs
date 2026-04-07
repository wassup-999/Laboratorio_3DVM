using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int Range = 10;
    public bool VerifyArea = false;
    public GameObject Player;
    public float speed = 1f;
    public ThirdPersonController playerTC;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<SphereCollider>().radius = Range;
        playerTC = Player.GetComponent<ThirdPersonController>();

      
    }

    
    void Update()
    {        
        if(VerifyArea == true && Vector3.Distance(Player.transform.position, transform.position)<1f )
        {
            Debug.Log("collision");
            Destroy(gameObject);
            playerTC.RecieveDamage(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VerifyArea = true;
            Player = other.gameObject;         
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
    
}
