using System;
using UnityEngine;
public class ItemGameObjectPlacer: Item
{
    [Header("Settings")]
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private Transform usePoint;
    [SerializeField] private float maxDistance;
    //TODO Faire comme les armes et afficher un truc dans les mains genre un marteau de construction

    [Header("Keybinds")]
    [SerializeField] private KeyCode placeKey = KeyCode.Mouse0;
    
    

    private void Update()
    {
        if (Input.GetKeyDown(placeKey))
        {
            RaycastHit objectHit;
            if (Physics.Raycast(usePoint.transform.position, usePoint.transform.forward, out objectHit, maxDistance))
            {
                Transform turretTransform = usePoint.transform;
                Quaternion normalRot = Quaternion.LookRotation(objectHit.normal);
                Quaternion x90degressRot = Quaternion.Euler(90,0, 0); 
                Quaternion turretAimDirection = Quaternion.Euler(0, turretTransform.rotation.eulerAngles.y, 0);
                Instantiate(turretPrefab, objectHit.point, normalRot * x90degressRot * turretAimDirection);
            }
        }
    }
}
