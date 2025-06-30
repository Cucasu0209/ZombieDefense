using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceRagdoll : MonoBehaviour
{
    [SerializeField] private float TargetRot;
    private Rigidbody2D Body;
    private Collider2D Col;
    [SerializeField] private float Force;
    private bool ToggleContraints = true;
    float timeStuck = 0;
    private void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) ToggleContraints = !ToggleContraints;
        if (ToggleContraints)
        {
            Body.MoveRotation(Mathf.LerpAngle(Body.rotation, TargetRot, Force * Time.fixedDeltaTime));

            CheckStuck();
        }
    }
    private void CheckStuck()
    {
        timeStuck += Time.deltaTime;
        if (Col.isTrigger == false && timeStuck > 3)
        {
            if (Mathf.Abs(Body.rotation - TargetRot) > 30)
            {
                timeStuck = 3;
                Col.isTrigger = true;
            }
        }
        else if (Col.isTrigger == true && timeStuck > 3.1f)
        {
            Col.isTrigger = false;
            timeStuck = 0;
        }
    }
}
