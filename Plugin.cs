using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using HarmonyLib;
using UnityEngine.Experimental.Rendering;
using Random = System.Random;
using BepInEx.AssemblyPublicizer;

namespace RealAirplaneTag
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            
            string filePath =  Paths.ConfigPath + "\\plane.json";
            string jsonContent = "";
            try
            {
                // 使用StreamReader读取文件
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // 一次性读取整个文件内容
                    jsonContent = reader.ReadToEnd();
                
                    // 打印出JSON字符串内容
                    Console.WriteLine(jsonContent);
                }
            }
            catch (FileNotFoundException)
            {
                Logger.LogInfo($"文件 {filePath} 未找到。");
            }
            catch (Exception ex)
            {
                Logger.LogInfo($"读取文件时发生错误: {ex.Message}");
            }
            var pt = PlaneType.FromJson(jsonContent);
            Logger.LogInfo(pt.Count);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogInfo($"Scene loaded: {scene.name}");

            if ((scene.name == "MapPlayer" || scene.name == "London") &&
                AircraftManager.Instance != null)
            {
                Logger.LogInfo("Hooking AircraftManager");
                AircraftManager.Instance.AircraftCreateEvent.AddListener(HookAircraft);
            }
        }

        private void HookAircraft(Vector2 pos, Aircraft aircraft)
        {
            Logger.LogInfo("Aircraft created: " + aircraft.name);

            AircraftStatHelper cp = aircraft.gameObject.AddComponent<AircraftStatHelper>();
            cp.m_Aircraft = aircraft;
        }
    }

    public class AircraftStatHelper : MonoBehaviour
    {
        private  Vector3 _apScale;
        private Vector3 _plScale;

        private TMP_Text m_Text;
        public Aircraft m_Aircraft;
        bool inited = false;
        Dictionary<string, string> airlineCodes = new Dictionary<string, string>
        {
            { "Delta", "DL" },
            { "American", "AA" },
            { "United", "UA" },
            { "JetBlue", "B6" }
        };
        Dictionary<string, string> airplaneDictionary = new Dictionary<string, string>
        {
            { "A380", "3" },
            { "B737", "2" },
            { "C919", "2" },
            { "CRJ900", "1" },
            { "EMB175", "1" },
            { "A220", "1" }, // 小型机
            { "B777", "3" }, // 大型机
            { "ARJ21", "1" }, // 小型机
            { "A350", "3" }, // 大型机
            { "E190-E2", "2" }, // 中型机
            { "MD-80", "2" }, // 中型机
            { "SSJ100", "1" } // 小型机
        };

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

            inited = true;
        }

        void Update()
        {
            if (!m_Aircraft)
                Destroy(gameObject);

            if (!inited || !m_Text)
            {
                return;
            }

            string calltext = m_Aircraft.callsign;
            m_Aircraft.GetInstanceID();

            
// 分割callsign为航空公司名称和编号
            string airtype = "C919";
            int num = 0;
            int separatorIndex = calltext.IndexOfAny("0123456789".ToCharArray());
            if (separatorIndex != -1)
            {
                string airlineName = calltext.Substring(0, separatorIndex);
                num = Convert.ToInt32(calltext.Substring(separatorIndex + 1, 1));
                // 查找并替换航空公司名称为简写
                if (airlineCodes.TryGetValue(airlineName, out string code))
                {
                    calltext = code + calltext.Substring(separatorIndex);
                }
            }
            

            switch (num)
            {
                case 0:
                    airtype = airplaneDictionary.Keys.ToArray()[0];
                    break;
                case 1:
                    airtype = airplaneDictionary.Keys.ToArray()[1];
                    break;
                case 2:
                    airtype = airplaneDictionary.Keys.ToArray()[2];
                    break;
                case 3:
                    airtype = airplaneDictionary.Keys.ToArray()[3];
                    break;
                case 4:
                    airtype = airplaneDictionary.Keys.ToArray()[4];
                    break;
                case 5:
                    airtype = airplaneDictionary.Keys.ToArray()[5];
                    break;
                case 6:
                    airtype = airplaneDictionary.Keys.ToArray()[6];
                    break;
                case 7:
                    airtype = airplaneDictionary.Keys.ToArray()[7];
                    break;
                case 8:
                    airtype = airplaneDictionary.Keys.ToArray()[8];
                    break;
                case 9:
                    airtype = airplaneDictionary.Keys.ToArray()[9];
                    break;
                default:
                    airtype = airplaneDictionary.Keys.ToArray()[10];
                    break;
            }
            string sizeOfPlane = "小型机";

            switch (airplaneDictionary[airtype])
            {
                case "1":
                    sizeOfPlane = "小型机";
                    m_Aircraft.AP.gameObject.transform.localScale = new Vector3(_apScale.x*0.9f, _apScale.y*0.9f, _apScale.z*0.9f);
                    m_Aircraft.Panel.gameObject.transform.localScale = new Vector3(_plScale.x*0.9f, _plScale.y*0.9f, _plScale.z*0.9f);                   

                    
                    break;
                case "2":
                    sizeOfPlane = "中型机";
                    break;
                case "3":
                    sizeOfPlane = "大型机";
                    m_Aircraft.AP.gameObject.transform.localScale = new Vector3(_apScale.x*1.5f, _apScale.y*1.5f, _apScale.z*1.5f);
                    m_Aircraft.Panel.gameObject.transform.localScale = new Vector3(_plScale.x*1.5f, _plScale.y*1.5f, _plScale.z*1.5f);
                    break;
                default:
                    sizeOfPlane = "未知机型";
                    break;
            }

            m_Text.text = $"{calltext} | {airtype} | {sizeOfPlane} \n";

        }
    }
}
    