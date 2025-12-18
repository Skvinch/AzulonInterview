using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody; // Reference to the Rigidbody2D used for movement
    [SerializeField] private Animator _animator; // Reference to the Animator controlling player animations
    [SerializeField] private Transform _playerVisual; // Visual root of the player (used for flipping left/right)
    [SerializeField] private float _moveSpeed; // Base movement speed
    [SerializeField] private float _moveLimiter = 0.7f; // Movement multiplier used when moving diagonally
    
    private Vector2 _input; // Stores player input direction
    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking"); // Cached animator parameter hash for performance

    private void Update()
    {
        // Read player input every frame
        ReadInput();

        // Update animation parameters based on input
        UpdateAnimation();

        // Flip player visual depending on movement direction
        UpdateVisualDirection();
    }

    private void FixedUpdate() => Move();

    // Reads raw horizontal and vertical input
    private void ReadInput()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
    }

    // Updates walking animation state
    private void UpdateAnimation()
    {
        _animator.SetBool(IsWalkingHash, _input != Vector2.zero);
    }

    // Flips the player sprite when moving left or right
    private void UpdateVisualDirection()
    {
        if (_input.x == 0) return;

        _playerVisual.rotation = _input.x > 0
            ? Quaternion.identity
            : Quaternion.Euler(0f, 180f, 0f);
    }

    // Applies movement to the Rigidbody2D
    private void Move()
    {
        Vector2 movement = _input;

        // Reduce speed when moving diagonally to keep consistent movement
        if (movement.x != 0 && movement.y != 0)
        {
            movement *= _moveLimiter;
        }

        // Apply velocity based on input and movement speed
        _rigidbody.linearVelocity = movement * _moveSpeed;
    }
}
