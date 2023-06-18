
using UnityEngine;

public class IAStaticHeaded : IAStatic
{
    public Transform head;

    protected override void Attack(Vector3 target)
    {
        base.Attack(target);
        head.LookAt(target);
    }
}
