using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravityMultiplier = 1f;
    public bool gameOver = false;

    public ParticleSystem fxDirt;

    public GameObject fxExplosionPrefab;

    public Animator animator;

    public AudioClip sfxCrash;

    public AudioSource audioSource;

    private Rigidbody rb;
    private InputAction jumpAction;

    private bool isOnGround = true;

    void Awake()
    {
        animator = GetComponent<Animator>();    

        rb = GetComponent<Rigidbody>();
        jumpAction = InputSystem.actions.FindAction("Jump");

        audioSource = GetComponent<AudioSource>();
        if (audioSource)
        {
        }
    }

    void Start()
    {
        Physics.gravity *= gravityMultiplier;

        if (animator != null)
        {
            animator.SetFloat("Speed_f", 1.0f);
        }
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        if (jumpAction.triggered && isOnGround)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            isOnGround = false;
            animator.SetTrigger("Jump_trig");
            fxDirt.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            fxDirt.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;

            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);

            Instantiate(fxExplosionPrefab,
                transform.position,
                Quaternion.identity
            );

            audioSource.PlayOneShot(sfxCrash);
        }
    }
}