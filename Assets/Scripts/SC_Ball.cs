using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class SC_Ball : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset inputAsset;

    private InputAction pointerClickInputAction;
    private InputAction pointerMoveInputAction;

    private int hitAmount = 1;

    private bool shouldMove = false;

    public int HitAmount { get { return hitAmount; } }

    private int speed = 100;

    Rigidbody2D rigidBody = null;

    Vector2 currentDirection = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;


    private void Awake()
    {
        pointerMoveInputAction = inputAsset.FindAction("Point");
        pointerClickInputAction = inputAsset.FindAction("Click");

        pointerClickInputAction.performed += OnPointerActionPerformed;

        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnPointerActionPerformed(InputAction.CallbackContext context)
    {
        if (shouldMove)
            return;

        currentDirection = mousePosition - (Vector2)transform.position;
        currentDirection.Normalize();

        shouldMove = true;
    }

    private void FixedUpdate()
    {
        if (!shouldMove)
            return;

        rigidBody.linearVelocity = speed * Time.fixedDeltaTime * currentDirection;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(pointerMoveInputAction.ReadValue<Vector2>());

        Debug.DrawLine(this.transform.position, mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Box"))
            return;

        OnBounce(other);
    }

    private void OnBounce(Collider2D objectToBounceOff)
    {
        Vector2 newDirection;

        Vector2 surfaceNormal = Vector2.zero;

        surfaceNormal = objectToBounceOff.ClosestPoint(this.transform.position) - (Vector2)objectToBounceOff.bounds.center;

        surfaceNormal.Normalize();

        // Change so that Vector2.down is a vector that chages depending on what side of the square was hit
        newDirection = Vector2.Reflect(rigidBody.linearVelocity.normalized, Vector2.down);

        currentDirection = newDirection;

    }
}
