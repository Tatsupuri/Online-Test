using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class PlayerMovement2 : MonoBehaviourPunCallbacks
{
    //private CharacterController characterController;
    private Animator animator;
    private NavMeshAgent characterController;

    [SerializeField]
    private GameObject visionCamera;

    [SerializeField]
    private float cameraZoom = 15f;

    [SerializeField]
    [Range(1f,3.99f)]
    private float sniperZoom = 2f;

    [SerializeField]
    private float zoomSteps = 5f;

    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float turnSpeed = 1f;

    [SerializeField]
    private float quickTurnSpeed = 5f;

    private float currentTurnSpeed;

    //public GameObject Scoop;
    public GameObject SniperMode;
    
    GameObject player;
    Gun gun;

    Camera myCam;

    private float zoom;
    private float initialFOV;

    //private bool scoop;

    void Awake()
    {
        //characterController = GetComponent<CharacterController>();
        characterController = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        player = GameObject.Find("Player");
        //gun = player.GetComponent<Gun>();

        myCam = visionCamera.GetComponent<Camera>();
        initialFOV = myCam.fieldOfView;
        
        if(photonView.IsMine){
            visionCamera.SetActive(true);
        }

        //scoop = gun.sniperMode;
        //Scoop.SetActive(false);
        //SniperMode.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {   
        if(!photonView.IsMine) return;
        //Debug.Log(scoop);
        //scoop 
        //bool scoop = gun.sniperMode;
        //var scoopDir =  new Vector3(0.15f/zoomSteps,-0.101f/zoomSteps,2.311f/zoomSteps);
        //Debug.Log(scoopDir);
        var myAxis =  new Vector3(0,0,1);
        var horizontalPotision = Input.GetAxis("Horizontal");
        var verticalPotision = Input.GetAxis("Vertical"); 
        var horizontalRotation = Input.GetAxis("HorizontalRot");
        var verticalRotation = Input.GetAxis("VerticalRot"); 
        var changeCoordinate = new Quaternion();

        //Debug
        // var juuji = Input.GetAxis("Sniper");
        // Debug.Log(juuji);

        // if(Input.GetAxis("Sniper")>0 && !Input.GetButton("Fire1")){
        //     scoop = true;
        // }else if(Input.GetAxis("Sniper")<0 && !Input.GetButton("Fire1")){
        //      scoop = false;
        // }

        // if(scoop == false)
        // {
        //     zoom = cameraZoom;
        //     SniperMode.SetActive(false);
        // }else{
        //     zoom = cameraZoom * sniperZoom;
        //     SniperMode.SetActive(true);
        // }

        //ベクトルの規格化（座標変換）
        changeCoordinate.SetFromToRotation(myAxis, transform.forward);


        //movementは入力装置から指定されたベクトル
        var inputMove = new Vector3(horizontalPotision,0,verticalPotision);
        var inputRot = new Vector3(horizontalRotation,0,0);
        //var inputRot = new Vector3(horizontalRotation,0,verticalRotation);
        var movment = changeCoordinate * inputMove;
        var rot = changeCoordinate * inputRot;
        
        //Debug.Log(horizontalRotation);
        //Debug.Log(rot.magnitude);

        animator.SetFloat("Speed",movment.magnitude);

        if(rot.magnitude >0)
            {
                Quaternion newDirection = Quaternion.LookRotation(rot);

                //if(Input.GetButton("Quick Turn"))
                //{
                //    currentTurnSpeed = quickTurnSpeed;
                //}else{
                    currentTurnSpeed = turnSpeed;
                //}
                transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * currentTurnSpeed);
            }

        if(!Input.GetButton("Fire1"))
            {   //Scoop.SetActive(false);
                if(myCam.fieldOfView < initialFOV)
                {
                    myCam.fieldOfView += zoom/zoomSteps;
                    //if(scoop == true){
                    //    visionCamera.transform.position -= changeCoordinate * scoopDir; 
                    //}
                }
                characterController.Move(movment*Time.deltaTime * moveSpeed);
        }else{
            
            if(myCam.fieldOfView > initialFOV - zoom)
            {
               myCam.fieldOfView -= zoom/zoomSteps;
            //    if(scoop == true){
            //            visionCamera.transform.position += changeCoordinate * scoopDir;
            //             if(myCam.fieldOfView < initialFOV - zoom/2){
            //                Scoop.SetActive(true);
            //            }
            //        }
            }
        }
    }
}
