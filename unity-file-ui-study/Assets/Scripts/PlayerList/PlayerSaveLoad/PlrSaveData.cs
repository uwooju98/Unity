
using System.Collections.Generic;

public abstract class PlrSaveData
{
    public int Version { get; protected set; }

    public abstract PlrSaveData PlrVersionUp();
}

public class PlrSaveDataV1 : PlrSaveData
{
    public List<SavePlrData> Players { get; set; } = new List<SavePlrData>();

    public PlrSaveDataV1()
    {
        Version = 1;
    }

    public override PlrSaveData PlrVersionUp()
    {
        throw new System.NotImplementedException();
    }
}
