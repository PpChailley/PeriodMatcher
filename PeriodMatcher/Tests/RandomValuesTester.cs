using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{
  public class RandomValuesTester : PeriodMatcherTester
  {

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

    [Test]
    [Timeout(1000)]
    public void a()
    {
      MultiplyByRandomPowerOf2NoOverflow(100);
    }



    [TestCase(100)]
    public long MultiplyByRandomPowerOf2NoOverflow(long selectedTimer)
    {
      Log.Debug("");
      Log.Debug("Trying to generate a multiplier for timer value " + selectedTimer);

      
      long multiplier = 1;
      long multiplicationResult = 0;
      long maxMultiplierForNotOverflowing = (long.MaxValue/selectedTimer);
      double maxLog2ForNotOverflowing = Math.Log(maxMultiplierForNotOverflowing, 2);
      int maxShiftForNotOverflowing = (int) maxLog2ForNotOverflowing;

      Log.Debug("  - Max multiplier = " + maxMultiplierForNotOverflowing);
      Log.Debug("  - Max Log2 = " + maxLog2ForNotOverflowing);
      Log.Debug("  - Max Shift = " + maxShiftForNotOverflowing);

      while (multiplier < maxMultiplierForNotOverflowing)
      {
        multiplier = (1 << _rnd.Next(maxShiftForNotOverflowing));

        if (multiplier > maxMultiplierForNotOverflowing)
        {
          Log.Warn("  => multiplier is bigger than its max " + multiplier);
          continue;
        }

        multiplicationResult = multiplier*selectedTimer;
        if (multiplicationResult < 0)
        {
          Log.Warn("  => multiplication result overflows " + multiplicationResult);
          Log.Warn("     (0x" + String.Format("{0:X}", multiplicationResult) + ")");
          continue;
        }
      }




      return multiplicationResult;
    }



  }



}
