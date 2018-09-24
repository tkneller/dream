using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2d;

    [Header("Movement")]
    [SerializeField]
    [Range (1f, 10f)]
    private float walkSpeed = 5f;
    private float moveInputHorizontal;
    private float moveInputVertical;

    void Start () {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Move();
    }

    void Update () {
        moveInputHorizontal = Input.GetAxis("Horizontal");
        moveInputVertical = Input.GetAxis("Vertical");
    }

    void DebugOutput()
    {
        GUI.Label(new Rect(50, 10, 300, 20), "Horizontal Input: " + moveInputHorizontal);
        GUI.Label(new Rect(50, 25, 300, 20), "Vertical Input: " + moveInputVertical);
    }

    void OnGUI()
    {
        DebugOutput();
    }

    private void Move() {
        Vector2 moveInput = new Vector2(moveInputHorizontal, moveInputVertical);
        Vector2 moveVelocity = moveInput.normalized * walkSpeed;

        rigid2d.MovePosition(rigid2d.position + moveVelocity * Time.fixedDeltaTime);
    }
}
