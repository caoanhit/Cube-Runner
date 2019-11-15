using UnityEngine;
using System;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    [Header("Ground Check")]
    public LayerMask mask;
    public float radius;
    public float speedIncrease;
    public GameEvent OnCharacterDie;
    public IntVariable score;
    public IntVariable additionalScore;

    private Checkpoint checkpoint = new Checkpoint(Vector3.zero, Vector3.forward);
    private Vector3 direction = Vector3.forward;
    Rigidbody rb;
    Animator anim;
    bool failed;
    float additionalSpeed;
    private Checker checker;
    void Start()
    {
        Vector3 original = transform.position;
        score.SetValue(0);
        additionalScore.SetValue(0);
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        checkpoint = new Checkpoint(original, direction);
    }
    // Update is called once per frame
    void Update()
    {
        if (TouchInput.Instance.tap && Grounded())
        {
            if (checker != null)
            {
                checker.Check(transform.position);
            }
            ChangeDirection();
        }
        if (Grounded())
        {
            score.SetValue((int)transform.position.x / 5 + (int)transform.position.z / 5 + additionalScore);
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void ChangeDirection()
    {
        if (direction == Vector3.forward)
        {
            direction = Vector3.right;
        }
        else if (direction == Vector3.right)
            direction = Vector3.forward;
    }
    public void Move()
    {
        if (!failed)
        {
            rb.MovePosition(rb.position + direction * (moveSpeed + additionalSpeed) * Time.fixedDeltaTime);
            additionalSpeed += Time.fixedDeltaTime * speedIncrease;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.fixedDeltaTime);
    }
    public void SetDirection(Vector3 dir)
    {
        direction = dir;
        transform.rotation = Quaternion.LookRotation(dir);
        checkpoint = new Checkpoint(checkpoint.position, dir);
    }
    public bool Grounded()
    {
        Collider[] colliders = new Collider[3];
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, radius, colliders, mask);
        if (hitCount > 0) return true;
        return false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Deadzone")
        {
            failed = true;
            OnCharacterDie?.Raise();
        }
        else if (other.gameObject.tag == "Ground")
        {
            checkpoint = new Checkpoint(other.transform.position + Vector3.up * 0.5f + other.transform.forward * 0.5f, other.transform.forward);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checker")
        {
            checker = other.GetComponent<Checker>();
        }
    }
    private Vector3 CheckDirection(Vector3 position)
    {
        if (Physics.Raycast(position + Vector3.up * 0.6f + Vector3.forward, Vector3.down, 0.3f, mask))
        {
            return Vector3.forward;
        }
        else if (Physics.Raycast(position + Vector3.up * 0.6f + Vector3.right, Vector3.down, 0.3f, mask))
        {
            return Vector3.right;
        }
        return Vector3.zero;
    }
    public void Respawn()
    {
        additionalSpeed = 0;
        transform.position = checkpoint.position;
        transform.rotation = Quaternion.LookRotation(checkpoint.direction);
        direction = checkpoint.direction;
        failed = false;
    }
}
[System.Serializable]
public class Checkpoint
{
    public Vector3 position;
    public Vector3 direction;
    public Checkpoint(Vector3 pos, Vector3 dir)
    {
        position = pos;
        direction = dir;
    }
}
