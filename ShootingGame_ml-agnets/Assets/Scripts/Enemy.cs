using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float m_velocity;

    [SerializeField]
    private float m_rotateSpeed;

    private Quaternion m_rotation;

    [SerializeField]
    private Rigidbody m_rigidbody;

    private DestroyCounter m_destroyCounter;

    // Start is called before the first frame update
    void Start()
    {
        // ランダムに回転するようにする
        Vector3 randomAxis = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        m_rotation = Quaternion.AngleAxis(m_rotateSpeed * Mathf.Deg2Rad, randomAxis);

        // 前に進める、回転させる
        m_rigidbody.velocity = new Vector3(0, 0, -m_velocity);
        m_rigidbody.angularVelocity = randomAxis.normalized * m_rotateSpeed * Mathf.Deg2Rad;
    }

    public void SetDestroyCounter(DestroyCounter destroyCounter) {
        m_destroyCounter = destroyCounter;
    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Bullet")) {
            Destroy(this.gameObject);
            m_destroyCounter.AddDestroyCount(true);     // 加点
        }
        else if(other.CompareTag("DestroyArea")) {
            Destroy(this.gameObject);
            m_destroyCounter.AddDestroyCount(false);    // 減点
        }

    }

}
