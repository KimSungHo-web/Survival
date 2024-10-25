using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager _instance;

    //외부에서 접근하기 위한
    public static CharacterManager Instance
    {
        get 
        {
            //null일 경우를 미리 방지
            if (_instance == null)
            {
                //캐릭터매니저를 새롭게 만듬
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
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
