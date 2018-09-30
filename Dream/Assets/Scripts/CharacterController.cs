using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private Rigidbody2D rigid2d;

    private int direction = 270;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    private float dashStartTimer = 0;

    void Start() {
        rigid2d = GetComponent<Rigidbody2D>();
    }

    /* SetDirection
     * 
     *  Sets current direction the character is facing
     */
    private void SetDirection(float moveInputHorizontal, float moveInputVertical) {
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

    /* Dash
     * 
     * Character starts to dash if the dash button is held down longer than the time
     * specified in dashStartDelay.
     * 
     * Character stops dashing when the dash button is no longer held down. 
     */
    public bool Dash(bool dashInput, float dashStartDelay, float dashSpeed) {
        if (dashInput == true) {

            if (dashStartTimer >= dashStartDelay) {

                // Player facing down
                if (direction == 270) {
                    moveVelocity = Vector2.down * dashSpeed;
                }
                // Player facing up
                else if (direction == 90) {
                    moveVelocity = Vector2.up * dashSpeed;
                }
                // Player facing left
                else if (direction == 0) {
                    moveVelocity = Vector2.left * dashSpeed;
                }
                // Player facing right
                else if (direction == 180) {
                    moveVelocity = Vector2.right * dashSpeed;
                }

                rigid2d.MovePosition(rigid2d.position + moveVelocity * Time.fixedDeltaTime);
            }
            else {
                dashStartTimer += Time.fixedDeltaTime;
            }

            return true;
        }

        dashStartTimer = 0;
        return false;
    }
}
