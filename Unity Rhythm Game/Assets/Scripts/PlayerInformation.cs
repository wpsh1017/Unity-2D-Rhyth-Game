using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public static class PlayerInformation
{
    public static int maxCombo { get; set; }
    public static float score { get; set; }
    public static string selectedMusic { get; set; }
    public static string musicTitle { get; set; }
    public static string musicArtist { get; set; }
    public static Firebase.Auth.FirebaseAuth auth; // 씬이 전환되면 정보가 사라지기 때문에 이를 저장하기 위한 변수 

    public static DatabaseReference GetDatabaseReference()
    {
        // 데이터베이스 접속 설정하기
        DatabaseReference reference;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-rhythm-game-tutori-17ce3.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference;
    }
}
