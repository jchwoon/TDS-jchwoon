using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public void Awake()
    {
        Manager.Resource.LoadAll();
        Manager.Data.LoadData();
        Manager.Game.StartGame();
    }
}
