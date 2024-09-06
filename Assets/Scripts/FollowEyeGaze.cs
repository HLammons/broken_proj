using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class FollowEyeGaze : MonoBehaviour
{

    public float defaultGazeDistance = 2.0f;
    private int layerMask = 31; //layer 31 is SpatialAwareness default layer

    private LineRenderer lineRenderer;
    public bool showLineRenderer = true;

    public int currentInspection = 0;
    public List<Inspection> inspections = new List<Inspection>();
    private bool inspecting = false;

    public TextMeshPro text;
    public GameObject gazeObject;


    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Spatial Awareness");
        lineRenderer = GetComponent<LineRenderer>();
        //inspections.Add(new Inspection());
    }

    // Update is called once per frame
    void Update()
    {
        if (inspecting)
        {
            Vector3 gazePosition = GetGazePosition(defaultGazeDistance);
            inspections[currentInspection].AddPoint(gazePosition);

            //Visual things
            if (gazeObject)
                gazeObject.transform.position = gazePosition;
            UpdateLineRenderer();

            if (text)
            {
                int numPoints = inspections[currentInspection].inspectionData.GazePoints.Count;
                float time = Time.realtimeSinceStartup;
                text.text = string.Format("Points:{0}\nDuration:{1:#.0}\nRate:{2:#.0}points/s", numPoints, time, numPoints / time);
            }
        }
    }

    public void UpdateLineRenderer()
    {
        if (showLineRenderer)
        {
            lineRenderer.positionCount = inspections[currentInspection].inspectionData.GazePoints.Count;
            lineRenderer.SetPositions(inspections[currentInspection].inspectionData.GazePoints.ToArray());
        }
        else
            lineRenderer.positionCount = 0;
    }

    public void StopInspection()
    {
        if (!inspecting)
            print("No inspection to stop");
        else
        {
            //Upload to DB
            StartCoroutine(DataBaseAPIController.InsertInspection(inspections[currentInspection]));
            inspecting = false;
        }
    }

    public void StartInspection(string name, Inspection baseInspection = null)
    {
        if (inspecting)
            print("Already inspecting!");
        else
        {
            inspections.Add(new Inspection() { name = name, inspector_id = DataBaseAPIController.activeInspector });
            currentInspection = inspections.Count - 1;
            if (baseInspection != null)
                inspections[currentInspection].reference_inspection_id = baseInspection.id;

            inspecting = true;
        }
    }

    public Vector3 GetGazePosition(float defaultDistance)
    {
        Vector3 gazeOrigin = CoreServices.InputSystem.EyeGazeProvider.GazeOrigin;
        Vector3 gazeDirection = CoreServices.InputSystem.EyeGazeProvider.GazeDirection;
        RaycastHit hit;

        if (Physics.Raycast(gazeOrigin, gazeDirection, out hit, float.PositiveInfinity, layerMask))
            return hit.point;
        else // If no target is hit, return the position at a default distance.
            return gazeOrigin + gazeDirection * defaultDistance;
    }


}
