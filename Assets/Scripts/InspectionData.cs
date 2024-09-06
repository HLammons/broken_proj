using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class InspectionData
{
    //TODO: get DateTime to serialize to JSON
    public List<DateTime> GazeTimes;
    public List<Vector3> GazePoints;

    public override string ToString()
    {
        return JsonUtility.ToJson(this);
    }
}

