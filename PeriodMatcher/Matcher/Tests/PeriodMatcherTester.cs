using System;
using Gbd.PeriodMatching.Matcher;
using NLog;
using NLog.Config;
using NLog.Targets;
using NUnit.Framework;

namespace Gbd.PeriodMatching.Tests
{

  [TestFixture]
  public abstract class PeriodMatcherTester
  {


    internal const int MaxP = AbstractPeriodMatcher.MaxPeriodsSupported;
    internal const int MaxT = AbstractPeriodMatcher.MaxTimersSupported;
    internal const int MaxS32 = int.MaxValue;
    internal const uint MaxU32 = uint.MaxValue;
    internal const int MinS32 = int.MinValue;
    internal const long MaxS64 = long.MaxValue;
    internal const ulong MaxU64 = ulong.MaxValue;
    internal const long MinS64 = long.MinValue;


    protected PeriodMatcher Sandbox;


    public const string ApplicationSourcePath = @"C:\ProjetsDev\trunk\Neosynergix\QA\Tools\PeriodMatcher\PeriodMatcher";
    public const string NlogCfgFile = ApplicationSourcePath + @"\NLog.cfg";
    public const string NlogDefaultLogFile = ApplicationSourcePath + @"\PeriodMatcher.log";

    [TestFixtureSetUp]
    public void TestFixtureSetup()
    {
      //System.Environment.SetEnvironmentVariable("NLOG_GLOBAL_CONFIG_FILE", NlogCfgFile);
      AbstractPeriodMatcher.InitializeNLog();
    }


    [SetUp]
    public void Setup()
    {
      //Log.Trace(" ** SetUp **");
      Sandbox = new PeriodMatcher();
    }


    [TearDown]
    public void TearDown()
    {
      //Log.Trace(" ** TearDown **");
      Sandbox = null;
      //System.GC.Collect();
    }
  }
}
