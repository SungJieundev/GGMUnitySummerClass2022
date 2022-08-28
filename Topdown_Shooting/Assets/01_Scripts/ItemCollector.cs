using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int _resourceLayer;
    private Player _player;

    private void Awake()
    {
        _resourceLayer = LayerMask.NameToLayer("Resource");
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == _resourceLayer)
        {
            Resource resource = collision.gameObject.GetComponent<Resource>();

            if(resource != null)
            {
                switch(resource.ResourceData.resourceType)
                {
                    case ResourceType.Ammo:
                        //�̰� �ϳװ� �� ����� �˴ϴ�.
                        break;
                    case ResourceType.Health:
                        int value = resource.ResourceData.GetAmount();
                        _player.Health += value;
                        //�˾��ؽ�Ʈ ����ٲ���
                        PopupText(value, resource);
                        resource.PickUpResource();
                        break;
                }
            }
        }
    }

    private void PopupText(int value, Resource res)
    {
        PopupText text = PoolManager.Instance.Pop("PopupText") as PopupText;
        text?.Setup(value, res.transform.position + new Vector3(0, 0.5f, 0), false, res.ResourceData.popupTextColor);
    }
}
