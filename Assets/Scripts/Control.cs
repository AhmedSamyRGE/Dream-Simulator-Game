using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    #region Referance
    [Header( "Referance" )]
    [Space( 2 )]

    public GameObject ButtonHolderObj;
    public GameObject Prefab_UIItem;
    public GameObject ShopViewObj;
    public RectTransform ShopViewList_Rect;
    public RectTransform InventoryViewList_Rect;

    public Player Player_s;
    public Shop ShopTools_s;
    public Shop ShopFood_s;
    public ATM ATM_s;

    public Transform Camera_Transfrom;

    public ItemDatabase ItemDB;
    #endregion


    void Start()
    {
        StartCoroutine( CameraControl() );
        StartCoroutine( MainButtonAppear() );
    }

    #region Buttons

    public void Button_Bed()
    {
        CameraTargetPos = new Vector3( 2.851f , -0.365f , -2.211f );
        ExitAllWindows();

        StartCoroutine( PlayBedAnim() );
    }
    public void Button_ATM()
    {
        CameraTargetPos = new Vector3( 0.654f , 0.157f , -2.407f );
        ExitAllWindows();

        for( int i = 0 ; i < ATM_s.ATM_Buttons.Length ; i++ )
            ATM_s.ATM_Buttons[ i ].GetComponent<Button>().interactable = true;
    }
    public void Button_ShopTools()
    {
        CameraTargetPos = new Vector3( -0.832f , -0.281f , -3.104f );
        ExitAllWindows();

        ShopViewObj.SetActive( true );
        ShopTools_s.ViewShopItems();
        Player_s.ViewPlayerItems();

        CurrentShopInUse = ShopTools_s;
    }
    public void Button_ShopFood()
    {
        CameraTargetPos = new Vector3( -2.17f , -0.281f , -2.987f );
        ExitAllWindows();

        ShopViewObj.SetActive( true );
        ShopFood_s.ViewShopItems();
        Player_s.ViewPlayerItems();

        CurrentShopInUse = ShopFood_s;
    }

    public void Button_ReturnToMenu()
    {
        CameraTargetPos = new Vector3( 0.67f , 0.48f , -5.66f );

        ExitAllWindows();
        ButtonHolderObj.SetActive( true );
    }
    void ExitAllWindows()
    {
        ClearShopView();
        ClearInventoryView();

        ShopViewObj.SetActive( false );
        ButtonHolderObj.SetActive( false );

        for( int i = 0 ; i < ATM_s.ATM_Buttons.Length ; i++ )
        {
            ATM_s.ATM_Buttons[ i ].GetComponent<Button>().interactable = false;
        }
    }
    #endregion

    #region CameraControl
    Vector3 CameraTargetPos;
    IEnumerator CameraControl()
    {
        Camera_Transfrom.position = new Vector3( 0.67f , 2.87f , -3.17f );
        CameraTargetPos = new Vector3( 0.67f , 0.48f , -5.66f );
        while( true )
        {
            Camera_Transfrom.position = Vector3.Lerp( Camera_Transfrom.position , CameraTargetPos , Time.deltaTime * 2 );

            yield return null;
        }
    }
    IEnumerator MainButtonAppear()
    {
        yield return new WaitForSeconds( 1 );
        ButtonHolderObj.SetActive( true );
    }
    #endregion

    #region Shop
    [HideInInspector]
    public Shop CurrentShopInUse;
    public void ClearShopView()
    {
        for( int i = ShopViewList_Rect.childCount - 1 ; i >= 0 ; i-- )
        {
            Destroy( ShopViewList_Rect.GetChild( i ).gameObject );
        }
    }
    public void ClearInventoryView()
    {
        for( int i = InventoryViewList_Rect.childCount - 1 ; i >= 0 ; i-- )
        {
            Destroy( InventoryViewList_Rect.GetChild( i ).gameObject );
        }
    }
    #endregion

    #region Bed
    [Header( "Bed" )]
    [Space( 2 )]
    public Image Overlay_Image_s;
    public AnimationCurve CurveFastSlowFast;

    IEnumerator PlayBedAnim()
    {
        float t = 0;
        while( t < 1 )
        {
            Overlay_Image_s.color = Color.black * CurveFastSlowFast.Evaluate( t );
            t += Time.deltaTime / 1.5f;
            yield return null;
        }
        Overlay_Image_s.color = Color.black;

        ATM_s.SetGold( Mathf.CeilToInt( ATM_s.GetGold * 0.1f ) );//+10% ez money

        t = 0;
        CameraTargetPos = new Vector3( 0.67f , 0.48f , -5.66f );
        while( t < 1 )
        {
            Overlay_Image_s.color = Color.black * CurveFastSlowFast.Evaluate( 1 - t );
            t += Time.deltaTime / 1.5f;
            yield return null;
        }
        Overlay_Image_s.color = Color.clear;

        Button_ReturnToMenu();
    }

    #endregion

}
