using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // 이중 리스트 사용하기 
    // Note 1: 10개=> 리스트 1
    // Note 2: 10개 => 리스트 2
    // Note 3  10개 => 리스트 3
    //...

    public List<GameObject> Notes;
    private List<List<GameObject>> poolsOfNotes;
    public int noteCount = 10;
    private bool more = true;

    // Start is called before the first frame update
    void Start()
    {
        poolsOfNotes = new List<List<GameObject>>();    // 비어있는 리스트들의 리스트 생성
        for(int i = 0;i < Notes.Count; i++) // 4번 반복
        {
            poolsOfNotes.Add(new List<GameObject>());   // 빈 리스트를 원소로 추가 
            for(int n = 0; n < noteCount; n++) // 10번 반복 
            {
                GameObject obj = Instantiate(Notes[i]); // Notes의 i 번째 원소를 생성 obj에 할당
                obj.SetActive(false);                               // obj 비활성화
                poolsOfNotes[i].Add(obj);                       // poolsOfNotes의 i번째 원소에 obj 추가
            }
        }
    }
    public void Judge(int noteType)
    {
        foreach(GameObject obj in poolsOfNotes[noteType - 1])
        {
            if (obj.activeInHierarchy)
            {
                obj.GetComponent<NoteBehaviour>().Judge();
            }
        }
    }
    public GameObject getObject(int noteType)
    {
        foreach(GameObject obj in poolsOfNotes[noteType - 1])
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        if (more)
        {
            GameObject obj = Instantiate(Notes[noteType - 1]);
            poolsOfNotes[noteType - 1].Add(obj);
            return obj;
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
