using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public KeyCode interactKey;
    public UnityEvent interaction;
    public GameObject Imagem;
    public bool EstaDentro;
    private Image sr;
    public bool computador;
    bool jaInt;

    private void Start()
    {
        sr = HudManager.instance.MiddleDot.GetComponent<Image>();
        computador = false;
        jaInt = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jaInt)
        {
            print("Entrou");
            EstaDentro = true;
            Imagem.SetActive(true);
            mudarImagem();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Saiu");
            EstaDentro = false;
            Imagem.SetActive(false);
            sr.sprite = HudManager.instance.Dot;
            sr.color = Color.red;
            HudManager.instance.MiddleDot.transform.localScale = new Vector3(0.05f, 0.05f, 1f);

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (EstaDentro && Input.GetKeyDown(interactKey) )
        {
            print("E presionado");
            Imagem.SetActive(false);
            jaInt = true;
            interaction.Invoke();
            sr.sprite = HudManager.instance.Dot;
            sr.color = Color.red;
            HudManager.instance.MiddleDot.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
        }
    }

    public void mudarImagem()
    {
        if(!computador)
        {
            sr.sprite = HudManager.instance.Mao;
        sr.color = Color.white;
        HudManager.instance.MiddleDot.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            sr.sprite = HudManager.instance.Computador;
            sr.color = Color.white;
            HudManager.instance.MiddleDot.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
