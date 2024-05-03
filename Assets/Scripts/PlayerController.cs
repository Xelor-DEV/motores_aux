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
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private UIManagerController uiManager;
    private bool canInteract;
    private NPCData currentNPCData;
    private bool isMoving;
    private Vector2 _movement = Vector2.zero;
    private bool _canJump;
    private void Start()
    {
        audioManager = AudioManagerController.Instance;
    }
    private void FixedUpdate()
    {
        Vector3 moveDirection = transform.forward * _movement.y + transform.right * _movement.x;
        _compRigidbody.velocity = new Vector3(moveDirection.x * velocity, _compRigidbody.velocity.y, moveDirection.z * velocity);
        _canJump = Physics.Raycast(transform.position,Vector3.down, checkdistance, groundLayer);
        transform.Rotate(0, _movement.x * velocity, 0);
        animatorPlayer.SetFloat("VelocityX", _movement.x);
        animatorPlayer.SetFloat("VelocityY", _movement.y);

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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            canInteract = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canInteract = false;
            currentNPCData = null;
        }
    }
}
