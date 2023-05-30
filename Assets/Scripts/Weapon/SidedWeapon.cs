
using STUDENT_NAME.Entity;
using UnityEngine;

public abstract class SidedWeapon: IWeapon
{
    [Header("Weapon Side")]
    public Side side;
    public int damage;
}
