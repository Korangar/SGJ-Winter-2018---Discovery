using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
    public Controller input;
    public float movementSpeed = 1;
    public Color myColor;
    private Transform cam;
    public Animator animator;
    public SpriteRenderer cooldownProgress;

    public float cooldown = 20;
    public UnityEvent OnCooldownOver;

    public float cameraTilt = 10;
    public float tiltSmooth = 3;

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
        cam = transform.Find("Main Camera");
        signal = GameObject.Find("SignalSystem").GetComponent<ParticleSystem>();
        TrailRenderer trail = transform.Find("GoalTrail").GetComponent<TrailRenderer>();
        trail.startColor = myColor;
        trail.endColor = myColor;

        cooldownProgress.color = myColor;
        cooldownProgress.material.SetFloat("_Progress", -Mathf.PI);
    }

    void Update()
    {
        if (cool > 0)
        {
            cool -= Time.deltaTime;
            cooldownProgress.material.SetFloat("_Progress", Mathf.PI * 2 * (cool / cooldown - 0.5F));
            if (cool <= 0)
            {
                cool = 0;
                OnCooldownOver.Invoke();
                cooldownProgress.enabled = false;
            }
        }

        float moveX = Input.GetAxis("Horizontal " + ((int) input + 1));
        float moveZ = Input.GetAxis("Vertical " + ((int) input + 1));
        Vector3 move = new Vector3(moveX, 0, moveZ);
        agent.Move(move * movementSpeed * Time.deltaTime);

        bool moving = move.sqrMagnitude > 0;
        animator.SetBool("Running", moving);
        if (moving)
            animator.transform.rotation = Quaternion.LookRotation(move);

        if (Input.GetButtonDown("Signal " + ((int)input + 1)))
            ShootSignal();
    }

    private void FixedUpdate()
    {

        float moveX = Input.GetAxis("Horizontal " + ((int)input + 1));
        float moveZ = Input.GetAxis("Vertical " + ((int)input + 1));
        cam.rotation = Quaternion.Slerp(cam.rotation, Quaternion.Euler(moveZ * -cameraTilt, 0, moveX * cameraTilt) * Quaternion.Euler(90 * Vector3.right), tiltSmooth);
    }

    public void ShootSignal()
    {
        if (cool > 0)
            return;

        ParticleSystem.EmitParams par = new ParticleSystem.EmitParams();
        par.position = transform.position;
        par.startColor = myColor;
        signal.Emit(par, 1);
        cool = cooldown;
        cooldownProgress.enabled = true;
        cooldownProgress.material.SetFloat("_Progress", -Mathf.PI);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
            other.GetComponent<VictoryController>().MarkFinished(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
            other.GetComponent<VictoryController>().MarkNotFinished(this);
    }
}
