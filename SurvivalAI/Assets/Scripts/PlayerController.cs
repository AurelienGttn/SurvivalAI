using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
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