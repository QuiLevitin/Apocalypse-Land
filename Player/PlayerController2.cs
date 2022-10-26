using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

public class PlayerController2 : MonoBehaviour
{
    private MasterControl masterControl;
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController playerCC;

    [SerializeField] private Vector2 moveVector;

    private bool isMovePressed = false;
    private bool isPayerMoving = false;
    private bool isAttackingPressed = false;

    [SerializeField] private float playerSpeed = 6f;
    [SerializeField] private float crouchSpeed = 0.1f;
    
    [SerializeField] private float rotationvelocityInterpolatorSpeed = 0.3f;
    [SerializeField] private float desiredVelocity;
    [SerializeField] private float previousVelocity;
    [SerializeField] private float velocityInterpolatorSpeed;

    private int velocityHash;
    private int isCrouchingHash;
    private int isAttackingHash;



    private WaitForFixedUpdate fixedUpdateTime = new WaitForFixedUpdate();

    private void Awake()
    {

        masterControl = new MasterControl();
        masterControl.Game.Move.performed += _ => 
        {
            moveVector = _.ReadValue<Vector2>();

            if(!isPayerMoving && !isAttackingPressed)
            {
                isMovePressed = true;
                StopCoroutine(DeceleratePlayer());
                StartCoroutine(MovePlayer());
            }
        };
        
        masterControl.Game.Move.canceled += _ =>         
        {
            moveVector = Vector2.zero;
            isMovePressed = false;
        };
        
        masterControl.Game.Attack.started += _ => AttackInit();

        masterControl.Game.Crouch.started += _ => animator.SetBool(isCrouchingHash, !animator.GetBool(isCrouchingHash));


    }

    private void Start()
    {
        velocityHash = Animator.StringToHash("velocity");
        isCrouchingHash = Animator.StringToHash("isCrouching");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    private void OnEnable()
    {
        masterControl.Enable();
    }

    private void OnDisable()
    {
        masterControl.Disable();
    }

    private IEnumerator MovePlayer()
    {
        isPayerMoving = true;
        while(isMovePressed)
        {
            desiredVelocity = Vector2.Distance(moveVector, Vector2.zero);
            Vector3 playerDir = new Vector3(moveVector.x, 0f, moveVector.y).normalized;
            playerDir = Quaternion.AngleAxis(45, Vector3.up) * playerDir;

            if(!isAttackingPressed)
            {
                if(!animator.GetBool(isCrouchingHash)) 
                {
                    playerCC.Move(playerDir * Time.fixedDeltaTime * playerSpeed * desiredVelocity);
                    animator.SetFloat(velocityHash, desiredVelocity);
                }
                else
                {
                    playerCC.Move(playerDir * Time.fixedDeltaTime * playerSpeed * crouchSpeed);
                    animator.SetFloat(velocityHash, crouchSpeed);
                }
            }

        
            HandleRotation(playerDir);

            yield return fixedUpdateTime;
        }

        isPayerMoving = false;

        if(!animator.GetBool(isCrouchingHash)) 
        {
            StartCoroutine(DeceleratePlayer());
        }
        else
        {
            animator.SetFloat(velocityHash, 0f);
        }
    }
    private void HandleRotation(Vector3 playerDir)
    {
        // Vector3 currPos = transform.position;
        // Vector3 newPos = new Vector3(moveVector.x, 0, moveVector.y);
        Quaternion newRot = Quaternion.LookRotation(playerDir, Vector3.up);
        Quaternion desiredRot = Quaternion.Slerp(transform.rotation, newRot, rotationvelocityInterpolatorSpeed);
        transform.rotation = desiredRot;
    }

    private IEnumerator DeceleratePlayer()
    {
        while(desiredVelocity > 0.001f)
        {
            desiredVelocity = Mathf.Lerp(desiredVelocity, 0f, velocityInterpolatorSpeed);
            animator.SetFloat(velocityHash, desiredVelocity);

            if(animator.GetBool(isCrouchingHash)) desiredVelocity = 0.0f;
            yield return fixedUpdateTime;
        }
        desiredVelocity = 0.0f;
        animator.SetFloat(velocityHash, desiredVelocity);
    }

    private void AttackInit()
    {
        if(!isAttackingPressed) 
        {
            Debug.Log("0");
            if(desiredVelocity <= 0f)
            {
                Debug.Log("1");
                animator.SetBool(isAttackingHash, true);
                isAttackingPressed = true;
                Invoke("DisableAttack", 1.6f);
            }

            else
            {
                Debug.Log("2");
                StopCoroutine(WaitForReadyAttack());
                StartCoroutine(WaitForReadyAttack());
            }
        }
    }

    private IEnumerator WaitForReadyAttack()
    {
        while(desiredVelocity > 0f)
        {
            Debug.Log("3");
            yield return fixedUpdateTime;
        }
            Debug.Log("4");
        animator.SetBool(isAttackingHash, true);
        isAttackingPressed = true;
        Invoke("DisableAttack", 1.6f);
    }

    private void DisableAttack()
    {
        animator.SetBool(isAttackingHash, false);
        animator.SetBool(isCrouchingHash, false);
        isAttackingPressed = false;
    }

}
