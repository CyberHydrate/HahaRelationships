using JetBrains.Annotations;
using System;

public enum E_CharacterType
{
    初始,
    行动后,
}
public static class Characters
{
    public static Character[] characters = {
        new Character1(),
        new Character2(),
        new Character3(),
        new Character4(),
        new Character5(),
        new Character6(),
        new Character7(),
    };
}
public class Character
{
    public int id;
    public E_CharacterType characterType;
    public Action effect;
    public void ChangeNpchp(int i)
    {
        PlayerDataManager.Instance.playerData.npchp += i;
    }
    public void ChangeRealtionshiphp(int i)
    {
        PlayerDataManager.Instance.playerData.relationshiphp += i;
    }
}
public class Character1:Character
{
    public Character1()
    {
        this.id = 1;
        this.characterType = E_CharacterType.初始;
        this.effect = Func;
    }
    public void Func()
    {
        //修改npc地图生成空白格的权重为0 
        throw new NotImplementedException();
    }
}

public class Character2 : Character
{
    public Character2()
    {
        this.id = 2;
        this.characterType = E_CharacterType.初始;
        this.effect = Func;
    }
    public void Func()
    {
        //npc拒绝互动的权重增加40% 
        throw new NotImplementedException();
    }
}
public class Character3 : Character
{
    public Character3()
    {
        this.id = 3;
        this.characterType = E_CharacterType.初始;
        this.effect = Func;
    }
    public void Func()
    {
        //npc拒绝互动的权重增加15%,主动和玩家互动的权重减少15% 
        throw new NotImplementedException();
    }
}
public class Character4:Character
{
    public Character4()
    {
        this.id = 4;
        this.characterType = E_CharacterType.初始;
        this.effect =() => ChangeNpchp(10);
    }
}
public class Character5 : Character
{
    int oldhp;
    public Character5()
    {
        this.oldhp = PlayerDataManager.Instance.playerData.npchp;
        this.id = 5;
        this.characterType = E_CharacterType.行动后;
        this.effect = Func;
    }
    private void Func()
    {
        if(PlayerDataManager.Instance.playerData.npchp<oldhp)
        {
            PlayerDataManager.Instance.playerData.npchp -= 1;
            oldhp = PlayerDataManager.Instance.playerData.npchp;
        }
    }
}
public class Character6 : Character
{
    public Character6()
    {
        this.id = 6;
        this.characterType = E_CharacterType.初始;
        this.effect = Func;
    }
    public void Func()
    {
        //玩家无法拒绝npc互动 
        throw new NotImplementedException();
    }
}
public class Character7 : Character
{
    public Character7()
    {
        this.id = 7;
        this.characterType = E_CharacterType.行动后;
        this.effect = Func;
    }
    public void Func()
    {
        throw new NotImplementedException();
    }
}
public class Character8 : Character
{
    public Character8()
    {
        this.id = 8;
        this.characterType = E_CharacterType.行动后;
        this.effect = Func;
    }
    public void Func()
    {
        int id = BlockManager.Instance.playerBlocks[PlayerDataManager.Instance.playerData.stepCount].blockEvent.eventId - 1;
        if (ExcelReader.eventData[id].eventType==E_EventType.娱乐)
        {
            //如果npc的某项数值增加，则将其再加2
            throw new NotImplementedException();
        }
    }
}
