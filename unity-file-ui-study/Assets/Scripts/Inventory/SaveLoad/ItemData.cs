using UnityEngine;

public enum ItemTypes
{
    Weapon,

    Equip,

    Consumable

}

public class ItemData
{
    public string Id { get; set; }
    public ItemTypes Type { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public int Value { get; set; }
    public int Cost { get; set; }
    public string Icon { get; set; }

    public string StringName => DataTableManager.StringTable.Get(Name);

    public string StringType => DataTableManager.StringTable.Get(Type.ToString().ToUpper());

    public string StringDesc => DataTableManager.StringTable.Get(Desc);
    public Sprite SpriteIcon => Resources.Load<Sprite>($"Icon/{Icon}");

}
