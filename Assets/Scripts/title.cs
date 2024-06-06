using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    // Start is called before the first frame updatepublic void Update()
    public void Update()
    {
        //エンターでスタート
         if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("GameScene");   
        }
    }
}
