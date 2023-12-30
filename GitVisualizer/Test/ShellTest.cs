using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Management.Automation;
using System.Collections.ObjectModel;
using System.Management.Automation.Runspaces;
using GitVisualizer.backend.git;

namespace ShellTestSpace
{

    /// <summary>
    /// The test suiter to test the Shell class.
    /// </summary>
    [TestClass]
    public class ShellTest
    {

        /// <summary>
        /// Tests a valid command.
        /// </summary>
        [TestMethod]
        public void Exec_ls_Test01()
        {
            ShellComRes result = Shell.Exec("ls");
            Assert.AreEqual(true, result.Success);
        }

        /// <summary>
        /// Tests an invalid command.
        /// </summary>
        [TestMethod]
        public void Exec_ls_Test02()
        {
            ShellComRes result = Shell.Exec("l");
            Assert.AreEqual(false, result.Success);
            StringAssert.StartsWith(result.Errmsg, "System.Management.Automation.CommandNotFoundException: The term 'l' is not recognized as a name of a cmdlet");
        }
    }
}
