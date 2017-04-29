.\packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -register:user "-filter: +[*]* -[*Tests]* -[AT]*" "-target:.\packages\NUnit.ConsoleRunner.3.6.1\tools\nunit3-console.exe" "-targetargs:.\GamingTests\bin\Debug\GamingTests.dll .\AT\bin\Debug\AT.dll .\SystemTests\bin\Debug\SystemTests.dll" "-output:results.xml" "-mergebyhash" "-skipautoprops"

.\packages\ReportGenerator.2.5.7\tools\ReportGenerator.exe "-reports:results.xml" "-targetdir:.\coverage"

pause

start "report" "coverage\index.htm"
exit /b %errorlevel%