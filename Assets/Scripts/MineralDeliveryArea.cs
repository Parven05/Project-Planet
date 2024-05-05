using System;
using System.Collections.Generic;
using UnityEngine;

public class MineralDeliveryArea : MonoBehaviour
{
    public static MineralDeliveryArea Instance { get; private set; }

    public Action<List<MineralDataSO>> OnMineralListModified;

    [SerializeField] private List<MineralDataSO> toCollectMineralSoList;

    private List<MineralDataSO> collectedMineralSoList = new List<MineralDataSO>();

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rover _))
        {
            HandleCollectMineral();
        }
    }

    private void HandleCollectMineral()
    {
        if (RoverPocketStorage.Instance.IsHoldingMineral())
        {
            collectedMineralSoList.Add(RoverPocketStorage.Instance.GetPickedMineralDataSO());

            OnMineralListModified?.Invoke(collectedMineralSoList);

            RoverPocketStorage.Instance.SetPickedMineral(null);
        }
    }

    public int GetTotalCollectableMineralsCount()
    {
       return toCollectMineralSoList.Count;
    }
}
