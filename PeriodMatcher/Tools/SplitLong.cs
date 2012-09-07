namespace Gbd.PeriodMatching.Tools
{

  public class SplitLong
  {
    public uint HOB;
    public uint LOB;

    public SplitLong(long value)
    {
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
      return (((long) HOB) << 32) | LOB;
    }

    public ulong ToULong()
    {
      return (((ulong)HOB) << 32) | LOB;
    }

  
  }
}
