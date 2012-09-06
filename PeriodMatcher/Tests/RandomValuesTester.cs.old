using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class RandomValuesTester //: PeriodMatcherTester
  {

    public class SplitLong
    {
      public uint HOB;
      public uint LOB;

      public SplitLong(long value)
      {
        LOB = (uint)(value & uint.MaxValue);
        HOB = (uint) (value >> 32);
      }

      public SplitLong(ulong value)
      {
        LOB = (uint)(value & uint.MaxValue);
        HOB = (uint)(value >> 32);
      }

      public long ToLong()
      {
        return (HOB << 32) | LOB;
      }

      public ulong ToULong()
      {
        return (HOB << 32) | LOB;
      }
    }


    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private readonly Random _rnd = new Random(DateTime.Now.Millisecond);


    private List<long> GeneratePeriodsForTimer(ICollection<long> timers, int nbPeriods)
    {
      List<long> periods = new List<long>(nbPeriods+100);

      for (int i = 0; i < nbPeriods ; i++)
      {
        long selectedTimer = timers.ElementAt(_rnd.Next(timers.Count));
        long generatedPeriod = MultiplyByRandomPowerOf2NoOverflow(selectedTimer);

        // periods.Add();
      }
      throw new NotImplementedException();
    }




    //[Test]
    //[Category("SelfTests")]
    //[Ignore]
    //public long MultiplyByRandomPowerOf2NoOverflow(
    //  [Range(0, int.MaxValue, 131)]            int selectedTimerHOB,
    //  [Range(0, int.MaxValue, 941)]            int selectedTimerLOB
    //  )
    //{
    //  return MultiplyByRandomPowerOf2NoOverflow((uint) selectedTimerHOB, (uint) selectedTimerLOB);
    //}

    public long MultiplyByRandomPowerOf2NoOverflow(long selectedTimer)
    {
      SplitLong split = new SplitLong(selectedTimer);
      return MultiplyByRandomPowerOf2NoOverflow(split.HOB, split.LOB);
    }
    public long MultiplyByRandomPowerOf2NoOverflow(SplitLong selectedTimer)
    {
      return MultiplyByRandomPowerOf2NoOverflow(selectedTimer.HOB, selectedTimer.LOB);
    }

//    public long MultiplyByRandomPowerOf2NoOverflow(uint selectedTimerHOB,uint selectedTimerLOB)
    //[Test]
    //[Category("SelfTests")]
    //[Ignore]
    public long MultiplyByRandomPowerOf2NoOverflow(
      [Range(0, int.MaxValue, 131)]            uint selectedTimerHOB,
      [Range(0, int.MaxValue, 941)]            uint selectedTimerLOB
      )
    {
      long selectedTimer = (((long) selectedTimerHOB) << 32) | selectedTimerLOB;

      Log.Warn("");
      Log.Warn("Trying to generate a multiplier for timer value 0x {0:X8} {0:X8} = {0:X16}", selectedTimerHOB, selectedTimerLOB, selectedTimer);

      long maxMultiplierForNotOverflowing = (long.MaxValue/selectedTimer);
      double maxLog2ForNotOverflowing = Math.Log(maxMultiplierForNotOverflowing, 2);
      int maxShiftForNotOverflowing = (int) maxLog2ForNotOverflowing;
      int currentShift = _rnd.Next(maxShiftForNotOverflowing);

      Log.Warn(String.Format("  - Max multiplier = {0:0} (0x {0:X16} )", maxMultiplierForNotOverflowing));
      Log.Warn("  - Max Log2 = " + maxLog2ForNotOverflowing);
      Log.Warn("  - Max Shift = " + maxShiftForNotOverflowing);


      if ((1 << currentShift) > maxMultiplierForNotOverflowing)
      {
        Log.Warn(String.Format("  => multiplier {0:0} (0x {0:X16}) is bigger than its max (shift {1:0}).", (1 << currentShift), currentShift));
        throw new NotImplementedException("Multiplier generation went wrong (1st check: shift comparison)");
      }

      long multiplicationResult = selectedTimer << currentShift;
      if (multiplicationResult < 0)
      {
        Log.Warn(String.Format("  => multiplication result overflows : {0:0} (0x {0:X16}). Shift = {1:0}", multiplicationResult, currentShift));
        Log.Warn(String.Format("    /  {0:X16} << {1:0} = {2:X16}", selectedTimer, currentShift, multiplicationResult));
        throw new NotImplementedException("Multiplier generation went wrong (2nd check: result comparison)");
      }

      Log.Warn(String.Format("Multiplier successfully generated - {0:X16} << {1:0} = {2:X16}", selectedTimer, currentShift, multiplicationResult));
      return multiplicationResult;
    }



  }



}
