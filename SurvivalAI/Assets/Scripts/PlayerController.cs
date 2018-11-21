using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private float movementSpeed;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        movementSpeed = agent.speed;
    }

    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement.sqrMagnitude > 0.1f)
            transform.rotation = Quaternion.LookRotation(movement);
        
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }
}