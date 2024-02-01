using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public Control Control_s;
    public TextMeshProUGUI Gold_TMP_s;
    int Gold = 0;

    public List<int> Inventory;

    private void Start()
    {
        GetSetGold( 1000 );
    }

    public void GetSetGold( int Amount )
    {
        Gold += Amount;
        Gold_TMP_s.text = Gold.ToString( "n0" );
    }
    public int GetGold => Gold;

    public void ViewPlayerItems()
    {
        for( int i = 0 ; i < Inventory.Count ; i++ )
        {
            Transform Item_i = Instantiate( Control_s.Prefab_UIItem , parent: Control_s.InventoryViewList_Rect ).transform;
            Item_i.GetChild( 0 ).GetComponent<Image>().sprite = Control_s.ItemDB.DB[ Inventory[ i ] ].sprite;
            Item_i.GetChild( 1 ).GetComponent<TextMeshProUGUI>().text = Control_s.ItemDB.DB[ Inventory[ i ] ].ItemName;
            Item_i.GetChild( 3 ).GetComponent<TextMeshProUGUI>().text = Control_s.ItemDB.DB[ Inventory[ i ] ].ItemPrice + "";

            int OutPut = Inventory[ i ];
            Item_i.GetComponent<Button>().onClick.AddListener( () => { OnPlayerSellItem( OutPut ); } );
        }
    }
    public void OnPlayerSellItem( int ItemIndex )
    {
        int Cost = Control_s.ItemDB.DB[ ItemIndex ].ItemPrice;

        Control_s.Player_s.GetSetGold( Cost );

        Control_s.CurrentShopInUse.AddItem( ItemIndex );
        RemoveItem( ItemIndex );
    }

    public void RefeashView()
    {
        Control_s.ClearInventoryView();
        ViewPlayerItems();
    }


    public void AddItem( int ItemIndex )
    {
        Inventory.Add( ItemIndex );
        RefeashView();
    }
    public void RemoveItem( int ItemIndex )
    {
        Inventory.Remove( ItemIndex );
        RefeashView();
    }
}
