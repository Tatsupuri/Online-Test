using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
   
   public GameObject DeathText;

    void Start()
    {
        DeathText.SetActive(false);
        Debug.Log("Zombie Trigger is ready");      
    }

    private void OnTriggerEnter(Collider collider){
        Debug.Log("Zombie Trigger is on");
        if(collider.gameObject.tag =="Player"){
            DeathText.SetActive(true);
            Time.timeScale = 0;
        }

    }

}
