using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class FimFase : MonoBehaviour
{
    public bool EstaDentro;
    public GameObject HUD;
    public GameObject Objetivos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EstaDentro = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EstaDentro = false;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EstaDentro)
        {
            StartCoroutine(AcabaAFase());
        } 
    }

    private IEnumerator AcabaAFase()
    {
        yield return new WaitForSeconds(3f);
        Score.instance.Fundo.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        HUD.SetActive(false);
        Objetivos.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        Score.instance.Fim = true;
    }
}
