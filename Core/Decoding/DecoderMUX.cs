namespace pdp11_emulator.Core.Decoding;

public class DecoderMUX
{
    protected Decoded DOUBLE_OPERAND(ushort opcode)
    {
        
        
        return new Decoded();
    }

    protected Decoded SINGLE_OPERAND(ushort opcode)
    {
        return new Decoded();
    }

    protected Decoded BRANCH(ushort opcode)
    {
        return new Decoded();
    }
    
    protected Decoded JSR(ushort opcode)
    {
        return new Decoded();
    }
    
    protected Decoded RTS(ushort opcode)
    {
        return new Decoded();
    }
}