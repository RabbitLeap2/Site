using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) {
            CreateBulletImpactEffect(collision);
            print("hit " + collision.gameObject.name + " !");
        Destroy(gameObject);

        }
    

   
        if (collision.gameObject.CompareTag("Wall"))
        {
            CreateBulletImpactEffect(collision);
            print("hit a Wall!!");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Beer"))
        {
          
            print("hit a beer bottle!!");
            collision.gameObject.GetComponent<BeerBottle>().Shatter();
        }
        if (collision.gameObject.CompareTag("Zombie"))
        {

            print("hit a Zombie!!");
            if(collision.gameObject.GetComponent<Zombie>().isDead == false)
            {
                collision.gameObject.GetComponent<Zombie>().TakeDamage(bulletDamage);
            }
            
            CreateBloodSprayEffect(collision);
            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayEffect(Collision ObjectWeHit)
    {
        ContactPoint contact = ObjectWeHit.contacts[0];

        GameObject bloodEffectPrefab = Instantiate(GlobalReferences.instance.BloodEffect, contact.point, Quaternion.LookRotation(contact.normal));

        bloodEffectPrefab.transform.SetParent(ObjectWeHit.gameObject.transform);
    }

    void CreateBulletImpactEffect(Collision ObjectWeHit)
    {
        ContactPoint contact = ObjectWeHit.contacts[0];

        GameObject hole = Instantiate(GlobalReferences.instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

        hole.transform.SetParent(ObjectWeHit.gameObject.transform);
    }
}
