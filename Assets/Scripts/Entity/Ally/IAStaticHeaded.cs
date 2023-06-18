
using UnityEngine;

public class IAStaticHeaded : IAStatic
{
    public Transform head;

    protected override void Attack(Vector3 target)
    {
        base.Attack(target);
        head.LookAt(target);
    }

    public override void TakeDamage(int damage, GameObject dammager)
    {
        base.TakeDamage(damage, dammager);
    }
}
