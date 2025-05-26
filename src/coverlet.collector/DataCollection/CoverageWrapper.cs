// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Coverlet.Collector.Utilities.Interfaces;
using Coverlet.Core;
using Coverlet.Core.Abstractions;

namespace Coverlet.Collector.DataCollection
{
  /// <summary>
  /// Implementation for wrapping over Coverage class in coverlet.core
  /// </summary>
  internal class CoverageWrapper : ICoverageWrapper
  {
    /// <summary>
    /// Creates a coverage object from given coverlet settings
    /// </summary>
    /// <param name="settings">Coverlet settings</param>
    /// <param name="coverletLogger">Coverlet logger</param>
    /// <param name="instrumentationHelper"></param>
    /// <param name="fileSystem"></param>
    /// <param name="sourceRootTranslator"></param>
    /// <param name="cecilSymbolHelper"></param>
    /// <returns>Coverage object</returns>
    public Coverage CreateCoverage(CoverletSettings settings, ILogger coverletLogger, IInstrumentationHelper instrumentationHelper, IFileSystem fileSystem, ISourceRootTranslator sourceRootTranslator, ICecilSymbolHelper cecilSymbolHelper)
    {
      CoverageParameters parameters = new()
      {
        IncludeFilters = settings.IncludeFilters,
        IncludeDirectories = settings.IncludeDirectories,
        ExcludeFilters = settings.ExcludeFilters,
        ExcludedSourceFiles = settings.ExcludeSourceFiles,
        ExcludeAttributes = settings.ExcludeAttributes,
        IncludeTestAssembly = settings.IncludeTestAssembly,
        SingleHit = settings.SingleHit,
        MergeWith = settings.MergeWith,
        UseSourceLink = settings.UseSourceLink,
        SkipAutoProps = settings.SkipAutoProps,
        DoesNotReturnAttributes = settings.DoesNotReturnAttributes,
        DeterministicReport = settings.DeterministicReport,
        ExcludeAssembliesWithoutSources = settings.ExcludeAssembliesWithoutSources
      };

      if (settings.SkipInstrumentModules)
      {

        CoveragePrepareResult coverageResult;

        string coverageResultsFile = "coverage-result.xml";

        // Expect the coverage results file to be adjecent to the test module
        string coverageResultsFilePath = Path.Combine(Path.GetDirectoryName(settings.TestModule), coverageResultsFile);

        using (FileStream inputStream = File.OpenRead(coverageResultsFilePath))
        using (var xmlWriter = XmlReader.Create(inputStream))
        {
          var serializer = new DataContractSerializer(typeof(CoveragePrepareResult));
          coverageResult = (CoveragePrepareResult)serializer.ReadObject(xmlWriter);
        }

        return new(coverageResult,
          coverletLogger,
          instrumentationHelper,
          fileSystem,
          sourceRootTranslator);
      }

      return new Coverage(
          settings.TestModule,
          parameters,
          coverletLogger,
          instrumentationHelper,
          fileSystem,
          sourceRootTranslator,
          cecilSymbolHelper);
    }

    /// <summary>
    /// Gets the coverage result from provided coverage object
    /// </summary>
    /// <param name="coverage">Coverage</param>
    /// <returns>The coverage result</returns>
    public CoverageResult GetCoverageResult(Coverage coverage)
    {
      return coverage.GetCoverageResult();
    }

    /// <summary>
    /// Prepares modules for getting coverage.
    /// Wrapper over coverage.PrepareModules
    /// </summary>
    /// <param name="coverage"></param>
    public void PrepareModules(Coverage coverage)
    {
      coverage.PrepareModules();
    }
  }
}
