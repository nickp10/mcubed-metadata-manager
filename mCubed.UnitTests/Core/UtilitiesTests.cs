using mCubed.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mCubed.UnitTests.Core
{
	[TestClass]
	public class UtilitiesTests
	{
		public TestContext TestContext { get; set; }

		[DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\TestsData\\UtilitiesTestsData.xml", "Row", DataAccessMethod.Sequential), DeploymentItem("mCubed.UnitTests\\TestsData\\UtilitiesTestsData.xml"), TestMethod]
		public void Utilities_CleanupTitle_DataDriven_Test()
		{
			// Arrange
			var input = TestContext.DataRow["Input"].ToString();
			var expected = TestContext.DataRow["Expected"].ToString();
			input = input == "null" ? null : input;
			expected = expected == "null" ? null : expected;

			// Act
			var actual = Utilities.CleanupTitle(input);

			// Assert
			Assert.AreEqual(expected, actual, string.Format("Input = \"{0}\", Expected = \"{1}\", Actual = \"{2}\"", input, expected, actual));
		}
	}
}
