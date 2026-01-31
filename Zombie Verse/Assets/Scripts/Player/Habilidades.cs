using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Habilidades : MonoBehaviour
{
    public int Personagem; // 1 - Zero, 2 - Witness, 3 - MailMan, 4- RandomM;
    public KeyCode Tatica1 = KeyCode.Q;
    public KeyCode Tatica2 = KeyCode.E;
    public KeyCode Ult = KeyCode.G;
    public Image Tatica1CollDown;
    public Image Tatica2CollDown;
    public Image UltCollDown;
    PlayerMovement pm;

    public float dashSpeed;
    public float dashTime;

    public float TempoNoAr;
    public float forcaParaCima;
    public float forcaParaBaixo;
    public GameObject Slam;

    public float TempoUlt;
    public float VelocidadeUlt;

    private float Tatica1Timer;
    public float Tatica1TimerInicial;

    private float Tatica2Timer;
    public float Tatica2TimerInicial; 
    
    private float UltTimer;
    public float UltInicial;

    void Start()
    {
        Slam.SetActive(false);
        UltTimer = UltInicial;
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Personagem)
        {
            case 1:
                if (Input.GetKeyDown(Tatica1) && Tatica1Timer <= 0) {
                    StartCoroutine(VerticalSlam(forcaParaCima, TempoNoAr, forcaParaBaixo));
                    Tatica1Timer = 10000000000000000000;
                }
                if (Input.GetKeyDown(Tatica2) && Tatica2Timer <= 0)
                {
                    StartCoroutine(Dash());
                    Tatica2Timer = Tatica2TimerInicial;
                }
                if (Input.GetKeyDown(Ult) && UltTimer <= 0)
                {
                    StartCoroutine(ZeroUlt());
                    UltTimer = 10000000000000000000;

                }
                break;
        }
        if(Tatica2Timer > 0)
        {
            Tatica2Timer -= Time.deltaTime;
        }
        Tatica2CollDown.fillAmount = Tatica2Timer / Tatica2TimerInicial;
        if (Tatica1Timer > 0)
        {
            Tatica1Timer -= Time.deltaTime;
        }
        Tatica1CollDown.fillAmount = Tatica1Timer / Tatica1TimerInicial;
        if (UltTimer > 0)
        {
            UltTimer -= Time.deltaTime;
        }
        UltCollDown.fillAmount = UltTimer / UltInicial;

    }




    IEnumerator Dash()
    {
        pm.isDashing = true; 
        Vector3 dashDir = pm.cameraTransform.forward;
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            pm.controller.Move(dashDir.normalized * dashSpeed * Time.deltaTime);
            yield return null;
        }

        pm.isDashing = false; 
    }
    IEnumerator VerticalSlam(float upForce, float hangTime, float downForce)
    {
        pm.isDashing = true; 
        float startTime = Time.time;

      
        pm.velocity.y = upForce;

        
        while (Time.time < startTime + hangTime)
        {
            Slam.SetActive(true);
            pm.controller.Move(new Vector3(0, pm.velocity.y * Time.deltaTime, 0));
            yield return null;
        }

        
        pm.velocity.y = -downForce;
        while (!pm.isGrounded)
        {
            pm.velocity.y += pm.gravity * Time.deltaTime; 
            pm.controller.Move(new Vector3(0, pm.velocity.y * Time.deltaTime, 0));
            yield return null;
        }
        Tatica1Timer = Tatica1TimerInicial;
        pm.velocity.y = 0;
        pm.isDashing = false;
        Slam.SetActive(false);

    }
    IEnumerator ZeroUlt()
    {
        float originalGravity = pm.gravity;
        pm.gravity = 0f;
        pm.isDashing = true;
        Vector3 dashDir = pm.cameraTransform.forward;
        dashDir.y = 0f;              // remove a componente vertical
        dashDir.Normalize();
        float startTime = Time.time;

        while (Time.time < startTime + TempoUlt)
        {
            pm.controller.Move(dashDir.normalized * VelocidadeUlt * Time.deltaTime);
            yield return null;
        }
        pm.isDashing = false;
        pm.gravity = -9.81f * 2;
        UltTimer = UltInicial;

    }

}
