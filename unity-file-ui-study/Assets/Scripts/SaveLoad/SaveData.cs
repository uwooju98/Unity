using System.Collections.Generic;
using System.Data;
using UnityEngine;

public abstract class SaveData
{
    public int Version { get; protected set; }
    public abstract SaveData VersionUp();
}

public class SaveDataV5 : SaveData
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Gold { get; set; } = 100;
    public List<SaveItemData> ItemList = new List<SaveItemData>();

    public SaveDataV5()
    {
        Version = 5;
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}

public class SaveDataV4 : SaveData
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Gold { get; set; } = 100;

    public SaveDataV4()
    {
        Version = 4;
    }

    public override SaveData VersionUp()
    {
        var data = new SaveDataV5();
        data.FirstName = FirstName;
        data.LastName = LastName;
        data.Gold = Gold;
        return data;
    }
}

public class SaveDataV3 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;
    public int Gold { get; set; } = 100;
    public int Exp { get; set; } = 0;

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        var data = new SaveDataV4();
        string[] parts = PlayerName.Split(" ");
        data.FirstName = parts[0];
        data.LastName = parts[1];
        data.Gold = Gold;
        return data;
    }
}

public class SaveDataV2 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;
    public int Gold { get; set; } = 100;

    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveData VersionUp()
    {
        var data = new SaveDataV3();
        data.PlayerName = PlayerName;
        data.Gold = Gold;
        return data;
    }
}

public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var data = new SaveDataV2();
        data.PlayerName = PlayerName;
        return data;
    }
}
