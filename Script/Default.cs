using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� ��ȯ �� ������ ��
// 1. �÷��̾� ����(HP/ST/COIN ��)
// 2. UI_Manager ������ ����

public class Default : MonoBehaviour
{
    public GameObject Player;
    public GameObject SoundManager;
    public GameObject SettingPanel;
    
    public  UI_Manager UI;
    public  P_Move p_move;
    public  Player player;
    public  EFM_Manager EFM_manager;


    void Awake()
    {
        DontDestroyOnLoad (Player);
        DontDestroyOnLoad (SoundManager);
        DontDestroyOnLoad (SettingPanel);

        p_move = Player.GetComponent<P_Move>();
        p_move.ui_manager = UI;
        player = Player.GetComponent<Player>();
        EFM_manager = GameObject.FindObjectOfType<EFM_Manager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Main" || SceneManager.GetActiveScene().name == "House")
        {
            UI = GameObject.FindObjectOfType<UI_Manager>();
            UI.EFM_Audio[0] = Player.GetComponent<AudioSource>();
            UI.EFM_Audio[1] = EFM_manager.audioSource;

        }
    }
}
