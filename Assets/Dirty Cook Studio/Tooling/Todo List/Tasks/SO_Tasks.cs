using System.Collections.Generic;
using UnityEngine;

namespace DirtyCookStudio.Tooling
{
    [CreateAssetMenu(fileName = "Tasks", menuName = "Dirty Cook Studio/New Task List")]
    public class SO_Tasks : ScriptableObject
    {
        List<string> tasks = new();

        public void AddTask(string task)
        {
            tasks.Add(task);
        }
        public void UpdateTasks(List<string> tasks)
        {
            this.tasks = tasks;
        }

        public List<string> GetTasks() => tasks;
    }
}
