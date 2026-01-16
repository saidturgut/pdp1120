using pdp11_emulator.Core.Executing.Components;

namespace pdp11_emulator;

public class Ram
{
    private readonly byte[] Memory = new byte[256];

    public void Respond(UniBus uniBus)
    {
        if(uniBus.GetAddress() >= 0x8000)
            return;

        uniBus.SetData(ReadWord(uniBus.GetAddress()));
    }
    
    private ushort ReadWord(ushort address)
    {
        if (address % 2 != 0)
        {
            throw new Exception("ODD ADDRESSES ARE ILLEGAL!!");
        }
        
        return (ushort)(Memory[address] | (Memory[address + 1] << 8));
    }

    private void WriteWord(ushort address, ushort value)
    {
        if (address % 2 != 0)
        {
            throw new Exception("ODD ADDRESSES ARE ILLEGAL!!");
        }

        Memory[address] = (byte)(value & 0xFF);
        Memory[address + 1] = (byte)(value >> 6);
    }

    private byte ReadByte(ushort address) 
        => Memory[address];

    private void WriteByte(ushort address, byte value)
    {
        Memory[address] = value;
    }
}