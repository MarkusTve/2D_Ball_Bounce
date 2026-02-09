using UnityEngine;

[CreateAssetMenu(fileName = "SCO_GameControls", menuName = "Scriptable Objects/SCO_GameControls")]
public class SCO_GameControls : ScriptableObject
{
    public int ballHitAmount = 1;

    public int boxHitAmount = 3;

    public int amountOfBalls = 1;

    public int ballSpeed = 350;

    public int round = 1;

    public float launchRate = 0.6f;

    public int columns = 10;

    public int rows = 10;

    public float spacing = 1.2f; 


    public int OnIncrementRound() 
    {
        round--;
        return round;
    }


}
