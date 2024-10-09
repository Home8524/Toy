using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    [SerializeField]
    Transform objectPool;

    //오브젝트 생성관련 타이머들
    [SerializeField]
    float regenTime;
    float regenUpdateTime;

    //오브젝트 생성위치
    Transform sponSpot;

    //생성될 오브젝트들을 관리하는 컨테이너
    GameObject obstaclePrefab;
    Stack<GameObject> obstaclePrefabContainer;
    List<MoveObject> moveObjectList;

    Text timerText;
    Button optionButton;

    bool activeGame;

    void Start()
    {
        sponSpot = Utils.FindComp<Transform>(transform, "SponObject");
        timerText = Utils.FindComp<Text>(transform, "UiCanvas/Timer");
        optionButton = Utils.FindComp<Button>(transform, "UiCanvas/Button");

        obstaclePrefabContainer = new Stack<GameObject>();
        moveObjectList = new List<MoveObject>();

        Init();
    }

    private void Init()
    {
        LoadPrefab();
        CreateObjectPool();

        ReadyAction();
    }

    void LoadPrefab()
    {
        obstaclePrefab = Resources.Load("Prefab/ObstacleObject") as GameObject;
    }

    void CreateObjectPool()
    {
        for(int i = 0; i < 20; i++)
        {
            var go = Instantiate(obstaclePrefab);
            go.transform.SetParent(objectPool);
            var moveObject = go.GetComponent<MoveObject>();
            moveObject.Init();
            obstaclePrefabContainer.Push(go);
        }
    }

    private void ReadyAction()
    {
        optionButton.gameObject.SetActive(false);
        activeGame = false;

        StartCoroutine(coReadyAction());
    }

    private IEnumerator coReadyAction()
    {
        float updateTime = 0f;

        while (true)
        {
            updateTime += Time.deltaTime;

            if (updateTime >= 1.2f)
            {
                timerText.gameObject.SetActive(false);
                break;
            }
            else if (updateTime >= 1.0f)
                timerText.text = "시작!";
            else if (updateTime >= 0.75f)
                timerText.text = "1";
            else if (updateTime >= 0.5f)
                timerText.text = "2";
            else if (updateTime >= 0.25f)
                timerText.text = "3";

            yield return new WaitForSeconds(Time.deltaTime);
        }

        optionButton.gameObject.SetActive(true);
        activeGame = true;
        RegenObject();
    }

    void Update()
    {
        if (activeGame == false) return;

        regenUpdateTime += Time.deltaTime;

        MoveObject();

        if (regenUpdateTime < regenTime) return;

        regenUpdateTime = 0;
        RegenObject();
    }

    void MoveObject()
    {
        var copyList = new List<MoveObject>(moveObjectList);

        foreach(var ob in copyList)
        {
            ob.MoveUpdate();
        }
    }

    void RegenObject()
    {
        if (obstaclePrefabContainer.Count < 2) CreateObjectPool();

        var ob1 = obstaclePrefabContainer.Pop();

        bool answer = true; // testcode - 이후 문제 데이터에서 문제를 뽑아오는 형식으로 변경

        MoveObject moveObject1 = Utils.FindComp<MoveObject>(ob1.transform, "");

        ob1.transform.SetParent(sponSpot);

        moveObject1.SetData(answer, () => { moveObjectList.Remove(moveObject1); ob1.transform.SetParent(objectPool); });

        moveObjectList.Add(moveObject1);
    }
}
