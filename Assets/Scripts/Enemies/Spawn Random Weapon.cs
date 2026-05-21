using System.Collections;

using UnityEngine;


public class SpawnRandomWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] weaponPrefabs;
    

    private void Start()
    {
        spawnRandomWeapon();
    }

    private void spawnRandomWeapon()
    {
        int randomIndex = Random.Range(0, weaponPrefabs.Length);
        GameObject WeaponToSpawn = weaponPrefabs[randomIndex];

        GameObject newWeapon = Instantiate(WeaponToSpawn,transform.position,transform.rotation);
    }
}
