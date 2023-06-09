using System;
using SDD.Events;
using UnityEngine;
using UnityEngine.AI;
using Event = SDD.Events.Event;

public class ItemGameObjectPlacer: Item, IEventHandler
{
    [Header("Turret")]
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private int turretCost;
    
    [Header("Item")]
    [SerializeField] private GameObject tool;
    [SerializeField] private Transform usePoint;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Keybinds")]
    [SerializeField] private KeyCode placeKey = KeyCode.Mouse0;

    private int lastPlayerGold = 0;
    
    private void Awake()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void OnEnable()
    {
        tool.SetActive(true);
    }

    private void OnDisable()
    {
        tool.SetActive(false);
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayerGoldChanged>(playerGoldChanged);
    }

    public void UnsubscribeEvents()
    { 
        EventManager.Instance.RemoveListener<PlayerGoldChanged>(playerGoldChanged);
    }
        
    private void Update()
    {
        if (!Input.GetKeyDown(placeKey)) return;
        
        RaycastHit objectHit;
        if (lastPlayerGold < turretCost || !Physics.Raycast(usePoint.transform.position, usePoint.transform.forward,
                out objectHit, maxDistance, whatIsGround)) return;
        
        Transform turretTransform = usePoint.transform;
        Quaternion normalRot = Quaternion.LookRotation(objectHit.normal);
        Quaternion x90degressRot = Quaternion.Euler(90,0, 0); 
        Quaternion turretAimDirection = Quaternion.Euler(0, turretTransform.rotation.eulerAngles.y, 0);
        Instantiate(turretPrefab, objectHit.point, normalRot * x90degressRot * turretAimDirection);
        
        EventManager.Instance.Raise(new PlayerGoldUpdate(-turretCost));
    }
    
    private void playerGoldChanged(PlayerGoldChanged e)
    {
        lastPlayerGold = (int) e.eGold;
    }

}
