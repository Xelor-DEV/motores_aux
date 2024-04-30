using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _compRigidbody;
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private float checkdistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioManagerController audioManager;
    private bool isMoving;
    private Vector2 _movement;
    private bool _canJump;
    private void Start()
    {
        audioManager = AudioManagerController.Instance;
    }
    private void FixedUpdate()
    {
        _compRigidbody.velocity = new Vector3(_movement.x * velocity, _compRigidbody.velocity.y , _movement.y * velocity);
        _canJump = Physics.Raycast(transform.position,Vector3.down, checkdistance, groundLayer);

    }
    public void Movimiento(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>() * velocity;

    }

    private void Update()
    {
        if (_movement.x != 0 || _movement.y != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving == true && audioManager.SfxAudioSource.isPlaying == false)
        {
            audioManager.SfxAudioSource.clip = audioManager.SfxClips[0];
            audioManager.SfxAudioSource.Play();
        }
        else if (isMoving == false && audioManager.SfxAudioSource.isPlaying == true)
        {
            audioManager.SfxAudioSource.Stop();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_canJump)
            {
                _compRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }
}
