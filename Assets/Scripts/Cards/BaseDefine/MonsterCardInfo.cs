using System.Collections.Generic;

public class MonsterCardInfo
{
    public string name;

    public int hp;
    public int bless = -1;

    public int atk;
    public int atkTimes=1;
    
    public int atkRange=1;
    public int moveRange=1;

    public bool canMove=true;
    public bool canAtk=true;
    public bool canSwaped=true;

    public bool summonFree=false;
    public int summonCost=1;
    public bool moveFree=false;
    public int moveCost=1;
    public bool atkFree = false;
    public int atkCost=1;

    public List<List<string>> eventListers;
    public List<List<string>> haloEffect;
    public List<string> summonEffect;
    public List<string> deadEffect;
}