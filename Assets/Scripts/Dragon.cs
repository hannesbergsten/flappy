using UnityEngine;

public class Dragon : PlayerCharacter
{
    public float rotationSpeed = 0.5f; 
    
    //ENCAPSULATION
    [SerializeField] public float upForce = 7f; 
    
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.AddScore(-10);
        }
    }

    void Update()
    {
        if (transform.position.y < -5)
        {
            GameManager.Instance.Exit();
        }

        AddForce();
        AdjustRotation();
    }
    void AdjustRotation()
    {
        // Calculate the angle based on the dragon's velocity
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;

        // Smoothly rotate towards the target angle
        Quaternion targetRotation = Quaternion.Euler(new Vector3(-angle, 0, 0));
        var quaternionB = Quaternion.Euler(targetRotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, quaternionB, rotationSpeed * Time.deltaTime);
    }

    // POLYMORPHISM
    public override void AddForce()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector3.up * upForce, ForceMode.VelocityChange);
        }
    }
}
