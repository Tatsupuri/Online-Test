using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Gun : MonoBehaviourPunCallbacks
{   
    public bool sniperMode;

    [SerializeField]
    [Range(0.5f,1.5f)]
    private  float fireRate = 1;

    [SerializeField]
    [Range(1,10)]
    private int damage = 1;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private ParticleSystem muzzleParticle;

    [SerializeField]
    private AudioSource gunFireSource;

    private Animator animator;

    private float timer;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
     if(!photonView.IsMine) return;
     timer += Time.deltaTime;
     if(Input.GetButton("Fire1"))
     {
        animator.SetBool("Aim",true);
         if(timer >= fireRate)
         {
            if(Input.GetButton("Fire2"))
            {
            timer = 0f;
            animator.SetTrigger("Shoot");
            //Debug.Log("shoot");
            FireGun();
            animator.SetTrigger("EndFire");
            }

        }
     } else{
         animator.SetBool("Aim",false);
     }  
    }

    private void FireGun()
    {
       Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f); 
       
       muzzleParticle.Play();
       gunFireSource.Play();

       Ray ray = new Ray(firePoint.position, firePoint.forward);
       RaycastHit hitInfo;

       if(Physics.Raycast(ray, out hitInfo, 100))
       {
           //Destroy(hitInfo.collider.gameObject);
           var health = hitInfo.collider.GetComponent<Health>();
           if(health != null)//敵以外のobjectはhealthを持っていないのでなにも起きないようにする
           {
               health.TakeDamage(damage);
           }
       }
    }
}
