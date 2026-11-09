using UnityEngine;

public abstract class SaveData
{
    public int Version { get; protected set; } //버전의 규칙을 정해야함 일관성 있게

    public abstract SaveData VersionUp();

}

public class SaveDataV4 : SaveDataV2
{
    public string firstName { get; set; } = string.Empty;
    public string lastName { get; set; } = string.Empty;
    public SaveDataV4()
    {
        Version = 4;
    }
    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}

public class SaveDataV3 : SaveDataV2
{
    public int Exp { get; set; } = 100;
    public SaveDataV3()
    {
        Version = 3;
    }
    public override SaveData VersionUp()
    {
        var data = new SaveDataV4();
        string[] name = PlayerName.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
        data.firstName = name[0];
        data.lastName = name[1];
        data.Gold = Gold;
        return data;
    }
}

public class SaveDataV2 : SaveDataV1
{
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


public class SaveDataV1 : SaveData // 버전업이 생기면 구현
{
    public string PlayerName { get; set; } = string.Empty; // 보통 데이터로만 사용하는 프로퍼티는 퍼블릭으로 두는게 관행


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



