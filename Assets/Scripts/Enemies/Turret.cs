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
    [SerializeField] float attackRange = 15f;

    PlayerHealth player;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());
    }
    private void Update()
    {
        if (player == null) {return; }

        float distanceToPlayer =
               Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < attackRange)
        {
            turretHead.LookAt(playerTargetPoint);
        }
    }
    IEnumerator FireRoutine()
    {
        while (player)
        {
            yield return new WaitForSeconds(FireRate);

            if (player == null)
                yield break;

            float distanceToPlayer =
           Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer > attackRange)
            {
                continue;
            }

            Projectile newProjectile = Instantiate(ProjectilePrefab, ProjectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
            newProjectile.transform.LookAt(playerTargetPoint);
            newProjectile.Init(damage);
        }
    }


}
