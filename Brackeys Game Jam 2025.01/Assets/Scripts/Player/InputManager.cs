using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public InputActionReference PrimaryAction;
    public InputActionReference MousePositionAction;

    public bool isDragging;
    public Vector3 mousePosition;
    public Camera camera;
    
    private GameObject selectedObject;

    public GameObject GetClickedObject() {

        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(mousePosition), Vector2.zero);
        
        if (hit) 
        {
            return hit.collider.gameObject;
        }
        return null;
        
    }

    private void Awake()
    {
        PrimaryAction.action.Enable();
        MousePositionAction.action.Enable();

        MousePositionAction.action.performed += context => { mousePosition = context.ReadValue<Vector2>(); };
        PrimaryAction.action.performed += _ => {         
            selectedObject = GetClickedObject();
            if (selectedObject != null)
            {
                isDragging = true;
            } };
        PrimaryAction.action.canceled += _ => { isDragging = false; selectedObject = null; };
    }
    
    private void Update()
    {
        if (isDragging && selectedObject)
        {
            Vector3 targetPosition = camera.ScreenToWorldPoint(mousePosition);
            targetPosition.z = 0;
            selectedObject.transform.position = targetPosition;
        }
    }
}
