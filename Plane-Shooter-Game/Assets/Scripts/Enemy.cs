using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemySystem enemySystem;
    private HeroMovement hero;
    private int health = 4;
    private float speed = 10f;
    private float rotateSpeed = 100f;
    private float changeDirCooldown = 2f;
    private float nextChange;
    private float direction;

    // Start is called before the first frame update
    void Start()
    {
        enemySystem = GameObject.Find("EnemySystem").GetComponent<EnemySystem>();
        hero = GameObject.Find("Hero").GetComponent<HeroMovement>();
        nextChange = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySystem.getMode())
        {
            randomMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            DestroyThisEnemy();
            hero.Touch();
        }
        else if (collision.gameObject.tag == "Egg")
        {
            health--;
            if (health > 0)
            {
                SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
                Color tempColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, renderer.color.a * 0.8f);
                renderer.color = tempColor;
            }
            else
            {
                DestroyThisEnemy();
            }
        }
        else if (collision.gameObject.tag == "Missile")
        {
            DestroyThisEnemy();
        }
        else if (collision.gameObject.tag == "VerEdge")
        {
            transform.RotateAround(transform.position, Vector3.up, 180f);
        } 
        else if (collision.gameObject.tag == "HorEdge")
        {
            transform.RotateAround(transform.position, Vector3.right, 180f);
        }
    }

    private void DestroyThisEnemy()
    {
        if (gameObject.activeSelf)
        {
            enemySystem.Die();
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void randomMovement()
    {
        transform.position += transform.up * (speed * Time.smoothDeltaTime);
        if (Time.time > nextChange)
        {
            direction = Random.Range(-1f, 1f);
            nextChange = Time.time + changeDirCooldown;
        }
        transform.Rotate(Vector3.forward, -1f * direction * (rotateSpeed * Time.smoothDeltaTime));
    }
}
