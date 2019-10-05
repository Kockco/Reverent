using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MomiFSMManager))]
public class MomiFSMState : MonoBehaviour
{
    protected MomiFSMManager manager;
    protected GameObject aim;
    protected Rigidbody rig;
    protected CapsuleCollider capCol;
    protected Animator anime;

    [SerializeField] protected float moveSpeed = 3;
    [SerializeField] protected float jumpPower = 3;
    [SerializeField] protected bool isGround = false;

    [SerializeField] AnimationCurve slopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));

    Vector3 groundContact;

    void Awake()
    {
        manager = GetComponent<MomiFSMManager>();
        rig = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
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

        if (Input.GetKeyDown(KeyCode.Space) && manager.CurrentState != MomiState.Jump)
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

        if (manager.CurrentState != MomiState.Jump)
            manager.SetState(MomiState.Move);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 5);
        rig.MovePosition(transform.position + desiredMoveDirection.normalized * SlopeMomi() * moveSpeed * Time.deltaTime);
        // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    protected virtual void JumpMomi()
    {
        rig.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        manager.SetState(MomiState.Jump);
    }

    protected float SlopeMomi()
    {
        float angle = Vector3.Angle(groundContact, Vector3.up);
        return slopeCurveModifier.Evaluate(angle);
    }

    void IsGrounded()
    {
        RaycastHit rayHit;

        if (Physics.SphereCast(transform.position, capCol.radius * (1.0f - 0.1f), Vector3.down, out rayHit, ((capCol.height / 2f) - capCol.radius) + 0.75f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            isGround = false;
            groundContact = rayHit.normal;
        }
        else
        {
            isGround = true;
            groundContact = Vector3.up;
            
        }
    }

}

/*
        RaycastHit rayHit;
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);

        if (Physics.Raycast(transform.position, Vector3.down, out rayHit, 0.05f))
            if (rayHit.transform.tag == "Untagged")
            {
                isGround = true;
                isJumped = false;
                return;
            }

        isGround = false;
 */
