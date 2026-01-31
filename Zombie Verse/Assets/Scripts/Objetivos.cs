using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Objetivos : MonoBehaviour
{
    public GameObject Fundo;
    public TextMeshProUGUI TextoObjetivo;
    public TextMeshProUGUI TextoNovoObjetivo;
    public static Objetivos instance;
    public int NumObjetivo;
    public Animator anim;
    private int NumObjetivoAtual;
    public float tempo;

    //---------------------Painel-----
    public GameObject Objetivo1;
    //---------------------Computador-----
    public GameObject Objetivo2;
    //---------------------Papel-----
    public GameObject Objetivo3;
    //---------------------porta-----
    public GameObject Objetivo4;
    public GameObject Objetivo5;


    [System.Serializable]
    public class Objetivo
    {
        public string nome;
    }

    public List<Objetivo> missoes = new List<Objetivo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Objetivo2.SetActive(false);
        Objetivo3.SetActive(false);
        Objetivo4.SetActive(false);
        NumObjetivo = 0;
        TextoObjetivo.text = missoes[NumObjetivo].nome;
        TextoNovoObjetivo.text = "";
        NumObjetivoAtual = NumObjetivo;
        anim = GetComponent<Animator>();
    }

   
    public void primeiroObjetivo()
    {
        ProximoObjetivo(Objetivo1, Objetivo2);
    }
    public void segundoObjetivo()
    {
        ProximoObjetivo(Objetivo2, Objetivo3);
    }
    public void terceiroObjetivo()
    {
        ProximoObjetivo(Objetivo3, Objetivo4);
    }
 
    public void ProximoObjetivo(GameObject objeto, GameObject proximo)
    {
        NumObjetivo = NumObjetivo + 1;
        Destroy(objeto);
        anim.SetBool("NovoObjetivo", true);
        NumObjetivoAtual = NumObjetivo;
        TextoNovoObjetivo.text = missoes[NumObjetivo].nome;
        StartCoroutine(MudarNome());
        proximo.SetActive(true);
    }
   public void UltimoObjetivo()
    {
        NumObjetivo += 1;
        Destroy(Objetivo4);
        anim.SetBool("NovoObjetivo", true);
        TextoNovoObjetivo.text = missoes[NumObjetivo].nome;
        NumObjetivoAtual = NumObjetivo;
        StartCoroutine(MudarNome());
        Destroy(Objetivo5);
    }
    private IEnumerator MudarNome()
    {
        yield return new WaitForSeconds(tempo);
        TextoObjetivo.text = missoes[NumObjetivo].nome;
        TextoNovoObjetivo.text = "";
        anim.SetBool("NovoObjetivo", false);

    }
}
