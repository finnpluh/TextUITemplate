using BepInEx;
using GorillaLocomotion.Gameplay;
using Photon.Pun;
using System;
using System.Collections.Generic;
using TextUITemplate.Libraries;
using TextUITemplate.Management;
using TMPro;
using UnityEngine;

namespace TextUITemplate
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public void OnEnable() => HarmonyPatches.Patch(true);

        public void OnDisable() => HarmonyPatches.Patch(false);

        public void Start() => Menu.Start();

        public void Update() => Menu.Load();
    }
}