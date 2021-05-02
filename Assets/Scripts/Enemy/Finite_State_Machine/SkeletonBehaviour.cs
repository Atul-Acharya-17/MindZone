using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
    /// <summary>
    /// State of the Enemy
    /// </summary>
    /// <remarks>
    /// Idle: Stay in idle position<br/>
    /// Chase: Chase the player<br/>
    /// Attack: Attack the player<br/>
    /// Die: Death of the Skeleton<br/>
    /// End: Game over state<br/>
    ///</remarks>
    public enum State 
    {
        Idle,
        Chase,
        Attack,
        Die, 
        End
    }

    /// <summary>
    /// Current state of the Skeleton
    /// </summary>
    private State currentState;

    /// <summary>
    /// Transform of the player
    /// </summary>
    private Transform player;

    /// <summary> 
    /// Minimum distance to the player to trigger a state change
    /// </summary>
    [SerializeField] private float minDistance = 5.0f;

    /// <summary>
    /// Animator of the Skeleton
    /// </summary>
    private Animator skeletonAnimator;

    /// <summary>
    /// SkeletonController class to control some behaviours
    /// </summary>
    private SkeletonController skeleton;

    /// <summary>
    /// Target class to check if the skeleton died
    /// </summary>
    private Target target;

    /// <summary>
    /// Start is called before the first frame update
    /// Initialises data members
    /// </summary>
    void Start()
    {
        currentState = State.Idle;
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        skeleton = GetComponent<SkeletonController>();
        skeletonAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        target = GetComponent<Target>();
    }

    /// <summary>
    /// Update is called once per frame
    /// Controls the Skeleton's Finite State Machine
    /// </summary>
    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.Idle:
                ChangeState(State.Chase);
                break;
            case State.Chase:
                skeleton.FacePlayer();
                ChaseState();
                break;
            case State.Attack:
                skeleton.FacePlayer();
                AttackState();
                break;
            case State.Die:
                skeleton.Die();
                break;
            case State.End:
                skeleton.FacePlayer();
                break;
        }
    }

    /// <summary>
    /// Changes the current state of the skeleton
    /// </summary>
    void ChangeState(State state)
    {
         currentState = state;
    }

    /// <summary>
    /// Controls the skeleton behaviour in the Chase state
    /// </summary>
    void ChaseState()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (gm != null && gm.IsGameOver())
        {
            ChangeState(State.End);
            skeletonAnimator.SetTrigger("gameOver");
        }
        else if (target.Health <= 0.0f)
        {
            if (gm!= null)
            {
                gm.IncreaseScore(1);
            }
            ChangeState(State.Die);
            skeletonAnimator.SetTrigger("death");
        }
        else if (distance >= minDistance)
        {
            skeleton.Chase();
        }
        else if (distance < minDistance)
        {
            ChangeState(State.Attack);
            skeletonAnimator.SetBool("isAway", false);
        }
    }

    /// <summary>
    /// Controls the enemy behaviour in the Attack state
    /// </summary>
    void AttackState()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (gm != null && gm.IsGameOver())
        {
            ChangeState(State.End);
            skeletonAnimator.SetTrigger("gameOver");
        }
        else if (target.Health <= 0.0f)
        {
            if (gm != null)
            {
                gm.IncreaseScore(1);
            }
            ChangeState(State.Die);
            skeletonAnimator.SetTrigger("death");
        }
        else if (distance >= minDistance)
        {
            ChangeState(State.Chase);
            skeletonAnimator.SetBool("isAway", true);
        }
        else
        {
            skeleton.Attack();
        }
    }
}
