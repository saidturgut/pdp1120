namespace pdp11_emulator.Arbitrating.Memory;
using Executing.Components;
using Signaling;
using Utility;
using Arbitrating;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];

    private readonly Dictionary<uint, byte> WriteRequests = new();
    
    private const uint startAddress = 0;
    
    public void LoadImage(byte[] image, bool hexDump)
    {
        for (uint i = 0; i < image.Length; i++)
            Memory[i + startAddress] = image[i];
        
        if (hexDump)
            HexDump.Write(Memory);
    }
    
    public void Respond(UniBus uniBus, TrapUnit trapUnit)
    {
        if(!uniBus.respondPermit)
            return;
        
        switch (uniBus.Operation)
        {
            case UniBusDriving.READ_WORD:
                uniBus.SetData(ReadWord(uniBus.GetAddress(), trapUnit)); break;
            case UniBusDriving.READ_BYTE:
                uniBus.SetData(ReadByte(uniBus.GetAddress())); break;
            case UniBusDriving.WRITE_WORD:
                WriteWord(uniBus.GetAddress(), uniBus.GetData(), trapUnit); break;
            case UniBusDriving.WRITE_BYTE:
                WriteByte(uniBus.GetAddress(), (byte)uniBus.GetData()); break;
            default:
                throw new Exception("UNKNOWN OPERATION!");
        } 
    }
    
    private ushort ReadWord(uint address, TrapUnit trapUnit)
    {
        if (address % 2 != 0)
        {
            trapUnit.Request(TrapVector.ODD_ADDRESS);
            return 0;
        }
        
        return (ushort)(Memory[address] | (Memory[address + 1] << 8));
    }

    private void WriteWord(uint address, ushort value, TrapUnit trapUnit)
    {
        if (address % 2 != 0)
        {
            trapUnit.Request(TrapVector.ODD_ADDRESS);
            return;
        }

        WriteRequests[address] = (byte)(value & 0xFF);
        WriteRequests[address + 1] = (byte)(value >> 8);
    }

    private byte ReadByte(uint address) 
        => Memory[address];

    private void WriteByte(uint address, byte value)
    {
        WriteRequests[address] = value;
    }

    public void Commit(bool abort)
    {
        if (abort)
        {
            WriteRequests.Clear();
            return;
        }
        
        foreach (uint address in WriteRequests.Keys)
        {
            Console.WriteLine($"MEMORY [{O(address)}] : {O(WriteRequests[address])}");
            Memory[address] = WriteRequests[address];
            WriteRequests.Remove(address);
        }
    }

    private string O(uint input)
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}