using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( menuName = "Actions/Hit")]
public class Hit : Action {
    public int _amount = 1;
    public override void Apply(GameObject go)
    {
        IHitable targ = go.GetComponent<IHitable>();
        if (targ != null)
            targ.Hit(_amount);
    }
}
