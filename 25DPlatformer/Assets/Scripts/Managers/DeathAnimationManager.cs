﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace platformer
{
    public class DeathAnimationManager : Singleton<DeathAnimationManager>
    {
        DeathAnimationLoader deathAnimationLoader;
        List<RuntimeAnimatorController> Candidates = new List<RuntimeAnimatorController>();

        void SetupDeathAnimationLoader()
        {
            if (deathAnimationLoader == null)
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);

                DeathAnimationLoader loader = obj.GetComponent<DeathAnimationLoader>();

                deathAnimationLoader = loader;
            }
        }

        public RuntimeAnimatorController GetAnimator(GeneralBodyPart generalBodyPart)
        {
            SetupDeathAnimationLoader();

            Candidates.Clear();

            foreach(DeathAnimationData data in deathAnimationLoader.DeathAnimationDataList)
            {
                foreach(GeneralBodyPart part in data.GeneralBodyParts)
                {
                    if(part == generalBodyPart)
                    {
                        Candidates.Add(data.Animator);
                        break;
                    }
                }
            }

            return Candidates[Random.Range(0, Candidates.Count)];
        }
    }
}
