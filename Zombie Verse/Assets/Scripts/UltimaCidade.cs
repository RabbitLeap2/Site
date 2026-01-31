using UnityEngine;
using UnityEngine.Events;
public class UltimaCidade : MonoBehaviour
{
    public Transform[] WaypointsTransform;
    public Transform Object;

    public int CurrentWaypointIndex;

    public Vector3 CurrentWaypointPosition;

    public float Speed;
   //----------------------------------------------
    public bool EstaDentro;

    public bool temEsperar;
    public bool SemJogador;
    //----------------------------------------------
    public GameObject portao;
    public GameObject vazio;
    public bool jaEntrou;
    public bool jaEntrou2;
    public bool jaEntrou3;
    public float novaVelocidade; 

    void Start()
    {
        jaEntrou = false;
        jaEntrou2 = false;
        jaEntrou3 = false;
        SemJogador = false;
        CurrentWaypointIndex = 0;
        CurrentWaypointPosition = WaypointsTransform[CurrentWaypointIndex].position;
    }

    private void ObjectMovement()
    {
        if(Vector3.Distance(Object.transform.position, CurrentWaypointPosition) < 0.02f)
        {
            CurrentWaypointIndex++;
            CurrentWaypointPosition = WaypointsTransform[CurrentWaypointIndex].position;
        }
        Object.transform.position = Vector3.MoveTowards(Object.transform.position, CurrentWaypointPosition, Speed * Time.deltaTime);
    }

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
    
    public void abriuPortao()
    {
        Destroy(portao);
        temEsperar = false;
        Objetivos.instance.ProximoObjetivo(vazio, vazio);
    }
    void Update()
    {
        if ((EstaDentro && temEsperar == false) || SemJogador) { 
        ObjectMovement();
        }
        if(CurrentWaypointIndex == 1 && jaEntrou == false)
        {
            temEsperar = true;
            Objetivos.instance.primeiroObjetivo();
            jaEntrou = true;
        }
    
        if (CurrentWaypointIndex == 3 && jaEntrou2 == false)
        {
            jaEntrou2 = true;
            SemJogador = true;
            Speed = novaVelocidade;
            Objetivos.instance.segundoObjetivo();
        }
        if (CurrentWaypointIndex == 5 && jaEntrou3 == false)
        {
            jaEntrou3 = true;
            temEsperar = true;
            Objetivos.instance.terceiroObjetivo();
        }
    }
}
