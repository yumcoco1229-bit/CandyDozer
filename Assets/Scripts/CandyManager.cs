using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 10;

    public int candy = DefaultCandyAmount;
    int counter;
    public TextMeshProUGUI amountText;
    public void ConsumeCandy()
    {
        if (candy > 0) candy--;
    }
    //getter
    public int GetCandyAmount()
    {
        return candy;
    }
    public void AddCandy(int amount)
    {
        candy += amount;
    }
    //簡易的な表示
    // void OnGUI()
    // {
    //     GUI.color = Color.black;
    //     string label = "Candy : " + candy;
    //     if (counter > 0) label = label + "(" + counter + "s)";
    //     GUI.Label(new Rect(50, 50, 100, 30), label);
    // }
    void DrawAmout()
    {
        string label = "Candy : " + candy;
        if (counter > 0) label = label + "(" + counter + "s)";
        amountText.text = label;
   }
    void Update()
    {
        if (candy < DefaultCandyAmount && counter <= 0)
        {
            StartCoroutine(RecoverCandy());
        }
        DrawAmout();
    }
    IEnumerator RecoverCandy()
    {
        counter = RecoverySeconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }
        candy++;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    
}
