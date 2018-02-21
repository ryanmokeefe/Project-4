using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		private Animator playerAnimator;
		private Animation jump;
		private Animation walk;
		private float moveHorizontal;
		private float moveVertical;
		private Vector3 movement;
		private float turningSpeed = 20f;
		private Rigidbody playerRigidbody;
		[SerializeField]
		//	private RandomSoundPlayer playerFootsteps;
        
        private void Start()
        {

			// Gather components from the Player GameObject
			playerAnimator = GetComponent<Animator> ();
			playerRigidbody = GetComponent<Rigidbody> ();
			jump = GetComponent<Animation> ();
			walk = GetComponent<Animation> ();


            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
			// Gather input from the keyboard
			moveHorizontal = Input.GetAxisRaw ("Horizontal");
			moveVertical = Input.GetAxisRaw ("Vertical");

			movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {

			// If the player's movement vector does not equal zero...
			if (movement != Vector3.zero) {

				// ...then create a target rotation based on the movement vector...
				Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);

				// ...and create another rotation that moves from the current rotation to the target rotation...
				Quaternion newRotation = Quaternion.Lerp (playerRigidbody.rotation, targetRotation, turningSpeed * Time.deltaTime);

				// ...and change the player's rotation to the new incremental rotation...
				playerRigidbody.MoveRotation(newRotation);

				// ...then play the walk animation...
				playerAnimator.SetFloat ("Speed", 3f);

				// ...play footstep sounds.
				//			playerFootsteps.enabled = true;

			} else {

				// Otherwise, don't play the walk animation.
				playerAnimator.SetFloat ("Speed", 0f);

				// Don't play footstep sounds
				//			playerFootsteps.enabled = false;

			}




            // read inputs
//            float h = CrossPlatformInputManager.GetAxis("Horizontal");
//            float v = CrossPlatformInputManager.GetAxis("Vertical");
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
//                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
//                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
//	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
