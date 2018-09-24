﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2d;

    // Movement
    [Header("Movement")]
    private int direction = 270;
    private float moveInputHorizontal = 0;
    private float moveInputVertical = 0;
    private Vector2 moveVelocity;

    [SerializeField]
    [Range(1f, 10f)]
    private float walkSpeed = 5f;

    // Dash
    private bool dashInput = false;

    [SerializeField]
    [Range(1f, 10f)]
    private float dashSpeed = 10f;

    [SerializeField]
    [Range(.1f, 2f)]
    private float dashStartDelay = 1f;
    private float dashStartTimer = 0;

    void Start () {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Move();
        Dash();
    }

    void Update () {
        Direction();

        moveInputHorizontal = Input.GetAxis("Horizontal");
        moveInputVertical = Input.GetAxis("Vertical");

        dashInput = Input.GetButton("Dash");
    }

    void DebugOutput() {
        GUI.Label(new Rect(50, 10, 300, 20), "Movement direction: " + direction);
        GUI.Label(new Rect(50, 25, 300, 20), "Movement horizontal: " + moveInputHorizontal);
        GUI.Label(new Rect(50, 40, 300, 20), "Movement vertical: " + moveInputVertical);
        GUI.Label(new Rect(50, 55, 300, 20), "Dash: " + dashInput);
        GUI.Label(new Rect(50, 70, 300, 20), "Dash start timer: " + dashStartTimer);
    }

    void OnGUI() {
        DebugOutput();
    }

    private void Direction()
    {
        // Player facing down
        if (moveInputVertical < 0) {
            direction = 270;
        }
        // Player facing up
        else if (moveInputVertical > 0) {
            direction = 90;
        }
        // Player facing left
        else if (moveInputHorizontal < 0) {
            direction = 0;
        }
        // Player facing right
        else if (moveInputHorizontal > 0) {
            direction = 180;
        }
    }

    private void Move() {
        Vector2 moveInput = new Vector2(moveInputHorizontal, moveInputVertical);
        moveVelocity = moveInput.normalized * walkSpeed;

        rigid2d.MovePosition(rigid2d.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Dash() {
        if (dashInput == true) {

            if (dashStartTimer >= dashStartDelay) {

                if (direction == 270) {
                    moveVelocity = Vector2.down * dashSpeed;
                }
                else if (direction == 90) {
                    moveVelocity = Vector2.up * dashSpeed;
                }
                else if (direction == 0) {
                    moveVelocity = Vector2.left * dashSpeed;
                }
                else if (direction == 180) {
                    moveVelocity = Vector2.right * dashSpeed;
                }

                rigid2d.MovePosition(rigid2d.position + moveVelocity * Time.fixedDeltaTime);
            }
            else {
                dashStartTimer += Time.fixedDeltaTime;
            }
        }
        else {
            dashStartTimer = 0;
        }
    }
}
