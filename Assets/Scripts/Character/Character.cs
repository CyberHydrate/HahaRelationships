using System;

public enum E_CharacterType
{
    初始,
    非初始,
}
public class Characters
{
   public Charater[] charaters= {
    new Character4(),
    }; 
}
public class Charater
{
    public int id;
    public E_CharacterType characterType;
    public Action effect;
}
public class Character4:Charater
{
    public Character4()
    {
        this.id = 4;
        this.characterType = E_CharacterType.初始;
        this.effect = Func;
    }
    private void Func()
    {
        PlayerDataManager.Instance.playerData.npchp += 10;
    }
}
public class Character5 : Charater
{
    int oldhp;
    public Character5()
    {
        this.oldhp = PlayerDataManager.Instance.playerData.npchp;
        this.id = 5;
        this.characterType = E_CharacterType.非初始;
        this.effect = Func;
    }
    private void Func()
    {
        if(PlayerDataManager.Instance.playerData.npchp<oldhp)
        {
            PlayerDataManager.Instance.playerData.npchp -= 1;
        }
    }
}