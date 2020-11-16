using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType poolObjectType;
        public float SchedubleOffTime;
        private Coroutine OffRoutine;

        private void OnEnable()
        {
            if(OffRoutine != null)
            {
                StopCoroutine(OffRoutine);
            }

            if(SchedubleOffTime > 0f)
            {
                OffRoutine = StartCoroutine(_ScheduledOff());
            }
        }

        public void TurnOff()
        {
            PoolManager.Instance.AddObject(this);
        }

        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(SchedubleOffTime);

            if (!PoolManager.Instance.PoolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
    }
}

