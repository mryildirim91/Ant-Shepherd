using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _levels;

    private void Awake()
    {
        var level = PlayerPrefs.GetInt("Level");

        if (level > _levels.Length - 1)
        {
            PlayerPrefs.DeleteKey("Level");
            level = 0;
        }
        Instantiate(_levels[level], Vector2.zero,Quaternion.identity);;
    }
}
