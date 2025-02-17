using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Vector3 mousePosition;


    private Vector3 GetMousePostion() {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
