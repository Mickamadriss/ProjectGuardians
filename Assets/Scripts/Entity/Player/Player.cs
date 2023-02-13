using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEventHandler
{

    [SerializeField] private float m_TranslationSpeed = 5f;
    [SerializeField] private float m_RotationSpeed = 100f;
    [SerializeField] private CharacterController m_CharacterController = null;
    private bool m_IsPlaying = false;

    private void Awake()
    {
        SubscribeEvents();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsPlaying)
        {
            return;
        }
        Mouvement();
    }

    void onDestroy()
    {
        UnsubscribeEvents();
    }

    #region Event's subscription

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
    }

    #endregion

    #region Event's callback

    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        m_IsPlaying = true;
    }

    #endregion

    private void Mouvement()
    {
        float hInput, vInput, mouseX;
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxisRaw("Mouse X");

        m_CharacterController.Move(transform.forward * m_TranslationSpeed * vInput * Time.fixedDeltaTime);
        m_CharacterController.Move(transform.right * m_TranslationSpeed * hInput * Time.fixedDeltaTime);
        transform.Rotate(Vector3.up * m_RotationSpeed * mouseX * Time.fixedDeltaTime);
    }
}