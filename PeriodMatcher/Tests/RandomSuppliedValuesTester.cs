using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gbd.PeriodMatching.Tools;
using NLog;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class RandomSuppliedValuesTester : PeriodMatcherTester
  {


    private static readonly Logger Log = LogManager.GetCurrentClassLogger();
    private readonly Random _rnd = new Random(DateTime.Now.Millisecond);






    #region Smoke Self Tests

    [Test]
    [Category("SelfTests")]
    [ExpectedException(typeof(NotImplementedException))]
    public void NCrunchSmokeTest()
    {
      throw new NotImplementedException("Just check that this is executed");
    }

    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(long.MinValue, long.MinValue + 1, -1, 0, 1, long.MaxValue - 1, long.MaxValue)] long selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      long rebuilt = s.ToLong();
      Assert.That(rebuilt, Is.EqualTo(selectedTimer));
    }

    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(0, 1, ulong.MaxValue-1, ulong.MaxValue)] ulong selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      ulong rebuilt = s.ToULong();
      Assert.That(rebuilt, Is.EqualTo(selectedTimer));
    }


    [TestCase(100)]
    [Category("SelfTests")]
    public void b(long selectedTimer)
    {
      //SplitLong split = new SplitLong(selectedTimer);
      //return MultiplyByRandomPowerOf2NoOverflow(split.HOB, split.LOB);
      throw new NotImplementedException();

    }


    #endregion


    #region Self Tests

    [Test]
    public void AssertIsAPowerOfTwoSelfTestsSuccess(
      [Range(0, 62, 1)]         int power)
    {
      long shouldBeAPowerOf2 = ((long) 1) << power;
      AssertIsAPowerOfTwo(shouldBeAPowerOf2);
    }

    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void AssertIsAPowerOfTwoSelfTestsFailure(
      [Range(2, 62, 1)]         int power,
      [Values(-1, 1)]           int offset)
    {
      long shouldNotBeAPowerOf2 = ((long)1) << power;
      shouldNotBeAPowerOf2 += offset;
      AssertIsAPowerOfTwo(shouldNotBeAPowerOf2);
    }


    [Test]
    public void MultiplyByRandomPowerOf2NoOverflowSelfTests(
      [Values(0, int.MaxValue, 99999999)]            int HOB,
      [Values(1, int.MaxValue, 99997598)]            int LOB
      )
    {
        long period = new SplitLong((uint) HOB, (uint) LOB).ToLong();
        long timer = MultiplyByRandomPowerOf2NoOverflow(HOB, LOB);
        long multiplier = timer/period;

        Assert.That(multiplier*period, Is.EqualTo(timer), "Generated timer is not a multiple of the period (integer rounding occurred)");
        AssertIsAPowerOfTwo(multiplier);
    }


    #endregion


    #region Helpers

    public void AssertIsAPowerOfTwo(long a)
    {
      long currentPowerOfTwo = (long.MaxValue/2) + 1;
      Assert.That(currentPowerOfTwo, Is.EqualTo(((long) 1) << 62));

      while(currentPowerOfTwo > 0)
      {
        if (a == currentPowerOfTwo)
          return;

        currentPowerOfTwo = currentPowerOfTwo / 2;
      }

      throw new AssertionException(String.Format("Provided number is not a power of 2: {0:0} (0x{0:X16})", a));
    }

    #endregion


    private List<long> GeneratePeriodsForTimer(ICollection<long> timers, int nbPeriods)
    {
      throw new NotImplementedException();
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
