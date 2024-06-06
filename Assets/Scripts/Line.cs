using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ゲームオーバーライン関係
public class Line : MonoBehaviour
{
    private float stayTime;//ゲームオーバーラインに触れている時間
    [SerializeField]  GameObject GameOverUI;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("seed"))//4fより長くlineに触れているとゲームオーバー
        {
            stayTime += Time.deltaTime;
            if (stayTime > 4.0f)
            {
                GameManager.Instance.isOver=true;
                GameOverUI.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("seed"))//触れている時間を初期化
        {
            stayTime = 0;
        }
    }
}
