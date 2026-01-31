using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionChoice : MonoBehaviour
{
    [Header("Menu das Missões")]
    public GameObject MissionMenu;
    private bool MisActive;
    public GameObject Botoes;

    [Header("Menu dos Personagens")]
    public GameObject EscolherPersonagem;


    public int Campanha;
    public int Mission;
    public string Title;
    public string Area;

    [Header("UI das Missões")]
    public GameObject MissaoHUD;
    public TextMeshProUGUI Titulo;
    public TextMeshProUGUI Personagens;
    public TextMeshProUGUI descricao;

    public TextMeshProUGUI TituloFinal;

    [System.Serializable]
    public class Missao
    {
        public string NomeDaFase;
        public int campanha;
        public int missao;
        public string Titulo;
        public string Personagens;
        [TextArea] public string descricao;
    }

    public List<Missao> missoes = new List<Missao>();

    void Start()
    {
        MisActive = false;
        MissionMenu.SetActive(false);
    }

    public void MissionMenuShow()
    {
        MisActive = !MisActive;
        MissionMenu.SetActive(MisActive);
        Cursor.lockState = MisActive ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

    public void CampainNumer(int numero)
    {
        MissaoHUD.SetActive(false);
        Campanha = numero;
        Botoes.SetActive(true);
        
    }

    public void EscolherMissao(int numeroMissao)
    {
        MissaoHUD.SetActive(true);
        Mission = numeroMissao;
        foreach (var m in missoes)
        {
            if (m.campanha == Campanha && m.missao == numeroMissao)
            {
                Titulo.text = m.Titulo;
                Personagens.text = m.Personagens;
                descricao.text = m.descricao;
                Title = m.Titulo;
                Area = m.NomeDaFase;
                return;
            }
        }

    }

    public void MenuReady()
    {
        MissionMenu.SetActive(false);
        EscolherPersonagem.SetActive(true);
        TituloFinal.text = Title;

    }

    public void StartMission()
    {
        SceneManager.LoadScene(Area);

    }
}
