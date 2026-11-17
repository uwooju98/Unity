using UnityEngine;
using System;

[Serializable]
public class MapSettings
{
    public float erodePercent = 0.3f;
    public int erodeIterations = 2;

    public int castlesCount = 1;
    public int townsCount = 3;

    public float lakePercent = 0.1f;
    public float treePercent = 0.1f;
    public float hillsPercent = 0.1f;
    public float mountainsPercent = 0.1f;
    public float monsterPercent = 0.1f;
}

