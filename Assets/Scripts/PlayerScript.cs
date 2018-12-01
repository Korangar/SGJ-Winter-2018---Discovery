using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    public Controller input;
    public float movementSpeed = 1;
    public Color myColor;

    public float cooldown = 20;
    public UnityEvent OnCooldownOver;

    private NavMeshAgent agent;
    private ParticleSystem signal;

    private float cool;

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
        TrailRenderer trail = transform.Find("GoalTrail").GetComponent<TrailRenderer>();
        trail.startColor = myColor;
        trail.endColor = myColor;
    }

    void Update()
    {
        if (cool > 0)
        {
            cool -= Time.deltaTime;
            if (cool <= 0)
            {
                cool = 0;
                OnCooldownOver.Invoke();
            }
        }

        float moveX = Input.GetAxis("Horizontal " + ((int) input + 1));
        float moveZ = Input.GetAxis("Vertical " + ((int) input + 1));
        agent.Move(new Vector3(moveX, 0, moveZ) * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Signal " + ((int)input + 1)))
            ShootSignal();
    }

    public void ShootSignal()
    {
        if (cool > 0)
            return;

        ParticleSystem.EmitParams par = new ParticleSystem.EmitParams();
        par.position = transform.position;
        par.startColor = myColor;
        signal.Emit(par, 10);
        cool = cooldown;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            FindObjectOfType<GoalCameraAnimation>().StartAnimation();
        }
    }
}
