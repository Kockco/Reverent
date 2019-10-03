using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MomiFSMManager))]
public class MomiFSMState : MonoBehaviour
{
    protected MomiFSMManager manager;
    protected GameObject aim;
    protected Rigidbody rig;
    protected Animator anime;

    [SerializeField] protected float moveSpeed = 3;
    [SerializeField] protected float jumpPower = 3;

    [SerializeField] protected bool isJumped = false;
    [SerializeField] protected bool isGround = false;

    void Awake()
    {
        manager = GetComponent<MomiFSMManager>();
        rig = GetComponent<Rigidbody>();
        anime = transform.GetChild(0).GetComponent<Animator>();
        aim = GameObject.Find("Aim");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public virtual void BeginState()
    {
    }

    public virtual void EndState()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        IsGrounded();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            KeyMoveMomi();

        if (Input.GetKeyDown(KeyCode.Space) && isJumped == false)
            JumpMomi();
    }

    protected virtual void OnTriggerStay(Collider col)
    {
        
    }

    protected void KeyMoveMomi()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 desiredMoveDirection = forward * inputZ + right * inputX;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 5);
        rig.MovePosition(transform.position + desiredMoveDirection * moveSpeed * Time.deltaTime);
        // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    protected virtual void JumpMomi()
    {
        isJumped = true;
        rig.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        manager.SetState(MomiState.Jump);
    }

    void IsGrounded()
    {
        RaycastHit rayHit;
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);

        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 0.5f))
            if (rayHit.transform.tag == "Untagged")
            {
                isGround = true;
                isJumped = false;
                return;
            }

        isGround = false;
    }

    
}
