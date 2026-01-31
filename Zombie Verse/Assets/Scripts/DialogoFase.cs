using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


public class DialogoFase : MonoBehaviour
{
    public Image potrait;
    public TextMeshProUGUI fala;
    private Image sr;
    public int atual;
    public bool bocaAberta;
    public Animator anim;

    [System.Serializable]
    public class Dialogo
    {
        public Sprite CaraBf;
        public Sprite CaraBa;
        [TextArea] public string frase;
        public int numDeLinhas;
        public UnityEvent interaction;
        public bool temInteracao;

    }

    public List<Dialogo> dialogo = new List<Dialogo>();
    void Start()
    {
        sr = potrait.GetComponent<Image>();
        atual = 0;
        bocaAberta = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void ChamaDialogo()
    {
        StartCoroutine(MostraD());
    }

    private IEnumerator MostraD()
    {
        fala.text = "";
        anim.SetBool("MostraD", true);
        yield return new WaitForSeconds(1f);
        int fim = atual + dialogo[atual].numDeLinhas;
        for (int i = atual;i <= fim; i++)
        {
            fala.text = "";
            foreach (char c in dialogo[i].frase)
            {
                fala.text += c;
                if (bocaAberta)
                {
                    sr.sprite = dialogo[i].CaraBf;
                    bocaAberta = false;
                }
                else
                {
                    sr.sprite = dialogo[i].CaraBa;
                    bocaAberta = true;
                }
               
                yield return new WaitForSeconds(0.05f);
            }
            if (dialogo[i].temInteracao)
            {
                dialogo[i].interaction.Invoke();
            }

            yield return new WaitForSeconds(0.8f); // pequeno delay após a frase completa
        }        
        atual = fim + 1;
        bocaAberta = false;
        anim.SetBool("MostraD", false);

    }
}
