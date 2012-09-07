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


    #endregion

    #region Testing helpers

    [Test]
    [Category("SelfTests")]
    public void SplitLongTests(
      [Values(long.MinValue, long.MinValue + 1L, -1L, long.MaxValue - 1L, long.MaxValue)] long selectedTimer)
    {
      SplitLong s = new SplitLong(selectedTimer);
      long rebuilt = s.ToLong();

        Assert.That(rebuilt, Is.EqualTo(selectedTimer));
    }

    [Test]
    [Category("SelfTests")]
    // NUnit is picky with longs that have int-compatible values (looks like it auto-casts them to int32)
    // For some obscure reason, values 0 and 1 fail the test outside the test code. Better ask StackOverflow someday
    public void SplitLongTests(
      [Values(int.MinValue, int.MinValue + 1, -1, int.MaxValue - 1, int.MaxValue)] int selectedTimer)
    {
      SplitLong s = new SplitLong((long)selectedTimer);
      long rebuilt = s.ToLong();

      if (selectedTimer > ((long)int.MaxValue) || selectedTimer < ((long)int.MinValue))
      {
        Assert.That(rebuilt, Is.EqualTo(selectedTimer));
      }
      else
      {
        int rebuiltCast = (int)rebuilt;
        int selectedTimerCast = (int)selectedTimer;
        Assert.That(rebuiltCast, Is.EqualTo(selectedTimerCast));
      }

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


    #endregion

    #region Self Tests

    [Test]
    public void AssertIsAPowerOfTwoSelfTestsSuccess(
      [Range(0, 62, 1)]         int power)
    {
      long shouldBeAPowerOf2 = ((long) 1) << power;
      Assert.That(IsAPowerOfTwo(shouldBeAPowerOf2), Is.True);
    }

    [Test]
    [ExpectedException(typeof(AssertionException))]
    public void AssertIsAPowerOfTwoSelfTestsFailure(
      [Range(2, 62, 1)]         int power,
      [Values(-1, 1)]           int offset)
    {
      long shouldNotBeAPowerOf2 = ((long)1) << power;
      shouldNotBeAPowerOf2 += offset;
      Assert.That(IsAPowerOfTwo(shouldNotBeAPowerOf2), Is.True);
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
        Assert.That(IsAPowerOfTwo(multiplier), Is.True);
    }



    [Test]
    public void GeneratePeriodsForTimerTester(
      [Values(0, 1, 10, 912, MaxS32 - 1, MaxS32)]             int timerHOB,
      [Values(1, 10, 951, MaxS32-1, MaxS32)]                  int timerLOB,
      [Values(1, 2, 3, 4, 10, 100, MaxP-1, MaxP)]             int nbPeriods)
    {
      SplitLong timer = new SplitLong((uint)timerHOB, (uint)timerLOB);

      var periods = GeneratePeriodsForTimer(timer.ToLong(), nbPeriods);

      Assert.That(periods, Is.All.Matches(new Predicate<long>(l => IsAPowerOfTwo(l))));
        

    }




    #endregion

    #region Helpers

    public bool IsAPowerOfTwo(long a)
    {
      long currentPowerOfTwo = (long.MaxValue/2) + 1;
      Assert.That(currentPowerOfTwo, Is.EqualTo(((long) 1) << 62));

      while(currentPowerOfTwo > 0)
      {
        if (a == currentPowerOfTwo)
          return true;

        currentPowerOfTwo = currentPowerOfTwo / 2;
      }

      Log.Info(String.Format("Provided number is not a power of 2: {0:0} (0x{0:X16})", a));
      return false;
    }

    #endregion


    private List<long> GeneratePeriodsForTimer(long timer, int nbPeriods)
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
        throw new InvalidOperationException("Multiplier generation went wrong (1st check: shift comparison)");
      }

      long multiplicationResult = selectedTimer << currentShift;
      if (multiplicationResult < 0)
      {
        Log.Warn(String.Format("  => multiplication result overflows : {0:0} (0x {0:X16}). Shift = {1:0}", multiplicationResult, currentShift));
        Log.Warn(String.Format("    /  {0:X16} << {1:0} = {2:X16}", selectedTimer, currentShift, multiplicationResult));
        throw new InvalidOperationException("Multiplier generation went wrong (2nd check: result comparison)");
      }

      Log.Warn(String.Format("Multiplier successfully generated - {0:X16} << {1:0} = {2:X16}", selectedTimer, currentShift, multiplicationResult));
      return multiplicationResult;
    }



  }
}
