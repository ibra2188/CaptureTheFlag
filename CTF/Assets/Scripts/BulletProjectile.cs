using System;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    public Transform enemyHit;
    public Transform normalHit;
    public Rigidbody bulletRigidBody;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 40f;
        bulletRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.GetComponent<EnemyTarget>() != null)
            {
                Instantiate(enemyHit, transform.position, Quaternion.identity);
                if (PlayerPrefs.GetInt("MyChoice") == 2)
                {
                    other.GetComponent<Health>().TakeDamage((int)(20 * 1.5));
                }
                else
                {
                    other.GetComponent<Health>().TakeDamage(20);
                }
            }
        else {
                Instantiate(normalHit, transform.position, Quaternion.identity);
        }
    }
}
