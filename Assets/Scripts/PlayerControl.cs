using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour {
    private CharacterController _controller;
    private Animator _animator;
    public float _speed = 5;
    public float _jumpForce = 10;
    private float _jumpingVel = 0;
    public bool _isGrounded = false;
    private bool _jumping = false;
    private float _dbugSpeed = 0;
	// Use this for initialization
	void Start () {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        //Make 5 check
        float disCheck = 0.1f;

        _isGrounded = Physics.Raycast(transform.position + _controller.center -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + transform.forward * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + -transform.forward * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + -transform.right * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + -transform.right * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);


        float speed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        Vector3 move = new Vector3(0,0,0);
        move += camForward.normalized * Input.GetAxis("Vertical") * speed; 
        move += camRight.normalized * Input.GetAxis("Horizontal") * speed;
        
        move += Physics.gravity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Shoot");
        }
        if(_isGrounded && move.y <= 0)
        {
            move.y = 0;
            _jumping = false;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= 1.2f;
        }

        _dbugSpeed = move.magnitude / _speed;
        _animator.SetFloat("Speed", move.magnitude / _speed);
        _controller.Move(move * Time.deltaTime);

        
    }
    private void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 40, 20), _dbugSpeed.ToString());
    }
}
