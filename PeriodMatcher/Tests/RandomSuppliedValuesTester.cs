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


    [Test]
    [Category("SelfTests")]
    public void NCrunchSmokeTest()
    {
      throw new NotImplementedException("Just check that this is executed");
    }

    private List<long> GeneratePeriodsForTimer(ICollection<long> timers, int nbPeriods)
    {
      throw new NotImplementedException();
    }

    //[Test]
    //[Category("SelfTests")]
    //[Ignore]
    public long MultiplyByRandomPowerOf2NoOverflow(
      [Range(0, int.MaxValue, 131)]            int selectedTimerHOB,
      [Range(0, int.MaxValue, 941)]            int selectedTimerLOB
      )
    {
      //return MultiplyByRandomPowerOf2NoOverflow((uint) selectedTimerHOB, (uint) selectedTimerLOB);
      throw new NotImplementedException();
    }

    public long MultiplyByRandomPowerOf2NoOverflow(long selectedTimer)
    {
      //SplitLong split = new SplitLong(selectedTimer);
      //return MultiplyByRandomPowerOf2NoOverflow(split.HOB, split.LOB);
            throw new NotImplementedException();

    }
    public long MultiplyByRandomPowerOf2NoOverflow(SplitLong selectedTimer)
    {
      //return MultiplyByRandomPowerOf2NoOverflow(selectedTimer.HOB, selectedTimer.LOB);
            throw new NotImplementedException();

    }

    //[Test]
    //[Category("SelfTests")]
    //[Ignore]
    public long MultiplyByRandomPowerOf2NoOverflow(
      [Range(0, int.MaxValue, 131)]            uint selectedTimerHOB,
      [Range(0, int.MaxValue, 941)]            uint selectedTimerLOB
      )
    {
      throw new NotImplementedException();
    }



  }
}
