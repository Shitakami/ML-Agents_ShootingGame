using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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

    private float m_time;

    // Start is called before the first frame update
    void Start()
    {
        m_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
            Move(1);
        else if(!Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            Move(2);
        
        if(Input.GetKey(KeyCode.Space))
            Shot(1);
        else
            Shot(0);

    }

    private void Move(int operation) {

        Vector3 position = transform.localPosition;
        
        if(operation == 1)
            position.x = Mathf.Min(position.x + m_moveSpeed * Time.deltaTime, m_movableWidth);
        else if(operation == 2)
            position.x = Mathf.Max(position.x - m_moveSpeed * Time.deltaTime, -m_movableWidth);

        transform.localPosition = position;

    }

    private void Shot(int operation) {

        m_time += Time.deltaTime;

        // インターバル中もしくは弾を発射しない場合は処理を行わない
        if(m_time < m_shotIntervalTime || operation == 0)
            return;
        
        var newBullet = Instantiate(m_bullet, transform.position, Quaternion.identity);
        Destroy(newBullet, 2f);
        m_time = 0;

    }

}
