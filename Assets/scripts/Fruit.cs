using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fruit : MonoBehaviour
{

    public Sprite[] fruitlist;
    public int num;
    public int fruitID;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private GameObject spawner;
    public int fruitTotalID = 1;
    bool clickState = true;
    
    private void Start()
    {
        spawner = GameObject.FindWithTag("spawner");
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        fruitTotalID = spawner.GetComponent<Spawner>().tolalSpawnerID;


        num = Random.Range(0, 5);
        fruitID = num;
        spriteRenderer.sprite = fruitlist[num];
        
        AdjustColliderToSprite();
    }
    private void Update()
    {
        if (GetComponent<Rigidbody2D>().gravityScale <= 0)
        {
            transform.position = spawner.transform.position;
        }


        if (clickState)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.gravityScale = 1;
                rb.velocity = (transform.position - transform.position).normalized * 10f;
                clickState = false;
                Invoke("ClickState", 1);
            }
        }
        

    }




    void AdjustColliderToSprite()
    {
        if (spriteRenderer.sprite == null) return;

        
        float spriteRadius = spriteRenderer.sprite.bounds.size.x / 2f;

        
        circleCollider.radius = spriteRadius;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("fruit"))
        {
            Fruit otherFruit = collision.gameObject.GetComponent<Fruit>();
            if (fruitID == otherFruit.fruitID && fruitTotalID < otherFruit.fruitTotalID)
            {
                fruitID++;
                spriteRenderer.sprite = fruitlist[fruitID];
                AdjustColliderToSprite();
                spawner.GetComponent<Spawner>().score++;
                Destroy(collision.gameObject);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("rip"))
        {
            SceneManager.LoadScene("Ded");
        }
    }
    void ClickState()
    {
        clickState = true;
    }

}
