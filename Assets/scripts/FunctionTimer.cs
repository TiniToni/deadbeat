using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionTimer
{
    public static FunctionTimer Create(Action action, float functionTime)
    {
        GameObject gameObj = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));

        FunctionTimer timer = new FunctionTimer(action, functionTime, gameObj);

        gameObj.GetComponent<MonoBehaviourHook>().onUpdate = timer.Update;
        return timer;
    }

    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;

        private void Update()
        {
            if (onUpdate != null)
            {
                onUpdate();
            }
        }
    }
    private Action action;
    private float time;
    private GameObject gameObj;
    private bool isDestroy;

    // Start is called before the first frame update
    private FunctionTimer(Action action, float time, GameObject gameObj)
    {
        this.action = action;
        this.time = time;
        this.gameObj = gameObj;
        isDestroy = false;
    }

    public void Update()
    {
        if (!isDestroy) {
            time -= Time.deltaTime;

            if (time < 0)
            {
                action();
                DestroySelf();
            }
        }
    }
    
    private void DestroySelf()
    {
        isDestroy = true;
        UnityEngine.Object.Destroy(gameObj);
    }
}
