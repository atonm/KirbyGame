using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seed : MonoBehaviour
{
    [SerializeField]  GameObject Right;
    private Rigidbody2D _rb;
    public bool isMergeFlag = false; //ものがくっつくか
    public bool isDrop = false; //落下するか
    public int seedNo; //モノ番号
    private float _xlimit; //左右移動できる範囲
    private float xpos; //x軸上のどこにいるか
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Transform myTransform = this.transform;
        Vector3 localScale = myTransform.localScale;
        _xlimit=localScale.x/2.0f; //左右移動できる範囲をどこまで制限するか
        Vector2 rightpos=Right.transform.position;
        xpos=rightpos.x;
    }

    // Update is called once per frame
    void Update()
    {   //ボタン操作
        if (GameManager.Instance.isOver==false){

        if (Input.GetKeyDown(KeyCode.Space) && isDrop == false) //モノを落とす
            Drop();
        if (isDrop) return;
        
        if (Input.GetKey (KeyCode.LeftArrow)){ //左移動
            this.transform.Translate(-0.004f,0.0f,0.0f);
        }
         if (Input.GetKey (KeyCode.RightArrow)) { //右移動
            this.transform.Translate (0.004f,0.0f,0.0f);
        }
        
        //モノの位置、動ける範囲
        var pos = transform.position;
        pos.x=Mathf.Clamp(pos.x, -xpos+_xlimit+0.16f, xpos-_xlimit-0.16f);
        pos.y=4.0f;
        transform.position=pos;
        }
        
        
        /*
        //マウス操作
        if (Input.GetMouseButton(0) && isDrop == false)
            Drop();
        if (isDrop) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Mathf.Clamp(mousePos.x, -2.7f, 2.7f);//要変更
        mousePos.y = 3.5f;
        transform.position = mousePos;*/
    }
    private void Drop() //モノを落とす
    {
        isDrop = true;
        _rb.simulated = true;
        GameManager.Instance.isNext=true;
    }

    private void OnCollisionEnter2D(Collision2D collision) //モノがぶつかった時
    {
        GameObject colobj = collision.gameObject;
        if (colobj.CompareTag("seed"))
        {
            seed colseed = collision.gameObject.GetComponent<seed>();
            /*if (seedNo == colseed.seedNo && 
                !isMergeFlag && 
                !colseed.isMergeFlag && 
                seedNo < GameManager.Instance.MaxSeedNo - 1) 
            {
                isMergeFlag = true;
                colseed.isMergeFlag = true;
                GameManager.Instance.MergeNext(transform.position, seedNo);
                Destroy(gameObject);
                Destroy(colseed.gameObject);
            }*/
            if (seedNo == colseed.seedNo &&  //同じ大きさのものがくっついた時
                !isMergeFlag && 
                !colseed.isMergeFlag) 
            {
                isMergeFlag = true;
                colseed.isMergeFlag = true;
                
               if (seedNo < GameManager.Instance.MaxSeedNo - 1)//次に大きいサイズのものを生成
                {
                GameManager.Instance.MergeNext(transform.position, seedNo);
                }
                else if (seedNo == GameManager.Instance.MaxSeedNo-1)//最大同士がくっついたら最小のものを生成
                {
                GameManager.Instance.MergeNext(transform.position, seedNo);
                
                } Destroy(gameObject);
                Destroy(colseed.gameObject);
            }
        }
    }
}
