using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ATM : MonoBehaviour
{
    public Control Control_s;
    int Gold = 0;
    public TextMeshProUGUI Gold_TMP_s;
    public GameObject[] ATM_Buttons;

    private void Start()
    {
        SetGold( 3000 );
    }
    public void SetGold( int Amount )
    {
        Gold += Amount;
        Gold_TMP_s.text = Gold.ToString( "n0" );
    }
    public int GetGold => Gold;

    public void Button_Deposit()
    {
        if( Control_s.Player_s.GetGold >= 1000 )
        {
            Control_s.Player_s.GetSetGold( -1000 );
            SetGold( 1000 );
        }
    }
    public void Button_Withdraw()
    {
        if( Gold >= 1000 )
        {
            SetGold( -1000 );
            Control_s.Player_s.GetSetGold( 1000 );
        }
    }
}
