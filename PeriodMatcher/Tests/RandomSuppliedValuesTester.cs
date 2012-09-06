using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


  }
}
