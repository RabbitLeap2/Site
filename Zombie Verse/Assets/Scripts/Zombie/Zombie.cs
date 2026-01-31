using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;

    private NavMeshAgent navAgent;

    public bool isDead;
    public bool JaContou;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        JaContou = false;

    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if(HP <= 0)
        {
            if (!JaContou)
            {
                Score.instance.ZombiesMortos++;
                JaContou = true;
            }
            int randomValue = Random.Range(0, 2);
            if(randomValue == 0)
            {
                animator.SetTrigger("DIE1");

            }
            else
            {
                animator.SetTrigger("DIE2");

            }
            isDead = true;
            SoundManager.instance.ZombieSound2.PlayOneShot(SoundManager.instance.zombieDeath);
        }
        else
        {
            animator.SetTrigger("DAMAGE");
     
            SoundManager.instance.ZombieSound2.PlayOneShot(SoundManager.instance.zombieHurt);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.5f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 18f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f);

    }
}
