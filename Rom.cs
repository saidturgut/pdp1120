namespace pdp11_emulator;
using Misc.External;

public class Rom
{
    public void Boot(Ram ram)
    {
        Assembler.Run();

        ram.LoadImage(File.ReadAllBytes("test.bin"), false);
    }
}