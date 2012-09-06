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
    public void NCrunchSmokeTest()
    {
      throw new NotImplementedException("Just check that this is executed");
    }

    [Test]
    [Category("SelfTests")]
    public void NCrunchSmokeTest2()
    {
      throw new NotImplementedException("Just check that this is executed");
    }

    [TestCase(100, Result = 10)]
    [Category("SelfTests")]
    public long a(long selectedTimer)
    {
      //SplitLong split = new SplitLong(selectedTimer);
      //return MultiplyByRandomPowerOf2NoOverflow(split.HOB, split.LOB);
      return (long)10;

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




  }
}
