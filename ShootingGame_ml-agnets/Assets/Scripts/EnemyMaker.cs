    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    [SerializeField]
    private float m_width;

    [SerializeField]
    private float m_respawnInterval;

    [SerializeField]
    private float m_respawnPositionZ;

    [SerializeField]
    private Enemy m_enemyPrefab;

    private float m_time = 0;

    [SerializeField]
    private DestroyCounter m_destroyCounter;

    [SerializeField]
    private Transform m_ShootintGameTransform;

    void Update()
    {

        m_time += Time.deltaTime;

        if(m_time >= m_respawnInterval) {
            m_time = 0;

            InstantiateEnemy();

        }

    }

    private void InstantiateEnemy() {

        Vector3 position = transform.position + new Vector3(Random.Range(-m_width, m_width), 0, m_respawnPositionZ);
        var enemy = Instantiate(
            m_enemyPrefab, 
            position, 
            Quaternion.Euler(Random.Range(0, 360f), Random.Range(0, 360f), Random.Range(0, 360f)),
            m_ShootintGameTransform);
        enemy.SetDestroyCounter(m_destroyCounter);

    }

}
