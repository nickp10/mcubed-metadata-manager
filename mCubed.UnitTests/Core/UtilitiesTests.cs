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
			ExecuteCleanupTitleTest("Song   (fEatUring Jimmey, Joe & Bob)   (Remix)   ", "Song (Remix)");
			ExecuteCleanupTitleTest(" My song is feat. You and You", "My Song Is");
			ExecuteCleanupTitleTest("Song feat Jimmy (Remix)", "Song (Remix)");
			ExecuteCleanupTitleTest("Song ft Joe jim bob (Remix)", "Song (Remix)");
			ExecuteCleanupTitleTest("Song feat. Jkjkd fjkdf j sfas  (Remix)", "Song (Remix)");
			ExecuteCleanupTitleTest("the name of the song j.k.i. ft. the all-stars featuring you of me", "The Name Of The Song J.K.I.");
			ExecuteCleanupTitleTest("The side left", "The Side Left");
			ExecuteCleanupTitleTest("Featuring you", "Featuring You");
			ExecuteCleanupTitleTest("The side defeat", "The Side Defeat");
			ExecuteCleanupTitleTest("My SONG produced by jimmy (feat. joey)", "My Song");
			ExecuteCleanupTitleTest("You're Jay-z bombing (prod. by jay-z)", "You're Jay-Z Bombing");
			ExecuteCleanupTitleTest("The left side (prod by johnny) Remix", "The Left Side Remix");
			ExecuteCleanupTitleTest("I am you featuring me and produced by you", "I Am You");
			ExecuteCleanupTitleTest("Why do you care? (ft. Jim) (PRODUCED BY Zilla)", "Why Do You Care?");
		}

		private void ExecuteCleanupTitleTest(string original, string expected)
		{
			Assert.AreEqual(expected, Utilities.CleanupTitle(original), string.Format("Original was \"{0}\"", original));
		}
	}
}
