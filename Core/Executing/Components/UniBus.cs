namespace pdp11_emulator.Core.Executing.Components;

public class UniBus
{
    private ushort address;
    private ushort data;
    
    public readonly Request?[] requesters = new Request?[5];
    
    public void Request(Request request)
    {
        requesters[request.Requester] = request;
    }

    public void Arbitrate()
    {
        for (int i = 0; i < requesters.Length; i++)
        {
            if (requesters[i] != null)
            {
                address = requesters[i]!.Value.Address;
                data = requesters[i]!.Value.Data;
                requesters[i] = null;
                return;
            }
        }
    }

    public ushort GetAddress()
        => address;

    public void SetData(ushort input)
        => data = input;

    public ushort GetData() 
        => data;
}

public struct Request
{
    public byte Requester;
    public ushort Address;
    public ushort Data;
}

public enum Requester
{
    NONE,
    CPU, 
}
