using UnityEngine;

[CreateAssetMenu(fileName = "NewTask", menuName = "ScriptableObjects/NewTask", order = 2)]
public class TaskSO : ScriptableObject
{
    [TextArea(3, 10)]
    [SerializeField] private string taskDescription;

    public string TaskDescription { get { return taskDescription; } }
}
