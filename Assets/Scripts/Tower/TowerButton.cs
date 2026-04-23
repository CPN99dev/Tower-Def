using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerButton : MonoBehaviour
{
    public TowerData data;
    public TextMeshProUGUI costText;

    void Start()
    {
        if (data != null)
        {
            costText.text = data.cost.ToString();
        }
    }

    public void OnClick()
    {
        if (GameManager.Instance.CanAfford(data.cost))
        {
            GridManager.Instance.SelectTowerToBuild(data);
        }
    }
}
