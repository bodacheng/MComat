using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    none = 0,
    player1 = 1,
    player2 = 2,
    player3 = 3,
    player4 = 4,
}

public class TeamConfig
{
    public Team myTeam;
    public List<Team> myEnemies;

    public string my_tag;
    public string[] myEnemyTags;

    public int mylayer;
    public List<int> myEnemyLayers;

    public int myWeaponLayer;
    public int myShieldLayer;

    public LayerMask myTeamLayerMask;
    public LayerMask myTeamAndMyEnemy;
    public LayerMask enemyLayerMask;
    public LayerMask enemyWeaponLayerMask;
    public LayerMask enemyShieldLayerMask;
    public LayerMask enemyAndEnemyWeaponLayerMask;
    public LayerMask mySensorAndWeaponTargetLayerMask;//不管是我的武器还是我的Sensor都是用这个layermask决定去检测哪些战场上的敌人信息
    public int deadLayer;

    public static TeamConfig defaultSet = new TeamConfig(Team.none,new List<Team>());

    public TeamConfig()
    {

    }

    public TeamConfig(Team myTeam, List<Team> myEnemies)
    {
        this.myTeam = myTeam;
        this.myEnemies = myEnemies;
        RefreshMyLayers();
    }

    public void RefreshMyLayers()
    {
        myEnemyLayers = new List<int>();
        switch (myTeam)
        {
            case Team.player1:
                my_tag = "Player1";
                mylayer = 9;
                myTeamLayerMask =  (1 << 9);
                myWeaponLayer = 11;
                myShieldLayer = 15;
                break;
            case Team.player2:
                my_tag = "Player2";
                mylayer = 10;
                myTeamLayerMask =  (1 << 10);
                myWeaponLayer = 12;
                myShieldLayer = 16;
                break;
            case Team.player3:
                my_tag = "Player3";
                break;
            case Team.player4:
                my_tag = "Player4";
                break;
            default:
                my_tag = "Untagged";
                mylayer = 0;
                myWeaponLayer = 0;
                myShieldLayer = 0;
                break;
        }
        List<string> EnemyTags = new List<string>();
        foreach (Team one in myEnemies)
        {
            switch (one)
            {
                case Team.player1:
                    EnemyTags.Add("Player1");
                    enemyLayerMask = (1 << 9);
                    enemyWeaponLayerMask = (1 << 11);
                    enemyShieldLayerMask = (1 << 15);
                    mySensorAndWeaponTargetLayerMask = (1 << 9) | (1 << 11) | (1 << 15);
                    enemyAndEnemyWeaponLayerMask = (1 << 9) | (1 << 11);
                    myTeamAndMyEnemy = (1 << 10) | (1 << 9);
                    //myTeamAndMyEnemy |=(1 << 9);
                    break;
                case Team.player2:
                    EnemyTags.Add("Player2");
                    enemyLayerMask = (1 << 10);
                    enemyWeaponLayerMask = (1 << 12);
                    enemyShieldLayerMask = (1 << 16);
                    mySensorAndWeaponTargetLayerMask = (1 << 10) | (1 << 12) | (1 << 16);
                    enemyAndEnemyWeaponLayerMask = (1 << 10) | (1 << 12);
                    myTeamAndMyEnemy = (1 << 10) | (1 << 9);
                    //myTeamAndMyEnemy |=(1 << 10);
                    break;
                case Team.player3:
                    EnemyTags.Add("Player3");
                    break;
                case Team.player4:
                    EnemyTags.Add("Player4");
                    break;
                default:
                    enemyLayerMask = 31;
                    enemyWeaponLayerMask = 31;
                    enemyShieldLayerMask = 31;
                    mySensorAndWeaponTargetLayerMask = 31;
                    enemyAndEnemyWeaponLayerMask = 31;
                    break;
            }
        }
        myEnemyTags = EnemyTags.ToArray();
        deadLayer = 14;
    }
}
