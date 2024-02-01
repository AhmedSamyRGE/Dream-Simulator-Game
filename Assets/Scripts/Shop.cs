using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public Control Control_s;
    public List<int> ShopDataBase;

    public void ViewShopItems()
    {
        for( int i = 0 ; i < ShopDataBase.Count ; i++ )
        {
            Transform Item_i = Instantiate( Control_s.Prefab_UIItem , parent: Control_s.ShopViewList_Rect ).transform;
            Item_i.GetChild( 0 ).GetComponent<Image>().sprite = Control_s.ItemDB.DB[ ShopDataBase[ i ] ].sprite;
            Item_i.GetChild( 1 ).GetComponent<TextMeshProUGUI>().text = Control_s.ItemDB.DB[ ShopDataBase[ i ] ].ItemName;
            Item_i.GetChild( 3 ).GetComponent<TextMeshProUGUI>().text = Control_s.ItemDB.DB[ ShopDataBase[ i ] ].ItemPrice + "";

            int OutPut = ShopDataBase[ i ];
            Item_i.GetComponent<Button>().onClick.AddListener( () => OnPlayerBuyItem( OutPut ) );
        }
    }
    public void OnPlayerBuyItem( int ItemIndex )
    {
        int Cost = Control_s.ItemDB.DB[ ItemIndex ].ItemPrice;

        if( Control_s.Player_s.GetGold >= Cost )
        {
            Control_s.Player_s.GetSetGold( -Cost );
          
            Control_s.Player_s.AddItem( ItemIndex );
            RemoveItem( ItemIndex );
        }
    }
    public void RefeashView()
    {
        Control_s.ClearShopView();
        ViewShopItems();
    }

    public void AddItem( int ItemIndex )
    {
        ShopDataBase.Add( ItemIndex );
        RefeashView();
    }
    public void RemoveItem( int ItemIndex )
    {
        ShopDataBase.Remove( ItemIndex );
        RefeashView();
    }
}
