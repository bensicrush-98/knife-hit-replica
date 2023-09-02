using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;
    private bool isActive = true;
    private Rigidbody2D rb;
    private BoxCollider2D knifeCollider2D;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knifeCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            GameControllerScript.Instance.GameUI.DecrementDisplayKnifeCount();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive) return;
        isActive = false;
        if (collision.collider.tag == "Log")
        {
            GetComponent<ParticleSystem>().Play();
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            this.transform.SetParent(collision.collider.transform);
            knifeCollider2D.offset = new Vector2(knifeCollider2D.offset.x, -0.4f);
            knifeCollider2D.size = new Vector2(knifeCollider2D.size.x, 1.2f);
            GameControllerScript.Instance.OnSuccessfullKnifeHit();
        }
        else if (collision.collider.tag == "Knife")
        {
            rb.velocity = new Vector2(rb.velocity.x, -2.0f);
            GameControllerScript.Instance.StartGameOverSequence(false);
        }
    }
}