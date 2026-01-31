using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PlayerScript;


public class Weapon : MonoBehaviour
{
    public int weaponDamage;

    [Header("Shooting")]
    public bool isShooting, readyToShoot;
    bool allowReset = true;
    public float shootingDelay = 2f;

    [Header("Burst")]
    public int bulletPerBurst = 3;
    public int burstBulletsLeft;

    [Header("Spread")]
    private float spreadIntensity;
    public float hitSpreadIntesity;
    public float adsSpreadIntesity;

    [Header("Bullets")]
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30;
    public float bulletPrefabLifeTime = 3f;

    public GameObject muzzleEffect;

    [Header("Reload")]
    public float reloadTime;
    public int magazineSize, bulletsLeft;
    public bool isReloading;

    bool isAds;
   public  bool isFirstW;
    public enum weaponModel{
        Pistola,
        Smg,
        M4
        }

    public weaponModel currentWeapon;

    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;


    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletPerBurst;

        bulletsLeft = magazineSize;

        spreadIntensity = hitSpreadIntesity;
        if (isFirstW)
        {
            GetComponentInParent<Animator>().SetTrigger("IDLE");
        }
        else
        {
            GetComponentInParent<Animator>().SetTrigger("IDLESec");
        }
    }

    void Update()
    {
        if (PlayerScript.instance.isDead == false) {
            
            if (Input.GetMouseButtonDown(1))
            {

                if (isFirstW)
                {
                    GetComponentInParent<Animator>().SetTrigger("EnterAds");
                }
                else
                {
                    GetComponentInParent<Animator>().SetTrigger("EnterAdsSec");
                }
                isAds = true;
                HudManager.instance.MiddleDot.SetActive(false);
                spreadIntensity = adsSpreadIntesity;
            }
        if (Input.GetMouseButtonUp(1))
        {
                if (isFirstW)
                {
                      GetComponentInParent<Animator>().SetTrigger("ExitAds");
                }
                else
                {
                    GetComponentInParent<Animator>().SetTrigger("ExitAdsSec");
                }
                isAds = false;
            HudManager.instance.MiddleDot.SetActive(true);
            spreadIntensity = hitSpreadIntesity;

        }

        if (bulletsLeft == 0 && isShooting)
        {
            SoundManager.instance.emptyMagSoundPistola.Play();
        }

        if (currentShootingMode == ShootingMode.Auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !isReloading && TrocarArma.instance.CheckAmmoLeftFor(currentWeapon) > 0)
        {
            Reload();
        }

        /*  if(readyToShoot && !isShooting && !isReloading && bulletsLeft <= 0)
          {
              Reload();
          }*/


        if (readyToShoot && isShooting && bulletsLeft > 0)
        {
            burstBulletsLeft = bulletPerBurst;
            FireWeapon();
        }
    } 
      
    }



    private void FireWeapon()
    {
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        if (isAds)
        {
            if (isFirstW)
            {
                  GetComponentInParent<Animator>().SetTrigger("RecoilAds");
            }
            else
            {
                GetComponentInParent<Animator>().SetTrigger("RecoilAdsSec");
            }

        }
        else
        {
            if (isFirstW)
            {
                  GetComponentInParent<Animator>().SetTrigger("RECOIL");
            }
            else
            {
                GetComponentInParent<Animator>().SetTrigger("RECOILSec");
            }
        }
        SoundManager.instance.PlayShootingSound(currentWeapon);
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bul = bullet.GetComponent<Bullet>();
        bul.bulletDamage = weaponDamage;

        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet,bulletPrefabLifeTime));

        if(allowReset)
        {
            Invoke("ResetShot", shootingDelay);
            allowReset = false;
        }

        if( currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
        {
            burstBulletsLeft--;
            Invoke("FireWeapon", shootingDelay);
        }

    }

    private void Reload()
    {

        SoundManager.instance.PlayReloadingSound(currentWeapon);

        if (isFirstW)
        {
              GetComponentInParent<Animator>().SetTrigger("RELOAD");
        }
        else
        {
            GetComponentInParent<Animator>().SetTrigger("RELOADSec");
        }
        isReloading = true;
        Invoke("ReloadCompleated", reloadTime);
    }

    private void ReloadCompleated()
    {
        if(TrocarArma.instance.CheckAmmoLeftFor(currentWeapon) > magazineSize)
        {
            bulletsLeft = magazineSize;
            TrocarArma.instance.DecreaseTotalAmmo(bulletsLeft, currentWeapon);
        }
        else
        {
            bulletsLeft = TrocarArma.instance.CheckAmmoLeftFor(currentWeapon);
            TrocarArma.instance.DecreaseTotalAmmo(bulletsLeft, currentWeapon);
        }
        isReloading = false;
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

    private Vector3 CalculateDirectionAndSpread()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - bulletSpawn.position;

        float z = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction  + new Vector3(0,y,z);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


}
