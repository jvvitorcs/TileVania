using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody;
    [SerializeField] BoxCollider2D bC2d;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsfacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);

        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    bool IsfacingRight()
    {
        return transform.localScale.x > 0;
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    /*public void Die()
    {

        Destroy(gameObject);
        Vector2 jumpVelocityToAdd = new Vector2(0f, 10f);
        myRigidBody.velocity += jumpVelocityToAdd;
        myRigidBody.bodyType = RigidbodyType2D.Dynamic;
        bC2d.isTrigger = true;

    }*/
}
