using System.Collections.Generic;
using UnityEngine;

public partial class BattleStateMachine {
    public class TargetSelectState : BattleState {
        private SelectorManager _selectManager = new SelectorManager();
        //private int _nextSelectedActor = 0; // Enemy Actor Selection

        public override void Enter(BattleStateInput i) {
            base.Enter(i);
            Debug.Log("Entering target select state");
            if (Input.ActiveActor() is EnemyActor) {
                // Enemy Actor Skill Selection
                BattleStateInput.ActiveSkillPrep activeSkill = EnemyAI.ChooseEnemyAISkill(Input.ActiveActor(), Input.GetTurnQueue());
                Input.SetSkillPrep(activeSkill.skill, activeSkill.targets);

                // i don't think we need this line but i don't want to delete it just in case
                //_nextSelectedActor = (_nextSelectedActor == 0) ? MySM.actorList.Count - 1 : 0;
                MySM.Transition<AnimateState>();
            }
            MySM.OnStateTransition.Invoke(this, Input);
        }

        public override void Update() {
            base.Update();
            Actor actor = _selectManager.CheckForSelect();
            if (actor != null) {
                 Input.SetSkillPrep(new Actor[] { actor });
                 MySM.Transition<AnimateState>();
            }
        }

        public override void Exit(BattleStateInput input) {
            base.Exit(input);
        }
    }
}