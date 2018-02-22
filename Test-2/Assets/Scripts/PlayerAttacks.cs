using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

	Rigidbody m_Rigidbody;
	Animator m_Animator;

	public GameObject target;

	bool attacking;
	bool gutHit;

	public float cooldown;
	public float attackTime;

//	kick - standing_melee_attack_kick_ver_1
//	attack1 - standing_melee_attack_horizontal
//	attack2 - standing_melee_attack_backhand
//	wounded - standing_react_large_gut




	// Use this for initialization
	void Start () {

		cooldown = 1.5f;
		attackTime = 0;

		m_Animator = GetComponent<Animator> ();
		m_Rigidbody = GetComponent<Rigidbody> ();

////		anim = GetComponent<Animation>();
//		anim.Play(anim.clip.name);
//		yield return new WaitForSeconds(anim.clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		// if "1": play melee	 // randomize melee attacks
//		if (Input.GetKeyDown(KeyCode.F)) {
//			m_Animator.Play("Attack");
//
//		}

		// if "2" - play kick


		// if hit - play gutWound


	}
}
