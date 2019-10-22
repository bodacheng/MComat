using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RanSe : MonoBehaviour {

    public float sensor_radius;
    private Collider[] _hits; //What was hit in this frame?
    public LayerMask _layers;

	// Use this for initialization
	void Start () {
		
	}
	
    // Update is called once per frame
    private int sensorRate = 0;
    void Update()
    {
        if (sensorRate >= 5)
        {
            _hits = Physics.OverlapSphere(transform.position, sensor_radius);//这个东西消耗太大，起码可以考虑减少运行次数
            sensorRate = 0;
        }
        sensorRate++;
    }

    List<FightAttriCalReference> FocousingNearbyEnemyHealthBodies;
    FightAttriCalReference studyingHealthBody;
    BO_Hitbox studyingHitBox;
    public List<FightAttriCalReference> getNearbyEnemyHealthBody(string[] enemy_tags)
    {
        FocousingNearbyEnemyHealthBodies = new List<FightAttriCalReference>();
        foreach (Collider hit in this._hits)
        {
            if (hit != null)
            {
                if (!(enemy_tags).Contains(hit.tag))
                {
                    studyingHealthBody = null;
                    continue;
                }
                studyingHealthBody = hit.GetComponent<FightAttriCalReference>();
                if (studyingHealthBody != null)
                {
                    if (!FocousingNearbyEnemyHealthBodies.Contains(studyingHealthBody))
                        FocousingNearbyEnemyHealthBodies.Add(studyingHealthBody);
                }
                studyingHitBox = hit.GetComponent<BO_Hitbox>();
                if (studyingHitBox != null)
                {
                    if (studyingHitBox.MainHealth != null)
                    {
                        if (!FocousingNearbyEnemyHealthBodies.Contains(studyingHitBox.MainHealth))
                        {
                            FocousingNearbyEnemyHealthBodies.Add(studyingHitBox.MainHealth);
                        }
                    }
                }
            }
        }
        return FocousingNearbyEnemyHealthBodies;
    }
}
