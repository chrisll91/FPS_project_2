using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int StartingHealth = 3;
    [SerializeField] GameObject RobotExplosionVFX;
    [SerializeField] AudioSource roamingAudioSource;
    [SerializeField] AudioClip explosionClip;

    public SurvivalModeSceneManager gameManager;
    public SpawnGate spawnGate;

    int currentEnemyHealth;

    private void Awake()
    {
        currentEnemyHealth = StartingHealth;
        gameManager = FindAnyObjectByType<SurvivalModeSceneManager>();
             
        
    }
    private void Start()
    {   
        gameManager?.adjustEnemiesText(1);

        if (roamingAudioSource != null)
        {
            roamingAudioSource.loop = true;
            roamingAudioSource.Play();
        }

    }
    public void takeDamage(int amount)
    {
        currentEnemyHealth -= amount;
        
        if (currentEnemyHealth <= 0) 
        {
            gameManager?.adjustEnemiesKilledText(1);
            selfDestruct();           
        }
    }

    public void selfDestruct()
    {
        
        gameManager?.adjustEnemiesText(-1);
        
        Instantiate(RobotExplosionVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionClip, transform.position, 0.5f);
        Destroy(this.gameObject);
        
        if (spawnGate != null)
        {
            spawnGate.adjustSpawnAmount(-1);
        }
    }
}
