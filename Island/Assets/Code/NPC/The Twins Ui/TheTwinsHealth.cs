using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class TheTwinsHealth : MonoBehaviour
{
    [SerializeField] private GameObject _Jaqline;
    [SerializeField] private GameObject _Samantha;
    [SerializeField] private GameObject _player;
    [SerializeField] private Image _sharedHealthBar;

    private bool _isFollowingPlayer;


    void Start()
    {
        _Jaqline = GetComponent<GameObject>();
        _Samantha = GetComponent<GameObject>();
    }

    void Update()
    {
        
    }


}
