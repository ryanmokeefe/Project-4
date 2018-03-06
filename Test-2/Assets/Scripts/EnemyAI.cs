//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//
//public class EnemyAI : MonoBehaviour {
//
//	//temp
//
//	[SerializeField]
//	private Transform target;
//	private NavMeshAgent monsterAgent;
//
//	//
//
//	public bool dead;
//	[SerializeField]
//	float deadZone = 5f;
//	[SerializeField]
//	float turnSpeed = 3f;
//
//
//	NavMeshAgent agent;
//	Animator anim;
//
//	public Transform targetEnemy;
//	public Transform targetDestination;
//	public Vector3 targetDesPos;
//	Vector3 lookPos;
//	public bool AttackEnemy;
//	public float distanceThreshold = 3;
//	//
//	float attackTimer;
//	bool interrupted;
//
//	public GameObject counterAttackIndicator;
//
//	// Use this for initialization
//	void Start () {
//
//		monsterAgent = GetComponent<NavMeshAgent> ();
//
//		agent = GetComponentInChildren<NavMeshAgent> ();
//		anim = GetComponent<Animator> ();
//
//		SetupAnimator ();
//
//		agent.updateRotation = false;
//		agent.speed = 1;
//		agent.stoppingDistance = distanceThreshold - .5f;
//
//		// convert deadZone area from degree - radians
//		deadZone *= Mathf.Deg2Rad;
//
//		targetDesPos = transform.position;
//
//		//		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ()  ;
//
//		counterAttackIndicator.SetActive (false);
//
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//		// basic follow:
//
//		// set destin
//		monsterAgent.SetDestination (target.position);
//		// measure magnitude of navMeshAgent velocity
//		float speed = monsterAgent.velocity.magnitude;
//		//pass velocity to the animator component
////******
//		anim.SetFloat ("Speed", speed);
//
//	//
//
//		if (!dead) {
//		
//			FindDestination ();
//			LookAtTheTarget ();
////			NavigationAI ();
//			HandleMovementAnimation ();
//			StateSwitch ();
//
//		
//		}
//
//
//	}
//
//
//
//
//	// switch destin/attacking
//	void StateSwitch()
//	{
//		if (AttackEnemy) {
//		
//			targetDesPos = targetEnemy.position;
//		
//		} else {
//		
//			if (targetDestination) {
//			
//				targetDesPos = targetDestination.position;
//
//			
//			}
//		
//		}
//	
//	}
//
//	// find target location
//	void FindDestination() 
//	{
//		float distanceFromTarget = Vector3.Distance (transform.position, targetDesPos);
//
//		agent.SetDestination (targetDesPos);
//
//		if (distanceFromTarget < distanceThreshold) {
//
//			attackTimer += Time.deltaTime;
//
//			if (attackTimer > 2 && !interrupted) {
//			
////				targetEnemy.GetComponent<CharacterMovement> ().EnableCounterAttack (true);
//				counterAttackIndicator.SetActive (true);
//
//				if (attackTimer > 4) {
//				
////					targetEnemy.GetComponent<CharacterMovement> ().EnableCounterAttack (false);
//					counterAttackIndicator.SetActive (false);
//					anim.SetBool ("currentlyAttacking", true);
//					attackTimer = 0;
//				}
//			}
//		}
//	}
//
//
//
////	public void CounterAttackInterruption() {
////		
////	}
//
//	// face target location
//	void LookAtTheTarget() {
//	
//		lookPos = targetEnemy.position;
//
//		Vector3 lookDir = lookPos - transform.position;
//		lookDir.y = 0;
//		// rotate to face target
//		Quaternion rot = Quaternion.LookRotation (lookDir);
//
//		// take from world space, turn to local space
//			// fix broken movement
////		Vector3 relativePos = transform.InverseTransformPoint (targetEnemy.position);
////
////		anim.SetFloat ("Turn", relativePos.normalized.x);
//
//		transform.rotation = Quaternion.Slerp (transform.rotation, rot, Time.deltaTime * turnSpeed);
//	
//	}
//
//
////	void NavigationAI() {
////		float angle;
////		angle = FindAngle(transform.forward, agent.desiredVelocity, transform.up);
////		if (Mathf.Abs(angle) < deadZone) {
////			
////			transform.LookAt (transform.position + agent.desiredVelocity);
////			angle = 0;
////		
////		}
////	}
//
//	// pass velocity to new location to animator:
//	void HandleMovementAnimation() {
//	
//		Vector3 desiredVelocityOnLocalCoordinates = transform.InverseTransformDirection (agent.desiredVelocity);
//
//		anim.SetFloat ("Forward", desiredVelocityOnLocalCoordinates.z, 0.1f, Time.deltaTime);
//		anim.SetFloat ("Sideways", desiredVelocityOnLocalCoordinates.x, 0.1f, Time.deltaTime);
//
//	
//	}
//
//
//		// ** Find targeting project:
//	// calculate angle between target
//	float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector){
//
//		// if angle being calculated is 0
//		if (toVector == Vector3.zero) {
//			// then angle between is 0
//			return 0f;
//		}
//
//		// float angle to store angle bwtween the enemy facing and the direction of target
//		float angle = Vector3.Angle(fromVector, toVector);
//
//		// find the cross product of the 2 vectors
//		Vector3 normal = Vector3.Cross(fromVector, toVector);
//
//			// product of the normal with vectorUp will be positive if they point
//		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
//
//		// convert angle from degrees to radians
//		angle *= Mathf.Deg2Rad;
//
//		return angle;
//	
//	}
//
//	// get anim for this game object:
//	void SetupAnimator() 
//	{
//		// reference to anim comp on root
//		anim = GetComponent<Animator>();
//
//////		 use avatar from Child comp, to enable swapping of character model as a child node
//			/// broken til child comp added
////		forEach (var childAnimator int GetComponentsInChildren<Animator>()) {
////
////			if (childAnimator != anim) {
////				anim.avatar = childAnimator.avatar;
////				Destroy(childAnimator);
////				break;
////			}
////
////		}
//
//	}
//}
