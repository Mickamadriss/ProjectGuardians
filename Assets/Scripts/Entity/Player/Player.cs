using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private float m_TranslationSpeed = 5f;
    [SerializeField] private float m_RotationSpeed = 100f;
    [SerializeField] private CharacterController m_CharacterController = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
