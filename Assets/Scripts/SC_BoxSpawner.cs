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

    Vector2[] boxPositions = new Vector2[100];

    void Awake()
    {
        screenHeightInWorldUnits = Camera.main.orthographicSize;
        screenWidthInWorldUnits = screenHeightInWorldUnits * Camera.main.aspect;

        OnCreateBoxPositions();
        OnSpawnRowOfBoxes();    
    }

    private void OnSpawnRowOfBoxes()
    {
        foreach (var position in boxPositions) 
        {
            Instantiate(boxPrefab, position, Quaternion.identity);
        }
    }

    private void OnCreateBoxPositions()
    {
        for (int x = 0; x < gameControls.columns; x++)
        {
            for (int y = 0; y < gameControls.rows; y++) 
            {
                boxPositions.SetValue(new Vector2(x, y), x);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
