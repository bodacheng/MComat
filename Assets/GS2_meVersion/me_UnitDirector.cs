using System;
using System.Collections;
using System.Collections.Generic;
using Gs2.Unity;
using Gs2.Unity.Gs2Distributor.Result;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2JobQueue.Model;
using Gs2.Unity.Util;
using Gs2.Weave.Core.Controller;
using Gs2.Weave.Core.Watcher;
using UnityEngine;
using UnityEngine.Events;

// 自由版本
namespace Gs2.Weave.Unit
{
    public class me_UnitDirector : MonoBehaviour
    {
        private Gs2.Unity.Client _client;
        private GameSession _session;
        private Dictionary<string, string> _config;

        private EzInventoryModel _inventoryModel;
        private List<EzItemModel> _itemModels;

        private InventoryWatcher _inventoryWatcher;
        private UnitSetting _unitSetting;

        public InventoryWatcher Watcher => _inventoryWatcher;

        public static me_UnitDirector target;

        void Awake()
        {
            target = this;
        }

        public void Start()
        {
            _inventoryWatcher = new InventoryWatcher();
            _unitSetting = GetComponent<UnitSetting>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="session"></param>
        /// <param name="config"></param>
        public IEnumerator Run(
            Gs2.Unity.Client client,
            GameSession session,
            Dictionary<string, string> config
        )
        {
            _client = client;
            _session = session;
            _config = config;

            void OnGetInventoryModel(
                string inventoryName,
                EzInventoryModel inventoryModel,
                List<EzItemModel> itemModels
            )
            {
                _inventoryModel = inventoryModel;
                _itemModels = itemModels;
            }

            _unitSetting.onGetInventoryModel.AddListener(OnGetInventoryModel);

            yield return InventoryController.GetInventoryModel(
                client,
                _unitSetting.inventoryNamespaceName,
                _unitSetting.inventoryModelName,
                _unitSetting.onGetInventoryModel,
                _unitSetting.onError
            );

            _unitSetting.onGetInventoryModel.RemoveListener(OnGetInventoryModel);

            yield return _inventoryWatcher.Run(
                    client,
                    session,
                    _unitSetting.inventoryNamespaceName,
                    _inventoryModel,
                    _itemModels,
                    _unitSetting.onGetInventory,
                    _unitSetting.onAcquire,
                    _unitSetting.onConsume,
                    _unitSetting.onError
                );

            Debug.Log(_inventoryWatcher.ItemModels.Count);

            _inventoryWatcher.Refresh();
            
            for (int i = 0; i < _inventoryWatcher.ItemModels.Count; i++)
            {
                //Debug.Log(_inventoryWatcher.ItemModels[i].Name + ":" + _inventoryWatcher.ItemModels[i].Metadata.Length);
                StartCoroutine(
                    InventoryController.Acquire(
                        _session,
                        _unitSetting.identifierAcquireUnitClientId,
                        _unitSetting.identifierAcquireUnitClientSecret,
                        _unitSetting.inventoryNamespaceName,
                        _unitSetting.inventoryModelName,
                        _inventoryWatcher.ItemModels[i].Name,
                        1,
                        _unitSetting.onAcquire,
                        _unitSetting.onError
                    )
                );
            }

            for (int i = 0; i < Watcher.ItemSets.Count; i++)
            {
                Debug.Log(Watcher.ItemSets[i].ItemName + ":" + Watcher.ItemSets[i].ItemSetId);
            }
        }

        public void OnAcquireItem(
            string itemModelName,
            int count
        )
        {
            if (string.IsNullOrEmpty(_unitSetting.identifierAcquireUnitClientId) ||
                string.IsNullOrEmpty(_unitSetting.identifierAcquireUnitClientSecret))
            {
                Debug.LogError("インストーラーの設定でデバッグ機能が無効化されています");
            }

            StartCoroutine(
                InventoryController.Acquire(
                    _session,
                    _unitSetting.identifierAcquireUnitClientId,
                    _unitSetting.identifierAcquireUnitClientSecret,
                    _unitSetting.inventoryNamespaceName,
                    _unitSetting.inventoryModelName,
                    itemModelName,
                    count,
                    _unitSetting.onAcquire,
                    _unitSetting.onError
                )
            );
        }

        public void ReleaseUnit(
            EzItemSet unit
        )
        {
            StartCoroutine(
                InventoryController.Consume(
                    _client,
                    _session,
                    _unitSetting.inventoryNamespaceName,
                    _unitSetting.inventoryModelName,
                    unit.ItemName,
                    1,
                    _unitSetting.onConsume,
                    _unitSetting.onError,
                    unit.Name
                )
            );
        }

        public void OnSelectUnit(
            EzItemSet itemSet
        )
        {
            StartCoroutine(
                InventoryController.Consume(
                    _client,
                    _session,
                    _unitSetting.inventoryNamespaceName,
                    _unitSetting.inventoryModelName,
                    itemSet.ItemName,
                    1,
                    _unitSetting.onConsume,
                    _unitSetting.onError,
                    itemSet.Name
                )
            );
        }

        public UnityAction<EzJob, EzJobResultBody> GetJobQueueAction()
        {
            return (job, jobResult) =>
            {
                Debug.Log("UnitDirector::GetJobQueueAction");

                if (job.ScriptId.EndsWith(
                    "system:script:general:script:execute_inventory_acquire_item_set_by_user_id"
                ))
                {
                    StartCoroutine(
                        _inventoryWatcher.Refresh()
                    );
                }
            };
        }

        public UnityAction<EzStampTask, EzRunStampTaskResult> GetTaskCompleteAction()
        {
            return (task, taskResult) =>
            {
                Debug.Log("UnitDirector::StateMachineOnDoneStampTask");

                if (task.Action == "Gs2Inventory:ConsumeItemSetByUserId")
                {
                    StartCoroutine(
                        _inventoryWatcher.Refresh()
                    );
                }
            };
        }

        public UnityAction<EzStampSheet, EzRunStampSheetResult> GetSheetCompleteAction()
        {
            return (sheet, sheetResult) =>
            {
                Debug.Log("UnitDirector::StateMachineOnCompleteStampSheet");

                if (sheet.Action == "Gs2Inventory:AcquireItemSetByUserId")
                {
                    StartCoroutine(
                        _inventoryWatcher.Refresh()
                    );
                }
            };
        }
    }
}