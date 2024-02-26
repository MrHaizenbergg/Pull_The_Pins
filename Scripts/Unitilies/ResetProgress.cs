using Flatformer.GameData;
using UnityEngine;

public class ResetProgress : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameDataManager.ResetProgress();
        }
    }
}
