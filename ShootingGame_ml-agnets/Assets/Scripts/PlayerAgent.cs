using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlayerAgent : Agent
{

    [SerializeField]
    private GameObject m_bullet;

    [SerializeField]
    private float m_shotIntervalTime;

    [SerializeField]
    private float m_moveSpeed;

    [Header("動ける幅の絶対値")]
    [SerializeField]
    private float m_movableWidth;

    [SerializeField]
    private DestroyCounter m_destroyCounter;

    private float m_time = 0;

    [SerializeField]
    private Transform m_ShootingGameTransform;

    public override void Initialize()
    {
        m_time = 0;
    }

    public override void OnEpisodeBegin() {
        m_destroyCounter.Reset();
    }

    public override void OnActionReceived(float[] vectorAction) {

        int moveOperation = (int)vectorAction[0];
        int shotOperation = (int)vectorAction[1];

        Move(moveOperation);
        Shot(shotOperation);
    }

    private void Move(int operation)
    {

        Vector3 position = transform.localPosition;

        if (operation == 1)
            position.x = Mathf.Min(position.x + m_moveSpeed * Time.deltaTime, m_movableWidth);
        else if (operation == 2)
            position.x = Mathf.Max(position.x - m_moveSpeed * Time.deltaTime, -m_movableWidth);

        transform.localPosition = position;

    }

    private void Shot(int operation)
    {

        m_time += Time.deltaTime;

        // インターバル中もしくは弾を発射しない場合は処理を行わない
        if (m_time < m_shotIntervalTime || operation == 0)
            return;

        var newBullet = Instantiate(m_bullet, transform.position, Quaternion.identity, m_ShootingGameTransform);
        Destroy(newBullet, 2f);
        m_time = 0;

    }

    public override void Heuristic(float[] actionsOut) {
        
        actionsOut[0] = 0;
        actionsOut[1] = 0;

        if(Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            actionsOut[0] = 1;
        else if(Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            actionsOut[0] = 2;
        
        if(Input.GetKey(KeyCode.Space))
            actionsOut[1] = 1;

    }

}
