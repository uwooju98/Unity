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
    public string StringHp => DataTableManager.StringTable.Get(Hp.ToString());
    public string StringAtk => DataTableManager.StringTable.Get(Atk.ToString());
    public string StringDef => DataTableManager.StringTable.Get(Def.ToString());
    public string StringSpeed => DataTableManager.StringTable.Get(Speed.ToString());
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");
}
