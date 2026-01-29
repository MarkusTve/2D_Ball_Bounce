using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class SC_Ball : MonoBehaviour
{
    [SerializeField]
    private InputActionReference playerInputActionReference;

    private int hitAmount = 1;

    private bool shouldMove = false;

    public int HitAmount { get { return hitAmount; } }

    private int speed = 100;

    Rigidbody2D rigidBody = null;

    private void Awake()
    {
        playerInputActionReference.action.performed += OnActionPerformed;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!shouldMove)
            return;

        rigidBody.linearVelocity = speed * Time.fixedDeltaTime * Vector2.up;
    }
    private void OnActionPerformed(InputAction.CallbackContext obj)
    {
        if (shouldMove)
            return;

        shouldMove = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Box"))
            return;

        OnBounce(other);
    }

    private void OnBounce(Collider2D objectToBounceOff)
    {
        Vector2 currentVelocity = rigidBody.linearVelocity;
    }
}
