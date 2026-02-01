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

    private int speed = 250;

    Rigidbody2D rigidBody = null;

    Collider2D collider = null;

    Vector2 currentDirection = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;

    float screenHeightInWorldUnits = 0.0f;
    float screenWidthInWorldUnits = 0.0f;

    private void Awake()
    {
        pointerMoveInputAction = inputAsset.FindAction("Point");
        pointerClickInputAction = inputAsset.FindAction("Click");

        pointerClickInputAction.performed += OnPointerActionPerformed;

        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        screenHeightInWorldUnits = Camera.main.orthographicSize;
        screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;
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

        OnBounceViewportBounds();
    }

    private void OnBounceViewportBounds()
    {
        if (this.transform.position.x >= screenWidthInWorldUnits)
        {
           currentDirection = Vector2.Reflect(rigidBody.linearVelocity.normalized, Vector2.left);
        }
        else if (this.transform.position.x <= -screenWidthInWorldUnits)
        {
            currentDirection = Vector2.Reflect(rigidBody.linearVelocity.normalized, Vector2.right);
        }
        else if (this.transform.position.y >= screenHeightInWorldUnits)
        {
            currentDirection = Vector2.Reflect(rigidBody.linearVelocity.normalized, Vector2.down);
        }
        else if (this.transform.position.y <= -screenHeightInWorldUnits)
        {
            currentDirection = Vector2.Reflect(rigidBody.linearVelocity.normalized, Vector2.up);
        }

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

        Vector2 roundedNormal = new Vector2(Mathf.RoundToInt(surfaceNormal.x), Mathf.RoundToInt(surfaceNormal.y));
        roundedNormal.Normalize();

        newDirection = Vector2.Reflect(rigidBody.linearVelocity.normalized, roundedNormal);

        currentDirection = newDirection;
    }

}
