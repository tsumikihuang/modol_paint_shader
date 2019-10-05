﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
視覺化畫面，可以選擇...

    口 模型(顯示或不顯示熱點)                     << 模型賦予該Shader，每個物體的熱點顯示與否buttom
    口 時間區間(ex. 0~30秒)                       << if判斷
    口 只顯現count數大於某數值                    << if判斷
    口 是否於熱點處放置球體(僅於整數位置)         << 放物體前將座標簡化，四捨五入，並檢查不要重複放
    口 球體/平面，熱點查看，可放大縮小位移

 */

/*
現階段動作步驟
     
    - 前面存的資料須改成 >> 秒數 + 熱點位置 + 次數
    - 傳array給shader前，須將相同位置的點合併(用CPU減少GPU的工作)
    

*/
public class StateBoard : MonoBehaviour
{
    public static bool recordMode = false;
    public Text recordState;

    void Start()
    {
        
    }
    public Text HotSpot_Num;

    void Update()
    {
        HotSpot_Num.text = "" + hotSpot.HS_Vector_list.Length;
        if (Input.GetKeyDown(KeyCode.Space))    //開始暫停錄製
        {
            recordMode = !recordMode;
            if (recordMode)
            {
                recordState.text = "停止錄製 [Space]";
                recordState.color = Color.red;
            }
            else
            {
                recordState.text = "開始錄製 [Space]";
                recordState.color = Color.black;
            }
        }
    }

    public GameObject SphereSize;
    public void OnSliderValueChanged(float value)
    {
        value = value*100;
        SphereSize.transform.localScale=new Vector3(value, value, value);
    }

    public Material Heatmap_mat;
    public Material Normal_mat;
    public GameObject Bunny_obj;
    public GameObject Plane_obj;
    private bool BunnyHM = false;
    private bool PlaneHM = false;
    private bool BunnySA = true;    //顯現
    private bool PlaneSA = true;

    public void Bunny_HeatMap()
    {
        BunnyHM = !BunnyHM;
        if (BunnyHM)
        {
            Bunny_obj.GetComponent<Renderer>().material = Heatmap_mat;
            Bunny_obj.GetComponent<HeatmapSurface>().material = Heatmap_mat;
        }
        else
            Bunny_obj.GetComponent<Renderer>().material = Normal_mat;
    }
    public void Plane_HeatMap()
    {
        PlaneHM = !PlaneHM;
        if (PlaneHM)
        {
            Plane_obj.GetComponent<Renderer>().material = Heatmap_mat;
            Plane_obj.GetComponent<HeatmapSurface>().material = Heatmap_mat;
        }
        else
            Plane_obj.GetComponent<Renderer>().material = Normal_mat;
    }

    public void Bunny_SetActive()
    {
        BunnySA = !BunnySA;
        Bunny_obj.SetActive(BunnySA);
    }
    public void Plane_SetActive()
    {
        PlaneSA = !PlaneSA;
        Plane_obj.SetActive(PlaneSA);
    }

}
