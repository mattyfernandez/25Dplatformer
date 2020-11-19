using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public class DamageDetector : MonoBehaviour
    {
        CharacterControl control;
        GeneralBodyPart DamagedPart;

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

                if(info.CurrentHits >= info.MaxHits)
                {
                    continue;
                }

                if(info.Attacker == control)
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
            foreach(TriggerDetector trigger in control.GetAllTriggers())
            {
                foreach(Collider collider in trigger.CollidingParts)
                {
                    foreach(string name in info.ColliderNames)
                    {
                        if (name == collider.gameObject.name)
                        {
                            DamagedPart = trigger.generalBodyPart;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        private void TakeDamage(AttackInfo info)
        {
            CameraManager.Instance.ShakeCamera(0.25f);
            Debug.Log(info.Attacker.gameObject.name + " hits: " + this.gameObject.name );
            Debug.Log(this.gameObject.name + " hit " + DamagedPart.ToString());

            //control.SkinnedMeshAnimator.runtimeAnimatorController = info.AttackAbility.GetDeathAnimator();
            control.SkinnedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.Instance.GetAnimator(DamagedPart, info);
            info.CurrentHits++;

            control.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;
        }
    }
}
    