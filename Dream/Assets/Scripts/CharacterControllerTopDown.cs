using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerTopDown : MonoBehaviour {

    // Character Components
    private Rigidbody2D rigid2d;

    // Movement
    private int direction = 270;
    private Vector2 moveInput;
    private Vector2 moveVelocity;

    // Run
    private float runStartTimer = 0;

    // Dash
    private float dashTimer = 0;

    /* Start
     */
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
    public bool Run(bool runInput, float runStartDelay, float runSpeed) {

        if (runInput == true) {

            if (runStartTimer >= runStartDelay) {

                // Character facing down
                if (direction == 270) {
                    moveVelocity = Vector2.down * runSpeed;
                }
                // Character facing up
                else if (direction == 90) {
                    moveVelocity = Vector2.up * runSpeed;
                }
                // Character facing left
                else if (direction == 0) {
                    moveVelocity = Vector2.left * runSpeed;
                }
                // Character facing right
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

    /* Dash
     * 
     * Character dashes in the oppisite direction it is facing, if no 
     * directional movement input is present.
     * 
     * If directional movement input is present, the character dashes
     * in this direction.
     */
    public bool Dash(bool dashInput, float dashSpeed, float dashTime, float moveInputHorizontal, float moveInputVertical) {

        if (dashInput == true || dashTimer > 0) {

            if (dashTimer < dashTime) {
                dashTimer += Time.fixedDeltaTime;

                // Character dashes in the opposite direction it is facing, 
                // if there is no directional movement input.
                if (moveInputHorizontal == 0 && moveInputVertical == 0) {
                    // Character facing down and dashing up
                    if (direction == 270) {
                        moveVelocity = Vector2.up * dashSpeed;
                    }
                    // Character facing up and dashing down
                    else if (direction == 90) {
                        moveVelocity = Vector2.down * dashSpeed;
                    }
                    // Character facing left and dashing right
                    else if (direction == 0) {
                        moveVelocity = Vector2.right * dashSpeed;
                    }
                    // Character facing right and dashing left
                    else if (direction == 180) {
                        moveVelocity = Vector2.left * dashSpeed;
                    }

                    rigid2d.MovePosition(rigid2d.position + moveVelocity * Time.fixedDeltaTime);
                  
                    return true;
                }

                // Character dashes in the direction specified by the movement input
                Move(moveInputHorizontal, moveInputVertical, dashSpeed);

                return true;
            }
            else {
                dashTimer = 0;
            }
        }

        return false;
    }
}
