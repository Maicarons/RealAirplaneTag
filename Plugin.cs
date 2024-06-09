﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using HarmonyLib;
using BepInEx.Configuration;
using Logger = BepInEx.Logging.Logger;
using Random = System.Random;


namespace RealAirplaneTag
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = MyPluginInfo.PLUGIN_GUID;
        public const string PluginName = MyPluginInfo.PLUGIN_NAME;
        public const string Version = MyPluginInfo.PLUGIN_VERSION;
        private ConfigEntry<bool> Enabled;
        ConfigFile config = new ConfigFile(Path.Combine(Paths.ConfigPath, $"{MyPluginInfo.PLUGIN_GUID}.cfg"), true);
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            Enabled = config.Bind("PluginSetting",
                "Enabled",
                true,
                "Enabled");

            Logger.LogInfo(Enabled.Value);
            if (Enabled.Value == false)
            {
                return;
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

        }

        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogInfo($"Scene loaded: {scene.name}");
            Logger.LogInfo("scene.path:"+scene.path);
            
            if ((scene.name == "MapPlayer" || scene.name == "London" ) &&
                AircraftManager.Instance != null && Enabled.Value)
            {
                
                Logger.LogInfo("Hooking AircraftManager");
                AircraftManager.Instance.AircraftCreateEvent.AddListener(HookAircraft);
            }
        }

        private void HookAircraft(Vector2 pos, Aircraft aircraft)
        {
            Logger.LogInfo("Aircraft created: " + aircraft.name);
            Logger.LogInfo("Aircraft ID: " + aircraft.GetInstanceID());
            AircraftStatHelper cp = aircraft.gameObject.AddComponent<AircraftStatHelper>();
            cp.m_Aircraft = aircraft;
        }
    }

    public class AircraftStatHelper : MonoBehaviour
    {
        private  Vector3 _apScale;
        private Vector3 _plScale;
        private Dictionary<int, PlaneTag> planeID= new Dictionary<int, PlaneTag> { };

        private TMP_Text m_Text;
        public Aircraft m_Aircraft;
        bool inited = false;
        Dictionary<string, PlaneType> planeTypes = PlaneType.FromJson(GetRes.GetFromRes(".res.plane.json"));
        private PlaneTag info= new PlaneTag("", "", 0);

        void Start()
        {
            
            _apScale= m_Aircraft.AP.gameObject.transform.localScale;
             _plScale= m_Aircraft.AP.gameObject.transform.localScale;
            GameObject obj = Instantiate(new GameObject("Text"), m_Aircraft.transform, true);
            
            m_Text = obj.AddComponent<TextMeshPro>();
            m_Text.fontSize = 2;
            m_Text.horizontalAlignment = HorizontalAlignmentOptions.Left;
            m_Text.verticalAlignment = VerticalAlignmentOptions.Top;
            m_Text.rectTransform.sizeDelta = new Vector2(3, 1);
            obj.transform.localPosition = new Vector3(1, -3f, 5);

            // make sorting layer of obj "Text"
            SortingGroup sg = obj.AddComponent<SortingGroup>();
            sg.sortingLayerName = "Text";
            sg.sortingOrder = 1;
            planeID.Add(m_Aircraft.GetInstanceID(), GetRandomTags());
            info = planeID[m_Aircraft.GetInstanceID()];
            inited = true;
        }

        private PlaneTag GetRandomTags()
        {
            Random random = new Random();
            int callNumber = random.Next(1, 10000);
            var mapName=MapManager.mapName;
            string flightCallSign = "MU" + callNumber.ToString("D4");
            string randomKey = planeTypes.Keys.ToList()[random.Next(planeTypes.Keys.ToList().Count)];
            var pf = new planeFunc(planeTypes[randomKey]);
            return new PlaneTag(flightCallSign, planeTypes[randomKey].Name,pf.WtCtoLevel());
        }

        void Update()
        {
            if (!m_Aircraft)
                //planeID.Remove(m_Aircraft.GetInstanceID());
                Destroy(gameObject);

            if (!inited || !m_Text)
            {
                return;
            }
            //var info = planeID[m_Aircraft.GetInstanceID()];
            m_Text.text = $"{info.CallSign} | {info.AirType} | {LevelToName(info.SizeOfPlane)} \n";
        }

        private string LevelToName(int i)
        {
            switch (i)
            {
                case  1:
                    return "超轻型";
                case  2:
                    return "轻型";
                case  3:
                    return "中型";
                case  4:
                    return "大型";
                case  5:
                    return "超大型";
                default:
                    return "中型";
            }
        }
    }
}
    