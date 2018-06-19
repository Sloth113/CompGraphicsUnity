using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour, IHitable {
    private CharacterController _controller;
    private Animator _animator;
    public float _speed = 5;
    public float _jumpForce = 10;
    private float _jumpingVel = 0;
    public bool _isGrounded = false;
    private bool _jumping = false;
    private float _dbugSpeed = 0;
    private int _hitIndex = 1;
    private float _hitTimer = 0;
    public float _hitCooldown = 2;
    private static GameObject _attached;

    public UnityEvent _onActionChange;
    public static GameObject Attached
    {
        get
        {
            return _attached;
        }
    }
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
        Vector3 inputForward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (inputForward.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(inputForward);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + transform.eulerAngles.y, transform.eulerAngles.z);
        }

        move += camForward.normalized * Input.GetAxis("Vertical") * speed; 
        move += camRight.normalized * Input.GetAxis("Horizontal") * speed;
        
        move += Physics.gravity;

        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Shoot");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetInteger("HitNo", _hitIndex);
            Camera.main.GetComponent<BlurEffect>().HitBlur();
        }
        if (_hitIndex > 0)
        {
            if (_hitTimer > _hitCooldown)
            {
                _hitIndex = 1;
                _hitTimer = 0;
            }
            else
            {
                _hitTimer += Time.deltaTime;
            }
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
        _animator.SetFloat("Speed", inputForward.magnitude);
        _controller.Move(transform.forward * inputForward.magnitude * _speed * Time.deltaTime);

        
    }
    private void OnGUI()
    {
        GUI.TextArea(new Rect(10, 10, 40, 20), _dbugSpeed.ToString());
    }
    
    private void OnAnimatorIK(int layerIndex)
    {
        Debug.Log("HI");
        Vector3 camForward = Camera.main.transform.forward * 100;
        _animator.SetLookAtWeight(1);
        _animator.SetLookAtPosition(camForward);
    }

    public void SetHit(int i)
    {
        _hitIndex = i;
        _hitTimer = 0;
        _animator.SetInteger("HitNo", 0);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * 10;

    }

    public void Hit()
    {
        Hit(1);
    }

    public void Hit(int amt)
    {
        _animator.SetInteger("HitNo", _hitIndex);
        Camera.main.GetComponent<BlurEffect>().HitBlur();
    }
    public void SetAttached(GameObject obj)
    {
            _attached = obj;
            _onActionChange.Invoke();
        
    }
}
