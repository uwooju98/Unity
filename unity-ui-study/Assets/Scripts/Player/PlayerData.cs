using UnityEngine;


public class PlayerData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int HP { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Speed { get; set; }
    public string Icon { get; set; }


    public string StringName => PlayerDataTableManager.StringTable.Get(Name);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}
//Id,Name,Hp,Atk,Def,Speed,Icon
