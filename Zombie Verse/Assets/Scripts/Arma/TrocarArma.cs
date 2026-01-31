using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class TrocarArma : MonoBehaviour
{
    public GameObject arma1;
    public GameObject arma2;
    public bool arma1NaMao = true;

    public static TrocarArma instance { get; set; }

    [Header("Ammo")]
    public int totalSmgAmmo = 0;
    public int totalPistolAmmo = 0;
    public int totalM4Ammo = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha1) && !arma1NaMao)
        {
            arma2.SetActive(false);
            arma1.SetActive(true);
            arma1NaMao = true;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && arma1NaMao)
        {
            arma1.SetActive(false);
            arma2.SetActive(true);
            arma1NaMao = false;

        }

    }

    internal void PickupAmmo(AmmoBox ammo)
    {
        switch (ammo.ammoType)
        {
            case AmmoBox.AmmoType.PistolAmmo:
                totalPistolAmmo += ammo.ammoAmount;
                break;
            case AmmoBox.AmmoType.SmgAmmo:
                totalSmgAmmo += ammo.ammoAmount;
                break;
            case AmmoBox.AmmoType.M4Ammo:
                totalSmgAmmo += ammo.ammoAmount;
                break;
        }
    }

    internal void DecreaseTotalAmmo(int bulletsToDecrease, Weapon.weaponModel currentWeapon)
    {
        switch(currentWeapon)
        {
            case Weapon.weaponModel.Smg:
                totalSmgAmmo -= bulletsToDecrease;
                break;
            case Weapon.weaponModel.Pistola:
                totalPistolAmmo -= bulletsToDecrease;
                break;
            case Weapon.weaponModel.M4:
                totalM4Ammo -= bulletsToDecrease;
                break;

        }
    }

    public int CheckAmmoLeftFor(weaponModel currentWeapon)
    {
        switch (currentWeapon)
        {
            case weaponModel.Smg:
                return totalSmgAmmo;

            case weaponModel.Pistola:
                return totalPistolAmmo;

            case weaponModel.M4:
                return totalM4Ammo;

            default:
                return 0;
        }
    }
}
