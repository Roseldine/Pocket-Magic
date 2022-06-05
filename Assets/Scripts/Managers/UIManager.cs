
using UnityEngine;
using StardropTools.UI;

public class UIManager : StardropTools.Singletons.SingletonCoreManager<UIManager>
{
    [SerializeField] UIRootCanvas rootCanvas;
    [SerializeField] UIMenu[] menus;
    [SerializeField] bool getMenus;

    public static readonly CoreEvent<int> OnUpdatePoints = new CoreEvent<int>();
    public static readonly CoreEvent<int> OnUpdateDistance = new CoreEvent<int>();

    public override void Initialize()
    {
        base.Initialize();
        SubscribeToEvents();
    }

    public override void LateInitialize()
    {
        base.LateInitialize();

        //rootCanvas.Initialize();
        for (int i = 0; i < menus.Length; i++)
            menus[i].Initialize();

        ChangeMenu(0);
    }

    public override void SubscribeToEvents()
    {
        base.SubscribeToEvents();

        GameManager.Instance.OnIdleEnter.AddListener(() => ChangeMenu(0));
        GameManager.Instance.OnPlayEnter.AddListener(() => ChangeMenu(1));
        GameManager.Instance.OnFinishEnter.AddListener(() => ChangeMenu(2));
    }

    public void ChangeMenu(int menuID)
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(false);

        menus[menuID].SetActive(true);
        menus[menuID].Open();
    }

    public void SetPoints(int value)
        => OnUpdatePoints?.Invoke(value);

    public void SetDistance(int value)
        => OnUpdateDistance?.Invoke(value);

    protected override void OnValidate()
    {
        base.OnValidate();

        if (getMenus)
        {
            menus = GetItems<UIMenu>(rootCanvas.Transform);
            getMenus = false;
        }
    }
}