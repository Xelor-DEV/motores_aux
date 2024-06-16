using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class NPCController : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private UIManagerController uiManager;
    private int currentPointIndex = 0;
    private NavMeshAgent agent;
    private bool isMoving = true;
    [SerializeField] private Animator animatorNPC;
    [SerializeField] private NPCData data;
    private bool isInteracting = false;
    private bool isPlayerInRange = false;
    private int savedPointIndex = -1;
    public bool IsPlayerInRange
    {
        get 
        { 
            return isPlayerInRange; 
        }
    }
    public NPCData Data
    {
        get
        {
            return data;
        }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Start()
    {
        uiManager = UIManagerController.Instance;
        agent.speed = data.Speed;
        agent.acceleration = agent.speed;
        if (data.PatrollingOnStart == true && patrolPoints.Length > 0)
        {
            MoveToNextPatrolPoint();
        }
        else
        {
            isMoving = false;
            agent.isStopped = true;
        }
    }
    void Update()
    {
        if (isMoving == true && isInteracting == false && patrolPoints.Length > 0)
        {
            // remainingDistance se refiere a la distancia que le falta recorrer al agente para llegar a su destino actual
            // pathPending sirve para saber si el agente esta buscando el camino a su destino
            if (agent.pathPending == false && agent.remainingDistance < 0.1f)
            {
                StartCoroutine(PauseAtPatrolPoint());
            }
        }
        animatorNPC.SetFloat("VelX", agent.velocity.x);
        animatorNPC.SetFloat("VelY", agent.velocity.z);
    }
    private void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
        {
            isMoving = false;
        }
        else
        {
            agent.destination = patrolPoints[currentPointIndex].position;
            isMoving = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
        }
    }
    private IEnumerator ShowDialogue()
    {
        uiManager.ShowDialogueInCanvas(data.NpcName, data.Message);
        yield return new WaitForSeconds(uiManager.DialogueDuration);
        uiManager.Dialogue.SetActive(false);
        isInteracting = false;
        if (isPlayerInRange == false && patrolPoints.Length > 0)
        {
            ResumeMovement();
        }
    }
    private void ResumeMovement()
    {
        if (savedPointIndex != -1)
        {
            currentPointIndex = savedPointIndex;
            savedPointIndex = -1;
        }
        isMoving = true;
        agent.isStopped = false;
        MoveToNextPatrolPoint();
    }
    public void Interact()
    {
        if (isInteracting == false)
        {
            savedPointIndex = currentPointIndex;
            isInteracting = true;
            agent.isStopped = true;
            animatorNPC.SetFloat("VelX", 0);
            animatorNPC.SetFloat("VelY", 0);
            StartCoroutine(ShowDialogue());
        }
    }
    private IEnumerator PauseAtPatrolPoint()
    {
        isMoving = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(data.WaitTime);
        if (agent.remainingDistance < 0.1f && isInteracting == false && isPlayerInRange == false)
        {
            if (currentPointIndex >= patrolPoints.Length - 1)
            {
                currentPointIndex = 0;
            }
            else
            {
                currentPointIndex = currentPointIndex + 1;
            }
        }
        if (isInteracting == false && isPlayerInRange == false)
        {
            ResumeMovement();
        }
    }
}
