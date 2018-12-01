using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public Controller input;
    public float movementSpeed = 1;

    private NavMeshAgent agent;

    public enum Controller
    {
        Controller1,
        Controller2,
        Controller3,
        Controller4
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal " + ((int) input + 1));
        float moveZ = Input.GetAxis("Vertical " + ((int) input + 1));

        /*
        if (moveX != 0)
            Debug.Log(gameObject.name + " Input X: " + moveX);

        if (moveZ != 0)
            Debug.Log(gameObject.name + "  Input Z: " + moveZ);
        */

        //agent.destination = transform.position + new Vector3(moveX, 0, moveZ) * movementSpeed;
        agent.Move(new Vector3(moveX, 0, moveZ) * movementSpeed * Time.deltaTime);
    }
}
