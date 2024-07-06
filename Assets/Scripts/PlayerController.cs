using UnityEngine;

public class CameraControler : MonoBehaviour {
	public Animator animator;
	public Rigidbody2D rigidBody;

	[SerializeField] int speed = 2;
	[SerializeField] float jumpHeight = 1f;
	[SerializeField] float sprintModifyer = 1.5f;

	float timer;
	bool isMoving = false;
	bool canSprint = false;
	bool isSprinting = false;

	void Start() {
		Debug.Log("Start");
		sprintTimer();
	}

	// Update is called once per frame
	void Update() {
		rayCastCollision(0.3f, 0.325f, -0.5f, true);
		handleInput();
		sprintTimer();

	}

	void rayCastCollision(float distance, float xOffset, float yOffset, bool debug) {
		float rayLength = distance;
		Vector2 startPos = (Vector2)transform.position + new Vector2(xOffset, yOffset);
		RaycastHit2D ray = Physics2D.Raycast(startPos, Vector2.right, rayLength);  //Create a new ray
		
		if(ray.collider != null){
			Debug.Log("Ray Collision: " + ray.collider.tag);
		}
		Debug.DrawRay(startPos, Vector2.right * rayLength, Color.red);

	}

	void sprintTimer() {
		if(!isMoving){            
			timer = 2f;
		}
		if ((!isSprinting) && (!canSprint) && (isMoving)){
			timer -= Time.deltaTime;
			Debug.Log("TIME: " + timer);
			if (timer <= 0f){
				canSprint = true;
			}
		}
		
	}

	void handleInput() {
		/* Key Held */
		if(Input.anyKey){
			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			}
			if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			}
			if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
				animator.SetBool("moving", true);
				isMoving = true;
				if(canSprint){
					transform.Translate((speed * sprintModifyer) * Time.deltaTime * Vector2.left);
					isSprinting = true;
				} else {
					transform.Translate(speed * Time.deltaTime * Vector2.left);
					isSprinting = false;
				}
			}
			if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
				animator.SetBool("moving", true);
				isMoving = true;
				if (canSprint){
					transform.Translate((speed * sprintModifyer) * Time.deltaTime * Vector2.right);
					isSprinting = true;
				} else {
					transform.Translate(speed * Time.deltaTime * Vector2.right);
					isSprinting = false;
				}
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				rigidBody.AddForce(Vector2.up * 225 * jumpHeight);  //225 is the force required to move one unit up
			}
		} else {
			animator.SetBool("moving", false);
			isSprinting = false;
			canSprint = false;
			isMoving = false;
		}
	
	}

}
