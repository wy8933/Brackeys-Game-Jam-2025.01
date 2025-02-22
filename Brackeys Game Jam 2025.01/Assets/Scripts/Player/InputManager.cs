using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public InputActionReference PrimaryAction;
    public InputActionReference MousePositionAction;
    public InputActionReference PauseAction;

    public bool isDragging;
    public Vector3 mousePosition;
    public Camera camera;
    
    public GameObject selectedObject;

    public void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject GetClickedObject() {

        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(mousePosition), Vector2.zero);
        
        if (hit) 
        {
            return hit.collider.gameObject;
        }
        return null;
        
    }

    private void OnEnable()
    {
        PrimaryAction.action.Enable();
        MousePositionAction.action.Enable();
        PauseAction.action.Enable();

        MousePositionAction.action.performed += OnMouseMove;
        PrimaryAction.action.performed += OnPrimaryAction;
        PrimaryAction.action.canceled += OnPrimaryActionCanceled;
        PauseAction.action.performed += OnPauseAction;
    }

    private void OnDisable()
    {
        MousePositionAction.action.performed -= OnMouseMove;
        PrimaryAction.action.performed -= OnPrimaryAction;
        PrimaryAction.action.canceled -= OnPrimaryActionCanceled;
        PauseAction.action.performed -= OnPauseAction;
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
    private void OnPrimaryAction(InputAction.CallbackContext context)
    {
        selectedObject = GetClickedObject();
        
        if (selectedObject)
        {
            if (selectedObject.CompareTag("Curtain"))
            {
                Curtain curtain = selectedObject.GetComponent<Curtain>();
                if (curtain)
                {
                    curtain.SetCover();
                    return;
                }
            }

            if (selectedObject.CompareTag("TVAntenna"))
            {
                TVAntenna antenna = selectedObject.GetComponent<TVAntenna>();
                if (antenna)
                {
                    antenna.ChangeAntennaPosition();
                    return;
                }
            }
            isDragging = true;
        }
    }
    private void OnPrimaryActionCanceled(InputAction.CallbackContext context)
    {
        isDragging = false;
        selectedObject = null;
    }
    private void OnPauseAction(InputAction.CallbackContext context)
    {
        if (PauseMenu.Instance.isPaused)
            PauseMenu.Instance.Resume();
        else
        {
            isDragging = false;
            selectedObject = null;
            PauseMenu.Instance.Pause();
        }
    }
    private void Update()
    {
        if (isDragging && selectedObject && !selectedObject.CompareTag("UnMoveable") && !PauseMenu.Instance.isPaused)
        {
            Vector3 targetPosition = camera.ScreenToWorldPoint(mousePosition);
            targetPosition.z = 0;
            selectedObject.transform.position = targetPosition;
        }
    }
}
