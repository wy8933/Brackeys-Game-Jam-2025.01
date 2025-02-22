using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject log;

    private void OnMouseDown()
    {
        if (log != null)
        {
            Vector3 spawnPosition = new Vector3(4, -2.5f, 0);
            GameObject gameObject = Instantiate(log, spawnPosition, Quaternion.identity);
            InputManager.Instance.selectedObject = gameObject;
        }
    }
}
