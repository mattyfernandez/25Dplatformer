﻿using System.Collections;
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

        private List<AttackInfo> FinishedAttacks = new List<AttackInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            GameObject obj = PoolManager.Instance.GetObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);

            info.ResetInfo(this, characterState.GetCharacterControl(animator));

            if (!AttackManager.Instance.CurrentsAttacks.Contains(info))
            {
                AttackManager.Instance.CurrentsAttacks.Add(info);
            }
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
            CheckCombo(characterState, animator, stateInfo);
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
                        info.Register(this);
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
                        info.GetComponent<PoolObject>().TurnOff();
                    }
                }
            }
        }

        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= StartAttackTime + (EndAttackTime - StartAttackTime) / 3f)
            {
                if (stateInfo.normalizedTime < EndAttackTime + ((EndAttackTime - StartAttackTime) / 2f))
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);
                    if (control.Attack)
                    {
                        Debug.Log("UpperCut Trigger");
                        animator.SetBool(TransitionParameter.Attack.ToString(), true);
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
            FinishedAttacks.Clear();
            
            foreach(AttackInfo info in AttackManager.Instance.CurrentsAttacks)
            {
                if (info == null || info.AttackAbility == this  /*info.IsFinished*/)
                {
                    FinishedAttacks.Add(info);
                }
            }

            foreach (AttackInfo info in FinishedAttacks)
            {
                if (AttackManager.Instance.CurrentsAttacks.Contains(info))
                {
                    AttackManager.Instance.CurrentsAttacks.Remove(info);
                }
            }
        }
    }

}
