using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    [System.Serializable]
    class SaveData
    {
        public int score;
        public string playerName;
    }

    // Start is called before the first frame update
    [SerializeField]
    private InputField inputField;

    [SerializeField]
    private Text scoreText;

    public static Menu Instance;

    public int best_score;
    public string best_player;

    public string playerName;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LoadScore();
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScore(int score, string pName)
    {
        SaveData data = new SaveData();
        data.score = score;
        data.playerName = pName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            best_score = data.score;
            best_player = data.playerName;

            scoreText.text = best_player + ":" + best_score.ToString(); 
        }
    }

    public void StartNew()
    {
        playerName = inputField.text;
        SceneManager.LoadScene(1);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void Exit()
    {
    #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
    #else
           Application.Quit(); // original code to quit Unity player
    #endif
    }

}
