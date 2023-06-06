using System.Collections;
using System.Collections.Generic;
using STUDENT_NAME.Entity;
using UnityEngine;

public interface IDamageable
{
    public Side getSide();
    public virtual void TakeDamage(int damage, GameObject dammager) { }
}
