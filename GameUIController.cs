using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
 struct TextNumber
{
    public Text CoinNum;
    public Text HpNum;
    public Text DiamondNum;
}
/*
 游戏UI控制类
     管理游戏UI界面按钮的类
	 这段代码用来控制Text的数字动态的变化
	 使用dotween，更简单方便。
     */
public class GameUIController : MonoBehaviour {
    #region Inspector
    [SerializeField]
    private TextNumber textNumber;
    #endregion


    #region Controlling Variables
    private bool allowAddNumber=true;
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	
    //增加金币
    public void AddCoin(int number) {
        if (allowAddNumber)
            StartCoroutine(IncreaseCoinContinuously(number));
    }
    private IEnumerator IncreaseCoinContinuously(int number) {
        float num = float.Parse(textNumber.CoinNum.text);
        float before = num;
        if (number < 0)
        {
            while (num >before + number&&num>=0)
            {
                num = Mathf.Lerp(num, num + number, Time.deltaTime);
                textNumber.CoinNum.text = ((int)num).ToString();
                Debug.Log(textNumber.CoinNum.text);
                allowAddNumber = false;
                yield return null;
            }
            num = Mathf.Clamp(before + number, 0, before + number);
            textNumber.CoinNum.text = ((int)num).ToString();
            allowAddNumber = true;
        }
        else if (number>0) {
            while (num < before + number)
            {
                num = Mathf.Lerp(num, num + number, Time.deltaTime);
                textNumber.CoinNum.text = ((int)num).ToString();
                Debug.Log(textNumber.CoinNum.text);
                allowAddNumber = false;
                yield return null;
            }

            num = Mathf.Clamp(before + number, before + number, num + number);
            textNumber.CoinNum.text = ((int)num).ToString();
            allowAddNumber = true;
        }



    }

    //增加血量
    public void AddHp(int number) {
        if (allowAddNumber)
            StartCoroutine(IncreaseHpContinuously(number));
    }
    private IEnumerator IncreaseHpContinuously(int number)
    {
        float num = float.Parse(textNumber.HpNum.text);
        float before = num;
        if (number > 0)
        {
            while (num < before + number)
            {
                yield return null;
                num = Mathf.Lerp(num, num + number, Time.deltaTime);
                textNumber.HpNum.text = ((int)num).ToString();
                Debug.Log(textNumber.HpNum.text);
                allowAddNumber = false;
            }
            num = Mathf.Clamp(before + number, before + number, num + number);
            textNumber.HpNum.text = ((int)num).ToString();
            allowAddNumber = true;
        }

        else if (number < 0) {
            while (num > before + number&&num>=0)
            {
                yield return null;
                num = Mathf.Lerp(num, num + number, Time.deltaTime);
                textNumber.HpNum.text = ((int)num).ToString();
                Debug.Log(textNumber.HpNum.text);
                allowAddNumber = false;
            }
            num = Mathf.Clamp(before + number, 0, before + number);
            textNumber.HpNum.text = ((int)num).ToString();
            allowAddNumber = true;
        }

     
    }

    //增加钻石
    public void AddDiamond(int number) {
        if(allowAddNumber)
            StartCoroutine(IncreaseDiamondContinuously(number));
    }
    private IEnumerator IncreaseDiamondContinuously(int number)
    {
        float num = float.Parse(textNumber.DiamondNum.text);
        float before = num;
        if (number > 0)
        {
            while (num < before + number)
            {
                yield return null;
                num = Mathf.Lerp(num, num + number, Time.deltaTime);
                textNumber.DiamondNum.text = ((int)num).ToString();
                Debug.Log(textNumber.DiamondNum.text);
                allowAddNumber = false;
            }
            num = Mathf.Clamp(before + number, before + number, num + number);
            textNumber.DiamondNum.text = ((int)num).ToString();
            allowAddNumber = true;
        }
        else if (number<0) {
            while (num > before + number&&num>=0)
            {
                yield return null;
                num = Mathf.Lerp(num, num + number, Time.deltaTime);
                textNumber.DiamondNum.text = ((int)num).ToString();
                Debug.Log(textNumber.DiamondNum.text);
                allowAddNumber = false;
            }
            num = Mathf.Clamp(before + number, 0, before + number);
            textNumber.DiamondNum.text = ((int)num).ToString();
            allowAddNumber = true;
        }
        
    }
//持续动态增数字，用dotween更快更方便
    public void AccelerateNumber(int num, Text text)
    {
        if (text.text.Contains("/")|| text.text==null)
        {
            text.text = "0";
        }

        int textV = int.Parse(text.text);
        DOTween.To(()=>textV,x=>textV=x, Mathf.Clamp(num,0,999999),0.5f).OnUpdate(delegate {

            text.text = textV.ToString();
        });
    }
}
