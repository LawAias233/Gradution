﻿/*****************************
Created by 师鸿博
*****************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DreamerTool.Extra;
using DreamerTool.GameObjectPool;
using DG.Tweening;
using DreamerTool.UI;

public class RectTransformData
{
    public RectTransformData(Vector2 p,Vector2 d,Vector3 r)
    {
        pos = p;
        deltaSize = d;
        rot = r;
    }
    public Vector2 pos;
    public Vector2 deltaSize;
    public Vector3 rot;

    public   void  Copy(RectTransform _transform)
    {
        _transform.anchoredPosition = pos;
        _transform.sizeDelta = deltaSize;
        _transform.eulerAngles = rot;
    }
}

public static class GameStaticData  
{
    public static List<List<int>> towerEnemys = new List<List<int>> { 
        new List<int>{2,2,2,2 },
         new List<int> {4,2,2,4},
         new List<int> {5,2,2,5},
         new List<int> {6,2,2,6},
       new List<int> {-1,3,3,-1},
        new List<int> {5,3,3,6},
    };
    public static RectTransformData WeaponUI = new RectTransformData(
        new Vector2(-20.9f, -13f),
        new Vector2(150, 150),
        new Vector3(0, 0,-47.9f)
        );
    public static RectTransformData EquipmentUI = new RectTransformData(
     new Vector2(0, 10),
     new Vector2(180, 180),
     new Vector3(0, 0, 0)
     );
    /*白色 绿色 蓝色 黄色 紫色 橙色 红色 黑色*/
    public static Dictionary<ItemLevel, Color> ITEM_COLOR_DICT = new Dictionary<ItemLevel, Color>()
    {
        { ItemLevel.谦卑,Color.white},
        { ItemLevel.诚实,Color.green },
        { ItemLevel.怜悯,Color.blue },
        { ItemLevel.英勇,Color.yellow },
        { ItemLevel.公正,"#BF2CAF".GetColorByString() },
        { ItemLevel.牺牲,"#E77E11".GetColorByString() },
        { ItemLevel.荣誉,Color.cyan },
        { ItemLevel.灵魂,Color.red },
    };
    public static Dictionary<string, ItemType> ITEM_CONFIG = new Dictionary<string, ItemType>()
    {
        {"WeaponConfig",ItemType.武器},
        {"TorsoConfig",ItemType.上衣},
        {"SleeveConfig",ItemType.手链},
        {"ShieldConfig",ItemType.盾牌},
        {"ArmConfig",ItemType.肩膀},
        {"PelvisConfig",ItemType.裤子},
        {"FootConfig",ItemType.鞋子},
        {"ConsumablesConfig",ItemType.消耗品},
    };


}
public class GameConstData
{
    public const string ENDLESSS_SCENE_NAME = "EndlessScene";
    public const string MAGIC_DEFEND_TIP_STR = "魔抗";
    public const string HEALTH_TIP_STR = "生命值";
    public const string DEFEND_TIP_STR = "防御";
    public const string  SPEED_TIP_STR = "移速";
    public const string COLOR_WHITE = "";
    public const string COLOR_YELLOW = "FFFA00";
    public const string FIGHT_SCENE_NAME = "FightScene";
    public const string LOGINING_PHONE_NUMBER = "logining_phone_number";
    public const string BMOB_APP_ID = "15fe84103d433cfdbbafb8754c88b5ef";
    public const string BMOB_REST_KEY = "ec2cf19d1d8bbecbf48c8427655284f6";
    public const string USER_TABLE_NAME = "user";
    public const string GAME_MAIN_SCENE_NAME ="test";
    public const int SAVE_DATA_COUNT = 3;
    public const string CHOOSE_ACTOR_SCENE_NAME = "ChooseActor";
    public const string CREATE_ACTOR_SCENE_NAME = "CreateActor";
    public const string DATA_BASE_PORT = "3306";
    public const string DATA_BASE_IP = "127.0.0.1";
    public const string SAVE_DATA_KEY = "game_data_";
    public const string USER_DATABASE_NAME = "user";
    public const byte NETWORK_READY_STATE_CHANGE_EVENT = 0;
     
}

public  class GameStaticMethod
{
    
    public static void GameInit()
    {
        if (ActorModel.Model == null)
            ActorModel.CreateModel();

        GameObjectPoolManager.InitByScriptableObject();
    
        SkillModel.Init();
 
    }
    public static void ExecuteBackCommond(string commondStr)
    {
        var commond = commondStr.Split(',');
        if (commond.Length > 0)
        {
            commondStr = commond[0];
        }
        switch (commondStr)
        {
            case "set_actor_attack":
                ActorModel.Model.SetPlayerAttribute(PlayerAttribute.攻击力, -double.Parse(commond[1]));
                break;
            case "set_actor_health":
                ActorModel.Model.SetPlayerAttribute(PlayerAttribute.生命值, -double.Parse(commond[1]));
                break;
            default:
                break;
        }
    }
    public static void ExecuteCommond(string commondStr)
    {
        var commond = commondStr.Split(',');
        if(commond.Length>0)
        {
            commondStr = commond[0];
        }
        switch (commondStr)
        {
            case "set_actor_attack":
                ActorModel.Model.SetPlayerAttribute(PlayerAttribute.攻击力, double.Parse(commond[1]));
                break;
            case "set_actor_health":
                ActorModel.Model.SetPlayerAttribute(PlayerAttribute.生命值, double.Parse(commond[1]));
                break;
            case "use_scratch_card":
                View.CurrentScene.OpenView<ScratchCardView>();
                break;
            case "buy_scratch_card":
                View.CurrentScene.OpenView<ShopView>();
                View.CurrentScene.CloseView<NPCView>();
                break;
            case "loadscene":
                LoadingScene.LoadScene(commond[1]);
                break;
            case "exit":
                View.CurrentScene.CloseView<NPCView>();
                break;
            case "make_face":
                View.CurrentScene.OpenView<FaceView>();
                View.CurrentScene.CloseView<NPCView>();
                break;
            default:
                break;
        }
    }
    public static void ChangeChildrenSpriteRendererColor(GameObject gameObject,Color color)
    {
       
        foreach (var item in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
             
            if (item.sprite != null)
            {
                item.DOKill(true);
                item.DOColor(color, 0.2f).SetEase(Ease.Linear).SetLoops(2, LoopType.Yoyo);
            }
        }
    }
}

