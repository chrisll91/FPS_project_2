using Cinemachine;
using NUnit.Framework;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class activeWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject ZoomWeapon;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text ammoReserve;

    AudioSource audioSource;
    
    public WeaponSO currentWeaponSO;
    Animator animator;
    StarterAssetsInputs StarterAssetsInputs;
    Weapon currentWeapon;
    FirstPersonController FirstPersonController;

    const string SHOOT_STRING = "Shoot";
    const string RELOAD_STRING = "Reload";
    float TimeSinceLastShot = 0f;
 
    float defaultZoom;
    float defaultRotationSpeed;
    public bool isRealoding = false;
    

    private void Awake()
    {
        FirstPersonController =GetComponentInParent<FirstPersonController>();
        defaultZoom = playerFollowCamera.m_Lens.FieldOfView;
        animator = GetComponent<Animator>();
        StarterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        defaultRotationSpeed = FirstPersonController.RotationSpeed;
        audioSource = GetComponent<AudioSource>();

    }
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDied += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDied -= HandlePlayerDeath;
    }
    private void Start()
    {
        PickUpWeapon(startingWeapon);
        
        AdjustAmmo(currentWeaponSO.magazineSize);
        
    }
    void Update()
    {   
        HandleShoot();
        HandleZoom();
        HandleReload();

    }

    public void AdjustAmmo(int amount)
    {

        currentWeaponSO.currentAmmo += amount;

        if (currentWeaponSO.currentAmmo > currentWeaponSO.magazineSize) 
        {
            currentWeaponSO.currentAmmo = currentWeaponSO.magazineSize;
        }

        ammoText.text = currentWeaponSO.currentAmmo.ToString("D2");
        
    }

    public void adjustAmmoReserve(int amount)
    {
        currentWeaponSO.reserveAmmo += amount;
        
        if(currentWeaponSO.reserveAmmo > currentWeaponSO.totalAmmoCapacity)
        {
            currentWeaponSO.reserveAmmo = currentWeaponSO.totalAmmoCapacity;
        }
        if (currentWeaponSO.reserveAmmo < 0)
        {
            currentWeaponSO.reserveAmmo = 0;
        }
        ammoReserve.text = currentWeaponSO.reserveAmmo.ToString("D2");
    }

    public void PickUpWeapon(WeaponSO weaponSO)
    {
        if (isRealoding) return;
        if (currentWeapon)
        {

            Destroy(currentWeapon.gameObject);
        }

        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab,transform).GetComponent<Weapon>(); 
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        


        AdjustAmmo(weaponSO.magazineSize);
        adjustAmmoReserve(weaponSO.totalAmmoCapacity);


    }
    public void HandleReload()
    {
        if (!StarterAssetsInputs.Reload) return;
        StarterAssetsInputs.Reload = false;

        if (isRealoding) return;
        if (currentWeaponSO.currentAmmo == currentWeaponSO.magazineSize) return;
        if (currentWeaponSO.reserveAmmo <= 0) return;

        isRealoding = true;
        StartCoroutine(ReloadGunRoutine());


    }
    private void HandlePlayerDeath()
    {
        ZoomWeapon.SetActive(false);

        playerFollowCamera.m_Lens.FieldOfView = defaultZoom;
        weaponCamera.fieldOfView = defaultZoom;

        FirstPersonController.ChangeRotationSpeed(defaultRotationSpeed);
    }


    IEnumerator ReloadGunRoutine()
    {
        if (currentWeaponSO.ReloadingSound != null)
        {
            audioSource.PlayOneShot(currentWeaponSO.ReloadingSound, currentWeaponSO.ShootVolume);
        }

        yield return new WaitForSeconds(currentWeaponSO.ReloadTime);
                
        int amountToReload = currentWeaponSO.magazineSize - currentWeaponSO.currentAmmo;

        if(currentWeaponSO.reserveAmmo < amountToReload)
        {
            amountToReload = currentWeaponSO.reserveAmmo;
        }

        adjustAmmoReserve(-amountToReload);
        AdjustAmmo(amountToReload);
        isRealoding = false;


    }


    private void HandleShoot()
    {
        TimeSinceLastShot += Time.deltaTime;

        if (!StarterAssetsInputs.shoot) return;


        if (TimeSinceLastShot >= currentWeaponSO.FireRate && currentWeaponSO.currentAmmo > 0 && !isRealoding)
        {

            currentWeapon.Shoot(currentWeaponSO);
            TimeSinceLastShot = 0f;

            animator.Play(SHOOT_STRING, 0, 0f);
            if (currentWeaponSO.shootSound != null)
            { 
                audioSource.PlayOneShot(currentWeaponSO.shootSound, currentWeaponSO.ShootVolume);
            }
            
            AdjustAmmo(-1);     
           
        }
        if (TimeSinceLastShot >= currentWeaponSO.FireRate && currentWeaponSO.currentAmmo <= 0 && !isRealoding)
        {
            TimeSinceLastShot = 0f;

            if (currentWeaponSO.emptyMagazineSound != null)
            {
                Debug.Log("empty magazine");
                audioSource.PlayOneShot(currentWeaponSO.emptyMagazineSound, currentWeaponSO.ShootVolume);
            }
        }


            if (!currentWeaponSO.IsAutomatic)
        { 
            StarterAssetsInputs.ShootInput(false);
            
        }

        
    }

    void HandleZoom()
    {
        if(!currentWeaponSO.canZoom) return;

        if (StarterAssetsInputs.Zoom)
        {
            ZoomWeapon.SetActive(true); 
            playerFollowCamera.m_Lens.FieldOfView = currentWeaponSO.ZoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;

            FirstPersonController.ChangeRotationSpeed(currentWeaponSO.rotationAmount);
        }
        else
        {
            ZoomWeapon.SetActive(false);
            playerFollowCamera.m_Lens.FieldOfView = defaultZoom;
            weaponCamera.fieldOfView = defaultZoom;

            FirstPersonController.ChangeRotationSpeed(defaultRotationSpeed);

        }
    }
}
