using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionContainer : MonoBehaviour, IActions{

    public List<Action> _actions;

    public void Apply(Action act)
    {
        act.Apply(gameObject);
    }

    public List<Action> GetActions()
    {
        return _actions;
    }
}
