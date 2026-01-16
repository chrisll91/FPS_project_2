using UnityEngine;
[CreateAssetMenu(fileName = "WeaponSO",menuName = "Scriptable Objects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject weaponPrefab;

    [Header("Combat")]
    public int Damage = 1;
    public float FireRate = 0.5f;
    public bool IsAutomatic = false;
    public float ReloadTime = 1f;

    [Header("Ammo")]
    public int magazineSize = 9;
    public int totalAmmoCapacity = 36;
    public int reserveAmmo;
    public int currentAmmo;

    [Header("Audio")]
    public GameObject HitVFXprefab;
    public AudioClip shootSound;
    public AudioClip emptyMagazineSound;
    public AudioClip ReloadingSound;
    public float ShootVolume = .5f;

    [Header("Zoom")]
    public bool canZoom = false;
    public float ZoomAmount = 10f;
    public float rotationAmount = 0.4f;

    [Header("Animations")]
    public AnimationClip reloadAnimation;
}
