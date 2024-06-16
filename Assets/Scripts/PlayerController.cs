using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioManagerController audioManager;
    [SerializeField] private GameManagerController gameManager;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private Camera playerCamera;
    private bool isMoving;
    private NavMeshAgent agent;
    private NPCController currentNPC;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        audioManager = AudioManagerController.Instance;
        gameManager = GameManagerController.Instance;
    }
    private void Update()
    {
        if (agent.velocity.magnitude > 0)
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
        animatorPlayer.SetFloat("VelocityX", agent.velocity.x);
        animatorPlayer.SetFloat("VelocityY", agent.velocity.z);
    }
    public void Movimiento(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            MoveToPoint();
        }
    }
    private void MoveToPoint()
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            agent.destination = hit.point;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            currentNPC = other.GetComponent<NPCController>();
        }
        else if (other.tag == "Portal")
        {
            string sceneToChange = other.GetComponent<PortalController>().SceneName;
            gameManager.ChangeScene(sceneToChange);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            currentNPC = null;
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed == true && currentNPC != null && currentNPC.IsPlayerInRange == true)
        {
            InteractProcess();
        }
    }
    public void InteractProcess()
    {
        currentNPC.Interact();
    }
}
