using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager
{
    public event Action<int> OnStageChanged;
    private int _currentStage = 1;

    public StageData CurrentStageData { get; private set; }
    public SpawningPool CurrentSpawningPool { get; private set; }

    public int CurrentStage
    {
        get { return CurrentStageData.stage; }
    }

    public void StartGame()
    {
        //Manager.Object.
        ChangeStage(_currentStage);
        CoroutineHelper.Instance.StartCoroutine(CoChangeStageRoutine());
    }

    private IEnumerator CoChangeStageRoutine()
    {
        while (true)
        {
            if (CurrentStageData == null)
                break;

            yield return new WaitForSeconds(CurrentStageData.stageDuration);
            ChangeStage(_currentStage + 1);
        }
    }

    private void ChangeStage(int stage)
    {
        if (Manager.Data.StageDict.TryGetValue(stage, out StageData stageData) == false)
        {
            CurrentStageData = null;
            return;
        }

        CurrentStageData = stageData;
        _currentStage = stage;
        RefreshSpawningPool();
        OnStageChanged?.Invoke(stage);
    }

    private void RefreshSpawningPool()
    {
        if (CurrentSpawningPool != null)
        {
            CurrentSpawningPool.Clear();
            Manager.Object.Despawn(CurrentSpawningPool.ObjectId);
        }
        SpawningPool pool = Manager.Object.SpawnSpawningPool();
        CurrentSpawningPool = pool;
    }
}
