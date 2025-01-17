using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private InputActionReference moveActionReference;
    [SerializeField] private InputActionReference boostActionReference;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        moveActionReference.action.Enable();
        boostActionReference.action.Enable();
    }

    private void Update()
    {
        float boost = 1;

        // Vérifie si le boost est activé
        if (boostActionReference.action.phase == InputActionPhase.Performed)
        {
            boost = boostSpeed;
        }

        // Lecture des inputs de mouvement
        Vector2 frameMovement = moveActionReference.action.ReadValue<Vector2>();
        Vector3 frameMovement3D = new Vector3(frameMovement.x, 0, frameMovement.y);

        // Déplacement du personnage
        Vector3 newPos = transform.position + frameMovement3D * movementSpeed * boost * Time.deltaTime;
        transform.position = newPos;

        // Calcul de la direction et rotation
        if (frameMovement3D.sqrMagnitude > 0.01f) // Vérifie un mouvement significatif
        {
            Quaternion targetRotation = Quaternion.LookRotation(frameMovement3D.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // Gestion du booléen "IsMoving" dans l'Animator
        bool isMoving = frameMovement3D.sqrMagnitude > 0.01f; // True si le personnage se déplace
        animator.SetBool("IsMoving", isMoving);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
