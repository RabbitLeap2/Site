using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Weapon;

public class HudManager : MonoBehaviour
{
    public static HudManager instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    public Image ammoTypeUI;

    [Header("Weapon")]
    public Image activeWeaponUI;
    public Image unActiveWeaponUI;

    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    public Image tacticalUI;
    public TextMeshProUGUI taticalAmountUI;

    public GameObject MiddleDot;
    public Sprite Mao;
    public Sprite Computador;
    public Sprite Dot;



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

    private void Update()
    {
        if (TrocarArma.instance.arma1NaMao == true)
        {

            Weapon activeWeapon = TrocarArma.instance.arma1.GetComponent<Weapon>();
            Weapon unActiveWeapon = TrocarArma.instance.arma2.GetComponent<Weapon>();
         

            EscreverUI(activeWeapon, unActiveWeapon);
    }
        else
        {
            Weapon unActiveWeapon = TrocarArma.instance.arma1.GetComponent<Weapon>();
            Weapon activeWeapon = TrocarArma.instance.arma2.GetComponent<Weapon>();
            EscreverUI(activeWeapon, unActiveWeapon);

        }

    }

    private void EscreverUI(Weapon activeWeapon, Weapon unActiveWeapon)
    {
        magazineAmmoUI.text = $"{activeWeapon.bulletsLeft / activeWeapon.bulletPerBurst}";
        totalAmmoUI.text = $"{TrocarArma.instance.CheckAmmoLeftFor(activeWeapon.currentWeapon)}";

        Weapon.weaponModel model = activeWeapon.currentWeapon;
        ammoTypeUI.sprite = GetAmmoSprite(model);

        activeWeaponUI.sprite = GetWeaponSprite(model);
     
         unActiveWeaponUI.sprite = GetWeaponSprite(unActiveWeapon.currentWeapon);
        
    }

    private Sprite GetWeaponSprite(weaponModel model)
    {
       switch (model)
        {
            case Weapon.weaponModel.Pistola:
                return Resources.Load<GameObject>("Pistola_Weapon").GetComponent<SpriteRenderer>().sprite;

            case Weapon.weaponModel.Smg:
                return Resources.Load<GameObject>("Smg_Weapon").GetComponent<SpriteRenderer>().sprite;

            case Weapon.weaponModel.M4:
                return Resources.Load<GameObject>("M4_Weapon").GetComponent<SpriteRenderer>().sprite;

            default:
                return null; 

        }
    }

    private Sprite GetAmmoSprite(weaponModel model)
    {
        switch (model)
        {
            case Weapon.weaponModel.Pistola:
                return Resources.Load<GameObject>("Pistola_Ammo").GetComponent<SpriteRenderer>().sprite;

            case Weapon.weaponModel.Smg:
                return Resources.Load<GameObject>("Smg_Ammo").GetComponent<SpriteRenderer>().sprite;

            case Weapon.weaponModel.M4:
                return Resources.Load<GameObject>("M4_Ammo").GetComponent<SpriteRenderer>().sprite;

            default:
                return null;
    }
    }
}
