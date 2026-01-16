using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform ProjectileSpawnPoint;
    [SerializeField] GameObject ProjectilePrefab;
    [SerializeField] float FireRate = 2f;
    [SerializeField] int damage = 2;


    PlayerHealth player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }
    private void Update()
    {
        turretHead.LookAt(playerTargetPoint);
        
    }
    IEnumerator FireRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(FireRate);
            Projectile newProjectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }


}
