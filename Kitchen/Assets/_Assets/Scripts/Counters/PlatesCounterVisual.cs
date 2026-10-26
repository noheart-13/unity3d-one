using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlatesCounterVisual : MonoBehaviour
{

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform PlateVisualPrefab;

    private List<GameObject> platesVisualCounterList;

    private void Awake()
    {
        platesVisualCounterList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnRemovedPlate += PlatesCounter_OnRemovedPlate;
    }

    private void PlatesCounter_OnRemovedPlate(object sender, System.EventArgs e)
    {
        GameObject plateVisualGameObject = platesVisualCounterList[platesVisualCounterList.Count - 1];
        platesVisualCounterList.Remove(plateVisualGameObject);
        Destroy(plateVisualGameObject);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(PlateVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * platesVisualCounterList.Count, 0f);

        platesVisualCounterList.Add(plateVisualTransform.gameObject);
    }

}
