using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] 
    private float _movementSpeed;
    [SerializeField] 
    private InputActionReference _moveActionReference;

    [Header("Boost Settings")]
    [SerializeField] 
    private float _boostSpeed;
    [SerializeField]
    private float _xpConsumptionRate = 10f;
    [SerializeField] 
    private InputActionReference _boostActionReference;

    private Animator animator;
    private const string ANIMATOR_BOOL_IS_MOVING = "IsMoving";

    private void Start()
    {
        animator = GetComponent<Animator>();

        _moveActionReference.action.Enable();
        _boostActionReference.action.Enable();
    }

    private void Update()
    {
        float boost = 1;

        // check if boost is activated and player has enough XP
        if (_boostActionReference.action.phase == InputActionPhase.Performed && GameManager.Instance.PlayerXP > 0)
        {
            boost = _boostSpeed;
            GameManager.Instance.ConsumeXP(_xpConsumptionRate); // reduce XP while boosting
        }

        // read movement inputs
        Vector2 frameMovement = _moveActionReference.action.ReadValue<Vector2>();
        Vector3 frameMovement3D = new Vector3(frameMovement.x, 0, frameMovement.y);

        // character movement
        Vector3 newPos = transform.position + frameMovement3D * _movementSpeed * boost * Time.deltaTime;
        transform.position = newPos;

        // calculate direction and rotation
        if (frameMovement3D.sqrMagnitude > 0.01f) // check for a significant movement
        {
            Quaternion targetRotation = Quaternion.LookRotation(frameMovement3D.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // set is moving bool
        bool isMoving = frameMovement3D.sqrMagnitude > 0.01f;
        animator.SetBool(ANIMATOR_BOOL_IS_MOVING, isMoving);
    }

}
