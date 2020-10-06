using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float m_velocity;

    [SerializeField]
    private Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // 前に進むようにする
        m_rigidbody.velocity = new Vector3(0, 0, m_velocity);
    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Enemy"))
            Destroy(gameObject);

    }

}
