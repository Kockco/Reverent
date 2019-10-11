using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MomiFSMManager))]
public class MomiFSMState : MonoBehaviour
{
    protected MomiFSMManager manager;
    protected GameObject aim;
    protected GameObject cam;
    protected Rigidbody rig;
    protected CapsuleCollider capCol;
    protected Animator anime;

    [SerializeField] protected float moveSpeed = 4;
    [SerializeField] protected float jumpPower = 3;
    [SerializeField] protected bool isGround = false;

    [SerializeField] AnimationCurve slopeCurveModifier = new AnimationCurve(new Keyframe(-90f, 1f), new Keyframe(0f, 1f), new Keyframe(90f, 0f));
    [SerializeField] Vector3 desiredMoveDirection;
    Vector3 groundContact;

    void Awake()
    {
        manager = GetComponent<MomiFSMManager>();
        rig = GetComponent<Rigidbody>();
        capCol = GetComponent<CapsuleCollider>();
        anime = transform.GetChild(0).GetComponent<Animator>();
        aim = GameObject.Find("Aim");
        cam = Camera.main.gameObject;
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
        AimRotation();

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && manager.CurrentState != MomiState.Handle)
            KeyMoveMomi();

        if (Input.GetKeyDown(KeyCode.Space) && manager.CurrentState != MomiState.Jump)
            JumpMomi();
    }

    protected void KeyMoveMomi()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * inputZ + right * inputX;

        if (manager.CurrentState != MomiState.Jump)
            manager.SetState(MomiState.Move);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 5);
        rig.MovePosition(transform.position + desiredMoveDirection.normalized * SlopeMomi() * moveSpeed * Time.deltaTime);
    }

    protected virtual void JumpMomi()
    {
        rig.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        manager.SetState(MomiState.Jump);
    }

    protected float SlopeMomi()
    {
        float angle = Vector3.Angle(groundContact, Vector3.up);

        if (manager.CurrentState == MomiState.Jump)
            angle = 0;

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

    void AimRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");

        Vector3 camForward = cam.transform.forward;

        camForward.Normalize();
        camForward.y = 0;

        Vector3 desiredMoveDirection = camForward;
        aim.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), 1);

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
