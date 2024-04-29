using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Floor Control

public enum EnvironmentType
{
    Heat,
    Cold
}

public class FloorSystem : HungrySingleton<FloorSystem>
{
    [SerializeField]private EnvironmentType currentType;
    private GameObject[] _heatFloor;
    private GameObject[] _coldFloor;

    private void Start()
    {
        _heatFloor = GameObject.FindGameObjectsWithTag("HeatFloor");
        _coldFloor = GameObject.FindGameObjectsWithTag("ColdFloor");
        OnSetEnvironment(currentType);
    }
    
    public void OnHeatFloorClear() => OnFloorClear(_heatFloor);
    
    public void OnColdFloorClear() => OnFloorClear(_coldFloor);

    public void OnHeatFloorAppear() => OnFloorAppear(_heatFloor);
    
    public void OnColdFloorAppear() => OnFloorAppear(_coldFloor);

    public void OnSetEnvironment(EnvironmentType environmentType)
    {
        if (environmentType == EnvironmentType.Heat)
        {
            OnChangeHeatEnvironment();
        }
        else
        {
            OnChangeColdEnvironment();
        }
    }

    public void OnChangeEnvironment()
    {
        if (currentType == EnvironmentType.Heat)
        {
            OnChangeColdEnvironment();
            currentType = EnvironmentType.Cold;
        }
        else
        {
            OnChangeHeatEnvironment();
            currentType = EnvironmentType.Heat;
        }
    }

    public void OnChangeHeatEnvironment()
    {
        currentType = EnvironmentType.Heat;
        OnChangeEnvironment(_heatFloor, _coldFloor);
    }

    public void OnChangeColdEnvironment()
    {
        currentType = EnvironmentType.Cold;
        OnChangeEnvironment(_coldFloor, _heatFloor);
    }

    private void OnChangeEnvironment(GameObject[] activefloors,GameObject[] unactivefloors)
    {
        foreach (var floor in activefloors)
        {
            floor.SetActive(true);
        }

        foreach (var floor in unactivefloors)
        {
            floor.SetActive(false);
        }
    }
    
    private void OnFloorClear(GameObject[] unactivefloors)
    {
        foreach (var floor in unactivefloors)
        {
            floor.SetActive(false);
        }
    }
    
    private void OnFloorAppear(GameObject[] activefloors)
    {
        foreach (var floor in activefloors)
        {
            floor.SetActive(true);
        }
    }
}
