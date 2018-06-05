using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {

    public Transform _target;
    public Vector3 _offset;
    public float _speed = 5;
    public float _zoomSpeed = 100;
    private float _distFrom;
    private float _lastMouseX, _lastMouseY;
    public float _distanceMin = 0.5f;
    public float _distanceMax = 20.0f;
    // Use this lastMouseYfor initialization
    void Start () {
		if(_target != null)
        {
            _distFrom = (_target.position - transform.position).magnitude;
        }
	}
	
	// Update is called once per frame
	void Update () {

        //Dist
        _distFrom -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed * Time.deltaTime;
        _distFrom = Mathf.Clamp(_distFrom, _distanceMin, _distanceMax);

        if (Input.GetMouseButton(1))
        {
            if (_lastMouseX > 0 || _lastMouseY > 0)
            {
                // TODO - this causes weird jumps if you lose focus and regain it.
                float deltaX = Input.mousePosition.x - _lastMouseX;
                float deltaY = Input.mousePosition.y - _lastMouseY;

                // set the camera's rotation
                Vector3 angles = transform.eulerAngles + (Vector3.right * deltaY + Vector3.up * deltaX) * Time.deltaTime * _speed;
                if (angles.x > 180)
                    angles.x -= 360;
                angles.x = Mathf.Clamp(angles.x, 0, 70);
                transform.eulerAngles = angles;
            }

            _lastMouseX = Input.mousePosition.x;
            _lastMouseY = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _lastMouseX = -1;
            _lastMouseY = -1;
        }
        // position the camera looking at the target, with a height offset so we don't focus on their feet
        transform.position = _target.position - transform.forward * _distFrom + _offset;
    }
}
