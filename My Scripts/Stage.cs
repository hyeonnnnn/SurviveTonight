using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stage : MonoBehaviour
{
    const int totalStage = 5;
    public int[] spawnCount = new int[totalStage];
    public int currentStage = 1;

    [SerializeField] Timer timer;
    [SerializeField] DayAndNight dayAndNight;
    [SerializeField] TextMeshProUGUI stageText;
    private bool isStageIncreased = false;

    void Start()
    {
        StartCoroutine(CheckDayNight());
    }

    IEnumerator CheckDayNight()
    {
        while (true)
        {
            // ��ħ�̸�
            if (!dayAndNight.isNight && !isStageIncreased)
            {
                timer.timerText.gameObject.SetActive(true); // Ÿ�̸� ����
                stageText.text = currentStage + " Stage";
                currentStage++;
                isStageIncreased = true;
            }

            // ���̸�
            else if (dayAndNight.isNight)
            {
                isStageIncreased = false;
            }

            // ���������� ������
            if (currentStage > totalStage)
            {
                Debug.Log("���� �¸�");
                yield break; // �ڷ�ƾ ����
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}