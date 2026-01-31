using UnityEngine;
using System.Collections;

public class SuperMecado : MonoBehaviour
{
    int casas;
    public GameObject porta;
    public float tempo;
    public GameObject Fim;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (casas == 3)
        {
            Objetivos.instance.primeiroObjetivo();
            casas = 0;
        }
    }

    public void EntrouCasa()
    {
        casas++;
    }
    public void EntrouSuperMecado()
    {
        porta.SetActive(true);
        Objetivos.instance.segundoObjetivo();
    }

    public void AtivouLuzes()
    {
        Objetivos.instance.terceiroObjetivo();
        StartCoroutine(Sobriviver());
    }
    private IEnumerator Sobriviver()
    {
        yield return new WaitForSeconds(tempo);
        Objetivos.instance.UltimoObjetivo();
        Fim.SetActive(true);
    }
}
