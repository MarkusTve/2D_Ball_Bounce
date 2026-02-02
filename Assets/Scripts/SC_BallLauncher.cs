using System;
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

    private Vector2 ballDirection = Vector2.zero;
    private Vector2 mousePosition = Vector2.zero;

    private float launchRateTimer = 0.0f;

    private bool isLaunching = false;
    private int launchCount = 0;

    private void Awake()
    {
        pointerMoveInputAction = inputAsset.FindAction("Point");
        pointerClickInputAction = inputAsset.FindAction("Click");

        pointerClickInputAction.performed += OnPointerActionPerformed;
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(pointerMoveInputAction.ReadValue<Vector2>());

        if (!isLaunching)
            return;

        OnLaunchBall();
    }

    private void OnLaunchBall()
    {
        if (launchCount >= gameControls.amountOfBalls)
        {
            isLaunching = false;
            return;
        }

        launchRateTimer += Time.deltaTime;

        if (launchRateTimer <= gameControls.launchRate)
            return;

        GameObject newBall = Instantiate(ballPrefab, new Vector3(0, -4, 0), Quaternion.identity);
        newBall.GetComponent<SC_Ball>().Launch(ballDirection, gameControls.ballSpeed);

        launchCount++;
        launchRateTimer = 0.0f;
    }

    private void OnPointerActionPerformed(InputAction.CallbackContext context)
    {
        SetBallTrajectory((mousePosition - new Vector2(0, -4)).normalized);
        isLaunching = true;
    }

    private void SetBallTrajectory(Vector2 newTrajectory)
    {
        ballDirection = newTrajectory;
    }

}
