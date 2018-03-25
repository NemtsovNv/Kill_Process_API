using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KillProcess.Infrastructure.Business.Services;
using KillProcess.Tests.Unit.Helpers;

namespace KillProcess.Tests.Infrastucture.Unit
{
    [TestClass]
    public class ProcessServiceTest
    {
        private IProcessService processService;


        [TestMethod]
        public void GetProcesses_Should_Returns_Correct_Result()
        {
            // Assert
            processService = new ProcessServiceTestable(true);
            var expectedProcessCount = 3;
            var expectedProcessName = "cmd";
            // Act
            var actualResult = processService.GetProcesses();

            // Arrange
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(actualResult.Count == expectedProcessCount);
            Assert.IsTrue(actualResult.All(x => x.Name == expectedProcessName));
        }

        [TestMethod]
        public void GetProcesses_Should_Returns_No_Result()
        {
            // Assert
            processService = new ProcessServiceTestable(false);
            var expectedProcessCount = 0;

            // Act
            var actualResult = processService.GetProcesses();

            // Arrange
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(actualResult.Count == expectedProcessCount);
        }

        [TestMethod]
        public void KillProcess_Should_Kill_Process()
        {
            // Assert
            processService = new ProcessServiceTestable(true);
            var testProcesses = processService.GetProcesses();
            var processToKillId = testProcesses.First().Id;

            // Act
            var actualResult = processService.KillProcess(processToKillId);

            // Arrange
            Assert.IsNotNull(actualResult);
            Assert.IsTrue(actualResult == processToKillId);
        }

        [TestMethod]
        public void KillProcess_Should_Throw_Exception_No_Process_Found()
        {
            // Assert
            processService = new ProcessServiceTestable(true);
            var testProcesses = processService.GetProcesses();
            var notExistingProcessId = -90000;

            // Act
            try
            {
                var actualResult = processService.KillProcess(notExistingProcessId);

                // Arrange
                Assert.Fail("Exception was not thrown");
            }
            catch (ArgumentException)
            {
            }
        }

        [TestCleanup]
        public void DeleteTestProcesses()
        {
            var testProcesses = processService.GetProcesses();

            foreach (var processInfo in testProcesses)
            {
                var process = Process.GetProcessById(processInfo.Id);

                if(!process.HasExited)
                {
                    process.Kill();
                    process.WaitForExit();
                }
            }
        }
    }
}
