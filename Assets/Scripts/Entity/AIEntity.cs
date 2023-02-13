using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public abstract class AIEntity : MovableEntity
{
    [SerializeField] MovementBehaviour movementBehaviour;
    [SerializeField] AggroBehaviour aggroBehaviour;

}
