using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterControllerTopDown controller;
    private Animator animator;

    // Movement
    private float moveInputHorizontal = 0;
    private float moveInputVertical = 0;
    [Header("Movement")]
    [SerializeField]
    [Range(1f, 10f)]
    private float moveSpeed = 5f;
    private bool isMoving = false;

    // Run
    private bool runInput = false;
    [SerializeField]
    [Range(1f, 10f)]
    private float runSpeed = 10f;
    [SerializeField]
    [Range(0, 2f)]
    private float runStartDelay = 1f;
    private bool isRunning = false;

    // Dash
    private bool dashInput = false;
    [SerializeField]
    [Range(1f, 20f)]
    private float dashSpeed = 10f;
    [SerializeField]
    [Range(0, 1f)]
    private float dashTime = 0.2f;
    private bool isDashing = false;

    /* Start
     *
     * Initializes the neccesary components
     */
    void Start () {
        controller = GetComponent<CharacterControllerTopDown>();
        animator = GetComponent<Animator>();
    }

    /* Fixed Update
     */
    void FixedUpdate() {
        isMoving = controller.Move(moveInputHorizontal, moveInputVertical, moveSpeed);
        isRunning = controller.Run(runInput, runStartDelay, runSpeed);
        isDashing = controller.Dash(dashInput, dashSpeed, dashTime, moveInputHorizontal, moveInputVertical);
    }

    /* Update
     */
    void Update () {
        moveInputHorizontal = Input.GetAxis("Horizontal");
        moveInputVertical = Input.GetAxis("Vertical");
        dashInput = Input.GetButtonDown("Dash");
        runInput = Input.GetButton("Run");
        
        // Disable directional movement while running
        if (runInput == true) {
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

        GUI.Label(new Rect(50, 70, 300, 20), "Run Input: " + runInput);
        GUI.Label(new Rect(50, 85, 300, 20), "isRunning: " + isRunning);

        GUI.Label(new Rect(50, 100, 300, 20), "Dash Input: " + dashInput);
        GUI.Label(new Rect(50, 115, 300, 20), "isDashing: " + isDashing);
    }

    /* OnGui
     * 
     * Displays the GUI
     */
    void OnGUI() {
        DebugOutput();
    }

    /* Animation
     * 
     * Player animation
     */
    private void Animation() {
        animator.SetInteger("Direction", controller.GetDirection());
        animator.SetBool("isRunning", isMoving);
    }
}
