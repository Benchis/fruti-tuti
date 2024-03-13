using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject[] fruitlist;
    private GameObject fruit;
    private bool fruitstate = true;
    private int num;

    void Update()
    {
        if (fruitstate)
        {
            num = Random.Range(0, 5);
            fruit = fruitlist[num];
            Instantiate(fruit, gameObject.transform.position, fruit.transform.rotation);

            fruitstate = false;
        }

        FollowMouse();

        if (Input.GetMouseButtonUp(0))
        {
            Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
            rb.gravityScale = 1;
            rb.velocity = (transform.position - fruit.transform.position).normalized * 10f; // Launch the fruit
        }
    }

    void FollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        fruit.transform.position = new Vector3(worldMousePos.x, worldMousePos.y, fruit.transform.position.z);
    }
}
