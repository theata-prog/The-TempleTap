using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OrbManager : MonoBehaviour
{
    private GameObject gameManager;
    public Sprite[] orbPicture = new Sprite[3]; // オーブの数
    public enum ORB_KIND{   //オーブの種類を定義
        BLUE,
        GREEN,
        PURPLE
    }
    private ORB_KIND orbKind; //オーブの種類

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddOrbpoint() {
        switch (orbKind)
        {
            case ORB_KIND.BLUE:
                gameManager.GetComponent<GameManager>().GetOrb(1);
                break;
            case ORB_KIND.GREEN:
                gameManager.GetComponent<GameManager>().GetOrb(5);
                break;
            case ORB_KIND.PURPLE:
                gameManager.GetComponent<GameManager>().GetOrb(10);
                break;
        }

        Destroy(this.gameObject);
    }
    //オーブの種類を設定
    public void SetKind(ORB_KIND kind){
        orbKind = kind;

        switch (orbKind) {
            case ORB_KIND.BLUE:
                GetComponent<Image>().sprite = orbPicture[0];
                break;
            case ORB_KIND.GREEN:
                GetComponent<Image>().sprite = orbPicture[1];
                break;
            case ORB_KIND.PURPLE:
                GetComponent<Image>().sprite = orbPicture[2];
                break;
        }
    }
    //オーブが飛ぶ
    public void FlyOrb() {
        RectTransform rect = GetComponent<RectTransform>();

        //オーブの軌跡設定
        Vector3[] path = {
            new Vector3(rect.localPosition.x * 4.0f, 300f, 0f),
            new Vector3(0f, 250f, 0f),
        };

        //DOTweenを使ったアニメーション作成
        rect.DOLocalPath(path, 0.5f, PathType.CatmullRom)
            .SetEase(Ease.OutQuad)
            .OnComplete(AddOrbpoint);

        //同時にサイズも変更
        rect.DOScale(
            new Vector3(0.5f, 0.5f, 0f),
            0.5f
        );
    }
}
