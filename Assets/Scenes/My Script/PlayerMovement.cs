using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    //private CharacterController characterController;
    private Animator animator;
    private NavMeshAgent characterController;

    [SerializeField]
    private float moveSpeed = 100;

    [SerializeField]
    private float turnSpeed = 5f;

    [SerializeField]
    private float quickTurnSpeed = 5f;

    private float currentTurnSpeed;
    
    void Awake()
    {
        //characterController = GetComponent<CharacterController>();
        characterController = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {    
        if(!photonView.IsMine) return;

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical"); 

        //movementは入力装置から指定されたベクトル
        var movment = new Vector3(horizontal,0,vertical);

        //magnitudeはコントローラの押し込み具合のパラメーター
        //これによってidleとrunが連続的に繋がる
        animator.SetFloat("Speed",movment.magnitude);

        if(!Input.GetButton("Fire1"))
            {
                //characterController.SimpleMove(movment*Time.deltaTime * moveSpeed);
                characterController.Move(movment*Time.deltaTime * moveSpeed);
        
            if(movment.magnitude >0)
            {
                //四元数で回転を指定する．LookRotarionはobjectのrotationをそっちの方向に回転させるという関数
                //Quaternion newDirection = Quaternion.LookRotation(movment);
                //その向きを新しい向きに設定することでそっちを向く．
                //transform.rotation = newDirection;

                //スムーズに動かすために以下をする.コンローラの入力がないときは何もしない,そうしないと向きが戻ってしまう
                //Slerpは二つの引数の間を補完して四元数を返す．
                Quaternion newDirection = Quaternion.LookRotation(movment);

                //if(vertical < 0 && Input.GetButton("Quick Turn"))
                //if(Input.GetButton("Quick Turn"))
                //{
                //    currentTurnSpeed = quickTurnSpeed;
                //}else{
                    currentTurnSpeed = turnSpeed;
                //}
                transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * currentTurnSpeed);

                //if(vertical < 0 && Input.GetButton("Turn")){}
            }
        }
    }
}
