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

    public GameObject GetClickedObject() {

        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

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
        PrimaryAction.action.performed += _ => { StartCoroutine(OnMouseDrag()); };
        PrimaryAction.action.canceled += _ => { isDragging = false; };
    }

    private IEnumerator OnMouseDrag()
    {
        isDragging = true;
        while (isDragging && GetClickedObject()!=null)
        {
            GetClickedObject().transform.position = camera.ScreenToWorldPoint(mousePosition) + new Vector3(0,0,10);
            yield return 0;
        }

    }
}
