
using UnityEngine;

public class HealthContainer : StardropTools.CoreComponent
{
    [SerializeField] GameObject[] hearts;

    public override void Initialize()
    {
        base.Initialize();

        GameManager.Instance.OnPlayEnter.AddListener(() => SetHealth(3));
        //PlayerManager.Instance.Player.OnDamaged.AddListener(SetHealth);
    }

    public void SetHealth(int value)
    {
        for (int i = 0; i < hearts.Length; i++)
            hearts[i].SetActive(false);

        for (int i = 0; i < value; i++)
            hearts[i].SetActive(true);
    }
}
