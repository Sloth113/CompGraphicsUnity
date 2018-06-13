using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectAttach : MonoBehaviour {
    public LineRenderer _line;
    private GameObject _obj;
    private Vector3 _point; // on object
    public EventSystem _eventSys;
    public float _connectTime = 2;
    public float _currentTime = 0;
    public Transform _connect;


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0) && _eventSys.IsPointerOverGameObject() == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                _obj = hit.collider.gameObject;
                _point = hit.point - _obj.transform.position;
                _currentTime = 0;
                _line.gameObject.SetActive(true);
            }
        }
        if (_obj)
        {

            _currentTime = _currentTime > _connectTime ? _connectTime :_currentTime + Time.deltaTime;
            _line.SetPosition(0, _connect.position);
            _line.SetPosition(1, Vector3.Lerp(_connect.position, _obj.transform.position + _point, _currentTime/_connectTime));
        }



    }
}
