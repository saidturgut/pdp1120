namespace pdp11_emulator.Arbitrating.Memory;
using Utility;

public class Rom
{
    public void Boot(Ram ram)
    {
        Assembler.Run();
        
        ram.LoadImage(File.ReadAllBytes("test.bin"), false);
    }
}