using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int ZombiesMortos;
    public float time;
    public int taticas;
    public int ult;
    public int quedas;
    public float pontosFinais;
    int minFinal;
    int segFinal;

    public TextMeshProUGUI ZombiesTxt;
    public TextMeshProUGUI TempoTxt;
    public TextMeshProUGUI TaticaTxt;
    public TextMeshProUGUI UltTxt;
    public TextMeshProUGUI quedastxt;
    public TextMeshProUGUI PontosFinaisTxt;
    public TextMeshProUGUI Quote;

    public Image ScoreFinal;
    public GameObject ScoreImage;
    public GameObject Fundo;

    public bool Fim;
    public bool Zombies;
    public bool Tempo;
    public bool TaticaBool;
    public bool UltBool;
    public bool Quedasbool;
    public bool Final;
    public bool Imagem;

    public static Score instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Fundo.SetActive(false);
        Zombies = true;
        Tempo = false;
        TaticaBool = false;
        UltBool = false;
        Quedasbool = false;
        Final = false;
        Imagem = false;
        ZombiesMortos = 0;
        time = 0;
        taticas = 0;
        ult = 0;
        quedas = 0;
        pontosFinais = 0;
        Fim = false;
        ScoreImage.SetActive(false);
        Quote.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        if (!Fim)
        {
            time += Time.deltaTime;

            int minutos = Mathf.FloorToInt(time / 60);
            int segundos = Mathf.FloorToInt(time % 60);
        }
        else
        {
            if (Zombies)
            {
                Zombies = false;
                StartCoroutine(MostrarPontos(ZombiesMortos,ZombiesTxt, "Tempo"));
            }
            else if(!Zombies && Tempo) { 
            StartCoroutine(MostrarTempo(time));
                Tempo = false;
            }
            else if (!Tempo && TaticaBool)
            {
                StartCoroutine(MostrarPontos(taticas,TaticaTxt, "Ult"));
                TaticaBool = false;
            }
            else if (!TaticaBool && UltBool)
            {
                StartCoroutine(MostrarPontos(ult, UltTxt, "Quedas"));
                UltBool = false;
            }
            else if (!UltBool && Quedasbool)
            {
                StartCoroutine(MostrarPontos(quedas, quedastxt, "Final"));
                Quedasbool = false;
            }
            else if (!Quedasbool && Final)
            {
                StartCoroutine(MostrarPontosFinais());
                Final = false;
            }
            else if (!Final && Imagem)
            {
                StartCoroutine(MostrarImagem());
                Imagem = false;
            }
        }
    }

    private IEnumerator MostrarPontos(int Pontos, TextMeshProUGUI texto, string proximoNome)
    {
        float atual = 0;
        float step = Mathf.Max(1, Pontos / 100f); 

        while (atual < Pontos)
        {
            atual += step;
            texto.text = Mathf.Min(Pontos, Mathf.FloorToInt(atual)).ToString();
            yield return null; 
          }
        switch (proximoNome)
        {
            case "Tempo": Tempo = true; break;
            case "Ult": UltBool = true; break;
            case "Quedas": Quedasbool = true; break;
            case "Final": Final = true; break;
        }

    }
    private IEnumerator MostrarTempo(float tempoFinal)
    {
        float atual = 0f;
        float step = Mathf.Max(0.1f, tempoFinal / 100f); // controla quantos passos terá a animação

        while (atual < tempoFinal)
        {
            atual += step;

            // formata minutos e segundos
            int minutos = Mathf.FloorToInt(atual / 60);
            int segundos = Mathf.FloorToInt(atual % 60);
            TempoTxt.text = $"{minutos:00}:{segundos:00}";

            yield return null; // 1 frame
        }

        // garante que chega exatamente ao valor final
        int minFinal = Mathf.FloorToInt(tempoFinal / 60);
        int segFinal = Mathf.FloorToInt(tempoFinal % 60);
        TempoTxt.text = $"{minFinal:00}:{segFinal:00}";
        TaticaBool = true;
    }
    private IEnumerator MostrarPontosFinais() 
    {
        float atual = 0;
        float step = Mathf.Max(1, ZombiesMortos / 100f);
        pontosFinais = ZombiesMortos * 4;

        while (atual < ZombiesMortos)
        {
            atual += step;
            PontosFinaisTxt.text = Mathf.Min(ZombiesMortos, Mathf.FloorToInt(atual)).ToString();
            yield return null;
        }
        PontosFinaisTxt.text = ZombiesMortos.ToString() + " X 4";
        yield return new WaitForSeconds(0.5f);
        while (atual < pontosFinais)
        {
            atual += step;
            PontosFinaisTxt.text = Mathf.Min(pontosFinais, Mathf.FloorToInt(atual)).ToString();
            yield return null;
        }
        if(minFinal < 20)
        {
            PontosFinaisTxt.text = pontosFinais.ToString() + " + 100";
            yield return new WaitForSeconds(0.5f);
            pontosFinais = pontosFinais + 100;
            while (atual < pontosFinais)
            {
                atual += step;
                PontosFinaisTxt.text = Mathf.Min(pontosFinais, Mathf.FloorToInt(atual)).ToString();
                yield return null;
            }
        }
        PontosFinaisTxt.text = pontosFinais.ToString() + "+" + taticas.ToString() + " X 2";
        yield return new WaitForSeconds(0.5f);
        pontosFinais = pontosFinais + (taticas * 2);
        while (atual < pontosFinais)
        {
            atual += step;
            PontosFinaisTxt.text = Mathf.Min(pontosFinais, Mathf.FloorToInt(atual)).ToString();
            yield return null;
        }
        PontosFinaisTxt.text = pontosFinais.ToString() + "+" + ult.ToString();
        yield return new WaitForSeconds(0.5f);
        pontosFinais = pontosFinais + ult ;
        while (atual < pontosFinais)
        {
            atual += step;
            PontosFinaisTxt.text = Mathf.Min(pontosFinais, Mathf.FloorToInt(atual)).ToString();
            yield return null;
        }
         atual = pontosFinais;
         step = Mathf.Max(1, (quedas * 3) / 50f); // controla a velocidade da animação

        // mostra o valor inicial com a indicação
        PontosFinaisTxt.text = pontosFinais.ToString() + " - " + quedas.ToString() + " X 3";
        yield return new WaitForSeconds(0.5f);

        float alvo = pontosFinais - (quedas * 3);

        while (atual > alvo)
        {
            atual -= step;
            if (atual < alvo) atual = alvo; // garante que não passa do valor final
            PontosFinaisTxt.text = Mathf.FloorToInt(atual).ToString();
            yield return null;
        }
        PontosFinaisTxt.text = alvo.ToString();
        pontosFinais = alvo;
        Imagem = true;
    }
    private IEnumerator MostrarImagem()
    {
        if(pontosFinais >= 1135)
        {
            ScoreImage.SetActive(true);
            ScoreFinal.color = Color.yellowNice;
            yield return new WaitForSeconds(0.2f);
            Quote.text = "Legend";
        }else if(pontosFinais >= 1000 && pontosFinais < 1135)
        {
            ScoreImage.SetActive(true);
            ScoreFinal.color = Color.yellowGreen;
            yield return new WaitForSeconds(0.2f);
            Quote.text = "Brutal";
        }
        else if (pontosFinais >= 800 && pontosFinais < 1000)
        {
            ScoreImage.SetActive(true);
            ScoreFinal.color = Color.lightGreen;
            yield return new WaitForSeconds(0.2f);
            Quote.text = "Super Good";
        }else if(pontosFinais >= 600 && pontosFinais < 800)
        {
            ScoreImage.SetActive(true);
            ScoreFinal.color = Color.darkGreen;
            yield return new WaitForSeconds(0.2f);
            Quote.text = "It`s...ok";
        }
        else if (pontosFinais >= 500 && pontosFinais < 600)
        {
            ScoreImage.SetActive(true);
            ScoreFinal.color = Color.darkRed;
            yield return new WaitForSeconds(0.2f);
            Quote.text = "Thats bad";
        }
        else if (pontosFinais < 500)
        {
            ScoreImage.SetActive(true);
            ScoreFinal.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            Quote.text = "Unistall the game!!";
        }
    }
}

