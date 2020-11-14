using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public class DamageDetector : MonoBehaviour
    {
        CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (AttackManager.Instance.CurrentsAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach(AttackInfo info in AttackManager.Instance.CurrentsAttacks)
            {
                if (info == null)
                {
                    continue;
                }

                if (!info.IsRegisterd)
                {
                    continue;
                }

                if (info.IsFinished)
                {
                    continue;
                }

                if (info.MustCollide)
                {
                    if (isCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool isCollided(AttackInfo info)
        {
            foreach(Collider collider in control.CollidingParts)
            {
                foreach(string name in info.ColliderNames)
                {
                    if (name == collider.gameObject.name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void TakeDamage(AttackInfo info)
        {
            Debug.Log(info.Attacker.gameObject.name + "hits: " + this.gameObject.name );
        }
    }
}
