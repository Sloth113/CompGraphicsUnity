using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IActions
{
    List<Action> GetActions();
    void Apply(Action act);
}

public abstract class Action : ScriptableObject{
    public Sprite _sprite;
    public string _name;
    public string _description;
    public abstract void Apply(GameObject go);
}
