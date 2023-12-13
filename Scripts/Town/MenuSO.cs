using UnityEngine;

[CreateAssetMenu(fileName = "MenuSO.asset", menuName = "Town/MenuSO")]

public class MenuSO : ScriptableObject
{
    public GameObject spawnable;

    public string prefabName;

    public Vector3 pos = new Vector3(0f, 20f, -5f);
}
