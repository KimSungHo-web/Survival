using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;
    public static CharacterManager Instance //�ܺο��� �����ϱ� ����
    {
        get 
        {
            if (_instance == null)//null�� ��츦 �̸� ����
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>(); //ĳ���͸Ŵ����� ���Ӱ� ����
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player 
    {
        get {return _player;}
        set {_player = value;}
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            if(_instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
