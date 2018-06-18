using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Actions/Rigid Toggle")]
public class RigidBodyToogle : Action {
    public override void Apply(GameObject go)
    {
        go.GetComponent<Rigidbody>().isKinematic = !go.GetComponent<Rigidbody>().isKinematic;
    }
}
