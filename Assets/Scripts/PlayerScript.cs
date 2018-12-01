using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public Controller input;
    public float movementSpeed = 1;
    public Color myColor;

    private NavMeshAgent agent;
    private ParticleSystem signal;

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
        signal = GameObject.Find("SignalSystem").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal " + ((int) input + 1));
        float moveZ = Input.GetAxis("Vertical " + ((int) input + 1));
        agent.Move(new Vector3(moveX, 0, moveZ) * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Signal " + ((int)input + 1)))
            ShootSignal();
    }

    public void ShootSignal()
    {
        ParticleSystem.EmitParams par = new ParticleSystem.EmitParams();
        par.position = transform.position;
        par.startColor = myColor;
        signal.Emit(par, 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            ShootSignal();
            Debug.Log(gameObject.name + " Finished the game!");
        }
    }
}
