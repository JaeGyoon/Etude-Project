using UnityEngine;
using System;
using System.Collections.Generic;

namespace EtudeProject
{
    [Serializable]
    public class HeroStateData
    {
        public string heroID;
        public bool unlocked;        
    }

    [Serializable]
    public class PlayerSaveData
    {
        public string currentHeroID;        
        public List<HeroStateData> heroStateDataList;
        public int currentStage;
        public int highestStage = 1;    // 0이면 튜토리얼 단계를 할지? 1부터 바로 시작할지 
    }

    [Serializable]
    public abstract class CharacterState
    {
        public virtual void Enter(CharacterController controller) { }
        public virtual void Stay(CharacterController controller) { }
        public virtual void Exit(CharacterController controller) { }
    }

    public class IdleState : CharacterState
    {
        public override void Enter(CharacterController controller)
        {
            controller.animator.SetFloat("MoveSpeed", 0);
        }
    }

    public class MoveState : CharacterState
    {
        public override void Stay(CharacterController controller)
        {
            Vector3 direction = controller.moveInput;

            //controller.Move(direction);

            controller.animator.SetFloat("MoveSpeed", direction.magnitude);
        }
    }


    [Serializable]
    public class CharacterStateMachine
    {
        CharacterController owner;

        Dictionary<CharacterStateType, CharacterState> states;
        CharacterState currentState;

        //생성자
        public CharacterStateMachine(CharacterController controller)
        {
            this.owner = controller;

            states = new Dictionary<CharacterStateType, CharacterState>()
            {
                { CharacterStateType.Idle, new IdleState() },
                { CharacterStateType.Move, new MoveState() },
            };
        }

        public void ChangeState(CharacterStateType type)
        {
            if ( currentState == states[type])
            {
                return;
            }

            currentState?.Exit(owner);

            currentState = states[type];

            currentState?.Enter(owner);
        }

        public void StayState()
        {
            currentState?.Stay(owner);
        }
    }

    
}
