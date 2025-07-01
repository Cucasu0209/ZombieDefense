using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public int Length;
    public LineRenderer LineRend;
    public Vector3[] SegmentPoses;
    private Vector3[] SegmentV;

    public Transform TargetDir;
    public float TargetDist;
    public float SmoothSpeed;
    public float TrailSpeed;

    [Header("Wiggle")]
    public float WiggleSpeed;
    public float WiggleMagnitude;
    public Transform WiggleDir;

    private void Start()
    {
        LineRend.positionCount = Length;
        SegmentPoses = new Vector3[Length];
        SegmentV = new Vector3[Length];
    }
    private void Update()
    {

        WiggleDir.localRotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * WiggleSpeed) * WiggleMagnitude);

        SegmentPoses[0] = TargetDir.position;
        for (int i = 1; i < SegmentPoses.Length; i++)
        {
            Vector3 targetpos = SegmentPoses[i - 1] + (SegmentPoses[i] - SegmentPoses[i - 1] + TargetDir.right).normalized * TargetDist;
            SegmentPoses[i] = Vector3.SmoothDamp(SegmentPoses[i], targetpos, ref SegmentV[i], SmoothSpeed);
        }
        LineRend.SetPositions(SegmentPoses);
    }
}