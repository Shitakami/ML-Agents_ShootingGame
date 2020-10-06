using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCounter : MonoBehaviour
{

    [SerializeField]
    private int m_destroyCountPerEpisode;

    [SerializeField]
    private PlayerAgent m_playerAgent;

    private int m_destoryCount = 0;

    public void Reset() {
        m_destoryCount = 0;
    }

    public void AddDestroyCount(bool ToAddReward) {
        
        m_destoryCount++;

        // 報酬の計算を行う
        if(ToAddReward)
            m_playerAgent.AddReward(1.0f/m_destroyCountPerEpisode);
        else
            m_playerAgent.AddReward(-1.0f/m_destroyCountPerEpisode);

        // 指定された個数の敵を倒したら、1回分の訓練を終了
        if(m_destoryCount == m_destroyCountPerEpisode)
            m_playerAgent.EndEpisode();


    }

}
