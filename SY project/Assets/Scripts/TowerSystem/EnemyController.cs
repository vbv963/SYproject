using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpee = 1.0f;
    public float SpeedMod = 1.0f;
    public float timeSinceStart = 0.0f;


    public float moveSpeed; //적 이동속도

    private MonsterPath thePath;  //패스정보 값
    private int currentPoint;     //패스 위치 커서값
    private bool reachedEnd;      //도달 완료검사 bool 값

    private bool modEnd = true;

    // Start is called before the first frame update
    void Start()
    {
        if (thePath == null)  //시작할 때 Path가 없으면
        {
            thePath = FindObjectOfType<MonsterPath>();   //Scene에서 찾는다.
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(modEnd == false)
        {
            timeSinceStart -= Time.deltaTime;

            if(timeSinceStart <= 0.0f)
            {
                SpeedMod = 1.0f;
                modEnd = true;
            }
        }

        if (reachedEnd == false)        //도달 완료가 아닐 경우
        {
            transform.LookAt(thePath.points[currentPoint]);   //지금 위치 커서값을 향해서 본다.

            transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime * SpeedMod);

            //나와 패스 포인트 위치의 거리를 계산해서 0.01 이하일경우 도착
            if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) < 0.01f)
            {
                currentPoint += 1;  //다음위치 커서로 옮기고

                if (currentPoint >= thePath.points.Length)       //다 돌았으면 정지
                {
                    reachedEnd = true;
                }
            }
        }
    }
    public void SetMode(float value)
    {
        modEnd = false;
        SpeedMod = value;
        timeSinceStart = 2.0f;
    }
}
