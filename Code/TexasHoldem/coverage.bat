.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-filter:+[*]*" "-target:.\packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe" "-targetargs:.\GamingTests\bin\Debug\GamingTests.dll" "-output:results.xml" "-mergebyhash" "-skipautoprops"

.\packages\ReportGenerator.2.5.6\tools\ReportGenerator.exe "-reports:results.xml" "-targetdir:.\coverage"

pause