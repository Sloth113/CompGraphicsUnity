﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IHitable
{
    void Hit();
    void Hit(int amt);
   
}

[RequireComponent(typeof(CharacterController))]
public class ZombieController : MonoBehaviour, IHitable, IActions {
    private CharacterController _controller;
    private NavMeshAgent _navAgent;
    private Animator _animator;
    public float _speed = 3;
    public int _maxHp = 3;
    public int _currentHp;
    public Transform _target;
    [Header("Attack")]
    public float _attkRange = 0.5f;
    private bool _attacking;
    private float _attkTimer = 0;
    public float _attkRate = 2;

    public List<Action> _actions;

    public SphereCollider _attkCollider;
    // Use this for initialization
    void Start () {
        _currentHp = _maxHp;
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(_target.position);

    }
	
	// Update is called once per frame
	void Update () {
        _navAgent.SetDestination(_target.position);
        //Vector3 moveDir = _target.position - transform.position;
        //transform.LookAt(_target);
        //_controller.Move(moveDir.normalized* Time.deltaTime* _speed);
        //Debug.Log(_controller.velocity.magnitude);
        _animator.SetFloat("Speed", _navAgent.velocity.magnitude/ _navAgent.speed);
        transform.LookAt(_target);
        if ((_target.position - transform.position).magnitude < _attkRange)
        {
            
            _animator.SetTrigger("Hit");
            _attkCollider.enabled = true;
            _attacking = true;
        }
        else
        {
            _attkCollider.enabled = false;
            _attacking = false;
        }
        if (_attkTimer < _attkRate)
            _attkTimer += Time.deltaTime;
        
	}

    public void Hit()
    {
        Hit(1);
    }
    public void Hit(int amt)
    {
        _currentHp -= amt;
        if(_currentHp <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        _animator.SetTrigger("Die");
        //Death particle
        //Death shader?

        Destroy(gameObject, 2);
    }

    public List<Action> GetActions()
    {
        return _actions;
    }

    public void Apply(Action act)
    {
        act.Apply(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_attacking && other.gameObject != gameObject && _attkTimer > _attkRate)
        {
            IHitable hit = other.GetComponent<IHitable>();
            if (hit != null)
            {
                hit.Hit();
                _attkTimer = 0;
            }
        }
    }
}
