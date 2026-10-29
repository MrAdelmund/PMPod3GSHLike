using UnityEngine;

public class MeleeEnemyManager : MonoBehaviour
{
    [Header("Melee Enemy Config")]
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float jumpForce = 15;
    [SerializeField] float attackCooldown = 1.5f;
    [SerializeField] float retreatTime = 2.5f;
    [SerializeField] float attackDistance = 1.5f;
    [SerializeField] ContactFilter2D groundCheckContactFilter;
    [Header("References")]
    [SerializeField] BoxCollider2D jumpCheckBoxL;
    [SerializeField] BoxCollider2D jumpCheckBoxR;
    [SerializeField] MeleeAttackBoxCheck attackHitBoxObject;
    Animator animator;
    SpriteRenderer spr;
    Rigidbody2D rb;
    GameObject player;
    bool isGrounded => rb.IsTouching(groundCheckContactFilter);
    bool properlyStandingStill;
    int inproperlyStandingStillFor;
    float retreatFor = 0;
    float stopMovingTimer = 0;
    float cantAttackFor = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        spr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        stopMovingTimer -= Time.deltaTime;
        cantAttackFor -= Time.deltaTime;
        if ( cantAttackFor <= 0)
        {
            retreatFor -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (stopMovingTimer <= 0)
        {
            RunMovement();
        }
        //starts an attack when conditions are met
        if (Vector2.Distance(player.transform.position, transform.position) < attackDistance && cantAttackFor <= 0)
        {
            StopMovingFor(1);
            StartAttack();
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //updates values for animaior
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("Moving", (rb.velocity.x != 0) ? true : false);
    }
    void RunMovement()
    {
        //will make the enemy jump if they get stuck not moving when they should be
        if (!properlyStandingStill && rb.velocity == Vector2.zero)
        {
            inproperlyStandingStillFor++;
            if (inproperlyStandingStillFor > 10)
            {
                inproperlyStandingStillFor = 0;
                Jump();
            }
        }else
        {
            inproperlyStandingStillFor = 0;
        }
        //sets the movement for the enemy based off player position
        int movedir = 0;
        if (player.transform.position.x - transform.position.x > 0.05)
        {
            movedir = 1;
            spr.flipX = true;
            properlyStandingStill = false;
        }
        else if (player.transform.position.x - transform.position.x < -0.05)
        {
            movedir = -1;
            spr.flipX = false;
            properlyStandingStill = false;
        }
        else
            properlyStandingStill = true;
        if (retreatFor > 0)
        {
            movedir *= -1;
            spr.flipX = !spr.flipX;
        }
        rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
    }
    //when called, will stop enemy from moving for (time)
    void StopMovingFor(float time)
    {
        stopMovingTimer = time;
    }
    //this gets triggered by the JumpBoxTirggerer script
    public void JumpBoxTriggered(bool isOnleft)
    {
        if (isOnleft && isGrounded && !spr.flipX)
            Jump();
        else if (!isOnleft && isGrounded && spr.flipX)
            Jump();
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce * 50));
    }
    void StartAttack()
    {
        cantAttackFor = attackCooldown;
        animator.SetBool("Attacking", true);
    }
    //this is called by an animation event on the enemie's sprite object
    public void Attack()
    {
        animator.SetBool("Attacking", false);
        attackHitBoxObject.gameObject.SetActive(true);
        attackHitBoxObject.currentAttemptedDammageTime = 0;
    }
    //makes enemy retreat only when an attack lands
    //gets called by MeleeAttackBoxCheck
    public void AttackHit()
    {
        retreatFor = retreatTime;
    }
}
