using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
    private CharacterController m_controller;
    private Animator m_animator;

    //Modelthing
    public GameObject m_gunModel;

    //Stats
    [Header("Stats")]
    public float m_speed = 10;
    //private float m_maxTotalSpeed = 10;
    public float m_hp = 100;
    public float m_maxHp = 100;//
    public float m_maxTotalHp = 200;
    public float m_incomeDamMod = 1.0f;


    //Ranged
    [Header("Range")]
    public float m_rangeCooldown = 5;
    public float m_rangeTimer = 0;
    public List<GameObject> m_bulletPrefabs; //ADD FUNCTIONS
    public Transform m_bulletExitPos;
    public int m_bulletIndex = 0;




    //Get controller and animator
    void Start() {
        m_animator = GetComponent<Animator>();
        m_controller = GetComponent<CharacterController>();
    }

    void Awake() {

    }

    // Update is called once per frame
    void Update() {
        //Input
        //Works with both WASD and Left joystick
        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * m_speed;
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        Vector3 move = new Vector3(0, 0, 0);
        move += camForward.normalized * Input.GetAxis("Vertical") * m_speed;
        move += camRight.normalized * Input.GetAxis("Horizontal") * m_speed;

        if (move.sqrMagnitude >= m_speed * m_speed) {
            move = move.normalized * m_speed;
        }

        transform.LookAt(GetMouseToPlayerPlanePoint()); //look at mouse
 


        //Moving back or forward for animation blending EXPAND to side ways as well
        if (Vector3.Dot(transform.forward, move) < 0) {
            m_animator.SetFloat("Move", -move.magnitude / m_speed);//Backwards
        } else {
            m_animator.SetFloat("Move", move.magnitude / m_speed);
        }
        //Keep grounded
        if (!m_controller.isGrounded) {
            //fall
            move.y += Physics.gravity.y * 10 * Time.deltaTime;
        } else {
            move.y = 0;
        }

        //Move using controller
        m_controller.Move(move * Time.deltaTime);

        //Range shoot input. only will do another action if in Movement (not doing other actions) 
        if (m_animator.GetCurrentAnimatorStateInfo(1).IsName("UpperBody.Movement")) {
            if ( Input.GetMouseButtonDown(0) && m_rangeTimer >= m_rangeCooldown && m_bulletPrefabs.Count > 0 && m_bulletExitPos != null) {
               
                m_animator.SetTrigger("Shoot");        
                //CreateBullet();//Animator now calls it
                m_rangeTimer = 0;
            }
        }
        //Timers
        if (m_rangeTimer < m_rangeCooldown) {
            m_rangeTimer += Time.deltaTime;
        }

    }
    
    public void HideGun()
    {
        //DO Effect
        m_gunModel.SetActive(false);
    }
    public void ShotGun()
    {
        //DO Effect
        m_gunModel.SetActive(true);
    }
  
    //Used by animator to 'shoot' 
    public void Shoot() {

        Instantiate<GameObject>(m_bulletPrefabs[m_bulletIndex], m_bulletExitPos.transform.position, m_bulletExitPos.transform.rotation);//make transform postition the point on the gun
    }

    //Used to use mouse on board
    private Vector3 GetMouseToPlayerPlanePoint() {

        Vector3 mousePos = Input.mousePosition;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);

        Plane playersYPlane = new Plane(Vector3.up, transform.position);
        float rayDist = 0;

        playersYPlane.Raycast(mouseRay, out rayDist);

        Vector3 castPoint = mouseRay.GetPoint(rayDist);
        return castPoint;
    }
    

}
