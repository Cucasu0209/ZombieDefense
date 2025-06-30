using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JumpRagdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody2D Hips;
    [SerializeField] private float Force;
    [SerializeField] private float Veloc;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) Hips.velocity = new Vector2(Hips.velocity.x, Force);
        if (Input.GetKey(KeyCode.D)) Hips.velocity = new Vector2(Veloc, Hips.velocity.y);
        if (Input.GetKeyUp(KeyCode.D)) Hips.velocity = new Vector2(0, Hips.velocity.y);
        if (Input.GetKey(KeyCode.A)) Hips.velocity = new Vector2(-Veloc, Hips.velocity.y);
        if (Input.GetKeyUp(KeyCode.A)) Hips.velocity = new Vector2(0, Hips.velocity.y);

    }
}
