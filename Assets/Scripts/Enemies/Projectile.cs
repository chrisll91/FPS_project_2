using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] GameObject projectileHitVFX;

    int damage; // damage value given in the turret script
    public Rigidbody rb;
    

    const string PLAYER_STRING = "Player";

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    public void Init(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        playerHealth?.takeDamage(damage);
        Instantiate(projectileHitVFX, transform.position, Quaternion.identity); 
        Destroy(this.gameObject);
            
    }
}
