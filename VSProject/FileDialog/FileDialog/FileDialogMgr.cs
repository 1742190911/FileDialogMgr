﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


/// <summary>
/// time:2019/10/10  15：43
/// author:Sun
/// des:FileDialog 调取Win窗口的管理器
///
/// github:https://github.com/KingSun5
/// csdn:https://blog.csdn.net/Mr_Sun88
/// </summary>
public class FileDialogMgr
{
    /// <summary>
    /// 自定义打开文件的数据格式
    /// </summary>
    public OpenDialogData CustomOpenData;
    /// <summary> 
    /// 自定义保存文件的数据格式
    /// </summary>
    public SaveDialogData CustomSaveData;
    public FileDialogMgr()
    {
        InitFileDialogMgr();
    }
    /// <summary>
    /// 初始化管理器的相关信息
    /// </summary>
    private void InitFileDialogMgr()
    {
        //初始打开窗口的数据
        CustomOpenData = new OpenDialogData();
        CustomOpenData.structSize = Marshal.SizeOf(CustomOpenData);
        CustomOpenData.filter =  "All Files\0*.*\0\0";
        CustomOpenData.file = new string(new char[256]);
        CustomOpenData.maxFile = CustomOpenData.file.Length;
        CustomOpenData.fileTitle = new string(new char[1000]);
        CustomOpenData.maxFileTitle = CustomOpenData.fileTitle.Length;
        CustomOpenData.initialDir = Application.dataPath.Replace('/', '\\') + "\\aaa\\";
        CustomOpenData.title = "打开项目";
        CustomOpenData.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

        //初始保存窗口的数据
        CustomSaveData = new SaveDialogData();
        CustomSaveData.structSize = Marshal.SizeOf(CustomSaveData);
        CustomSaveData.filter = "All files (*.*)|*.*";
        CustomSaveData.file = new string(new char[256]);
        CustomSaveData.maxFile = CustomSaveData.file.Length;
        CustomSaveData.fileTitle = new string(new char[64]);
        CustomSaveData.maxFileTitle = CustomSaveData.fileTitle.Length;
        CustomSaveData.initialDir = Application.dataPath.Replace('/', '\\') ;  // default path  
        CustomSaveData.title = "保存项目";
        CustomSaveData.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

    }

    #region -------------------------------------打开窗口-------------------------------------

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <returns></returns> 和该文件相关的数据
    public OpenDialogData OpenFileDlg()
    {
        if (OpenFileDialog.GetOpenFileName(CustomOpenData))
        {
            return CustomOpenData;
        }
        return null;
    }

    #endregion -------------------------------------------------------------------------------

    #region -------------------------------------保存窗口-------------------------------------
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <returns></returns> 和该文件相关的数据
    public SaveDialogData SaveFileDlg()
    {
        if (SaveFileDialog.GetSaveFileName(CustomSaveData))
        {
            return CustomSaveData;
        }
        return null;
    }
    #endregion -------------------------------------------------------------------------------

    #region -----------------------------------设置过滤方式-----------------------------------

    /// <summary>
    /// 设置单个过滤方式
    /// </summary>
    public void SetFilteringWay(EnumFilteringWay type)
    {
        CustomOpenData.filter = GetFilteringWay(type);
    }

    /// <summary>
    /// 设置多个过滤方式
    /// </summary>
    /// <param name="types"></param>
    public void SetFilteringWays(EnumFilteringWay[] types)
    {
        if (types == null || types.Length == 1)
        {
            CustomOpenData.filter = "All Files\0*.*\0\0";
            return;
        }
        CustomOpenData.filter = "";
        for (int i = 0; i < types.Length; i++)
        {
            if (types[i] == EnumFilteringWay.All)
            {
                CustomOpenData.filter = "All Files\0*.*\0\0";
                break;
            }
            CustomOpenData.filter += GetFilteringWay(types[i]);
        }
    }
    private string GetFilteringWay(EnumFilteringWay type)
    {
        switch (type)
        {
            case EnumFilteringWay.Txt:
                return "文本文件\0*.txt\0";
            case EnumFilteringWay.Json:
                return "Json文件\0*.json\0";
            case EnumFilteringWay.Excel:
                return "Excel文件\0*.xlsx\0";
            case EnumFilteringWay.Png:
                return "Png图片\0*.png\0";
            case EnumFilteringWay.Jpg:
                return "Jpg图片\0*.jpg\0";
        }
        return "All Files\0*.*\0\0";
    }


    #endregion -------------------------------------------------------------------------------

    #region ----------------------------------设置文件拓展名----------------------------------
    public void SetFileExtension(string extension)
    {
        CustomSaveData.defExt = extension;
    }
    #endregion -------------------------------------------------------------------------------

}

public enum EnumFilteringWay
{
    All,
    Txt,
    Json,
    Excel,
    Png,
    Jpg,
}
