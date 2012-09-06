using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gbd.PeriodMatching.Matcher;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{

  [TestFixture]
  public class PeriodMatcherTester
  {

    protected const int MaxP = PeriodMatcher.MaxPeriodsSupported;
    protected const int MaxT = PeriodMatcher.MaxTimersSupported;

    protected PeriodMatcher Sandbox;

    #region Setup And TearDown

    [SetUp]
    public void Setup()
    {
      Sandbox = new PeriodMatcher();
    }


    [TearDown]
    public void TearDown()
    {
      Sandbox = null;
      System.GC.Collect();
    }


    #endregion

 
  }
}
