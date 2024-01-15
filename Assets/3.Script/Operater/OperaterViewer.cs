using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Operater
{
    Addition = 0, Subtraction, Multiplication, Division
}
public class OperaterViewer : MonoBehaviour
{

    private OperaterCreater operater;
    private Text text;
    private string oper_str;
    private int num;
    private int oper_num;
    private int oper_rate;

    public void SetUp(OperaterCreater operater)
    {
        this.operater = operater;
        TryGetComponent(out text);
        oper_rate= Random.Range(0, 100);
        if(oper_rate>60) //40%
        {
            oper_num = 0;
        }
        else if(oper_rate > 20) //40%
        {
            oper_num = 1;
        }
        else if (oper_rate > 10) //10%
        {
            oper_num = 2;
        }
        else//10%
        {
            oper_num = 3;
        }

        oper_str = OperaterToString((Operater)oper_num);
        if(oper_num>1)
        {
            num = Random.Range(2, 6);
        }
        else
        {
            num = Random.Range(1, 10);
        }
        text.text = $"{oper_str} {num}";
    }
    private string OperaterToString(Operater operater_type)
    {
        string str = string.Empty;

        switch (operater_type)
        {
            case Operater.Addition:
                str = "+";
                break;
            case Operater.Subtraction:
                str = "-";
                break;
            case Operater.Multiplication:
                str = "¡¿";
                break;
            case Operater.Division:
                str = "¡À";
                break;
        }
        return str;
    }
    public int GetOperater()
    {
        return oper_num;
    }
    public int GetNum()
    {
        return num;
    }

}
