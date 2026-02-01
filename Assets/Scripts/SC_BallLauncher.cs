using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SC_BallLauncher : MonoBehaviour
{
    [SerializeField]
    private SCO_GameControls gameControls;

    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private InputActionAsset inputAsset;
    private InputAction pointerClickInputAction;
    private InputAction pointerMoveInputAction;

    Vector2 ballDirection = Vector2.zero;
    Vector2 mousePosition = Vector2.zero;

    private bool shouldMove = false;


    private void Awake()
    {
        pointerMoveInputAction = inputAsset.FindAction("Point");
        pointerClickInputAction = inputAsset.FindAction("Click");

        pointerClickInputAction.performed += OnPointerActionPerformed;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(pointerMoveInputAction.ReadValue<Vector2>());

        Debug.DrawLine(this.transform.position, mousePosition);
    }

    private void OnPointerActionPerformed(InputAction.CallbackContext context)
    {
        if (shouldMove)
            return;

        StartCoroutine(IInstanciateBalls());

        ballDirection = mousePosition - (Vector2)transform.position;
        ballDirection.Normalize();

        shouldMove = true;
    }
    IEnumerator IInstanciateBalls()
    {
        for (int i = 0; i < gameControls.amountOfBalls; i++)
        {
            GameObject ball =
            Instantiate(ballPrefab, new Vector3(0, -4, 0), Quaternion.identity);

            ball.GetComponent<SC_Ball>().Launch(ballDirection, gameControls.ballSpeed);


            yield return new WaitForSeconds(.6f);   
        }
    }


}
