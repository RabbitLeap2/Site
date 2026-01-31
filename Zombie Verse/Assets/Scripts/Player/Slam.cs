using UnityEngine;

public class Slam : MonoBehaviour
{
    public int Damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Zombie z = other.GetComponent<Zombie>();
            if (z != null && !z.isDead)
            {
                z.TakeDamage(Damage);
            }
        }
    }




}
