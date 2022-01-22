#region << 文 件 说 明 >>

/*----------------------------------------------------------------
// 文件名称：Launcher
// 创 建 者：keeee
// 创建时间：2022年01月22日 星期六 14:21
// 文件版本：V1.0.0
//===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/

#endregion

using UnityEngine;

public class Launcher:MonoBehaviour
{
    void Awake()
    {
           
        Object[] initsObjects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (Object go in initsObjects) {
            DontDestroyOnLoad(go);
        }  
        GameManager.instance.BackSelect();
    }
}