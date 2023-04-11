using Code.Services.StaticData;
using Code.StaticData;
using Code.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.UI.InventoryWithSlots
{
  public class LockedSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
  {
    [SerializeField]
    private GameObject LockObject;

    [SerializeField]
    private GameObject PriceObject;

    [SerializeField]
    private Image LockImage;

    [SerializeField]
    private Sprite UnlockableIcon;

    [SerializeField]
    private TextMeshProUGUI PriceText;

    [SerializeField]
    private Image PriceImage;

    [SerializeField]
    private Button BuyButton;

    private bool _unlockable;
    private ISlotService _slotService;
    private IStaticDataService _staticData;

    public void Construct(ISlotService slotService, IStaticDataService staticData)
    {
      _staticData = staticData;
      _slotService = slotService;
    }

    public void Initialize(bool unlockable)
    {
      _unlockable = unlockable;
      if (unlockable)
        LockImage.sprite = UnlockableIcon;
      PriceImage.sprite = PriceItemStaticData().Icon;
      PriceText.text = $"{_staticData.ForLockedSlot().Price}";
    }

    private ItemStaticData PriceItemStaticData() =>
      _staticData.ForItem(_staticData.ForLockedSlot().PriceItemId);

    private void Start()
    {
      BuyButton.onClick.AddListener(BuySlot);
    }

    private void BuySlot()
    {
      if (!_slotService.TryToBuySlot(transform.GetSiblingIndex()))
        return;

      _unlockable = false;
      Destroy(gameObject);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      ShowLock(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      ShowLock(false);
    }

    private void ShowLock(bool locked)
    {
      if (!_unlockable)
        return;

      LockObject.SetActive(!locked);
      PriceObject.SetActive(locked);
    }

    public void MakeUnlockable()
    {
      _unlockable = true;
      LockImage.sprite = UnlockableIcon;
    }
  }
}