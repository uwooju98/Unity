using UnityEngine;

public class PlrData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int Speed { get; set; }
    public string Icon { get; set; }

public string StringName => DataTableManager.StringTable.Get(Name);
public string StringHp => Hp.ToString();
public string StringAtk => Atk.ToString();
public string StringDef => Def.ToString();
public string StringSpeed => Speed.ToString();
public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}
