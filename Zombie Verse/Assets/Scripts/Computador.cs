using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class Computador : MonoBehaviour
{
  
    [Header("Textos")]
    public TMP_InputField textoPlayer;
    public TextMeshProUGUI textoDescritivo;
    public TextMeshProUGUI Pontos;
    [Header("Imagens")]
    public Image ImgTempo;
    public GameObject Fundo;
    [Header("Collider")]
    public GameObject Collider;
    [Header("Tempo")]
    public float tempo;
    //pontos
    private int Corretos;
    // tempo restante
    private float tempoRestante;
    //Numeros aleatorios
    private int num;
    private int numAnterior;

    private bool MenuIsActive;

    //Não esquecer adicionar animação de perda e de vitoria
    void Start()
    {
        Collider.SetActive(false);
        textoPlayer.Select();
        textoPlayer.ActivateInputField();
        tempoRestante = tempo;
        MudarFrase();
        Corretos = 0;
        MenuIsActive = false;
    }

    public void MostrarMenu()
    {
        Objetivos.instance.Fundo.SetActive(false);
        MenuIsActive = true;
        Fundo.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined ;
    }
    void Update()
    {
     
        if(MenuIsActive){
        tempoRestante -= Time.deltaTime;
        tempoRestante = Mathf.Clamp(tempoRestante, 0f, tempo);
        ImgTempo.fillAmount = tempoRestante / tempo;
        if (Corretos == 3)
        {
            Fundo.SetActive(false);
            MenuIsActive = false;
            Cursor.lockState = CursorLockMode.Locked;
            Objetivos.instance.Fundo.SetActive(true);
            Objetivos.instance.UltimoObjetivo();
            }
        if (tempoRestante > 0) { 
        if (Input.GetKeyDown(KeyCode.Return))
        {
           
            if (textoPlayer.text == textoDescritivo.text)
            {
               
                Corretos++;
                Pontos.text = Corretos.ToString()+" / 3";
                textoDescritivo.color = Color.green;
                StartCoroutine(ColorChange()); 
                MudarFrase();
                    tempoRestante = tempoRestante + 5;
                    textoPlayer.text = "";
                    textoPlayer.ActivateInputField();

                }
                else
            {
                textoDescritivo.color = Color.red;
                StartCoroutine(ColorChange());
                MudarFrase();
                    tempoRestante = tempoRestante -3;
                    textoPlayer.text = "";
                    textoPlayer.ActivateInputField();

                }
            }
        }
        else
        {
            Fundo.SetActive(false);
            Corretos = 0;
            Pontos.text = Corretos.ToString() + " / 3";
            tempoRestante = tempo;
            textoPlayer.text = "";
            MenuIsActive = false;
            Cursor.lockState = CursorLockMode.Locked;
            Objetivos.instance.Fundo.SetActive(true);

            }
        }
    }
    private IEnumerator ColorChange()
    {
        yield return new WaitForSeconds(0.5f);
        textoDescritivo.color = Color.black;
    }

    public void MudarFrase()
    {
        numAnterior = num;
        num = Random.Range(1, 5);
        while (num == numAnterior)
        {
            num = Random.Range(1, 5);
        }
        switch (num)
        {
            case 1:
                textoDescritivo.text = "Eu não vou matar o presidente";
                break;

            case 2:
                textoDescritivo.text = "Eu não vou atirar em ninguem";
                break;

            case 3:
                textoDescritivo.text = "Eu trabalho pela paz";
                break;

            case 4:
                textoDescritivo.text = "Eu nunca cometi um crime";
                break;
        }
    }
}
