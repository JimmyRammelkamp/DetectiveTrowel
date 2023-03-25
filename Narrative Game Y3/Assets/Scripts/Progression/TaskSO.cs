using UnityEngine;

[CreateAssetMenu(fileName = "NewTask", menuName = "ScriptableObjects/NewTask", order = 2)]
public class TaskSO : ScriptableObject
{
    [TextArea(3, 10)]
    [SerializeField] private string taskDescription;

    [System.NonSerialized] private bool isCompleted = false;

    private void OnEnable()
    {
        isCompleted = false;
    }


    public string TaskDescription { get { return taskDescription; } }
    public bool IsCompleted { get { return isCompleted; } set { isCompleted = value; } }
}
