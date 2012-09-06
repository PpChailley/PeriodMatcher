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

    private static Logger Log;


    protected const int MaxP = PeriodMatcher.MaxPeriodsSupported;
    protected const int MaxT = PeriodMatcher.MaxTimersSupported;

    protected PeriodMatcher Sandbox;



    public const string ApplicationSourcePath = @"C:\ProjetsDev\trunk\Neosynergix\QA\Tools\PeriodMatcher\PeriodMatcher";
    public const string NlogCfgFile = ApplicationSourcePath + @"\NLog.cfg";
    public const string NlogDefaultLogFile = ApplicationSourcePath + @"\PeriodMatcher.log";

    [TestFixtureSetUp]
    public void TestFixtureSetup()
    {
      //System.Environment.SetEnvironmentVariable("NLOG_GLOBAL_CONFIG_FILE", NlogCfgFile);
      InitializeNLog();
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


  
 

    private static void InitializeNLog()
    {
      var config = new LoggingConfiguration();
      var target = new FileTarget {FileName = NlogDefaultLogFile};
      config.AddTarget("logfile", target);
      config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, target));
      LogManager.Configuration = config;

      Log = LogManager.GetCurrentClassLogger();

      Log.Info("NLog Initialized");
    }



  }
}
