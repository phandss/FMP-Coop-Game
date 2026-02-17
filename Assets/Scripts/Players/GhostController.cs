using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    
    private RectTransform cursor;

    [SerializeField] private float speed = 5;

    private Vector2 cursorInput;
    private Vector2 velocity;
    private Vector2 screenBounds;
    
    

    private void Awake()
    {
        cursor = GetComponentInChildren<RectTransform>();


    }
    private void Start()
    {
        screenBounds = new Vector2(Screen.width, Screen.height);
    }


    private void Navigate(InputAction.CallbackContext context)
    {
        cursorInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector2 pos = cursor.position;

        pos += cursorInput * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, 0, screenBounds.x);
        pos.y = Mathf.Clamp(pos.y, 0, screenBounds.y);

        cursor.position = pos;
    }
}
