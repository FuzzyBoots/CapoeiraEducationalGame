using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace DirtyCookStudio.Tooling
{
    public class TaskList : EditorWindow
    {
        const string UXML = "Todo";
        const string USS = "Todo";

        TextField taskText;
        ListView taskListScrollView;
        SO_Tasks so_Tasks;
        VisualElement settingsBox;
        VisualElement sessionBox;
        UnityEditor.UIElements.ObjectField tasksField;

        VisualElement container;

        ProgressBar progressBar;
        Label progressLabel;
        Button sessionButton;
        bool sessionEnabled;

        int maxTasks;
        int currentTasks;


        [MenuItem("Dirty Cook Studio/Tooling/TodoLITE Window")]
        public static void ShowWindow()
        {
            var window = GetWindow<TaskList>();
            window.titleContent = new GUIContent("TodoLITE");
        }

        public void CreateGUI()
        {
            container = rootVisualElement;
            var visualTree = (VisualTreeAsset)Resources.Load(UXML, typeof(VisualTreeAsset));
            var styleSheet = (StyleSheet)Resources.Load(USS, typeof(StyleSheet));

            if (container == null)
            {
                Debug.LogError("container is null");
                return;
            }
            if (visualTree == null)
            {
                Debug.LogError("uxml not found");
                return;
            }
            if (styleSheet == null)
            {
                Debug.LogWarning("USS is missing. This may cause issues if Editor window needs it");
            }


            visualTree.CloneTree(container);

            if(styleSheet)
                container.styleSheets.Add(styleSheet);


            progressBar = container.Q<ProgressBar>();
            if (progressBar == null)
            {
                Debug.LogWarning("Progress bar couldn't be found");
            }

            progressLabel = container.Q<Label>("ProgressLabel");
            if (progressLabel == null)
            {
                Debug.LogWarning("ProgressLabel is missing");
            }


            var settingsButton = container.Q<Button>("SettingsButton");
            if (settingsButton == null)
            {
                Debug.LogError("Can't access Settings Button");
            }
            else
            {
                settingsButton.clicked += OnSettingsButton;
            }

            sessionButton = container.Q<Button>("SessionButton");
            if (sessionButton == null)
            {
                Debug.LogError("Couldn't find session button");
                return;
            }
            else
            {
                sessionButton.clicked += OnSessionButton;
            }

            sessionBox = container.Q<VisualElement>("SessionBox");
            if (sessionBox == null)
            {
                Debug.LogError("Couldn't get the session box");
                return;
            }

            settingsBox = container.Q<VisualElement>("SettingsBox");
            if (settingsBox == null)
            {
                Debug.LogError("couldn't find settings box");
            }

            tasksField = container.Q<UnityEditor.UIElements.ObjectField>();
            if (tasksField == null)
            {
                Debug.LogError("object field isn't found");
                return;
            }

            tasksField.RegisterValueChangedCallback(InitTasks);

            taskListScrollView = container.Q<ListView>("TaskList");
            if (taskListScrollView == null)
            {
                Debug.LogError("main task list is missing");
                return;
            }


            taskText = container.Q<TextField>("Input");
            if (taskText == null)
            {
                Debug.LogError("user input field is missing"); 
                return;
            }


            taskText.RegisterCallback<FocusOutEvent>(UpdateTasks);
            //RefreshTasks();
        }


        void InitTasks(ChangeEvent<UnityEngine.Object> evt)
        {
            if(tasksField.value == null)
            {
                Debug.LogError("Tasks not found");
                return;
            }

            so_Tasks = tasksField.value as SO_Tasks;
            if (so_Tasks == null)
            {
                Debug.LogError("Tasks not found");
                return;
            }
            
            RefreshTasks();
        }



        public void UpdateTasks(FocusOutEvent evt)
        {
            if (!String.IsNullOrEmpty(taskText.value))
            {
                so_Tasks.AddTask(taskText.value);
                RefreshTasks();
                taskText.value = "";
                maxTasks++;
            }
        }

        void RefreshTasks()
        {
            UpdateProgress();

            if (taskListScrollView == null)
            {
                Debug.LogError("trying to add tasks when the list view is null");
                return;
            }

            string[] taskHolder = so_Tasks.GetTasks()?.ToArray();
            
            taskListScrollView.itemsSource = taskHolder;
            
            taskListScrollView.makeItem = () =>
            {
                Toggle toggle = new();
                toggle.RegisterCallback<ChangeEvent<bool>>(OnToggleChanged);
                return toggle;
            };
            

            taskListScrollView.bindItem = (element, index) =>
            {
                string[] newestTaskList = so_Tasks.GetTasks().ToArray();
                (element as Toggle).text = newestTaskList[index];
            };
                
            taskListScrollView.Rebuild();
        }

        void OnToggleChanged(ChangeEvent<bool> evt)
        {
            List<string> newTaskList = new();
            var toggles = taskListScrollView.Query<Toggle>().ToList();
            //Debug.Log(toggles.Count);

            foreach (Toggle toggle in toggles)
            {
                if (toggle == null)
                    continue;
                if (toggle.value == false)
                    newTaskList.Add(toggle.text);
            }
            so_Tasks.UpdateTasks(newTaskList);
            RefreshTasks();
        }

        void OnSettingsButton()
        {
            settingsBox.ToggleInClassList("Open");
            settingsBox.ToggleInClassList("Close");
        }

        void OnSessionButton()
        {
            sessionBox.ToggleInClassList("SessionOpen");
            sessionBox.ToggleInClassList("SessionClose");
            sessionButton.ToggleInClassList("SessionButtonStart");
            sessionButton.ToggleInClassList("SessionButtonStop");

            //After toggle, if open session is enabled
            sessionEnabled = sessionBox.ClassListContains("SessionOpen");
            ToggleSession(sessionEnabled);
        }

        void ToggleSession(bool enabled)
        {
            sessionButton.text = !enabled ? "Start Session" : "Stop Session";

            if (!enabled)
                return;

            maxTasks = currentTasks = taskListScrollView.Query<Toggle>().ToList().Count;

            UpdateProgress();

        }

        void UpdateProgress()
        {
            currentTasks = so_Tasks.GetTasks().Count;

            if (progressBar==null)
                return;
            if (progressLabel != null)
            {
                progressLabel.text = $"{currentTasks} tasks left!! " + GetEncouragement();
            }

            //Debug.Log($"Max: {maxTasks}, cur:{currentTasks}, prog: {(float)(maxTasks - currentTasks) / maxTasks}");
            progressBar.value = (float)(maxTasks - currentTasks) / maxTasks;
        }

        string GetEncouragement()
        {
            if (currentTasks == 0)
            {
                return $"You completed all {maxTasks} tasks!";
            }

            return encouragingMessages[UnityEngine.Random.Range(0, encouragingMessages.Length)];
        }

        string[] encouragingMessages = new string[]
        {
            "You're doing amazing work—keep pushing forward!",
            "Every line of code brings you closer to success.",
            "Stay positive; you're solving real problems.",
            "One bug at a time, you're making progress.",
            "Believe in your skills; you’ve got this!",
            "Small steps lead to big achievements.",
            "Your dedication will pay off in the end.",
            "Challenges are just opportunities to grow.",
            "Every error is a lesson in disguise.",
            "Persistence is the key to mastering your craft.",
            "You're capable of more than you realize.",
            "Keep learning, keep growing!",
            "Success is built on perseverance.",
            "You're creating something valuable—keep at it!",
            "Your hard work will be rewarded.",
            "Progress, not perfection, is the goal.",
            "You're making a difference with your code.",
            "Embrace the journey; it's worth it.",
            "Mistakes are proof that you're trying.",
            "Your creativity is your superpower.",
            "You have the skills to solve this!",
            "Keep moving forward—success is ahead.",
            "You're stronger than the challenges you face.",
            "Your efforts are not in vain.",
            "Keep coding—you're on the right path.",
            "You’ve overcome challenges before; you can do it again.",
            "Coding is an art, and you’re an artist.",
            "The process may be tough, but the results are worth it.",
            "Your persistence is inspiring.",
            "Every problem has a solution—find it!",
            "You're learning with every keystroke.",
            "You have the power to create amazing things.",
            "Stay focused, stay determined.",
            "Great things take time—keep going.",
            "Your work today will make tomorrow easier.",
            "Every challenge is an opportunity to shine.",
            "You’re closer to success than you think.",
            "You’re capable of incredible things.",
            "Believe in your journey—you're doing great!",
            "The best is yet to come—keep coding!"
        };

    }
}
