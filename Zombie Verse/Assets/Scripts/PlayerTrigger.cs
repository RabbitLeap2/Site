using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class PlayerTrigger : MonoBehaviour
{
    public bool EstaDentro;
    public GameObject objeto;
    public UnityEvent interaction;


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

    // Update is called once per frame
    void Update()
    {
        if (EstaDentro)
        {
            interaction.Invoke();
            Destroy(objeto);

        }
    }

   
}

