using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable]
public class Inspection
{
    public int id;
    public string name;
    public DateTime date;
    public string data;
    public int inspector_id;
    public int? reference_inspection_id;
    public float score;
    public InspectionData inspectionData;

    public Inspection()
    {
        date = DateTime.Now;
        inspectionData = new InspectionData() { GazePoints = new List<Vector3>(), GazeTimes = new List<DateTime>() };
    }

    public void AddPoint(Vector3 position)
    {
        inspectionData.GazeTimes.Add(DateTime.Now);
        inspectionData.GazePoints.Add(position);
    }
}
