using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    public Vector3 startPosition;
    public Animator enemyAnimator;
    private Rigidbody2D enemyBody;
    public PlayerMovement playerMovement;
    public bool alive;
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        startPosition = transform.position;
        ComputeVelocity();
        alive = true;
    }
    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }
    void Movegoomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
        {
            // move goomba
            // todo uncomment movegoomba
            Movegoomba();
        }
        else
        {
            // change direction
            moveRight *= -1;
            ComputeVelocity();
            Movegoomba();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player") && alive)
        {
            GameManager.instance.MarioDeath();
        }
    }
    public void Die()
    {
        enemyAnimator.SetTrigger("dieTrigger");
        alive = false;
        GameManager.instance.IncreaseScore(1);
    }
    public void DieCallback()
    {
        gameObject.SetActive(false);
    }
    public void GameRestart()
    {
        gameObject.SetActive(true);
        alive = true;
        // todo change animator state
        enemyAnimator.Play("goomba-walk");
        transform.position = startPosition;
        originalX = transform.position.x;
        moveRight = -1;
        ComputeVelocity();
    }
}