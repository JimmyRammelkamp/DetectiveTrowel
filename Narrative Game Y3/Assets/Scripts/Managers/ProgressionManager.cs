using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager instance;

    [SerializeField] private List<ProgressionTasks> progressionTasks;
    private void Awake()
    {
        if (instance != null) Debug.Log("Error: There are multiple instances exits at the same time (ProgressionManager)");
        instance = this;
    }

    private void Start()
    {
        if (progressionTasks.Count > 0) HUDManager.instance.UpdateTaskManager(progressionTasks[0]);
    }
}

[System.Serializable]
public class ProgressionTasks
{
    [TextArea(3, 10)]
    public string taskName;
    public List<TaskSO> taskDescription;
}
