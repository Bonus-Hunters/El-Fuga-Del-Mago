using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private static QuestManager instance;
    private HashSet<string> completedQuests = new HashSet<string>();

    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("QuestManager");
                instance = obj.AddComponent<QuestManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    public static void CompleteQuest(string questID)
    {
        Instance.completedQuests.Add(questID);
        Debug.Log($"Quest completed: {questID}");
    }

    public static bool IsQuestCompleted(string questID)
    {
        return Instance.completedQuests.Contains(questID);
    }
}