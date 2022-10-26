using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		[SerializeField] private Vector2 move;
		[SerializeField] private bool sprint;
		[SerializeField] private bool crouch = false;
		public Vector2 Move{get{return move;}  set{move = value;}}
		public bool Sprint{get{return sprint;}  set{sprint = value;}}
		public bool Crouch{get{return crouch;}  set{crouch = value;}}



		[Header("Movement Settings")]
		[SerializeField] private  bool analogMovement;
		public bool AnalogMovement{get; set;}

		[Header("Mouse Cursor Settings")]
		[SerializeField] private  bool cursorLocked = false;
		[SerializeField] private  bool cursorInputForLook = true;
		public bool CursorLocked{get{return cursorLocked;}  set{cursorLocked = value;}}
		public bool CursorInputForLook{get{return cursorInputForLook;}  set{cursorInputForLook = value;}}

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnCrouch(InputValue value)
		{
			CrouchInput();
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void CrouchInput()
		{
			crouch = !crouch;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}