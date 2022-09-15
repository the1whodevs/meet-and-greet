using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	[Header("Other Player Components")]
    // Make sure this is not null!
	[SerializeField] private PlayerBody playerBody;

	[Header("Movement")]
	[SerializeField] private Transform moveCenter;

    // Make sure these are never "KeyCode.None"
	[SerializeField] private KeyCode forwardButton;
	[SerializeField] private KeyCode backwardButton;
	[SerializeField] private KeyCode leftButton;
	[SerializeField] private KeyCode rightButton;

	[Space]

    // Make sure this is never zero!
	[SerializeField] private float moveSpeed = 5.0f;

	[SerializeField, 
    Tooltip("This is checked using rigidbody.velocity.magnitude.")] 
    // Make sure this is always greater than 0.
    private float maxSpeed = 25.0f;

	[Header("Actions")]
    // Make sure these are never "KeyCode.None"
	[SerializeField] private KeyCode jumpButton;

	[Space]

    // Make sure this is always greater than 0!
	[SerializeField] private float jumpStrength = 100.0f;

	private Vector3 moveThisFrame = Vector3.zero;

	// As input is read per frame, read it in Update, not FixedUpdate, otherwise functions like 'GetKeyDown' won't always trigger!
	private void Update() {
        if (Input.GetKey(forwardButton)){
			moveThisFrame.z += 1;
		}

        // Using 'if' instead of 'else if' to allow player to cancel out his movement by tapping the opposite direction!
        if (Input.GetKey(backwardButton)){
			moveThisFrame.z -= 1;
		}

        if (Input.GetKey(leftButton)){
			moveThisFrame.x -= 1;
		}

        // Using 'if' instead of 'else if' to allow player to cancel out his movement by tapping the opposite direction!
        if (Input.GetKey(rightButton)){
			moveThisFrame.x += 1;
		}

		// Normalize before adding jump force to prevent moving diagonally faster!
		moveThisFrame.Normalize();
		moveThisFrame *= moveSpeed;

        if (Input.GetKeyDown(jumpButton)){
			moveThisFrame.y = jumpStrength;
		}

		// Transform move vector from world to local!
		moveThisFrame = moveCenter.InverseTransformVector(moveThisFrame);

		// Apply movement to player using PlayerBody.
		playerBody.MoveMainBody(moveThisFrame, maxSpeed);
		
        // TODO: Maybe call this in FixedUpdate of PlayerMove always (too) ?
        // playerBody.LimitToMaxSpeed(maxSpeed);

		// Reset move vector.
		moveThisFrame = Vector3.zero;
	}

    // For unit testing.
    public PlayerBody GetPlayerBody(){
		return playerBody;
	}

    // For unit testing.
    public KeyCode GetForwardButton(){
		return forwardButton;
	}

    // For unit testing.
    public KeyCode GetBackwardButton(){
		return backwardButton;
	}

    // For unit testing.
    public KeyCode GetRightButton(){
		return rightButton;
	}

    // For unit testing.
    public KeyCode GetLeftButton(){
		return leftButton;
	}

    // For unit testing.
    public KeyCode GetJumpButton(){
		return jumpButton;
	}

    // For unit testing.
    public float GetMoveSpeed(){
		return moveSpeed;
	}

    // For unit testing.
    public float GetMaxSpeed(){
		return maxSpeed;
	}

    // For unit testing.
    public float GetJumpStrength(){
		return jumpStrength;
	}
}
