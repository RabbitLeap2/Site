using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Lever : MonoBehaviour
{
    public Transform leverRoda;
    public Transform Jaula;
    public GameObject bombas;
    public bool isRoda;
    public bool isW;
    public bool isD;
    public bool isS;
    public bool isA;
    public bool isAnim;
    public int Acabou;
    public float posicaoInicial;
    //----------------------
    public GameObject bomba;
    void Start()
    {
        isW = false;
        isD = true;
        isS = false;
        isA = false;
        isRoda = false;
        Acabou = 0;
        posicaoInicial = Jaula.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRoda)
        {
            PlayerMovement.instance.canMove = false;
            if (Input.GetKeyDown(KeyCode.W) && isW && !isAnim)
            {
                StartCoroutine(RodarAlavanca());
                StartCoroutine(DescerJaula(5f));
                isW = false;
                isD = true;
                Acabou += 1;
            }
            if (Input.GetKeyDown(KeyCode.D) && isD && !isAnim)
            {
                StartCoroutine(RodarAlavanca());
                StartCoroutine(DescerJaula(5f));
                isD = false;
                Acabou += 1;
                isS = true;
            }
            if (Input.GetKeyDown(KeyCode.S) && isS && !isAnim)
            {
                StartCoroutine(RodarAlavanca());
                StartCoroutine(DescerJaula(5f));
                isS = false;
                Acabou += 1;
                isA = true;
            }
            if (Input.GetKeyDown(KeyCode.A) && isA && !isAnim)
            {
                StartCoroutine(RodarAlavanca());
                StartCoroutine(DescerJaula(5f));
                Acabou += 1;
                isA = false;
                isW = true;
            }
            if(Acabou == 7)
            {

                PlayerMovement.instance.canMove = true;
                isRoda = false;
                bombas.SetActive(true);
                
            }
        }  
    }
    IEnumerator RodarAlavanca()
    {
        isAnim = true;
        Quaternion inicio = leverRoda.rotation;
        Quaternion fim = inicio * Quaternion.Euler(90f, 0f, 0f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            leverRoda.rotation = Quaternion.Slerp(inicio, fim, t);
            yield return null;
        }
        isAnim = false;
    }
    IEnumerator DescerJaula(float distancia)
    {
        isAnim = true;

        Vector3 inicio = Jaula.position;
        Vector3 fim = inicio + Vector3.down * distancia;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime *2f;
            Jaula.position = Vector3.Lerp(inicio, fim, t);
            yield return null;
        }
        isAnim = false;

    }


    public void Rodar()
    {
        isRoda = true;
    }

    public void Bomba()
    {
        bomba.SetActive(true);
        StartCoroutine(BombaExplode());
    }
    IEnumerator BombaExplode()
    {
        yield return new WaitForSeconds(2f);
        Objetivos.instance.UltimoObjetivo();
    }

}
