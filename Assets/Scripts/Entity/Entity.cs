using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private int life;
    
    private void heal(int n)
    {
        life += n;
    }

    private void takeDamage(int n)
    {
        life -= n;
    }
}
