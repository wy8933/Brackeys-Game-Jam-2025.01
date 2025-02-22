using UnityEngine;

[RequireComponent(typeof(Collider2D))] // or Collider if using 3D
public class RuleBook : MonoBehaviour
{
    public GameObject rulebookDisplay;

    private bool isOpen = false;

    private void Start()
    {
        if (rulebookDisplay != null)
        {
            rulebookDisplay.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (!isOpen)
        {
            OpenRulebook();
        }
    }

    private void Update()
    {
        if (isOpen && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null || hit.collider.gameObject != gameObject)
            {
                CloseRulebook();
            }
        }
    }

    private void OpenRulebook()
    {
        if (rulebookDisplay != null)
        {
            rulebookDisplay.SetActive(true);
            isOpen = true;
        }
    }

    private void CloseRulebook()
    {
        if (rulebookDisplay != null)
        {
            rulebookDisplay.SetActive(false);
            isOpen = false;
        }
    }
}
