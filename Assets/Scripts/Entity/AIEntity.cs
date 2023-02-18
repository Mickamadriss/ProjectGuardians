using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public abstract class AIEntity : MovableEntity, IEnnemy
{
    [SerializeField] public Transform destination;
}
