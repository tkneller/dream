using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    // Movement
    private float moveInputHorizontal = 0;
    private float moveInputVertical = 0;
    [Header("Movement")]
    [SerializeField]
    [Range(1f, 10f)]
    private float moveSpeed = 5f;
    private bool isMoving = false;

    // Dash
    private bool dashInput = false;
    [SerializeField]
    [Range(1f, 10f)]
    private float dashSpeed = 10f;
    [SerializeField]
    [Range(0, 2f)]
    private float dashStartDelay = 1f;
    private bool isDashing = false;
        
    void Start () {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        isMoving = controller.Move(moveInputHorizontal, moveInputVertical, moveSpeed);
        isDashing = controller.Dash(dashInput, dashStartDelay, dashSpeed);
    }

    void Update () {
        moveInputHorizontal = Input.GetAxis("Horizontal");
        moveInputVertical = Input.GetAxis("Vertical");
        dashInput = Input.GetButton("Dash");

        // Disable directional movement while dashing
        if (dashInput == true) {
            moveInputHorizontal = 0;
            moveInputVertical = 0;
        }

        Animation();
    }

    /* DebugOutput
     * 
     * Prints debug information on screen.
     */
    void DebugOutput() {
        GUI.Label(new Rect(50, 10, 300, 20), "Movement direction: " + controller.GetDirection());
        GUI.Label(new Rect(50, 25, 300, 20), "Movement horizontal: " + moveInputHorizontal);
        GUI.Label(new Rect(50, 40, 300, 20), "Movement vertical: " + moveInputVertical);
        GUI.Label(new Rect(50, 55, 300, 20), "isMoving: " + isMoving);
        GUI.Label(new Rect(50, 70, 300, 20), "Dash Input: " + dashInput);
        GUI.Label(new Rect(50, 85, 300, 20), "isDashing: " + isDashing);
    }

    /* OnGui
     * 
     * Displays the GUI
     */
    void OnGUI() {
        DebugOutput();
    }

    private void Animation() {
        animator.SetInteger("Direction", controller.GetDirection());
        animator.SetBool("isRunning", isMoving);
    }
}
