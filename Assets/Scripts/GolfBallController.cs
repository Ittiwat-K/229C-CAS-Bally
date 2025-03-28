using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrength;  
    [SerializeField] float verticalStrength; 
    [SerializeField] float minDamping = 0.5f; 
    [SerializeField] float maxDamping = 2.0f; 
    //[SerializeField] float maxHp = 100f;  
    //[SerializeField] float currentHp;  
    [SerializeField] float dampingInterval; 
    private Rigidbody rb;  
    private float golfBallVelocity = 0.1f;  
    //private int hitCount; 
    private Vector3 lastHitPosition;  
    //private Collider ballCollider;  
    //private PhysicsMaterial ballPhysicsMaterial;  

    public AirMeter airMeter;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  
        //ballCollider = GetComponent<Collider>();  
        //ballPhysicsMaterial = ballCollider.sharedMaterial;  
        //currentHp = maxHp;  
        lastHitPosition = transform.position;  

        InvokeRepeating("RandomDamping", 0f, dampingInterval);

        UpdateUI();
    }

    void Update()
    {
        if (rb.linearVelocity.magnitude < golfBallVelocity)
        {   
            if (Input.GetMouseButtonDown(0)) 
            {
                Vector3 hitDirection = Camera.main.transform.forward;  
                rb.AddForce(hitDirection * hitStrength, ForceMode.Impulse);  
                lastHitPosition = transform.position; 
                //hitCount++;
            }

            if (Input.GetMouseButtonDown(1)) 
            {
                Vector3 hitDirection = Camera.main.transform.forward;  
                Vector3 curvedDirection = hitDirection + Vector3.up * verticalStrength; 
                rb.AddForce(curvedDirection * hitStrength, ForceMode.Impulse);  
                lastHitPosition = transform.position; 
                //hitCount++;
            }
        }
        UpdateUI();
    }

    void RandomDamping()
    {
        float randomDamping = Random.Range(minDamping, maxDamping);
        rb.linearDamping = randomDamping;
    }

    void UpdateUI()
    {
        if (airMeter != null)
        {
            airMeter.UpdateUI(rb.linearDamping);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.CompareTag("damage"))
        {
            currentHp -= 10f;  

            transform.position = lastHitPosition;
            rb.linearVelocity = Vector3.zero; 
        }
        else if (collision.gameObject.CompareTag("heal"))
        {
            currentHp += 10f;  
            if (currentHp > maxHp)  
                currentHp = maxHp;
        }

        if (currentHp <= 0f)
        {
            currentHp = 0f;
        }*/

        if (collision.gameObject.CompareTag("damage"))
        {
            transform.position = lastHitPosition;
            rb.linearVelocity = Vector3.zero;
        }
    }
}
