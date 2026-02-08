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
        OnCreateBoxPositions();
        OnSpawnRowOfBoxes();
    }

    private void OnSpawnRowOfBoxes()
    {
        for (int i = 0; i < gameControls.columns * gameControls.rows; i++)
        {
            Instantiate(boxPrefab, new Vector3((-screenWidthInWorldUnits + 1) + (screenWidthInWorldUnits / (gameControls.columns) * (i % gameControls.columns)), (screenHeightInWorldUnits - 1)+ (-1.2f * (i / gameControls.columns)), 0), Quaternion.identity);
        }
    }

    private void OnCreateBoxPositions()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


}
