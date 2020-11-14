using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    [CreateAssetMenu(fileName = "New State", menuName = "MyAssets/AbilityData/Attack")]
    public class Attack : StateData
    {
        public float StartAttackTime;
        public float EndAttackTime;
        public List<string> ColliderNames = new List<string>();
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;
        public List<RuntimeAnimatorController> DeathAnimators = new List<RuntimeAnimatorController>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
            AttackInfo info = obj.GetComponent<AttackInfo>();

            info.ResetInfo(this);

            if (!AttackManager.Instance.CurrentsAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentsAttacks.Add(info);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
        }

        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach(AttackInfo info in AttackManager.Instance.CurrentsAttacks)
                {
                    if(info == null)
                    {
                        continue;
                    }

                    if(!info.IsRegisterd && info.AttackAbility == this)
                    {
                        info.Register(this, characterState.GetCharacterControl(animator));
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(stateInfo.normalizedTime >= EndAttackTime)
            {
                foreach(AttackInfo info in AttackManager.Instance.CurrentsAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.AttackAbility == this && !info.IsFinished)
                    {
                        info.IsFinished = true;
                        Destroy(info.gameObject);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
        }

        public void ClearAttack()
        {
            for (int i = 0; i < AttackManager.Instance.CurrentsAttacks.Count; i++)
            {
                if(AttackManager.Instance.CurrentsAttacks[i] == null || AttackManager.Instance.CurrentsAttacks[i].IsFinished)
                {
                    AttackManager.Instance.CurrentsAttacks.RemoveAt(i);
                }
            }
        }
    }

}
