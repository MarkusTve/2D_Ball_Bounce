using UnityEngine;

[CreateAssetMenu(fileName = "SCO_GameControls", menuName = "Scriptable Objects/SCO_GameControls")]
public class SCO_GameControls : ScriptableObject
{
    public int ballHitAmount = 1;

    public int amountOfBalls = 1;

    public int ballSpeed = 350;

    public int round = 1;
}
