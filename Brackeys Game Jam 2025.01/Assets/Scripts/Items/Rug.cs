using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Rug : MonoBehaviour
{
    [Header("Smoothing Settings")]
    [Tooltip("The amount of mouse movement required to smooth out the rug")]
    public float smoothingThreshold = 100f;

    [Tooltip("Multiplier for how fast the rug smooths based on mouse movement")]
    public float smoothingSpeed = 1f;

    private float _currentSmoothing = 0f;
    private bool _isSmoothed = false;

    private Vector3 _lastMouseWorldPos;
    private bool _wasMouseOver = false;

    private Collider2D _rugCollider;

    public void Awake()
    {
        _rugCollider = GetComponent<Collider2D>();
    }

    public void StartRugMonster() 
    {
        _isSmoothed = false;
    }

    private IEnumerator OnMouseDrag() {
        if (InputManager.Instance.isDragging && !_isSmoothed)
        {
            Vector3 mousePos = InputManager.Instance.mousePosition;
            mousePos.z = 0f;
            
            // If this is the first frame the mouse is over the rug, initialize the last position
            if (!_wasMouseOver)
            {
                _lastMouseWorldPos = mousePos;
                _wasMouseOver = true;
            }
            else
            {
                // Calculate the distance the mouse has moved
                float deltaMovement = Vector3.Distance(mousePos, _lastMouseWorldPos);

                // Increase the smoothing amount
                _currentSmoothing += deltaMovement * smoothingSpeed;

                // Update last position
                _lastMouseWorldPos = mousePos;

                // Check if the smoothing threshold has been reached
                if (_currentSmoothing >= smoothingThreshold)
                {
                    _isSmoothed = true;
                    RugSmoothed();
                }
            }
        }
        else
        {
            _wasMouseOver = false;
        }
        yield return null;
    }

    private void RugSmoothed()
    {
        _currentSmoothing = 0;
        EventManager.Instance.TriggerGhostEventExternally(GameEvent.RugMonsterRepel);
    }
}
