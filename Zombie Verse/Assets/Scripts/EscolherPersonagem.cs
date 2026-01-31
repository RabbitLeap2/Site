using UnityEngine;

public class EscolherPersonagem : MonoBehaviour
{
    //Mudar para lista depois
    public GameObject pesonagemAtual;
    public GameObject Zero;
    public GameObject Witness;
    private GameObject personagemNovo;
    public Transform posicao;

    public GameObject PersonagemMenu;
    private bool PerActive;
    public void MenuPersShow()
    {
        PerActive = !PerActive;
        PersonagemMenu.SetActive(PerActive);
        Cursor.lockState = PerActive ? CursorLockMode.Confined : CursorLockMode.Locked;
    }

   
    public void Personagem(int personagem)
    {
        switch (personagem)
        {
            case 1:
                Destroy(pesonagemAtual);
                personagemNovo = Instantiate(Zero,posicao.position,posicao.rotation);
                PersonagemMenu.SetActive(false);
                pesonagemAtual = personagemNovo;
                break;
            case 2:
                Destroy(pesonagemAtual);
                personagemNovo = Instantiate(Witness, posicao.position, posicao.rotation);
                PersonagemMenu.SetActive(false);
                pesonagemAtual = personagemNovo;
                break;
        }
    }
}
