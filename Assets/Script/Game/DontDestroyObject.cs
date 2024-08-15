using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    static List<string> dontdestroy_objects = new List<string>();
    void Awake()
    {
        //씬에 자기 자신이 있는지 확인 후 있으면 이 오브젝트 삭제
        if(dontdestroy_objects.Contains(gameObject.name))
        {
            Destroy(gameObject);
            return;
        }
        
        //자기자신을 리스트에 추가, 파괴되지 않게 함
        dontdestroy_objects.Add(gameObject.name);
        DontDestroyOnLoad(gameObject);
    }
}
