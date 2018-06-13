using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustFootSteps : MonoBehaviour {
    public Transform _lftFoot;
    public Transform _rhtFoot;
    public GameObject _particlePre;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StepL()
    {
        GameObject obj = Instantiate<GameObject>(_particlePre, _lftFoot.position, _lftFoot.rotation);
        Destroy(obj, 1);
    }
    void StepR()
    {
        GameObject obj = Instantiate<GameObject>(_particlePre, _rhtFoot.position, _rhtFoot.rotation);
        Destroy(obj, 1);
    }
}
