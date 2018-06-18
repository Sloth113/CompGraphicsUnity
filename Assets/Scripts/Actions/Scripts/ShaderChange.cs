﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Actions/ShaderAction")]
public class ShaderChange : Action {
    public Material _newMat;
    private Material _defaultMat;
    private bool _applied;
    public override void Apply(GameObject go)
    {
        if (!_applied)
        {
            if(_defaultMat == null)
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