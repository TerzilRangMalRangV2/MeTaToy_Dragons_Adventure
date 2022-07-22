using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class AutoSave : MonoBehaviour
{
    // ---�̱������� ����--- 
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    public static AutoSave _instance;
    public static AutoSave instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(AutoSave)) as AutoSave;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public GameObject[] char_Prefeb;
    public GameObject char_Prefeb_Choice;
    public GameObject save_char_Img;
    public string save_char_Name;
    public int save_nft_Number;
    public int save_StageClear;
    public bool isTitleSkip = false;
    public string TicketRandNum;

    public class GameData
    {
        public bool isClear_1 = false;
        public bool isClear_2 = false;
        public bool isClear_3 = false;
        public bool isClear_4 = false;
        public bool isClear_5 = false;
    }

    // --- ���� ������ �����̸� ���� ---
    public string GameDataFileName = "DragonsData.json";

    // "���ϴ� �̸�(����).json"
    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            // ������ ���۵Ǹ� �ڵ����� ����ǵ���
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "DragonsData.json");
        LoadGameData();
        SaveGameData();
    }

    private void Awake()
    {
        _instance = this;
    }


    // ����� ���� �ҷ����� //Application.persistentDataPath
    public void LoadGameData()
    {
        string filePath = Path.Combine(Application.persistentDataPath + GameDataFileName);

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            print("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
        }

        // ����� ������ ���ٸ�
        else
        {
            print("���ο� ���� ����");
            _gameData = new GameData();
        }
    }

    // ���� �����ϱ�
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Path.Combine(Application.persistentDataPath + GameDataFileName);

        // �̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);

        // �ùٸ��� ����ƴ��� Ȯ�� (�����Ӱ� ����)
        print("����Ϸ�");
        print("2�� " + gameData.isClear_2);
        print("3�� " + gameData.isClear_3);
        print("4�� " + gameData.isClear_4);
        print(filePath);
    }

    void Update()
    {
        DontDestroyOnLoad(this);
        print("1" + gameData.isClear_1);
        print("2" + gameData.isClear_2);
        print("3" + gameData.isClear_3);
        print("4" + gameData.isClear_4);
        print("5" + gameData.isClear_5);
    }

    // ������ �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            SaveGameData();
        }
    }

}