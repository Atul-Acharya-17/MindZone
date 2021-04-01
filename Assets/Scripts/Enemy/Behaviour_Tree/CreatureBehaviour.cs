using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Node;

public class CreatureBehaviour : MonoBehaviour
{
    /// <summary>
    /// Minimum distance to the player to trigger a state change
    /// </summary>
    [SerializeField] private float minDistance = 5.0f;

    /// <summary>
    /// Attack Pattern of the creature
    /// </summary>
    enum Attack
    {
        AttackOne,
        AttackTwo,
        AttackThree
    }

    /// <summary>
    /// Current attack to be executed
    /// </summary>
    private Attack attackPattern;

    /// <summary>
    /// CreatureController class to control some behaviours
    /// </summary>
    private CreatureController creature;

    /// <summary>
    /// Root node of the Behaviour Tree
    /// </summary>
    private BehaviourTree root;

    /// <summary>
    /// Time the creature enters the game world
    /// </summary>
    private float startTime;

    /// <summary>
    /// Time to increase the difficulty of the creature
    /// </summary>
    [SerializeField] private float timeDifficult = 90.0f;

    /// <summary>
    /// Target class to check if the creature died
    /// </summary>
    private Target target;

    /// <summary>
    /// Player to attack
    /// </summary>
    private Transform player;

    /// <summary>
    /// Animator component of the creature
    /// </summary>
    private Animator enemyAnimator;

    /// <summary>
    /// Time delay between attacks
    /// </summary>
    private float timeToAttack = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        attackPattern = Attack.AttackOne;
        root = CreateBehaviourTree();
        target = GetComponent<Target>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        creature = GetComponent<CreatureController>();
        enemyAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    /// <summary>
    /// Evaluates the behaviour tree
    /// </summary>
    void FixedUpdate()
    {
        root.Evaluate();
    }

    /// <summary>
    /// Idle state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState Idle()
    {
        if (Time.time - 1.0f <= startTime)
        {
            creature.Idle();
            enemyAnimator.SetInteger("fast", -1);
            return NodeState.Success;
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// Walk state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState Walk()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        if( distance >= minDistance)
        {
            if (Time.time <= timeDifficult)
            {
                creature.Walk();
                enemyAnimator.SetInteger("fast", 0);
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// Run state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState Run()
    {
        float distance = Vector3.Distance(this.transform.position, player.position);
        if (distance >= minDistance)
        {
            if (Time.time > timeDifficult)
            {
                creature.Run();
                enemyAnimator.SetInteger("fast", 1);
                return NodeState.Success;
            }
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// Die state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState Die()
    {
        if (target.Health <= 0.0f)
        {
            creature.Die();
            enemyAnimator.SetInteger("fast", -1);
            enemyAnimator.SetTrigger("death");
            return NodeState.Success;
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// Howl state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState Howl()
    {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        if (gm != null && gm.IsGameOver())
        {
            creature.Howl();
            enemyAnimator.SetInteger("fast", -1);
            enemyAnimator.SetTrigger("gameOver");
            return NodeState.Success;
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// LeftAttack state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState LeftAttack()
    {
        creature.FacePlayer();
        if (Time.time >= timeToAttack)
        {
            if (attackPattern == Attack.AttackOne)
            {
                attackPattern = Attack.AttackTwo;
                timeToAttack = Time.time + 1.0f;
                enemyAnimator.SetInteger("fast", -1);
                enemyAnimator.SetTrigger("leftAttack");
                creature.Attack();
            }

            return NodeState.Success;
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// RightAttack state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState RightAttack()
    {
        creature.FacePlayer();
        if (Time.time >= timeToAttack)
        {
            if (attackPattern == Attack.AttackTwo)
            {
                attackPattern = Attack.AttackThree;
                timeToAttack = Time.time + 1.0f;
                enemyAnimator.SetInteger("fast", -1);
                enemyAnimator.SetTrigger("rightAttack");
                creature.Attack();
            }

            return NodeState.Success;
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// DualAttack state
    /// </summary>
    /// <returns>Returns NodeState</returns>
    NodeState DualAttack()
    {
        creature.FacePlayer();
        if (Time.time >= timeToAttack)
        {
            if (attackPattern == Attack.AttackThree)
            {
                attackPattern = Attack.AttackOne;
                timeToAttack = Time.time + 1.0f;
                enemyAnimator.SetInteger("fast", -1);
                enemyAnimator.SetTrigger("dualAttack");
                creature.Attack();
            }

            return NodeState.Success;
        }
        return NodeState.Failure;
    }

    /// <summary>
    /// Creates the behaviour tree of the Creature
    /// </summary>
    /// <returns></returns>
    private BehaviourTree CreateBehaviourTree()
    {
        
        Leaf idle = new Leaf(Idle);
        Leaf die = new Leaf(Die);
        Leaf run = new Leaf(Run);
        Leaf walk = new Leaf(Walk);
        Leaf howl = new Leaf(Howl);
        Leaf attackOne = new Leaf(LeftAttack);
        Leaf attackTwo = new Leaf(RightAttack);
        Leaf attackThree = new Leaf(DualAttack);

        return new BehaviourTree(
            new Selector(
                howl,
                die,
                idle,
                new Selector(
                    walk,
                    run
                ),
                new Sequence(
                    attackOne,
                    attackTwo,
                    attackThree
                )
            )
        );
    }
}
