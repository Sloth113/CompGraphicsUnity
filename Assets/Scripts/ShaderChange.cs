using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ShaderAction")]
public class ShaderChange : Action {
    public Material _newMat;
    public Material _defaultMat;
    public bool _applied;
    public override void Apply(GameObject go)
    {
        if (!_applied)
        {
            _defaultMat = go.GetComponent<MeshRenderer>().material;
            go.GetComponent<MeshRenderer>().material = _newMat;
            _applied = true;
        }
        else
        {
            go.GetComponent<MeshRenderer>().material = _defaultMat;
        }
    }
}
