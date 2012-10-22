using mCubed.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mCubed.UnitTests.Core
{
	[TestClass]
	public class UtilitiesTests
	{
		[TestMethod]
		public void Utilities_CleanupTitle_DataDriven_Test()
		{
			ExecuteCleanupTitleTest(null, null);
			ExecuteCleanupTitleTest(string.Empty, string.Empty);
			ExecuteCleanupTitleTest("     ", string.Empty);
			ExecuteCleanupTitleTest(" Hey   ", "Hey");
			ExecuteCleanupTitleTest("   HEY   ", "Hey");
			ExecuteCleanupTitleTest(" hey ", "Hey");
			ExecuteCleanupTitleTest("Title of the song", "Title Of The Song");
			ExecuteCleanupTitleTest("title Of The song", "Title Of The Song");
			ExecuteCleanupTitleTest("Title of the sonG", "Title Of The Song");
			ExecuteCleanupTitleTest("TiTLe OF the song", "Title Of The Song");
			ExecuteCleanupTitleTest("Song feat", "Song");
			ExecuteCleanupTitleTest("Song ft", "Song");
			ExecuteCleanupTitleTest("Song feat.", "Song");
			ExecuteCleanupTitleTest("Song ft.", "Song");
			ExecuteCleanupTitleTest("Song featuring", "Song");
			ExecuteCleanupTitleTest("Song FEATURING", "Song");
			ExecuteCleanupTitleTest("Song (FT.", "Song");
			ExecuteCleanupTitleTest("Song (FT. blah)", "Song");
			ExecuteCleanupTitleTest("Song (Feat jim boby and joe)", "Song");
			ExecuteCleanupTitleTest("Song (Remix) feat Jimmey", "Song (Remix)");
			ExecuteCleanupTitleTest("Song (fEatUring Jimmey, Joe & Bob)   (Remix)   ", "Song (Remix)");
			ExecuteCleanupTitleTest("Song   (fEatUring Jimmey, Joe & Bob)   (Remix)   ", "Song   (Remix)");
			ExecuteCleanupTitleTest(" My song is feat. You and You", "My Song Is");
			ExecuteCleanupTitleTest("Song feat Jimmy (Remix)", "Song (Remix)");
			ExecuteCleanupTitleTest("Song ft Joe jim bob (Remix)", "Song (Remix)");
			ExecuteCleanupTitleTest("Song feat. Jkjkd fjkdf j sfas  (Remix)", "Song (Remix)");
		}

		private void ExecuteCleanupTitleTest(string original, string expected)
		{
			Assert.AreEqual(expected, Utilities.CleanupTitle(original), string.Format("Original was \"{0}\"", original));
		}
	}
}
