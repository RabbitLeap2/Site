using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    public static InteractManager instance { get; set; }
    public AmmoBox hoveredAmmobox = null;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
       // Update is called once per frame
   private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if( Physics.Raycast(ray, out hit))
        {
            GameObject objectHitbyRaycast = hit.transform.gameObject;

            if (objectHitbyRaycast.GetComponent<AmmoBox>())
            {
                hoveredAmmobox = objectHitbyRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmobox.GetComponent<Outline>().enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    TrocarArma.instance.PickupAmmo(hoveredAmmobox);
                    Destroy(objectHitbyRaycast.gameObject);
                }
            }
        }
        else
        {
            if (hoveredAmmobox)
            {
                hoveredAmmobox.GetComponent<Outline>().enabled = false;
            }
        }
    }
}
