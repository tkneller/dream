using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private Rigidbody2D rigid2d;

    private int direction = 270;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private float runStartTimer = 0;

    void Start() {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    /* SetDirection
     * 
     *  Sets current direction the character is facing
     */
    private void SetDirection(float moveInputHorizontal, float moveInputVertical) {
        // Character facing down
        if (moveInputVertical < 0) {
            direction = 270;
        }
        // Character facing up
        else if (moveInputVertical > 0) {
            direction = 90;
        }
        // Character facing left
        else if (moveInputHorizontal < 0) {
            direction = 0;
        }
        // Character facing right
        else if (moveInputHorizontal > 0) {
            direction = 180;
        }
    }

    /* GetDirection
     * 
     * Returns the direction the character is facing. 
     */
    public int GetDirection() {
        return direction;
    }

    /* Move
     * 
     * Character directional movement.
     */
    public bool Move(float moveInputHorizontal, float moveInputVertical, float speed) {
        if (moveInputHorizontal == 0 && moveInputVertical == 0) {
            return false;
        }

        moveInput = new Vector2(moveInputHorizontal, moveInputVertical);
        moveVelocity = moveInput.normalized * speed;

        rigid2d.MovePosition(rigid2d.position + moveVelocity * Time.fixedDeltaTime);
        SetDirection(moveInputHorizontal, moveInputVertical);

        return true;
    }

    /* Run
     * 
     * Character starts to run if the run button is held down longer than the time
     * specified in runStartDelay.
     * 
     * Character stops running when the run button is no longer held down. 
     */
    public bool Run(bool dashInput, float runStartDelay, float runSpeed) {
        if (dashInput == true) {

            if (runStartTimer >= runStartDelay) {

                // Player facing down
                if (direction == 270) {
                    moveVelocity = Vector2.down * runSpeed;
                }
                // Player facing up
                else if (direction == 90) {
                    moveVelocity = Vector2.up * runSpeed;
                }
                // Player facing left
                else if (direction == 0) {
                    moveVelocity = Vector2.left * runSpeed;
                }
                // Player facing right
                else if (direction == 180) {
                    moveVelocity = Vector2.right * runSpeed;
                }

                rigid2d.MovePosition(rigid2d.position + moveVelocity * Time.fixedDeltaTime);
            }
            else {
                runStartTimer += Time.fixedDeltaTime;
            }

            return true;
        }

        runStartTimer = 0;
        return false;
    }
}
