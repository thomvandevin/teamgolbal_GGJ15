using UnityEngine;
using System.Collections;

public enum EnemyType {
    Melee,
    Ranged,
}

public class EnemyBehaviour : Entity {

	//public fields

    public EnemyType type;
	
	//private fields
	
	//public methods
	
	//private methods
    private void Awake() {
        foreach (Enemy e in EnemyController.Get().Enemies) {
            if (e.type == type) {
                //SetMaxHealth(e.health);


            }
        }

    }
	
}
