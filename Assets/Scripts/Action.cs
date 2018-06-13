using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Action : ScriptableObject{
    public Image _sprite;
    public string _name;
    public string _description;
    public abstract void Apply(GameObject go);
}
