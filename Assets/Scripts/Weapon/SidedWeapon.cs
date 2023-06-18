
using STUDENT_NAME.Entity;
using System;
using UnityEngine;

public abstract class SidedWeapon: IWeapon
{
    [Header("Weapon Side")]
    public Side side;
    public int damage;
    public int price;
    public String attackSound;
}
