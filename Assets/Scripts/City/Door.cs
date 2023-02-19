using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform enterPosition;
    [SerializeField] private Transform exitPosition;

    private void OnTriggerEnter(Collider other)
    {
        //si le gameobject est de la class player -> faut peut-être créé une interface pour les entités jouables
        if (other.GetComponent<Player>() != null)
        {
            //à modifier si possible psk je pense pas que ce sois optimal, voir si séparer en deux sous objet est mieux
            //si le joueur est plus proche de la porte d'entrée, il sort sinon il rentre 
            if (Vector3.Distance(other.transform.position, enterPosition.position) < Vector3.Distance(other.transform.position, exitPosition.position))
            {
                other.transform.position = exitPosition.position;
            }
            else
            {
                other.transform.position = enterPosition.position;
            }
        }
    }
}
