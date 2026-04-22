using EtudeProject;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{
    [Header("Stage")]
    public Transform stageContent;

    [SerializeField] private List<BasePopupUI> popupList;

    private Dictionary<UIType, BasePopupUI> popupDict = new Dictionary<UIType, BasePopupUI>();
    private Stack<BasePopupUI> popupStack = new Stack<BasePopupUI>();

    [Header("Btns")]
    public Image stageImage;
    public TextMeshProUGUI stageName;

    [Header("Hero")]
    public PreviewSpawner spawner;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //PopupRegist();
    }

    public void PopupRegist()
    {
        foreach (BasePopupUI popup in popupList)
        {
            popupDict.Add(popup.type, popup);
            popup.gameObject.SetActive(false);
            Debug.Log($"{popup.type}팝업 등록 ");
        }
    }

    public void OpenUI(UIType type)
    {
        
        // 방어코드
        if (!popupDict.ContainsKey(type))
        {
            Debug.Log($"해당 Key 없음 : {type}");
            return;
        }

        BasePopupUI popup = popupDict[type];

        // 이미 팝업창이 열려있다면
        if (popupStack.Contains(popup))
        {
            CloseUI(type);

            return;
        }

        popup.Open();
        popupStack.Push(popup);
    }

    public void CloseUI(UIType type)
    {
        if (popupStack.Count == 0)
        {
            return;
        }

        BasePopupUI popup = popupStack.Pop();
        popup.Close();
    }

    public void StageApply()
    {        
        StageSO so = StageManager.Instance.currentStageSO;

        stageImage.sprite = so.stageImage;
        stageName.text = so.stageName;

        Debug.Log("스테이지 적용!");
    }
}
