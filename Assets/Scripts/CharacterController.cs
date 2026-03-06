using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public Animator anim;

    [Header("Animator Parameters")]
    public string moveParameter = "Speed_f";
    public string crouchParameter = "Crouch_b";
    public string weaponTypeParameter = "WeaponType_int";
    [Header("Weapons")]
    public GameObject handgun;

    [Header("Movement")]
    public float moveSpeed = 5f;

    private InputAction moveAction;
    private InputAction crouchAction;
    private InputAction useWeaponAction;
    private Vector2 moveInput;
    private bool isCrouching;
    private bool isUsingHandgun;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        useWeaponAction = InputSystem.actions.FindAction("UseWeapon");
    }

    void Update()
    {
        if (moveAction != null)
        {
            moveInput = moveAction.ReadValue<Vector2>();
        }
        else
        {
            moveInput = Vector2.zero;
        }

        if (crouchAction != null)
        {
            isCrouching = crouchAction.ReadValue<float>() > 0.5f;
        }
        else
        {
            isCrouching = false;
        }

        // Move the character horizontally based on input.x
        if (!isCrouching)
        {
            Vector3 delta = new Vector3(0f, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
            transform.Translate(delta);
        }

        Vector3 rotation = new Vector3(0f, moveInput.x * 100f * Time.deltaTime, 0f);
        transform.Rotate(rotation);

        if (useWeaponAction.triggered)
        {
            isUsingHandgun = !isUsingHandgun;
        }

        // Update animator parameters
        if (anim != null)
        {
            float speed = Mathf.Abs(moveInput.y);
        }
    }
}
