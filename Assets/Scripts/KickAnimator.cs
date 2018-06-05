using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickAnimator : MonoBehaviour {
    private Animator _animator;
    public KeyCode _key = KeyCode.K;
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(_key))
        {
            _animator.SetTrigger("Kick");
        }
        else
        {
            
        }
	}
}
