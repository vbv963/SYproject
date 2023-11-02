using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpee = 1.0f;
    public float SpeedMod = 1.0f;
    public float timeSinceStart = 0.0f;


    public float moveSpeed; //�� �̵��ӵ�

    private MonsterPath thePath;  //�н����� ��
    private int currentPoint;     //�н� ��ġ Ŀ����
    private bool reachedEnd;      //���� �Ϸ�˻� bool ��

    private bool modEnd = true;

    // Start is called before the first frame update
    void Start()
    {
        if (thePath == null)  //������ �� Path�� ������
        {
            thePath = FindObjectOfType<MonsterPath>();   //Scene���� ã�´�.
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

        if (reachedEnd == false)        //���� �Ϸᰡ �ƴ� ���
        {
            transform.LookAt(thePath.points[currentPoint]);   //���� ��ġ Ŀ������ ���ؼ� ����.

            transform.position = Vector3.MoveTowards(transform.position, thePath.points[currentPoint].position, moveSpeed * Time.deltaTime * SpeedMod);

            //���� �н� ����Ʈ ��ġ�� �Ÿ��� ����ؼ� 0.01 �����ϰ�� ����
            if (Vector3.Distance(transform.position, thePath.points[currentPoint].position) < 0.01f)
            {
                currentPoint += 1;  //������ġ Ŀ���� �ű��

                if (currentPoint >= thePath.points.Length)       //�� �������� ����
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
