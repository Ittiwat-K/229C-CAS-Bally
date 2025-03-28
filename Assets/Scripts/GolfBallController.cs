using UnityEngine;

public class GolfBallController : MonoBehaviour
{
    [SerializeField] float hitStrengthMax;
    [SerializeField] float hitStrengthMin;
    [SerializeField] float chargeTimeMax;
    [SerializeField] float verticalStrength; 
    [SerializeField] float minDamping; 
    [SerializeField] float maxDamping; 
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

    private float chargeTimeLeft;  
    private float chargeTimeRight; 
    private bool isChargingLeft = false;  
    private bool isChargingRight = false;

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
                isChargingLeft = true;
                chargeTimeLeft = 0f;
            }

            if (isChargingLeft && Input.GetMouseButton(0))
            {
                chargeTimeLeft += Time.deltaTime;
                chargeTimeLeft = Mathf.Min(chargeTimeLeft, chargeTimeMax);
            }

            if (Input.GetMouseButtonUp(0))
            {
                float hitStrength = Mathf.Lerp(hitStrengthMin, hitStrengthMax, chargeTimeLeft / chargeTimeMax);
                Vector3 hitDirection = Camera.main.transform.forward;
                rb.AddForce(hitDirection * hitStrength, ForceMode.Impulse);
                lastHitPosition = transform.position;

                isChargingLeft = false;
                chargeTimeLeft = 0f;
            }

            if (Input.GetMouseButtonDown(1))
            {
                isChargingRight = true;
                chargeTimeRight = 0f;
            }

            if (isChargingRight && Input.GetMouseButton(1))
            {
                chargeTimeRight += Time.deltaTime;
                chargeTimeRight = Mathf.Min(chargeTimeRight, chargeTimeMax);
            }

            if (Input.GetMouseButtonUp(1))
            {
                float hitStrength = Mathf.Lerp(hitStrengthMin, hitStrengthMax, chargeTimeRight / chargeTimeMax);
                Vector3 hitDirection = Camera.main.transform.forward;
                Vector3 curvedDirection = hitDirection + Vector3.up * verticalStrength;
                rb.AddForce(curvedDirection * hitStrength, ForceMode.Impulse);
                lastHitPosition = transform.position;

                isChargingRight = false;
                chargeTimeRight = 0f;
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
