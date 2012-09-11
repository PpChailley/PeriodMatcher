using System;
using System.Collections.Generic;
using Gbd.PeriodMatching.Tools;
using NLog;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class RandomSuppliedValuesTester : PeriodMatcherTester
  {


    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    internal Random Rnd = new Random(DateTime.Now.Millisecond);


    #region Helpers

    public static bool IsAPowerOfTwo(long a)
    {
      long currentPowerOfTwo = (long.MaxValue/2) + 1;
      Assert.That(currentPowerOfTwo, Is.EqualTo(((long) 1) << 62));

      while(currentPowerOfTwo > 0)
      {
        if (a == currentPowerOfTwo)
          return true;

        currentPowerOfTwo = currentPowerOfTwo / 2;
      }

      //Log.Info(String.Format("Provided number is not a power of 2: {0:0} (0x{0:X16})", a));
      return false;
    }

    public static bool IsAPowerOfTwoProduct(long product, long notPowerOfTwo)
    {
      long currentPowerOfTwo = (long.MaxValue / 2) + 1;
      Assert.That(currentPowerOfTwo, Is.EqualTo(((long)1) << 62));

      while (currentPowerOfTwo > 0)
      {
        if (product == (currentPowerOfTwo*notPowerOfTwo))
          return true;

        currentPowerOfTwo = currentPowerOfTwo / 2;
      }

      //Log.Info(String.Format("Provided number is not a power of 2: {0:0} (0x{0:X16})", a));
      return false;
    }


    protected internal List<long> GeneratePeriodsForTimer(long timer, int nbPeriods)
    {
      List<long> periods = new List<long>(nbPeriods);

      for (int i = 0; i < nbPeriods; i++)
      {
        periods.Add(MultiplyByRandomPowerOf2NoOverflow(timer));
      }

      return periods;
    }

    public long MultiplyByRandomPowerOf2NoOverflow(int selectedTimerHOB,int selectedTimerLOB)
    {
      return MultiplyByRandomPowerOf2NoOverflow((uint) selectedTimerHOB, (uint) selectedTimerLOB);
    }
    public long MultiplyByRandomPowerOf2NoOverflow(long selectedTimer)
    {
      SplitLong split = new SplitLong(selectedTimer);
      return MultiplyByRandomPowerOf2NoOverflow(split.HOB, split.LOB);
    }
    public long MultiplyByRandomPowerOf2NoOverflow(SplitLong selectedTimer)
    {
      return MultiplyByRandomPowerOf2NoOverflow(selectedTimer.HOB, selectedTimer.LOB);
    }
    public long MultiplyByRandomPowerOf2NoOverflow(uint selectedTimerHOB,uint selectedTimerLOB)
    {
      long selectedTimer = (((long) selectedTimerHOB) << 32) | selectedTimerLOB;

      //Log.Warn("");
      //Log.Warn("Trying to generate a multiplier for timer value 0x {0:X8} {0:X8} = {0:X16}", selectedTimerHOB, selectedTimerLOB, selectedTimer);

      long maxMultiplierForNotOverflowing = (long.MaxValue/selectedTimer);
      double maxLog2ForNotOverflowing = Math.Log(maxMultiplierForNotOverflowing, 2);
      int maxShiftForNotOverflowing = (int) maxLog2ForNotOverflowing;
      int currentShift = Rnd.Next(maxShiftForNotOverflowing);

      //Log.Warn(String.Format("  - Max multiplier = {0:0} (0x {0:X16} )", maxMultiplierForNotOverflowing));
      //Log.Warn("  - Max Log2 = " + maxLog2ForNotOverflowing);
      //Log.Warn("  - Max Shift = " + maxShiftForNotOverflowing);


      if ((1 << currentShift) > maxMultiplierForNotOverflowing)
      {
        Log.Warn(String.Format("  => multiplier {0:0} (0x {0:X16}) is bigger than its max (shift {1:0}).", (1 << currentShift), currentShift));
        throw new InvalidOperationException("Multiplier generation went wrong (1st check: shift comparison)");
      }

      long multiplicationResult = selectedTimer << currentShift;
      if (multiplicationResult < 0)
      {
        Log.Warn(String.Format("  => multiplication result overflows : {0:0} (0x {0:X16}). Shift = {1:0}", multiplicationResult, currentShift));
        Log.Warn(String.Format("    /  {0:X16} << {1:0} = {2:X16}", selectedTimer, currentShift, multiplicationResult));
        throw new InvalidOperationException("Multiplier generation went wrong (2nd check: result comparison)");
      }

      //Log.Warn(String.Format("Multiplier successfully generated - {0:X16} << {1:0} = {2:X16}", selectedTimer, currentShift, multiplicationResult));
      return multiplicationResult;
    }

    #endregion


  }
}
