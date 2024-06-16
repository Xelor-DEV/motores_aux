using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControlller : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 input;
    private Camera cam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    public void CameraMovement(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }
    private void Update()
    {
        Vector3 newPosition = transform.position + new Vector3(input.x, 0, input.y) * speed * Time.deltaTime;
        transform.position = newPosition;
    }
}
