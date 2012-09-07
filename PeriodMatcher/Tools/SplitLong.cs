namespace Gbd.PeriodMatching.Tools
{

  public class SplitLong
  {
    public uint HOB;
    public uint LOB;

    public SplitLong(long value)
    {
      long tmpLOB = (value & (long)uint.MaxValue);

      LOB = (uint) value;
      HOB = (uint)(value >> 32);
    }

    public SplitLong(ulong value)
    {
      LOB = (uint)(value & uint.MaxValue);
      HOB = (uint)(value >> 32);
    }

    public SplitLong(uint hob, uint lob)
    {
      this.HOB = hob;
      this.LOB = lob;
    }

    public long ToLong()
    {
      long shiftedLeft = (((long)HOB) << 32);
      shiftedLeft = shiftedLeft | LOB;
      return shiftedLeft;
    }

    public ulong ToULong()
    {
      ulong shiftedLeft = (((ulong)HOB) << 32);
      shiftedLeft = shiftedLeft | LOB;
      return shiftedLeft;
    }
  }
}
