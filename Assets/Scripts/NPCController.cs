using System.Collections;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;
    private int currentPointIndex = 0;
    private Rigidbody _compRigidbody;
    private bool isMoving = true;
    [SerializeField] private Animator animatorNPC;
    [SerializeField] private NPCData data;
    void Start()
    {
        _compRigidbody = GetComponent<Rigidbody>();
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position + new Vector3(0.1f, 0, 0);
            transform.LookAt(patrolPoints[currentPointIndex]);
        }
    }
    void FixedUpdate()
    {
        if (patrolPoints.Length != 0 && isMoving == true)
        {
            Transform targetPoint = patrolPoints[currentPointIndex];
            Vector3 directionToTarget = (targetPoint.position - transform.position).normalized;
            _compRigidbody.velocity = directionToTarget * data.Speed;
            transform.rotation = Quaternion.LookRotation(directionToTarget);
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                if (currentPointIndex + 1 < patrolPoints.Length)
                {
                    currentPointIndex = currentPointIndex + 1;
                }
                else
                {
                    currentPointIndex = 0;
                }
                _compRigidbody.velocity = Vector3.zero;
                StartCoroutine(PauseAtPatrolPoint());
            }
            animatorNPC.SetFloat("VelX", _compRigidbody.velocity.x);
            animatorNPC.SetFloat("VelY", _compRigidbody.velocity.z);
        }
    }

    IEnumerator PauseAtPatrolPoint()
    {
        isMoving = false;
        yield return new WaitForSeconds(data.WaitTime);
        isMoving = true;
    }

    public void StopMovement()
    {
        isMoving = false;
        _compRigidbody.velocity = Vector3.zero;
        animatorNPC.SetFloat("VelX", 0);
        animatorNPC.SetFloat("VelY", 0);
    }

    public void ResumeMovement()
    {
        isMoving = true;
    }
}
