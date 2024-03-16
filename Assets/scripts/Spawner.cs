using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject fruit;
    private bool fruitstate = true;
    public int tolalSpawnerID = 1;
    bool clickState = true;
    public GameObject rip;
    public int score = 0;
    public TextMeshPro tmp;


    void Update()
    {
        tmp.GetComponent<TextMeshPro>().text = "Score: "+ Convert.ToString(score);
        if (fruitstate)
        {

            Instantiate(fruit, gameObject.transform.position, fruit.transform.rotation);
            tolalSpawnerID++;
            
            fruitstate = false;
        }

        
        if (GetComponent<Rigidbody2D>().gravityScale <= 0)
        {

            FollowMouse();
        }
        rip.GetComponent<BoxCollider2D>().enabled = clickState;
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
    void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        if (worldMousePos.x > -2.8f && worldMousePos.x < 2.8f)
        {
            transform.position = new Vector3(worldMousePos.x, 4, transform.position.z);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("check"))
        {
            transform.position = new Vector2(transform.position.x, 4);
            GetComponent<Rigidbody2D>().gravityScale = 0;
            Invoke("Fruitstate", 1);

        }
    }

    void Fruitstate()
    {
        fruitstate = true;
    }

    void ClickState()
    {
        clickState = true;
    }
}
