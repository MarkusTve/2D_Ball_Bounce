using System;
using UnityEngine;

public class SC_BoxSpawner : MonoBehaviour
{
    [SerializeField]
    private SCO_GameControls gameControls;

    [SerializeField]
    private GameObject boxPrefab;

    float screenHeightInWorldUnits = 0.0f;
    float screenWidthInWorldUnits = 0.0f;

    void Awake()
    {
        screenHeightInWorldUnits = Camera.main.orthographicSize;
        screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;
    }

    private void Start()
    {
        OnSpawnRowOfBoxes();
    }

    private void OnSpawnRowOfBoxes()
    {
        for (int i = 0; i < gameControls.columns * gameControls.rows; i++)
        {
            if (OnGetRandomSeed(1, 6) < 2)
                return;

            GameObject newBox = Instantiate(boxPrefab, new Vector3((-screenWidthInWorldUnits + 1) + (gameControls.spacing * (i % gameControls.columns)), (screenHeightInWorldUnits - 1) + (-gameControls.spacing * (i / gameControls.columns)), 0), Quaternion.identity);
            newBox.GetComponent<SC_Box>().HitAmount = gameControls.boxHitAmount;
        }
    }

    private int OnGetRandomSeed(int minRange, int maxRange)
    {
        return UnityEngine.Random.Range(minRange, maxRange);
    }
}
