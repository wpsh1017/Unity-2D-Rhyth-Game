﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    // Game Manager를 싱글톤 처리 
    public static GameManager instance { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) Destroy(gameObject);
    }

    public float noteSpeed;

    public GameObject scoreUI;
    public float score;
    private Text scoreText;

    public GameObject comboUI;
    private int combo;
    private Text comboText;
    private Animator comboAnimator;
    public int maxCombo;

    public enum judges { NONE = 0,  BAD,  GOOD,  PERFECT,  MISS };
    public GameObject judgeUI;
    private Sprite[] judgeSprites;
    private Image judgementSpriteRenderer;
    private Animator judgementSpriteAnimator;

    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderers;

    //음악 변수
    private AudioSource audioSource;

    // 판정 모드 변수
    public bool autoPerfect;

    // 음악을 실행할 함수입니다.
    void MusicStart()
    {
        // 리소스에서 비트 음악 파일을 불러와 재생합니다.
        AudioClip audioClip = Resources.Load<AudioClip>("Beats/" + PlayerInformation.selectedMusic);
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = noteObjectPooler.GetComponent<ObjectPooler>();
        Invoke("MusicStart", 2);
        judgementSpriteRenderer = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        // 판정 결과를 보여주는 스프라이트 이미지를 미리 초기화합니다.
        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        trailSpriteRenderers = new SpriteRenderer[trails.Length];
        for(int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }

    public GameObject noteObjectPooler;
    private ObjectPooler objectPooler;

    // Update is called once per frame
    void Update()
    {

        // 터치가 한 개 이상 발생하고 있다면
        if(Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch tempTouch = Input.GetTouch(i);
                if (tempTouch.phase == TouchPhase.Began)
                {

                    Ray ray = Camera.main.ScreenPointToRay(tempTouch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (hit.collider.name == "Trail 1")
                        {
                            ShineTrail(0);
                            objectPooler.Judge(1);
                        }
                        if (hit.collider.name == "Trail 2")
                        {
                            ShineTrail(1);
                            objectPooler.Judge(2);
                        }
                        if (hit.collider.name == "Trail 3")
                        {
                            ShineTrail(2);
                            objectPooler.Judge(3);
                        }
                        if (hit.collider.name == "Trail 4")
                        {
                            ShineTrail(3);
                            objectPooler.Judge(4);
                        }
                    }
                }
            }
        }
        
        // 사용자가 입력한 키에 해당하는 라인을 빛나게 처리합니다.
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(1);
        if (Input.GetKey(KeyCode.J)) ShineTrail(2);
        if (Input.GetKey(KeyCode.K)) ShineTrail(3);
        // 한 번 빛나게 된 라인은 반복적으로 다시 어둡게 처리합니다.
        for(int i = 0; i < trailSpriteRenderers.Length; i++)
        {
            Color color = trailSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailSpriteRenderers[i].color = color;
        }
    }

    // 특정한 키를 눌러 해당 라인을 빛나게 처리합니다.
    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderers[index].color;
        color.a = 0.32f;
        trailSpriteRenderers[index].color = color;
    }

    // 노트 판정 이후에 판정 결과를 화면에 보여줍니다.
    void ShowJudgement()
    {
        // 점수 이미지를 보여줍니다. 
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);
        // 판정 이미지를 보여줍니다.
        judgementSpriteAnimator.SetTrigger("Show");
        //콤보가 2 이상일 때만 콤보 이미지를 보여줍니다.
        if(combo >= 2)
        {
            comboText.text = "COMBO " + combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
        if(maxCombo < combo)
        {
            maxCombo = combo;
        }
    }

    // 노트 판정을 진행합니다.
    public void ProcessJudge(judges judge, int noteType)
    {
        if (judge == judges.NONE) return;
        // MISS 판정을 받은 경우 콤보를 종료하고, 점수를 많이 깎습니다.
        if(judge == judges.MISS)
        {
            judgementSpriteRenderer.sprite = judgeSprites[2];
            combo = 0;
            if (score >= 15) score -= 15;
        }
        // BAD 판정을 받은 경우 콤보를 종료하고, 점수를 조금 깎습니다. 
        else if(judge == judges.BAD)
        {
            judgementSpriteRenderer.sprite = judgeSprites[0];
            combo = 0;
            if (score >= 5) score -= 5;
        }
        // PERFECT 혹은 GOOD 판정을 받은 경우 콤보 및 점수를 올립니다. 
        else
        {
            if(judge == judges.PERFECT)
            {
                judgementSpriteRenderer.sprite = judgeSprites[3];
                score += 20;
            }else if(judge == judges.GOOD)
            {
                judgementSpriteRenderer.sprite = judgeSprites[1];
                score += 15;
            }
            combo += 1;
            score += (float)combo * 0.1f;
        }
        ShowJudgement();
    }
}
