using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    public float RotationSpeed;
    private Vector2 Direction;

    public float MoveSpeed;

    private void Update()
    {
        Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime);

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(cursorPos, transform.position) > 1)
            transform.position = Vector2.MoveTowards(transform.position, cursorPos, MoveSpeed * Time.deltaTime);
    }
}
