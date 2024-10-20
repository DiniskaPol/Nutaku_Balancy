using System;
using System.ComponentModel;
using SRDebugger.Services;
using SRF.Service;
using UnityEngine;

public partial class SROptions
{
    private bool _showNewArsenal = true;


    private void Close()
    {
        SRServiceManager.GetService<IDebugService>().HideDebugPanel();
    }

    [Category("Utilities")] 
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}