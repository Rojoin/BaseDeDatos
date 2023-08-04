using UnityEngine;

[CreateAssetMenu(menuName = "Create CurrentUserName", fileName = "CurrentUserName", order = 0)]
public class CurrentUserName : ScriptableObject
{
    public int userId;
    public int score;
}