using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int HP = 100;
    public int MaxHP;
    public GameObject bloodyScreen;

    public Image playerHealthUI;
    public GameObject gameOverUI;
    public bool isDead;
    public static PlayerScript instance { get; set; }

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
    private void Start()
    {
        MaxHP = HP;
        playerHealthUI.fillAmount = HP/MaxHP;
  
    }
    
    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            isDead = true;
            print("Player Dead");
            PlayerDead();
        }
        else
        {
            print("Player Hit");
            StartCoroutine(BloodyScreenEffect());
            playerHealthUI.fillAmount = HP / MaxHP;
            SoundManager.instance.playerChannel.PlayOneShot(SoundManager.instance.PlayerHurt);


        }
    }

   private void PlayerDead()
    {
        SoundManager.instance.playerChannel.PlayOneShot(SoundManager.instance.Playerdead);

        GetComponentInParent<MouseMovement>().enabled = false;
        GetComponentInParent<PlayerMovement>().enabled = false;
        GetComponentInParent<Animator>().enabled = true;
        playerHealthUI.gameObject.SetActive(false);
        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOverUI());
       
        
    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);
    }

    private IEnumerator BloodyScreenEffect()
    {
       if(bloodyScreen.activeInHierarchy == false)
        {
            bloodyScreen.SetActive(true);
        }
        var image = bloodyScreen.GetComponent<Image>();

        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;
            elapsedTime += Time.deltaTime;

            yield return null; ; 
        }

        if (bloodyScreen.activeInHierarchy)
        {
            bloodyScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (!isDead)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }
        }
    }
  

}
