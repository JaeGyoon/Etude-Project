using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageBtn : MonoBehaviour
{
    public Image stageImage;
    public TextMeshProUGUI stageName;

    [SerializeField] private StageSO stageData;

    public void SetStage(StageSO data)
    {
        stageData = data;

        stageImage.sprite = data.stageImage;
        stageName.text = data.stageName;
    }

    public void OnClickStage()
    {
        Debug.Log($"선택한 스테이지 : {stageData.stageName}");

        StageManager.Instance.SelectStage(stageData);
        PlayerDataManager.Instance.currentSaveData.currentStage = stageData.stageNumber;
        PlayerDataManager.Instance.PlayerDataSave();

        UIManager.Instance.StageApply();
    }

}
