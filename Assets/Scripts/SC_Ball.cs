using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class SC_Ball : MonoBehaviour
{
    private bool shouldMove = false;

    private int speed = 350;

    Rigidbody2D rigidBody = null;

    float screenHeightInWorldUnits = 0.0f;
    float screenWidthInWorldUnits = 0.0f;

    private Vector2 currentDirection = Vector2.zero;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        screenHeightInWorldUnits = Camera.main.orthographicSize;
        screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;
    }

    private void FixedUpdate()
    {
        if (!shouldMove)
            return;

        rigidBody.linearVelocity = speed * Time.fixedDeltaTime * currentDirection;
    }

    private void Update()
    {
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
            Destroy(this.gameObject);
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

    public void Launch( Vector2 newDirection, int newSpeed)
    {
        speed = newSpeed;
        currentDirection = newDirection;
        shouldMove = true;
    }

}
